
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
namespace study.Controllers
{
    public class InspectController : Controller
    {
        private readonly studyContext _db1 = new studyContext();
        static List<Ptoken> tokens = new List<Ptoken>();
        class Ptoken
        {
            public string Identity { get; set; }
            public string Token { get; set; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                _db1.Dispose();
            }
            base.Dispose(disposing);
        }
        [Route("LoginAndQuery")]
        [HttpPost]
        public LoginAndQueryResponse LoginAndQuery([FromBody] LoginAndQueryRequest inputRequest)
        {
            try
            {
                if (inputRequest == null)
                {
                    Log.Error("LoginAndQuery,{0}", Global.Status[responseCode.studyRequestError].Description);
                    return new LoginAndQueryResponse
                    {
                        StatusCode = Global.Status[responseCode.studyRequestError].StatusCode,
                        Description = Global.Status[responseCode.studyRequestError].Description
                    };

                }
              
                Log.Information("LoginAndQuery inputRequest={0}",JsonConvert.SerializeObject(inputRequest));
                var identity = inputRequest.Identity;
                var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity);
                if (theuser == null)
                {
                    Log.Error("LoginAndQuery,{0}", Global.Status[responseCode.studyNotNecessary].Description+identity);
                    return new LoginAndQueryResponse
                    {
                        StatusCode = Global.Status[responseCode.studyNotNecessary].StatusCode,
                        Description = Global.Status[responseCode.studyNotNecessary].Description+identity
                    };
                }

                //need update?
                theuser.Name = inputRequest.Name;
                theuser.Licensetype = ((int)inputRequest.DrivingLicenseType).ToString();//elements?
                theuser.Phone = inputRequest.Phone;
                theuser.Wechat = inputRequest.Wechat;
                _db1.SaveChanges();

                //token process
                var toke1n = GetToken();
                var found = false;
                foreach (var a in tokens)
                {
                    if (a.Identity == identity)
                    {
                        a.Token = toke1n;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    tokens.Add(new Ptoken { Identity = identity, Token = toke1n });
                }

                //drugrelated judge
                var allow = theuser.Drugrelated == null ? true : false;
                var allstatus = string.Empty;
                if (allow)
                {
                    allstatus = theuser.Studylog;
                }
                else allstatus = "您不能参加网络学习，可以参加现场学习";

                return new LoginAndQueryResponse
                {
                    Token = toke1n,
                    StatusCode = Global.Status[responseCode.studyOk].StatusCode,
                    Description = Global.Status[responseCode.studyOk].Description,
                    AllowedToStudy = allow,
                    AllStatus = allstatus
                };
            }
            catch (Exception ex)
            {
                Log.Error("LoginAndQuery,{0}", ex);
                return new LoginAndQueryResponse
                {
                    StatusCode = Global.Status[responseCode.studyProgramError].StatusCode,
                    Description = Global.Status[responseCode.studyProgramError].Description
                };
            }

        }
        [Route("LogSignature")]
        [HttpPost]
        public CommonResponse LogSignature([FromBody] LogSignatureRequest inputRequest)
        {
            try
            {
                if (inputRequest == null)
                {
                    Log.Error("LogSignature,{0}", Global.Status[responseCode.studyRequestError].Description);
                    return new CommonResponse
                    {
                        StatusCode = Global.Status[responseCode.studyRequestError].StatusCode,
                        Description = Global.Status[responseCode.studyRequestError].Description
                    };

                }
                var found = false;
                var identity = string.Empty;
                foreach (var a in tokens)
                {
                    if (a.Token == inputRequest.Token)
                    {
                        identity = a.Identity;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Log.Error("LogSignature,{0}", Global.Status[responseCode.studyTokenError].Description);
                    return new CommonResponse
                    {
                        StatusCode = Global.Status[responseCode.studyTokenError].StatusCode,
                        Description = Global.Status[responseCode.studyTokenError].Description
                    };

                }
                // var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity);
                // if (theuser == null)
                // {
                //     return new CommonResponse { StatusCode = "100004", Description = "error identity" };
                // }

                //System.IO.File.WriteAllBytes(inputRequest.SignatureFile);//todo 

                return new CommonResponse
                {

                    StatusCode = Global.Status[responseCode.studyOk].StatusCode,
                    Description = Global.Status[responseCode.studyOk].Description,

                };
            }
            catch (Exception ex)
            {
                Log.Error("LogSignature,{0}", ex);
                return new CommonResponse
                {
                    StatusCode = Global.Status[responseCode.studyProgramError].StatusCode,
                    Description = Global.Status[responseCode.studyProgramError].Description
                };
            }

        }
        // [Route("GetToken")]
        // [HttpGet]
        // public GetTokenResponse GetToken(string identity)
        // {
        //     try
        //     {
        //         if (string.IsNullOrEmpty(identity))
        //         {
        //             return new GetTokenResponse { StatusCode = "100004", Description = "error identity" };
        //         }
        //         var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity);
        //         if (theuser == null)
        //         {
        //             return new GetTokenResponse { StatusCode = "100004", Description = "error identity" };
        //         }
        //         var toke1n = GetToken();
        //         var found = false;
        //         foreach (var a in tokens)
        //         {
        //             if (a.Identity == identity)
        //             {
        //                 a.Token = toke1n;
        //                 found = true;
        //                 break;
        //             }
        //         }
        //         if (!found)
        //         {
        //             tokens.Add(new Ptoken { Identity = identity, Token = toke1n });
        //         }
        //         return new GetTokenResponse
        //         {
        //             Token = toke1n,
        //             StatusCode = "100000",
        //             Description = "ok",
        //             DrivingLicenseType = string.IsNullOrEmpty(theuser.Licensetype) ? DrivingLicenseType.Unknown : (DrivingLicenseType)int.Parse(theuser.Licensetype),
        //             Identity = theuser.Identity,
        //             Name = theuser.Name,
        //             //   Photo = string.IsNullOrEmpty(theuser.Photo) ? string.Empty : theuser.Photo,
        //         };
        //     }
        //     catch (Exception ex)
        //     {
        //         // Log.Error("GetLearnerInfo", ex);
        //         return new GetTokenResponse { StatusCode = "100003", Description = ex.Message };
        //     }
        // }
        [Route("InspectCompleteCourses")]
        [HttpPost]

        public CommonResponse InspectCompleteCourses([FromBody] CompleteCoursesRequest inputRequest)
        {
            try
            {
                if (inputRequest == null)
                {
                    Log.Error("InspectCompleteCourses,{0}", Global.Status[responseCode.RequestError].Description);
                    return new CommonResponse
                    {
                        StatusCode = Global.Status[responseCode.RequestError].StatusCode,
                        Description = Global.Status[responseCode.RequestError].Description
                    };

                }
                var found = false;
                var identity = string.Empty;
                foreach (var a in tokens)
                {
                    if (a.Token == inputRequest.Token)
                    {
                        identity = a.Identity;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Log.Error("InspectCompleteCourses,{0},token={1}",
                  Global.Status[responseCode.TokenError].Description, inputRequest.Token);
                    return new CommonResponse
                    {
                        StatusCode = Global.Status[responseCode.TokenError].StatusCode,
                        Description = Global.Status[responseCode.TokenError].Description
                    };

                }
                var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity);
                if (theuser == null)
                {
                    Log.Error("InspectCompleteCourses,{0},identity={1}",
                 Global.Status[responseCode.InvalidIdentiy].Description, identity);
                    return new CommonResponse
                    {
                        StatusCode = Global.Status[responseCode.InvalidIdentiy].StatusCode,
                        Description = Global.Status[responseCode.InvalidIdentiy].Description
                    };

                }
                // theuser. = DateTime.Now;
                theuser.Studylog += inputRequest.AllRecords + inputRequest.AllStatus;

                _db1.History.Add(new History
                {
                    Identity = theuser.Identity,
                    Name = theuser.Name,
                    Phone = theuser.Phone,
                    Syncdate = theuser.Syncdate,
                    Startdate = theuser.Startdate,

                    Finishdate = DateTime.Now,
                    Stoplicense = theuser.Stoplicense,
                    Noticedate = theuser.Noticedate,
                    Wechat = theuser.Wechat,
                    Studylog = theuser.Studylog,

                    Drugrelated = theuser.Drugrelated,
                    // Photo = theuser.Photo,
                    Fullmark = theuser.Fullmark,
                    Inspect = theuser.Inspect,
                    Licensetype = theuser.Licensetype,
                    //  Timestamp = DateTime.Now
                });
                _db1.User.Remove(theuser);
                _db1.SaveChanges();
                return new CommonResponse
                {
                    StatusCode = Global.Status[responseCode.ok].StatusCode,
                    Description = Global.Status[responseCode.ok].Description,
                };

            }
            catch (Exception ex)
            {
                Log.Error("InspectCompleteCourses,{0},exception={1}",
                  Global.Status[responseCode.ProgramError].Description, ex);
                return new CommonResponse
                {
                    StatusCode = Global.Status[responseCode.ProgramError].StatusCode,
                    Description = Global.Status[responseCode.ProgramError].Description
                };

            }

        }
        [Route("InspectPostStudyStatus")]
        [HttpPost]

        public CommonResponse InspectPostStudyStatus([FromBody] StudyStatusRequest inputRequest)
        {
            try
            {
                if (inputRequest == null)
                {
                    Log.Error("InspectPostStudyStatus,{0}", Global.Status[responseCode.RequestError].Description);
                    return new CommonResponse
                    {
                        StatusCode = Global.Status[responseCode.RequestError].StatusCode,
                        Description = Global.Status[responseCode.RequestError].Description
                    };

                }
                var found = false;
                var identity = string.Empty;
                foreach (var a in tokens)
                {
                    if (a.Token == inputRequest.Token)
                    {
                        identity = a.Identity;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Log.Error("InspectPostStudyStatus,{0},token={1}",
                 Global.Status[responseCode.TokenError].Description, inputRequest.Token);
                    return new CommonResponse
                    {
                        StatusCode = Global.Status[responseCode.TokenError].StatusCode,
                        Description = Global.Status[responseCode.TokenError].Description
                    };

                }
                var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity);
                if (theuser == null)
                {
                    Log.Error("InspectPostStudyStatus,{0},identity={1}",
                  Global.Status[responseCode.InvalidIdentiy].Description, identity);
                    return new CommonResponse
                    {
                        StatusCode = Global.Status[responseCode.InvalidIdentiy].StatusCode,
                        Description = Global.Status[responseCode.InvalidIdentiy].Description
                    };

                }

                if (theuser.Startdate == null)
                {
                    theuser.Startdate = DateTime.Now;
                }

                theuser.Studylog += JsonConvert.SerializeObject(inputRequest);
                _db1.SaveChanges();
                return new CommonResponse
                {
                    StatusCode = Global.Status[responseCode.ok].StatusCode,
                    Description = Global.Status[responseCode.ok].Description,
                };
            }
            catch (Exception ex)
            {
                Log.Error("InspectPostStudyStatus,{0},exception={1}",
                  Global.Status[responseCode.ProgramError].Description, ex);
                return new CommonResponse
                {
                    StatusCode = Global.Status[responseCode.ProgramError].StatusCode,
                    Description = Global.Status[responseCode.ProgramError].Description
                };

            }

        }
        [Route("InspectGetLearnerInfo")]
        [HttpGet]

        public GetLearnerInfoResponse InspectGetLearnerInfo(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    Log.Error("InspectGetLearnerInfo,{0},token={1}",
                 Global.Status[responseCode.TokenError].Description, "invalid");
                    return new GetLearnerInfoResponse
                    {
                        StatusCode = Global.Status[responseCode.TokenError].StatusCode,
                        Description = Global.Status[responseCode.TokenError].Description
                    };

                }
                var found = false;
                var identity = string.Empty;
                foreach (var a in tokens)
                {
                    if (a.Token == token)
                    {
                        identity = a.Identity;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Log.Error("InspectGetLearnerInfo,{0},token={1},no corresponding identity",
                 Global.Status[responseCode.TokenError].Description, token);
                    return new GetLearnerInfoResponse
                    {
                        StatusCode = Global.Status[responseCode.TokenError].StatusCode,
                        Description = Global.Status[responseCode.TokenError].Description+token
                    };

                }
                var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity);
                if (theuser == null)
                {
                    Log.Error("InspectGetLearnerInfo,{0},identity={1}",
                  Global.Status[responseCode.InvalidIdentiy].Description, identity);
                    return new GetLearnerInfoResponse
                    {
                        StatusCode = Global.Status[responseCode.InvalidIdentiy].StatusCode,
                        Description = Global.Status[responseCode.InvalidIdentiy].Description
                    };

                }
                return new GetLearnerInfoResponse
                {
                    StatusCode = Global.Status[responseCode.ok].StatusCode,
                    Description = Global.Status[responseCode.ok].Description,
                    DrivingLicenseType = string.IsNullOrEmpty(theuser.Licensetype) ? DrivingLicenseType.Unknown : (DrivingLicenseType)int.Parse(theuser.Licensetype),

                    Identity = theuser.Identity,
                    Name = theuser.Name,
                    //  Photo = new byte[1]//todo
                };
            }
            catch (Exception ex)
            {
                Log.Error("InspectGetLearnerInfo,{0},exception={1}",
                  Global.Status[responseCode.ProgramError].Description, ex);
                return new GetLearnerInfoResponse
                {
                    StatusCode = Global.Status[responseCode.ProgramError].StatusCode,
                    Description = Global.Status[responseCode.ProgramError].Description
                };

            }

        }
        private string GetToken()
        {
            var seed = Guid.NewGuid().ToString("N");
            return seed;
        }
    }
}
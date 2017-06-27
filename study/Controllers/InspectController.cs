
using System;
using System.IO;
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
                Log.Information("LoginAndQuery,input={0},from {1}",
                    JsonConvert.SerializeObject(inputRequest), Request.HttpContext.Connection.RemoteIpAddress);
                if (inputRequest == null)
                {
                    Log.Error("LoginAndQuery,{0}", Global.Status[responseCode.studyRequestError].Description);
                    return new LoginAndQueryResponse
                    {
                        StatusCode = Global.Status[responseCode.studyRequestError].StatusCode,
                        Description = Global.Status[responseCode.studyRequestError].Description
                    };

                }

                //  Log.Information("LoginAndQuery inputRequest={0}", JsonConvert.SerializeObject(inputRequest));
                var identity = inputRequest.Identity;
                var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity);
                if (theuser == null)
                {
                    Log.Error("LoginAndQuery,{0}", Global.Status[responseCode.studyNotNecessary].Description + identity);
                    return new LoginAndQueryResponse
                    {
                        StatusCode = Global.Status[responseCode.studyNotNecessary].StatusCode,
                        Description = Global.Status[responseCode.studyNotNecessary].Description + identity
                    };
                }


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
                var allow = theuser.Drugrelated != "1" ? true : false;
                var allstatus = string.Empty;
                if (allow)
                {
                    allstatus = theuser.Studylog;

                    //need update?
                    //   theuser.Name = inputRequest.Name;
                    //  theuser.Licensetype = ((int)inputRequest.DrivingLicenseType).ToString();//elements?
                    //  theuser.Phone = inputRequest.Phone;
                    // theuser.Wechat = inputRequest.Wechat;
                    if (theuser.Startdate == null)
                    {
                        theuser.Startdate = DateTime.Now;
                    }

                    _db1.SaveChanges();
                }
                else allstatus = "您不能参加网络学习，可以参加现场学习";

                return new LoginAndQueryResponse
                {
                    Token = toke1n,
                    StatusCode = Global.Status[responseCode.studyOk].StatusCode,
                    Description = Global.Status[responseCode.studyOk].Description,
                    AllowedToStudy = allow,
                    Completed=theuser.Completed=="1"?true:false,
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
                Log.Information("LogSignature,input={0},from {1}",
                     JsonConvert.SerializeObject(inputRequest), Request.HttpContext.Connection.RemoteIpAddress);
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
                var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity);
                if (theuser == null)
                {
                    return new CommonResponse { StatusCode = "100004", Description = "error identity" };
                }

                var fname = Path.Combine(Global.SignaturePath, identity);
                //     System.IO.File.WriteAllBytes(fname,inputRequest.SignatureFile);//todo 
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

        [Route("InspectCompleteCourses")]
        [HttpPost]

        public CommonResponse InspectCompleteCourses([FromBody] CompleteCoursesRequest inputRequest)
        {
            try
            {
                Log.Information("InspectCompleteCourses,input={0},from {1}",
                       JsonConvert.SerializeObject(inputRequest), Request.HttpContext.Connection.RemoteIpAddress);
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
                if (theuser.Completed == "1")
                {
                    return new CommonResponse
                    {
                        StatusCode = Global.Status[responseCode.ok].StatusCode,
                        Description = Global.Status[responseCode.ok].Description,
                    };
                }
                // theuser. = DateTime.Now;
                theuser.Completelog = inputRequest.AllRecords + inputRequest.AllStatus;
                theuser.Completed = "1";
                // _db1.History.Add(new History
                // {
                //     Identity = theuser.Identity,
                //     Name = theuser.Name,
                //     Phone = theuser.Phone,
                //     Syncdate = theuser.Syncdate,
                //     Startdate = theuser.Startdate,

                //     Finishdate = DateTime.Now,
                //     Stoplicense = theuser.Stoplicense,
                //     Noticedate = theuser.Noticedate,
                //     Wechat = theuser.Wechat,
                //     Studylog = theuser.Studylog,

                //     Drugrelated = theuser.Drugrelated,
                //     // Photo = theuser.Photo,
                //     Fullmark = theuser.Fullmark,
                //     Inspect = theuser.Inspect,
                //     Licensetype = theuser.Licensetype,
                //     //  Timestamp = DateTime.Now
                // });
                // _db1.User.Remove(theuser);
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
                Log.Information("InspectPostStudyStatus,input={0},from {1}",
                       JsonConvert.SerializeObject(inputRequest), Request.HttpContext.Connection.RemoteIpAddress);
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
                    theuser.Studylog = string.Format("{0},{1},{2}", inputRequest.CourseTitle, inputRequest.StartTime, inputRequest.EndTime);
                }
                else
                    theuser.Studylog += string.Format("-{0},{1},{2}", inputRequest.CourseTitle, inputRequest.StartTime, inputRequest.EndTime);
                _db1.SaveChanges();

                var filename = Path.Combine(Global.LogPhotoPath, identity, string.Format("{0}.pic", inputRequest.CourseTitle));
                System.IO.File.WriteAllBytes(filename, inputRequest.Pictures);
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
                Log.Information("InspectGetLearnerInfo,input={0},from {1}",
                  token, Request.HttpContext.Connection.RemoteIpAddress);
                if (string.IsNullOrEmpty(token))
                {
                    Log.Error("InspectGetLearnerInfo,{0},token={1}, from {2}",
                 Global.Status[responseCode.TokenError].Description, "invalid", Request.HttpContext.Connection.RemoteIpAddress);
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
                        Description = Global.Status[responseCode.TokenError].Description + token
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
                Log.Error("InspectGetLearnerInfo,{0},={1}", Global.PhotoPath, Global.PhotoPath);
                var filename = Path.Combine(Global.PhotoPath, identity + ".jpg");
                Log.Error("InspectGetLearnerInfo,{0},={1}", identity, filename);
                return new GetLearnerInfoResponse
                {
                    StatusCode = Global.Status[responseCode.ok].StatusCode,
                    Description = Global.Status[responseCode.ok].Description,
                    DrivingLicenseType = string.IsNullOrEmpty(theuser.Licensetype) ? DrivingLicenseType.Unknown : (DrivingLicenseType)int.Parse(theuser.Licensetype),

                    Identity = theuser.Identity,
                    Name = theuser.Name,
                    Photo = System.IO.File.ReadAllBytes(filename)
                };
            }
            catch (Exception ex)
            {
                Log.Error("InspectGetLearnerInfo,{0},{2},exception={1}",
                  Global.Status[responseCode.ProgramError].Description, ex, Global.PhotoPath);
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
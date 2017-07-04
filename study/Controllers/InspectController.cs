
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

        private readonly studyinContext _db1 = new studyinContext();
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
        [Route("SignatureQuery")]
        [HttpPost]
        public SignatureQueryResponse SignatureQuery([FromBody] SignatureQueryRequest inputRequest)
        {
            try
            {
                Log.Information("SignatureQuery,input={0},from {1}",
                    JsonConvert.SerializeObject(inputRequest), Request.HttpContext.Connection.RemoteIpAddress);
                if (inputRequest == null)
                {
                    Log.Error("SignatureQuery,{0}", Global.Status[responseCode.studyRequestError].Description);
                    return new SignatureQueryResponse
                    {
                        StatusCode = Global.Status[responseCode.studyRequestError].StatusCode,
                        Description = Global.Status[responseCode.studyRequestError].Description
                    };
                }
                var allstatus = string.Empty;
                var completed = true;
                var signed = true;

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
                    Log.Error("SignatureQuery,{0}", Global.Status[responseCode.studyTokenError].Description);
                    return new SignatureQueryResponse
                    {
                        StatusCode = Global.Status[responseCode.studyTokenError].StatusCode,
                        Description = Global.Status[responseCode.studyTokenError].Description
                    };
                }


                var cryptographicid = CryptographyHelpers.StudyEncrypt(identity);
                var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity || async.Identity == cryptographicid);
                if (theuser == null)
                {
                    var his = _db1.History.FirstOrDefault(async => async.Identity == identity || async.Identity == cryptographicid);
                    if (his == null)
                    {
                        Log.Error("LoginAndQuery,{0}", Global.Status[responseCode.studyNotNecessary].Description + identity);
                        return new SignatureQueryResponse
                        {
                            StatusCode = Global.Status[responseCode.studyNotNecessary].StatusCode,
                            Description = Global.Status[responseCode.studyNotNecessary].Description + identity
                        };
                    }

                    completed = his.Completed == "1" ? true : false;
                    signed = his.Signed == "1" ? true : false;
                    allstatus = his.Studylog;
                }
                else
                {

                    completed = theuser.Completed == "1" ? true : false;
                    signed = theuser.Signed == "1" ? true : false;

                    allstatus = theuser.Studylog;

                }


                return new SignatureQueryResponse
                {

                    StatusCode = Global.Status[responseCode.studyOk].StatusCode,
                    Description = Global.Status[responseCode.studyOk].Description,

                    Completed = completed,
                    Signed = signed,

                    AllStatus = allstatus
                };
            }
            catch (Exception ex)
            {
                Log.Error("LoginAndQuery,{0}", ex);
                return new SignatureQueryResponse
                {
                    StatusCode = Global.Status[responseCode.studyProgramError].StatusCode,
                    Description = Global.Status[responseCode.studyProgramError].Description
                };
            }
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
                var allstatus = string.Empty;
                var allow = true;
                var completed = true;
                var signed = true;
                var firstsigned = true;
                var drivinglicense = string.Empty;
                var deductedmarks = 0;
                var identity = inputRequest.Identity;
                var fname = identity + ".jpg";
                var cryptographicid = CryptographyHelpers.StudyEncrypt(identity);
                var pic = new byte[8];
                var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity || async.Identity == cryptographicid);
                if (theuser == null)
                {
                    var his = _db1.History.FirstOrDefault(async => async.Identity == identity || async.Identity == cryptographicid);
                    if (his == null)
                    {
                        Log.Error("LoginAndQuery,{0}", Global.Status[responseCode.studyNotNecessary].Description + identity);
                        return new LoginAndQueryResponse
                        {
                            StatusCode = Global.Status[responseCode.studyNotNecessary].StatusCode,
                            Description = Global.Status[responseCode.studyNotNecessary].Description + identity
                        };
                    }
                    allow = his.Drugrelated != "1" ? true : false;
                    completed = his.Completed == "1" ? true : false;
                    signed = his.Signed == "1" ? true : false;
                    firstsigned = his.Firstsigned == "1" ? true : false;
                    if (!string.IsNullOrEmpty(his.Drivinglicense)) drivinglicense = his.Drivinglicense;
                    if (his.Deductedmarks != null)
                    {
                        deductedmarks = (int)his?.Deductedmarks;
                    }
                    try
                    {

                        if (!string.IsNullOrEmpty(his.Photofile))
                        {
                            fname = his.Photofile;
                            pic = CryptographyHelpers.StudyFileDecrypt(Path.Combine(Global.PhotoPath, fname));
                        }
                        else
                        {
                            var filename = Path.Combine(Global.PhotoPath, fname);
                            pic = System.IO.File.ReadAllBytes(filename);
                        }

                    }
                    catch (Exception ex)
                    {
                        Log.Error("loginandquery,{0},={1}", identity, ex.Message);
                    }
                    if (allow)
                    {
                        allstatus = his.Studylog;

                    }
                    else allstatus = "您不能参加网络学习，可以参加现场学习";
                }
                else
                {
                    //drugrelated judge
                    allow = theuser.Drugrelated != "1" ? true : false;
                    completed = theuser.Completed == "1" ? true : false;
                    signed = theuser.Signed == "1" ? true : false;
                    firstsigned = theuser.Firstsigned == "1" ? true : false;
                    if (!string.IsNullOrEmpty(theuser.Drivinglicense)) drivinglicense = theuser.Drivinglicense;
                    if (theuser.Deductedmarks != null)
                    {
                        deductedmarks = (int)theuser?.Deductedmarks;
                    }
                    if (allow)
                    {
                        allstatus = theuser.Studylog;
                        //need update?
                        if (!string.IsNullOrEmpty(inputRequest.Name)) theuser.Name = inputRequest.Name;
                        //  theuser.Licensetype = ((int)inputRequest.DrivingLicenseType).ToString();//elements?
                        if (!string.IsNullOrEmpty(inputRequest.Phone)) theuser.Phone = inputRequest.Phone;
                        // theuser.Wechat = inputRequest.Wechat;
                        if (theuser.Startdate == null)
                        {
                            theuser.Startdate = DateTime.Now;
                        }

                        _db1.SaveChanges();
                    }
                    else allstatus = "您不能参加网络学习，可以参加现场学习";
                    try
                    {
                        if (!string.IsNullOrEmpty(theuser.Photofile))
                        {
                            fname = theuser.Photofile;
                            pic = CryptographyHelpers.StudyFileDecrypt(Path.Combine(Global.PhotoPath, fname));
                        }
                        else
                        {
                            var filename = Path.Combine(Global.PhotoPath, fname);
                            pic = System.IO.File.ReadAllBytes(filename);
                        }



                    }
                    catch (Exception ex)
                    {
                        Log.Error("loginandquery,{0},={1}", identity, ex.Message);
                    }
                }

                //token process
                var toke1n = GetToken();
                var found = false;
                var lasttoken = string.Empty;
                foreach (var a in tokens)
                {
                    if (a.Identity == identity)
                    {
                        lasttoken = a.Token;
                        a.Token = toke1n;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    tokens.Add(new Ptoken { Identity = identity, Token = toke1n });
                }


                return new LoginAndQueryResponse
                {
                    Token = toke1n,
                    LastToken = lasttoken,
                    StatusCode = Global.Status[responseCode.studyOk].StatusCode,
                    Description = Global.Status[responseCode.studyOk].Description,
                    AllowedToStudy = allow,
                    Completed = completed,
                    Signed = signed,
                    FirstSigned = firstsigned,
                    DrivingLicense = drivinglicense,
                    DeductedMarks = deductedmarks,
                    Photo = pic,
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
                var cryptographicid = CryptographyHelpers.StudyEncrypt(identity);
                var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity || async.Identity == cryptographicid);
                if (theuser == null)
                {
                    return new CommonResponse { StatusCode = "100004", Description = "error identity" };
                }
                var date = DateTime.Today;
                var dir = string.Format("{0}{1}{2}", date.Year, date.Month, date.Day);
                var fpath = Path.Combine(Global.SignaturePath, dir);
                if (!Directory.Exists(fpath)) Directory.CreateDirectory(fpath);
                var subfpath = identity;
                if (!string.IsNullOrEmpty(theuser.Photofile)) subfpath = theuser.Photofile;
                var fname = Path.Combine(fpath, subfpath + inputRequest.SignatureType);
                var index = inputRequest.SignatureFile.IndexOf("base64,");
                //  Log.Information("LogSignature,{0}", inputRequest.SignatureFile.Substring(index + 7));
                System.IO.File.WriteAllBytes(fname, Convert.FromBase64String(inputRequest.SignatureFile.Substring(index + 7)));

                switch (inputRequest.SignatureType)
                {
                    case SignatureType.PhysicalCondition:
                        theuser.Firstsigned = "1";
                        if (!string.IsNullOrEmpty(inputRequest.PostalAddress))
                        {
                            theuser.Postaladdress = inputRequest.PostalAddress;
                        }
                        break;
                    case SignatureType.EducationalRecord:
                        theuser.Signed = "1";
                        break;
                    default:
                        break;
                }
                if (inputRequest.SignatureType == SignatureType.EducationalRecord)
                {
                    _db1.History.Add(new History
                    {
                        Identity = cryptographicid,
                        Drivinglicense = theuser.Drivinglicense,
                        Deductedmarks = theuser.Deductedmarks,
                        Name = theuser.Name,
                        Syncphone = theuser.Syncphone,
                        Phone = theuser.Phone,
                        Syncdate = theuser.Syncdate,
                        Startdate = theuser.Startdate,
                        Completed = theuser.Completed,
                        Finishdate = DateTime.Now,
                        Stoplicense = theuser.Stoplicense,
                        Noticedate = theuser.Noticedate,
                        Wechat = theuser.Wechat,
                        Studylog = theuser.Studylog,
                        Postaladdress = theuser.Postaladdress,
                        Drugrelated = theuser.Drugrelated,
                        Fullmark = theuser.Fullmark,
                        Inspect = theuser.Inspect,
                        Completelog = theuser.Completelog,
                        Signed = theuser.Signed,
                        Photostatus = theuser.Photostatus,
                        Firstsigned = theuser.Firstsigned,
                        Licensetype = theuser.Licensetype
                    });
                    _db1.User.Remove(theuser);
                }
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
                var cryptographicid = CryptographyHelpers.StudyEncrypt(identity);
                var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity || async.Identity == cryptographicid);
                //  var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity);
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
                var clog = inputRequest.AllStatus.Length < 80 ? inputRequest.AllStatus : inputRequest.AllStatus.Substring(0, 78);
                theuser.Completelog = clog;
                theuser.Completed = "1";

                _db1.SaveChanges();

                if (inputRequest.AllRecords != null)
                {
                    var date = DateTime.Today;
                    var dir = string.Format("{0}{1}{2}", date.Year, date.Month, date.Day);
                    var subfpath = identity;
                    if (!string.IsNullOrEmpty(theuser.Photofile)) subfpath = theuser.Photofile;
                    var fpath = Path.Combine(Global.LogPhotoPath, dir, subfpath);
                    if (!Directory.Exists(fpath)) Directory.CreateDirectory(fpath);

                    var fname = Path.Combine(fpath, "exam_result.txt");
                    Log.Information("filename is: {0}", fname);
                    System.IO.File.WriteAllBytes(fname, inputRequest.AllRecords);
                }

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
                Log.Information("InspectPostStudyStatus,input 170888={0},from ip={1}",
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
                var cryptographicid = CryptographyHelpers.StudyEncrypt(identity);
                var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity || async.Identity == cryptographicid);
                //  var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity);
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
                if (theuser.Studylog.Length < 500) _db1.SaveChanges();
                if (!string.IsNullOrEmpty(inputRequest.CourseTitle))
                {
                    if (inputRequest.Pictures != null)
                    {
                        var date = DateTime.Today;
                        var dir = string.Format("{0}{1}{2}", date.Year, date.Month, date.Day);
                        var subfpath = identity;
                        if (!string.IsNullOrEmpty(theuser.Photofile)) subfpath = theuser.Photofile;
                        var fpath = Path.Combine(Global.LogPhotoPath, dir, subfpath);
                        if (!Directory.Exists(fpath)) Directory.CreateDirectory(fpath);

                        var fname = Path.Combine(fpath, inputRequest.StartTime.ToString() + inputRequest.EndTime.ToString() + ".jpg");
                        Log.Information("filename is: {0}", fname);
                        System.IO.File.WriteAllBytes(fname, inputRequest.Pictures);
                    }
                }

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
                var cryptographicid = CryptographyHelpers.StudyEncrypt(identity);
                var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity || async.Identity == cryptographicid);
                // var theuser = _db1.User.FirstOrDefault(async => async.Identity == identity);
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
                var pic = new byte[1];
                var photook = true;
                try
                {
                    Log.Information("InspectGetLearnerInfo,{0},={1}", Global.PhotoPath, Global.PhotoPath);
                    var ff = identity + ".jpg";
                    if (!string.IsNullOrEmpty(theuser.Photofile))
                    {
                        ff = theuser.Photofile;
                        var filename = Path.Combine(Global.PhotoPath, ff);
                        pic = CryptographyHelpers.StudyFileDecrypt(filename);
                    }
                    else
                    {
                        var filename = Path.Combine(Global.PhotoPath, ff);
                        pic = System.IO.File.ReadAllBytes(filename);
                    }


                }
                catch (Exception ex)
                {
                    photook = false;
                    Log.Error("InspectGetLearnerInfo,{0},={1}", identity, ex.Message);
                }

                return new GetLearnerInfoResponse
                {
                    StatusCode = Global.Status[responseCode.ok].StatusCode,
                    Description = Global.Status[responseCode.ok].Description,
                    DrivingLicenseType = string.IsNullOrEmpty(theuser.Licensetype) ? DrivingLicenseType.Unknown : (DrivingLicenseType)int.Parse(theuser.Licensetype),

                    //   Identity = theuser.Identity,
                    Name = theuser.Name,
                    PhotoOk = photook,
                    Photo = pic
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
        [Route("salt")]
        [HttpGet]

        public GetLearnerInfoResponse salt(string token)
        {
            try
            {

                if (string.IsNullOrEmpty(token))
                {

                    return new GetLearnerInfoResponse
                    {
                        StatusCode = Global.Status[responseCode.TokenError].StatusCode,
                        Description = Global.Status[responseCode.TokenError].Description
                    };
                }
                var key = "2cff5601e52f4747bfb9e271fe45042a";
                var salt = "d31beaac47b44b45b1c6066712d49ff6";
                var original_value = token;
                var encrypted_value = CryptographyHelpers.Encrypt(key, salt, original_value);
                var target = CryptographyHelpers.Decrypt(key, salt, encrypted_value);


                return new GetLearnerInfoResponse
                {
                    Name = encrypted_value,
                    //   Identity = encrypted_value.Length.ToString(),
                    StatusCode = encrypted_value.Length.ToString(),
                    Description = target,

                };
            }
            catch (Exception ex)
            {

                return new GetLearnerInfoResponse
                {
                    StatusCode = Global.Status[responseCode.ProgramError].StatusCode,
                    Description = Global.Status[responseCode.ProgramError].Description + ex.Message
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
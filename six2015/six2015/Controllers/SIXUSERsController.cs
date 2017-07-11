﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using six2015.Models;
using six2015.request;
using Newtonsoft.Json;
using log4net;
using System.Reflection;
using System.Threading.Tasks;
using InternalEncrypt;
using System.Configuration;

namespace six2015.Controllers
{
    public class SIXUSERsController : ApiController
    {
        private Model1 _db1 = new Model1();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static List<Ptoken> tokens = new List<Ptoken>();
       // public static readonly string DataSource = ConfigurationManager.ConnectionStrings["signaturepath"].ConnectionString;
        public static readonly string signaturepath = ConfigurationManager.AppSettings["signaturepath"];
        class Ptoken
        {
            public string Identity { get; set; }
            public string Token { get; set; }
            public int power { get; set; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db1.Dispose();
            }
            base.Dispose(disposing);
        }
        [Route("pushmessage")]
        [HttpPost]
        public userresponse pushmessage([FromBody] messagerequest inputRequest)
        {
            try
            {
                var input = JsonConvert.SerializeObject(inputRequest);
                if (inputRequest == null)
                {
                    Log.InfoFormat("pushmessage,{0}", sixerrors.invalidrequest);
                    return new userresponse
                    {
                        status = (int)sixerrors.invalidrequest
                    };
                }
                Log.InfoFormat("pushmessage,input={0},", input);//, Request.HttpContext.Connection.RemoteIpAddress);
                var token = Request.Headers.GetValues("Token").First();
                Log.InfoFormat("pushmessage,token is {0},{1},id", token, inputRequest.id);
                var found = false;
                foreach (var a in tokens)
                {
                    if (a.Token == token)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Log.InfoFormat("pushmessage,{0}", sixerrors.invalidtoken);
                    return new userresponse
                    {
                        status = (int)sixerrors.invalidtoken
                    };
                }

                var theusers = _db1.HISTORY.FirstOrDefault(b => b.ID == inputRequest.id);
                if (theusers == null)
                {
                    Log.InfoFormat("pushmessage,{0}", sixerrors.invalididentity);
                    return new userresponse
                    {
                        status = (int)sixerrors.invalididentity
                    };
                }
                theusers.MESSAGED = "1";

                _db1.MESSAGE.Add(new MESSAGE
                {
                    TIME=DateTime.Now,CONTENT=inputRequest.content,HISTORYID=inputRequest.id
                });
                _db1.SaveChanges();

                return new userresponse
                {
                    status = 0,
                };
            }
            catch (Exception ex)
            {
                Log.Error("pushmessage", ex);
                return new userresponse
                {
                    status = (int)sixerrors.processerror
                };
            }
        }
        [Route("processed")]
        [HttpPost]
        public userresponse processed([FromBody] commonrequest inputRequest)
        {
            try
            {
                var input = JsonConvert.SerializeObject(inputRequest);
                if (inputRequest == null)
                {
                    Log.InfoFormat("processed,{0}", sixerrors.invalidrequest);
                    return new userresponse
                    {
                        status = (int)sixerrors.invalidrequest
                    };
                }
                Log.InfoFormat("processed,input={0},", input);//, Request.HttpContext.Connection.RemoteIpAddress);
                var token = Request.Headers.GetValues("Token").First();
                Log.InfoFormat("processed,token is {0},{1},id", token, inputRequest.id);
                var found = false;
                foreach (var a in tokens)
                {
                    if (a.Token == token)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Log.InfoFormat("processed,{0}", sixerrors.invalidtoken);
                    return new userresponse
                    {
                        status = (int)sixerrors.invalidtoken
                    };
                }

                var theusers = _db1.HISTORY.FirstOrDefault(b => b.ID == inputRequest.id);
                if (theusers == null)
                {
                    Log.InfoFormat("processed,{0}", sixerrors.invalididentity);
                    return new userresponse
                    {
                        status = (int)sixerrors.invalididentity
                    };
                }
                theusers.PROCESSED = "1";
                _db1.SaveChanges();

                return new userresponse
                {
                    status = 0,
                };
            }
            catch (Exception ex)
            {
                Log.Error("processed", ex);
                return new userresponse
                {
                    status = (int)sixerrors.processerror
                };
            }
        }
        [Route("printed")]
        [HttpPost]
        public userresponse printed([FromBody] commonrequest inputRequest)
        {
            try
            {

                var input = JsonConvert.SerializeObject(inputRequest);
                if (inputRequest == null)
                {
                    Log.InfoFormat("printed,{0}", sixerrors.invalidrequest);
                    return new userresponse
                    {
                        status = (int)sixerrors.invalidrequest
                    };
                }
                Log.InfoFormat("printed,input={0},", input);//, Request.HttpContext.Connection.RemoteIpAddress);
                var token = Request.Headers.GetValues("Token").First();
                Log.InfoFormat("printed,token is {0},{1},id", token, inputRequest.id);
                var found = false;
                foreach (var a in tokens)
                {
                    if (a.Token == token)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Log.InfoFormat("printed,{0}", sixerrors.invalidtoken);
                    return new userresponse
                    {
                        status = (int)sixerrors.invalidtoken
                    };
                }


                var theusers = _db1.HISTORY.FirstOrDefault(b => b.ID == inputRequest.id);
                if (theusers == null)
                {
                    Log.InfoFormat("printed,{0}", sixerrors.invalididentity);
                    return new userresponse
                    {
                        status = (int)sixerrors.invalididentity
                    };
                }
                theusers.PRINTED = "1";
                _db1.SaveChanges();

                return new userresponse
                {
                    status = 0,
                };
            }
            catch (Exception ex)
            {
                Log.Error("printed", ex);
                return new userresponse
                {
                    status = (int)sixerrors.processerror
                };
            }
        }
        [Route("login")]
        [HttpPost]
        public loginresponse login([FromBody] loginrequest inputRequest)
        {
            try
            {

                var input = JsonConvert.SerializeObject(inputRequest);
             //   LogRequest(input, "SignatureQuery", Request.HttpContext.Connection.RemoteIpAddress.ToString());

                if (inputRequest == null)
                {
                    Log.InfoFormat("login,{0}", sixerrors.invalidrequest);
                    return new loginresponse
                    {
                        status=(int)sixerrors.invalidrequest
                    };
                }
                Log.InfoFormat("login,input={0},", input);//, Request.HttpContext.Connection.RemoteIpAddress);
                var allstatus = string.Empty;
                var identity = inputRequest.username;

                var theuser = _db1.SIXUSER.FirstOrDefault(async => async.NAME == identity);//|| async.Identity == cryptographicid);
                if (theuser == null)
                {
                    Log.InfoFormat("login,{0}", sixerrors.nouser);
                    return new loginresponse
                    {
                        status = (int)sixerrors.nouser
                    };
                }
                if (theuser.PASSWORD != inputRequest.password)

                {
                    Log.InfoFormat("login,{0}", sixerrors.errorpassword);
                    return new loginresponse
                    {
                        status = (int)sixerrors.errorpassword
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
                        a.power = (int)theuser.POWER;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    tokens.Add(new Ptoken { Identity = identity, Token = toke1n,power= (int)theuser.POWER });
                }


                //   var cryptographicid = CryptographyHelpers.StudyEncrypt(identity);
            
                return new loginresponse
                {
                    status = 0,token= toke1n,power=(int)theuser.POWER
                };
            }
            catch (Exception ex)
            {
                Log.Error("login,{0}", ex);
                return new loginresponse
                {
                    status = (int)sixerrors.processerror
                };
            }
        }


        [Route("users")]
        [HttpGet]
        public async Task<usersresponse> users(int startNum, int endNum)
        {
            try
            {
                var found = false;
                var token = Request.Headers. GetValues("Token").First();
                Log.InfoFormat("token is {0},{1},{2}", token,startNum,endNum);
                var username = string.Empty;
                var power = 0;
                foreach (var a in tokens)
                {
                    if (a.Token == token)
                    {
                        username = a.Identity;
                        power = a.power;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Log.InfoFormat("login,{0}", sixerrors.invalidtoken);
                    return new usersresponse
                    {
                        status = (int)sixerrors.invalidtoken
                    };
                }

                if (power!=1)
                {
                    Log.InfoFormat("login,{0}", sixerrors.unauthorized);
                    return new usersresponse
                    {
                        status = (int)sixerrors.unauthorized
                    };
                }
                //   var cryptographicid = CryptographyHelpers.StudyEncrypt(identity);
                var end = 100;
                if (endNum != null && endNum > 0) end = endNum;
                var theusers= _db1.SIXUSER.Take(end);
                var ulist = new List<oneuser>();
                var index = 0;
             foreach(var theone in theusers)
                {
                    if (index++ < startNum) continue;
                    ulist.Add(new oneuser
                    {
                        username=theone.NAME,power=(int)theone.POWER
                    });
                }
               
                return new usersresponse
                {
                    status = 0,users=ulist
                  
                };
            }
            catch (Exception ex)
            {
                Log.Error("users,{0}", ex);
                return new usersresponse
                {
                    status = (int)sixerrors.processerror
                };
            }
        }

        [Route("user")]
        [HttpPut]
        public async Task<userresponse> user([FromBody] userrequest inputRequest)
        {
            try
            {
                var input = JsonConvert.SerializeObject(inputRequest);
                var found = false;
                var token = Request.Headers.GetValues("Token").First();
                Log.InfoFormat("user token is {0},input={1}", token, input);

                var username = string.Empty;
                var power = 0;
                foreach (var a in tokens)
                {
                    if (a.Token == token)
                    {
                        username = a.Identity;
                        power = a.power;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Log.InfoFormat("login,{0}", sixerrors.invalidtoken);
                    return new userresponse
                    {
                        status = (int)sixerrors.invalidtoken
                    };
                }

                if (power != 1)
                {
                    Log.InfoFormat("login,{0}", sixerrors.unauthorized);
                    return new userresponse
                    {
                        status = (int)sixerrors.unauthorized
                    };
                }
                //   var cryptographicid = CryptographyHelpers.StudyEncrypt(identity);
               
                var theusers = _db1.SIXUSER.FirstOrDefault(c=>c.NAME==inputRequest.username);
                if (theusers == null) {
                    if (string.IsNullOrEmpty(inputRequest.password))
                    {
                        Log.InfoFormat("user,{0}", sixerrors.passwordisnull);
                        return new userresponse
                        {
                            status = (int)sixerrors.passwordisnull
                        };
                    }
                    if (inputRequest.power == null)
                    {
                        Log.InfoFormat("user,{0}", sixerrors.invalidpower);
                        return new userresponse
                        {
                            status = (int)sixerrors.invalidpower
                        };
                    }
                    _db1.SIXUSER.Add(new SIXUSER
                    {
                        NAME=inputRequest.username,PASSWORD= inputRequest.password,POWER= (int)inputRequest.power
                    });
                }
                else
                {
                    if (!string.IsNullOrEmpty(inputRequest.password)) theusers.PASSWORD = inputRequest.password;
                    if (inputRequest.power != null) theusers.POWER = (int)inputRequest.power;
                }
                _db1.SaveChanges();
                return new userresponse
                {
                    status = 0,
                };
            }
            catch (Exception ex)
            {
                Log.Error("user,exception", ex);
                return new userresponse
                {
                    status = (int)sixerrors.processerror
                };
            }
        }

        [Route("deleteuser")]
        [HttpDelete]
        public async Task<userresponse> deleteuser(string username)
        {
            try
            {
               
                var found = false;
                var token = Request.Headers.GetValues("Token").First();
                Log.InfoFormat("deleteuser token is {0},input={1}", token, username);
                
                var power = 0;
                foreach (var a in tokens)
                {
                    if (a.Token == token)
                    {
                        power = a.power;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Log.InfoFormat("login,{0}", sixerrors.invalidtoken);
                    return new userresponse
                    {
                        status = (int)sixerrors.invalidtoken
                    };
                }

                if (power != 1)
                {
                    Log.InfoFormat("login,{0}", sixerrors.unauthorized);
                    return new userresponse
                    {
                        status = (int)sixerrors.unauthorized
                    };
                }
                //   var cryptographicid = CryptographyHelpers.StudyEncrypt(identity);

                var theusers = _db1.SIXUSER.FirstOrDefault(c => c.NAME == username);
                if (theusers == null)
                {
                    
                        Log.InfoFormat("user,{0}", sixerrors.nouser);
                        return new userresponse
                        {
                            status = (int)sixerrors.nouser
                        };
                    
                }

                //   _db1.SIXUSER.Remove(new SIXUSER { NAME = username ,PASSWORD=theusers.PASSWORD,POWER=theusers.POWER});
                _db1.SIXUSER.Remove(theusers);
                  _db1.SaveChanges();
                return new userresponse
                {
                    status = 0,
                };
            }
            catch (Exception ex)
            {
                Log.Error("deleteuser,{0}", ex);
                return new userresponse
                {
                    status = (int)sixerrors.processerror
                };
            }
        }


        [Route("EduRecordForm")]
        [HttpGet]
        public async Task<picresponse> EduRecordForm(int picID)
        {
            try
            {
                var found = false;
                var token = Request.Headers.GetValues("Token").First();
                Log.InfoFormat("EduRecordForm,token is {0},{1},picID", token, picID);
                var username = string.Empty;
                var power = 0;
                foreach (var a in tokens)
                {
                    if (a.Token == token)
                    {
                        username = a.Identity;
                        power = a.power;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Log.InfoFormat("EduRecordForm,{0}", sixerrors.invalidtoken);
                    return new picresponse
                    {
                        status = (int)sixerrors.invalidtoken
                    };
                }

                //var cypher = new CryptographyHelpers();
                //   var cryptographicid = cypher.StudyEncrypt(picID);

                var theusers = _db1.HISTORY.FirstOrDefault(b => b.ID == picID);
                if (theusers==null)
                {
                    Log.InfoFormat("EduRecordForm,{0}", sixerrors.invalididentity);
                    return new picresponse
                    {
                        status = (int)sixerrors.invalididentity
                    };
                }

                var fname = System.IO.Path.Combine(signaturepath, theusers.FILENAME + SignatureType.EducationalRecord+".png");
                Log.InfoFormat("EduRecordForm,picfile={0}", fname);
                var bbbytes = System.IO.File.ReadAllBytes(fname);
                var basestr = Convert.ToBase64String(bbbytes);
                return new picresponse
                {
                    status = 0,pic=basestr

                };
            }
            catch (Exception ex)
            {
                Log.Error("EduRecordForm,{0}", ex);
                return new picresponse
                {
                    status = (int)sixerrors.processerror
                };
            }
        }
        [Route("DeclarationForm")]
        [HttpGet]
        public async Task<picresponse> DeclarationForm(int picID)
        {
            try
            {
                var found = false;
                var token = Request.Headers.GetValues("Token").First();
                Log.InfoFormat("DeclarationForm,token is {0},{1},picID", token, picID);
                var username = string.Empty;
                var power = 0;
                foreach (var a in tokens)
                {
                    if (a.Token == token)
                    {
                        username = a.Identity;
                        power = a.power;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Log.InfoFormat("DeclarationForm,{0}", sixerrors.invalidtoken);
                    return new picresponse
                    {
                        status = (int)sixerrors.invalidtoken
                    };
                }

                //var cypher = new CryptographyHelpers();
                //var cryptographicid = cypher.StudyEncrypt(picID);

                var theusers = _db1.HISTORY.FirstOrDefault(b => b.ID == picID);
                if (theusers == null)
                {
                    Log.InfoFormat("DeclarationForm,{0}", sixerrors.invalididentity);
                    return new picresponse
                    {
                        status = (int)sixerrors.invalididentity
                    };
                }

                var fname = System.IO.Path.Combine(signaturepath, theusers.FILENAME + SignatureType.PhysicalCondition+".png");
                Log.InfoFormat("DeclarationForm,picfile={0}", fname);
                var bbbytes = System.IO.File.ReadAllBytes(fname);
                var basestr = Convert.ToBase64String(bbbytes);
                return new picresponse
                {
                    status = 0,
                    pic = basestr

                };
            }
            catch (Exception ex)
            {
                Log.Error("DeclarationForm,{0}", ex);
                return new picresponse
                {
                    status = (int)sixerrors.processerror
                };
            }
        }

        [Route("StudyRecords")]
        [HttpGet]
        public async Task<StudyRecordsresponse> StudyRecords(string startTime, string endTime , string name ,
            string identity ,int? illegal ,int? message, int? print)
        {
            try
            {
                var found = false;
                var token = Request.Headers.GetValues("Token").First();
                Log.InfoFormat("StudyRecords,token is {0},{1},startTime,  endTime,{2}  name,{3}  identity,{4} illegal ,{5}  message,{6} print,{7}",
                    token,  startTime,  endTime,  name,  identity, illegal ,  message, print);
              

                foreach (var a in tokens)
                {
                    if (a.Token == token)
                    {
                       
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Log.InfoFormat("StudyRecords,{0}", sixerrors.invalidtoken);
                    return new StudyRecordsresponse
                    {
                        status = (int)sixerrors.invalidtoken
                    };
                }

                var start = DateTime.Now;
                if (!DateTime.TryParse(startTime, out start))
               //     if (string.IsNullOrEmpty(startTime))
                {
                    Log.InfoFormat("StudyRecords,{0}", sixerrors.invalidstarttime);
                    return new StudyRecordsresponse
                    {
                        status = (int)sixerrors.invalidstarttime
                    };
                }
                //  var start = DateTime.Parse(startTime);
                var end = DateTime.Now;
                if (!DateTime.TryParse(endTime, out end))
                {
                    Log.InfoFormat("StudyRecords,{0}", sixerrors.invalidendtime);
                    return new StudyRecordsresponse
                    {
                        status = (int)sixerrors.invalidendtime
                    };
                }
               

                var theusers = _db1.HISTORY.Where(b => b.TIME.CompareTo(start) >= 0&&b.TIME.CompareTo(end) <= 0);

                if (!string.IsNullOrEmpty(name))
                {
                    theusers = theusers.Where(c => c.NAME == name);
                }
                var cypher = new CryptographyHelpers();
                var cryptographicid = cypher.StudyEncrypt(identity);
                if (!string.IsNullOrEmpty(identity))
                {                   
                    theusers = theusers.Where(c => c.IDCARD == cryptographicid);
                }
                if (illegal!=null&&illegal==1)
                {
                    theusers = theusers.Where(c => c.STATUS != null&& c.STATUS != string.Empty);
                }
                if (print != null && print == 1)
                {
                    theusers = theusers.Where(c => c.PRINTED == "1");
                }
                if (message != null && message == 1)
                {
                    theusers = theusers.Where(c => c.MESSAGED == "1");
                }
                theusers = theusers.Take(1000);

                var records = new List<record>();
                foreach(var a in theusers)
                {
                    var recodr = new record
                    {
                        id = a.ID.ToString(),
                        name = a.NAME,
                        phone = a.PHONENUMBER,
                        identity = cypher.StudyDecrypt(a.IDCARD),
                        studyTime = a.TIME,
                        illegal = a.STATUS
                    };
                    if (a.MESSAGED=="1")
                    {
                        var mess = _db1.MESSAGE.Where(aa => aa.HISTORYID == a.ID);
                        var messages = new List<message>();
                        foreach(var b in mess)
                        {
                            messages.Add(new message
                            {
                                dateTime=b.TIME,content=b.CONTENT
                            });
                        }
                        recodr.message = messages;
                    }
                    records.Add(recodr);
                }
                return new StudyRecordsresponse
                {
                    status = 0,records=records
                };
            }
            catch (Exception ex)
            {
                Log.Error("StudyRecords,{0}", ex);
                return new StudyRecordsresponse
                {
                    status = (int)sixerrors.processerror
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
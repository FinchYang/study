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
using System.Data.Entity.Validation;
using System.Web.UI.WebControls;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Web.Http.Cors;

namespace six2015.Controllers
{
 //   [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SIXUSERsController : ApiController
    {
        private Model1 _db1 = new Model1();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static List<Ptoken> tokens = new List<Ptoken>();
       // public static readonly string DataSource = ConfigurationManager.ConnectionStrings["signaturepath"].ConnectionString;
        public static readonly string signaturepath = ConfigurationManager.AppSettings["signaturepath"];
        public static readonly string signaturepicspath = ConfigurationManager.AppSettings["signaturepicspath"];
        public static readonly string examresultpath = ConfigurationManager.AppSettings["examresultpath"];
        public static readonly string illegalunprocessed = ConfigurationManager.AppSettings["illegalunprocessed"];
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
        [Route("pushmessage2")]
        [HttpPost]
        public userresponse pushmessage2([FromBody] messagerequest2 inputRequest)
        {
            try
            {
                var input = JsonConvert.SerializeObject(inputRequest);
                if (inputRequest == null)
                {
                    Log.InfoFormat("pushmessage2,{0}", sixerrors.invalidrequest);
                    return new userresponse
                    {
                        status = (int)sixerrors.invalidrequest
                    };
                }
                Log.InfoFormat("pushmessage2,input={0},", input);//, Request.HttpContext.Connection.RemoteIpAddress);
                var token = Request.Headers.GetValues("Token").First();
                Log.InfoFormat("pushmessage2,token is {0},{1},id", token, inputRequest.id);
                var found = false;
                foreach (var a in tokens)
                {
                    if (a.Token == token)
                    {
                        found = true;
                        break;
                    }
                }
                Log.InfoFormat("pushmessage2,{0},,", 111);
                if (!found)
                {
                    Log.InfoFormat("pushmessage2,{0}", sixerrors.invalidtoken);
                    return new userresponse
                    {
                        status = (int)sixerrors.invalidtoken
                    };
                }
                Log.InfoFormat("pushmessage2,{0},,", 222);
                var theusers = _db1.HISTORY.FirstOrDefault(b => b.ID == inputRequest.id);
                if (theusers == null)
                {
                    Log.InfoFormat("pushmessage2,{0}", sixerrors.invalididentity);
                    return new userresponse
                    {
                        status = (int)sixerrors.invalididentity
                    };
                }
                Log.InfoFormat("pushmessage2,{0},,", 333);
                theusers.MESSAGED = "1";
                theusers.PROCESSED = inputRequest.processed.ToString();
                theusers.OVERTIME = DateTime.Now;
                theusers.FAILURE = inputRequest.failure.ToString();
                Log.InfoFormat("pushmessage2,{0},,", 444);
                _db1.MESSAGE.Add(new MESSAGE
                {
                    TIME = DateTime.Now,
                    CONTENT = inputRequest.message,
                    HISTORYID = inputRequest.id,
                    SENT = "0"
                });
                Log.InfoFormat("pushmessage2,{0},,", 555);
                if (inputRequest.failure != 1) {
                    var theab = _db1.ABSTUDY.FirstOrDefault(b => b.IDCARD == theusers.IDCARD);
                    if (theab != null)
                    {
                        _db1.ABSTUDY.Remove(theab);
                    }
                }
                Log.InfoFormat("pushmessage2,{0},,", 666);
                _db1.SaveChangesAsync();
                Log.InfoFormat("pushmessage2,{0},,", 777);
                return new userresponse
                {
                    status = 0,
                };
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Log.InfoFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Log.InfoFormat("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return new userresponse
                {
                    status = (int)sixerrors.processerror
                };
            }
            catch (EntityDataSourceValidationException ex)
            {
                Log.Error("EntityDataSourceValidationException", ex);
                return new userresponse
                {
                    status = (int)sixerrors.processerror
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
                    TIME=DateTime.Now,CONTENT=inputRequest.content,HISTORYID=inputRequest.id,SENT="0"
                });
                _db1.SaveChanges();

                return new userresponse
                {
                    status = 0,
                };
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Log.InfoFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Log.InfoFormat("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return new userresponse
                {
                    status = (int)sixerrors.processerror
                };
            }
            catch (EntityDataSourceValidationException ex)
            {
                Log.Error("EntityDataSourceValidationException", ex);
                return new userresponse
                {
                    status = (int)sixerrors.processerror
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
                if (theusers.STATUS.Contains('H'))
                {
                    Log.InfoFormat("processed,{0}", sixerrors.illegalunprocessed);
                    illegalunprocessedmsg(inputRequest);
                    return new userresponse
                    {
                        status = (int)sixerrors.illegalunprocessed
                    };
                }
                var current = _db1.ABSTUDY.FirstOrDefault(b => b.IDCARD == theusers.IDCARD);
                if (current != null && current.STATUS.Contains('H'))
                {
                    Log.InfoFormat("processed,{0}", sixerrors.illegalunprocessed);
                    illegalunprocessedmsg(inputRequest);
                    return new userresponse
                    {
                        status = (int)sixerrors.illegalunprocessed
                    };
                }               

                theusers.PROCESSED = "1";
                _db1.SaveChangesAsync();

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
        [Route("failure")]
        [HttpPost]
        public userresponse failure([FromBody] commonrequest inputRequest)
        {
            try
            {
                var input = JsonConvert.SerializeObject(inputRequest);
                if (inputRequest == null)
                {
                    Log.InfoFormat("failure,{0}", sixerrors.invalidrequest);
                    return new userresponse
                    {
                        status = (int)sixerrors.invalidrequest
                    };
                }
                Log.InfoFormat("failure,input={0},", input);//, Request.HttpContext.Connection.RemoteIpAddress);
                var token = Request.Headers.GetValues("Token").First();
                Log.InfoFormat("failure,token is {0},{1},id", token, inputRequest.id);
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
                    Log.InfoFormat("failure,{0}", sixerrors.invalidtoken);
                    return new userresponse
                    {
                        status = (int)sixerrors.invalidtoken
                    };
                }

                var theusers = _db1.HISTORY.FirstOrDefault(b => b.ID == inputRequest.id);
                if (theusers == null)
                {
                    Log.InfoFormat("failure,{0}", sixerrors.invalididentity);
                    return new userresponse
                    {
                        status = (int)sixerrors.invalididentity
                    };
                }        
               

                theusers.FAILURE = "1";
                _db1.SaveChangesAsync();

                return new userresponse
                {
                    status = 0,
                };
            }
            catch (Exception ex)
            {
                Log.Error("failure", ex);
                return new userresponse
                {
                    status = (int)sixerrors.processerror
                };
            }
        }

        private void illegalunprocessedmsg(commonrequest inputRequest)
        {
            _db1.MESSAGE.Add(new MESSAGE
            {
                TIME = DateTime.Now,
                CONTENT = illegalunprocessed,
                HISTORYID = inputRequest.id,
                SENT = "0"
            });
            _db1.SaveChangesAsync();
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


        [Route("signaturepics")]
        [HttpGet]
        public async Task<signaturepicsresponse> signaturepics(int id)
        {
            try
            {
                var found = false;
                var token = Request.Headers.GetValues("Token").First();
                Log.InfoFormat("signaturepics,token is {0},{1},id", token, id);
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
                    Log.InfoFormat("signaturepics,{0}", sixerrors.invalidtoken);
                    return new signaturepicsresponse
                    {
                        status = (int)sixerrors.invalidtoken
                    };
                }

                //var cypher = new CryptographyHelpers();
                //   var cryptographicid = cypher.StudyEncrypt(picID);

                var theusers = _db1.HISTORY.FirstOrDefault(b => b.ID == id);
                if (theusers==null)
                {
                    Log.InfoFormat("signaturepics,{0}", sixerrors.invalididentity);
                    return new signaturepicsresponse
                    {
                        status = (int)sixerrors.invalididentity
                    };
                }

                var fname = System.IO.Path.Combine(signaturepicspath, theusers.FILENAME + SignatureType.EducationalRecord);
               
                Log.InfoFormat("signaturepics,picfile={0}", fname);
                var bbbytes = System.IO.File.ReadAllBytes(fname);
                var basestr = Convert.ToBase64String(bbbytes);

                var fname1 = System.IO.Path.Combine(signaturepicspath, theusers.FILENAME + SignatureType.PhysicalCondition);
                Log.InfoFormat("signaturepics,PhysicalCondition={0}", fname1);
                var PhysicalConditionbbbytes = System.IO.File.ReadAllBytes(fname1);
                var PhysicalConditionbasestr = Convert.ToBase64String(PhysicalConditionbbbytes);

                return new signaturepicsresponse
                {
                    status = 0,
                    EducationalRecord = basestr,
                   PhysicalCondition= PhysicalConditionbasestr
                };
            }
            catch (Exception ex)
            {
                Log.Error("signaturepics,{0}", ex);
                return new signaturepicsresponse
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
                if (theusers == null)
                {
                    Log.InfoFormat("EduRecordForm,{0}", sixerrors.invalididentity);
                    return new picresponse
                    {
                        status = (int)sixerrors.invalididentity
                    };
                }

                var fname = System.IO.Path.Combine(signaturepath, theusers.FILENAME + SignatureType.EducationalRecord + "1.png");
                Log.InfoFormat("EduRecordForm,picfile={0}", fname);
                var bbbytes = System.IO.File.ReadAllBytes(fname);
                var basestr = Convert.ToBase64String(bbbytes);
                return new picresponse
                {
                    status = 0,
                    pic = basestr
                    //   pic = bbbytes
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
        [Route("EduRecordFormB")]
        [HttpGet]
        public async Task<picresponse> EduRecordFormB(int picID)
        {
            try
            {
                var found = false;
                var token = Request.Headers.GetValues("Token").First();
                Log.InfoFormat("EduRecordFormB,token is {0},{1},picID", token, picID);
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
                    Log.InfoFormat("EduRecordFormB,{0}", sixerrors.invalidtoken);
                    return new picresponse
                    {
                        status = (int)sixerrors.invalidtoken
                    };
                }

                //var cypher = new CryptographyHelpers();
                //   var cryptographicid = cypher.StudyEncrypt(picID);

                var theusers = _db1.HISTORY.FirstOrDefault(b => b.ID == picID);
                if (theusers == null)
                {
                    Log.InfoFormat("EduRecordFormB,{0}", sixerrors.invalididentity);
                    return new picresponse
                    {
                        status = (int)sixerrors.invalididentity
                    };
                }

                var fname = System.IO.Path.Combine(signaturepath, theusers.FILENAME + SignatureType.EducationalRecord+    "2.png");
                Log.InfoFormat("EduRecordFormB,picfile={0}", fname);
                var bbbytes = System.IO.File.ReadAllBytes(fname);
                var basestr = Convert.ToBase64String(bbbytes);
                return new picresponse
                {
                    status = 0,
                    pic = basestr
                    //   pic = bbbytes
                };
            }
            catch (Exception ex)
            {
                Log.Error("EduRecordFormB,{0}", ex);
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
                  //  pic = bbbytes
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
        [Route("ExamResult")]
        [HttpGet]
        public async Task<examresponse> ExamResult(int id)
        {
            try
            {
                var found = false;
                var token = Request.Headers.GetValues("Token").First();
                Log.InfoFormat("ExamResult,token is {0},{1}", token, id);
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
                    Log.InfoFormat("ExamResult,{0}", sixerrors.invalidtoken);
                    return new examresponse
                    {
                        status = (int)sixerrors.invalidtoken
                    };
                }

                var theusers = _db1.HISTORY.FirstOrDefault(b => b.ID == id);
                if (theusers == null)
                {
                    Log.InfoFormat("ExamResult,{0}", sixerrors.invalididentity);
                    return new examresponse
                    {
                        status = (int)sixerrors.invalididentity
                    };
                }

                var fname = System.IO.Path.Combine(examresultpath, theusers.FILENAME , "exam_result.txt");
                Log.InfoFormat("ExamResult,picfile={0}", fname);
                var bbbytes = System.IO.File.ReadAllBytes(fname);
                var basestr = Convert.ToBase64String(bbbytes);
                return new examresponse
                {
                    status = 0,
                    //  pic = bbbytes
                    content = basestr
                };
            }
            catch (Exception ex)
            {
                Log.Error("ExamResult,{0}", ex);
                return new examresponse
                {
                    status = (int)sixerrors.processerror
                };
            }
        }
        [Route("courses")]
        [HttpGet]
        public async Task<examresponse> courses(int id)
        {
            try
            {
                var found = false;
                var token = Request.Headers.GetValues("Token").First();
                Log.InfoFormat("courses,token is {0},{1}", token, id);
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
                    Log.InfoFormat("courses,{0}", sixerrors.invalidtoken);
                    return new examresponse
                    {
                        status = (int)sixerrors.invalidtoken
                    };
                }

                var theusers = _db1.HISTORY.FirstOrDefault(b => b.ID == id);
                if (theusers == null)
                {
                    Log.InfoFormat("courses,{0}", sixerrors.invalididentity);
                    return new examresponse
                    {
                        status = (int)sixerrors.invalididentity
                    };
                }

              
                return new examresponse
                {
                    status = 0,
                    content = theusers.STUDYLOG
                };
            }
            catch (Exception ex)
            {
                Log.Error("courses,{0}", ex);
                return new examresponse
                {
                    status = (int)sixerrors.processerror
                };
            }
        }
        [Route("pictures")]
        [HttpGet]
        public async Task<picturesresponse> pictures(int id)
        {
            try
            {
                var found = false;
                var token = Request.Headers.GetValues("Token").First();
                Log.InfoFormat("pictures,aaa,token is {0},{1}", token, id);
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
                    Log.InfoFormat("pictures,{0}", sixerrors.invalidtoken);
                    return new picturesresponse
                    {
                        status = (int)sixerrors.invalidtoken
                    };
                }

                var theusers = _db1.HISTORY.FirstOrDefault(b => b.ID == id);
                if (theusers == null)
                {
                    Log.InfoFormat("pictures,{0}", sixerrors.invalididentity);
                    return new picturesresponse
                    {
                        status = (int)sixerrors.invalididentity
                    };
                }
                var slog = theusers.STUDYLOG;
                var courses = slog.Split('-');
                var coursestatus = new List<coursestatus>();
                foreach (var course in courses)
                {
                    if (course.Length < 10) continue;
                   
                    var sss = course.Split(',');
                    var title = sss[0];
                    var start = getdate(sss[1]);
                    var end = getdate(sss[2]);
                    Log.InfoFormat("pictures,bbb,{0},{1},{2},{3},{4},{5}", sss[1],sss[2], start,end,title,DateTime.Now.Ticks);
                    var fname = sss[1] + sss[2] + ".zip";
                    var zipfile = System.IO.Path.Combine(examresultpath, theusers.FILENAME, fname);
                    var temp=Path.Combine(System.Web.HttpContext.Current.Request.MapPath("/"),"logphoto", theusers.FILENAME, sss[1] + sss[2]);
                    if (!Directory.Exists(temp)) Directory.CreateDirectory(temp);
                    var a = new FastZip();
                    Log.InfoFormat("temp extract path={0}", temp);
                    a.ExtractZip(zipfile, temp, "");
                    var picp = new DirectoryInfo(temp);
                    var pics=picp.GetFiles();
                    var onestatus = new coursestatus();
                    onestatus.title = title;
                    onestatus.start = start;
                    onestatus.end = end;
                    var coursepics = new List<coursepics>();                  

                    foreach (var onepic in pics)
                    {
                        var ts =getdate( onepic.Name.Substring(0,10));//onepic.LastWriteTime
                        Log.InfoFormat("logphoto name={0}", onepic.FullName);
                        var bbbytes = System.IO.File.ReadAllBytes(onepic.FullName);
                        var basestr = Convert.ToBase64String(bbbytes);
                        coursepics.Add(new coursepics {
                            timestamp = ts,
                            pic= basestr
                        });
                    }
                    onestatus.coursepics = coursepics;
                    coursestatus.Add(onestatus);
                }

                return new picturesresponse
                {
                    status = 0,coursestatus= coursestatus

                };
            }
            catch (Exception ex)
            {
                Log.Error("pictures,", ex);
                return new picturesresponse
                {
                    status = (int)sixerrors.processerror
                };
            }
        }
        private string getdate(string ls)
        {
            try
            {
                Int64 begtime = Convert.ToInt64(ls) * 10000000;//100毫微秒为单位,textBox1.text需要转化的int日期
                DateTime dt_1970 = new DateTime(1970, 1, 1, 8, 0, 0);
                long tricks_1970 = dt_1970.Ticks;//1970年1月1日刻度
                long time_tricks = tricks_1970 + begtime;//日志日期刻度
                DateTime dt = new DateTime(time_tricks);//转化为DateTime
                return dt.ToString();
            }catch(Exception ex)
            {
                return DateTime.Now.ToString();
            }
        }
        [Route("StudyRecords")]
        [HttpGet]
        public async Task<StudyRecordsresponse> StudyRecords(string startTime, string endTime , string name ,
            string identity ,int? illegal ,int? message, int? print, int? processed, int? failure,string region, int startNum, int endNum)
        {
            try
            {
                var found = false;
                var token = Request.Headers.GetValues("Token").First();
                Log.InfoFormat("StudyRecords,token is {0},{1},startTime,  endTime,{2}  name,{3}  identity,{4} illegal ,{5}  message,{6} print,{7},processed{8}",
                    token,  startTime,  endTime,  name,  identity, illegal ,  message, print, processed);              

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
                var theusers = _db1.HISTORY.Where(b =>  b.TIME.CompareTo(DateTime.Now) <= 0);
                var start = DateTime.Now;
                if (DateTime.TryParse(startTime, out start))
                {
                    //Log.InfoFormat("StudyRecords,{0}", sixerrors.invalidstarttime);
                    //return new StudyRecordsresponse
                    //{
                    //    status = (int)sixerrors.invalidstarttime
                    //};
                    theusers.Where(aa => aa.TIME.CompareTo(start) >= 0);
                }
                
                var end = DateTime.Now;
                if (!DateTime.TryParse(endTime, out end))
                {
                    //Log.InfoFormat("StudyRecords,{0}", sixerrors.invalidendtime);
                    //return new StudyRecordsresponse
                    //{
                    //    status = (int)sixerrors.invalidendtime
                    //};
                    theusers.Where(aa => aa.TIME.CompareTo(end) <= 0);
                }               
                             

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
                if (!string.IsNullOrEmpty(region))
                {
                    theusers = theusers.Where(c => c.COUNTY.Contains(  region));
                }
                if (illegal!=null)
                {
                    if( illegal == 1)
                    theusers = theusers.Where(c => c.STATUS != null&& c.STATUS.Contains('H'));
                    else
                        theusers = theusers.Where(c => c.STATUS == null || !c.STATUS.Contains('H'));
                }
                if (print != null )
                {
                    if( print == 1)
                    theusers = theusers.Where(c => c.PRINTED == "1");
                    else
                        theusers = theusers.Where(c => c.PRINTED != "1");
                }
                if (message != null )
                {
                    if( message == 1)
                    theusers = theusers.Where(c => c.MESSAGED == "1");
                    else
                        theusers = theusers.Where(c => c.MESSAGED != "1");
                }
                if (processed != null )
                {
                    if( processed == 1)
                    theusers = theusers.Where(c => c.PROCESSED == "1");
                    else
                        theusers = theusers.Where(c => c.PROCESSED != "1");
                }
                if (failure != null )
                {
                    if(failure == 1)
                    theusers = theusers.Where(c => c.FAILURE == "1");
                    else
                        theusers = theusers.Where(c => c.FAILURE != "1");
                }
                var total = theusers.Count();
                theusers = theusers.Take(endNum);
                Log.InfoFormat("StudyRecords,{0}", 11111);
                var records = new List<record>();
                var index = 0;
                foreach(var a in theusers)
                {
                    if(index++<startNum) continue;
                    var sfz = string.Empty;
                    if (!string.IsNullOrEmpty(a.IDCARD)&& sfz.Length < 44) sfz = cypher.StudyDecrypt(a.IDCARD);
                    Log.InfoFormat("StudyRecords,{0}", 33333);
                    var yxqz = DateTime.Now;
                    if (a.SYYXQZ != null) yxqz = (DateTime)a.SYYXQZ;
                    Log.InfoFormat("StudyRecords,{0}", 44444);
                    var recodr = new record
                    {
                        id = a.ID.ToString(),
                        name = a.NAME,
                        phone = a.PHONENUMBER,
                        identity = sfz,
                        studyTime = a.TIME,
                        syyxqz = yxqz,
                        illegal = a.STATUS
                    };
                    Log.InfoFormat("StudyRecords,{0}", 55555);
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
                    Log.InfoFormat("StudyRecords,{0}", 66666);
                    records.Add(recodr);
                }
                return new StudyRecordsresponse
                {
                    status = 0,records=records,total=total
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
        [Route("StudyRecord")]
        [HttpGet]
        public async Task<StudyRecordsresponse> StudyRecord( string identity)
        {
            try
            {
                var found = false;
                var token = Request.Headers.GetValues("Token").First();
                Log.InfoFormat("StudyRecords,token is {0},{1},  identity,",
                    token,  identity);

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
               
                var cypher = new CryptographyHelpers();
                var cryptographicid = cypher.StudyEncrypt(identity);
               
                var theusers = _db1.HISTORY.Where(c => c.IDCARD == cryptographicid);

                var records = new List<record>();
                foreach (var a in theusers)
                {
                    var recodr = new record
                    {
                        id = a.ID.ToString(),
                        name = a.NAME,
                        phone = a.PHONENUMBER,
                        identity = cypher.StudyDecrypt(a.IDCARD),
                        studyTime = a.TIME,
                        syyxqz=(DateTime)a.SYYXQZ,
                        illegal = a.STATUS
                    };
                    if (a.MESSAGED == "1")
                    {
                        var mess = _db1.MESSAGE.Where(aa => aa.HISTORYID == a.ID);
                        var messages = new List<message>();
                        foreach (var b in mess)
                        {
                            messages.Add(new message
                            {
                                dateTime = b.TIME,
                                content = b.CONTENT
                            });
                        }
                        recodr.message = messages;
                    }
                    records.Add(recodr);
                }
                return new StudyRecordsresponse
                {
                    status = 0,
                    records = records
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
        [Route("UnprocessedRecords")]
        [HttpGet]
        public async Task<StudyRecordsresponse> UnprocessedRecords(int startNum, int endNum)
        {
            try
            {
                var found = false;
                var token = Request.Headers.GetValues("Token").First();
                Log.InfoFormat("UnprocessedRecords,token is {0},", token);

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
                    Log.InfoFormat("UnprocessedRecords,{0}", sixerrors.invalidtoken);
                    return new StudyRecordsresponse
                    {
                        status = (int)sixerrors.invalidtoken
                    };
                }
                
                var cypher = new CryptographyHelpers();
             //   Log.InfoFormat("UnprocessedRecords, {0},", 1111);
                var total = _db1.HISTORY.Where(c => c.PROCESSED!="1").Count();
              //  if(startNum!=null&&startNum>-1)
                var theusers = _db1.HISTORY.Where(c => c.PROCESSED != "1").Take(endNum);
                //   Log.InfoFormat("UnprocessedRecords, {0},", 2222);
                var records = new List<record>();
                var index = 0;
                foreach (var a in theusers)
                {
                    if (index++ < startNum) continue;
                    if (!string.IsNullOrEmpty(a.FAILURE)&& a.FAILURE == "1")
                    {
                     //   if(a.STATUS.Contains('H')) continue;

                        var study = _db1.ABSTUDY.FirstOrDefault(c => c.IDCARD == a.IDCARD);
                        if (study == null) continue;
                        if (study.STATUS.Contains('H')) continue;
                    }
                    Log.InfoFormat("UnprocessedRecords, {0},", 3333);
                  
                    var sfz = string.Empty;
                    if (!string.IsNullOrEmpty(a.IDCARD) && sfz.Length < 44) sfz = cypher.StudyDecrypt(a.IDCARD);

                    var yxqz = DateTime.Now;
                    if (a.SYYXQZ != null) yxqz = (DateTime)a.SYYXQZ;

                    DateTime finishdate = a.FINISHDATE == null ? a.TIME : (DateTime)a.FINISHDATE;
                    var recodr = new record
                    {
                        id = a.ID.ToString(),
                        name = a.NAME,
                        phone = a.PHONENUMBER,
                        identity = sfz,
                        //studyTime = a.TIME,
                        studyTime = finishdate,
                        syyxqz =yxqz,
                        illegal = a.STATUS
                    };
                    Log.InfoFormat("UnprocessedRecords, {0},", 44444);
                    if (a.MESSAGED == "1")
                    {
                        var mess = _db1.MESSAGE.Where(aa => aa.HISTORYID == a.ID);
                        var messages = new List<message>();
                        Log.InfoFormat("UnprocessedRecords, {0},", 55555);
                        foreach (var b in mess)
                        {
                            messages.Add(new message
                            {
                                dateTime = b.TIME,
                                content = b.CONTENT
                            });
                        }
                        recodr.message = messages;
                    }
                    Log.InfoFormat("UnprocessedRecords, {0},", 6666);
                    records.Add(recodr);
                }
                return new StudyRecordsresponse
                {
                    status = 0,total=total,
                    records = records
                };
            }
            catch (Exception ex)
            {
                Log.Error("UnprocessedRecords,", ex);
                return new StudyRecordsresponse
                {
                    status = (int)sixerrors.processerror
                };
            }
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("Statistics")]
        [HttpGet]
        public async Task<StatisticsResponse> Statistics(string sdate=null)
        {
            try
            {
                var today = DateTime.Now;
                if (sdate != null)
                {
                    today = DateTime.Parse(sdate);
                }
                var yesterday = today.AddDays(-1);
                Log.InfoFormat("today={0}，yesteday={1}", today, yesterday);
                var todaydb = _db1.COUNT.FirstOrDefault(c => c.TIME.CompareTo(today) <=0 &&c.TIME.CompareTo(yesterday)>0);
                Log.InfoFormat("today={0}，yesteday={1}", 11, 22);
                if (todaydb == null)
                {
                    todaydb= new COUNT
                    {
                        PAGEVIEW = 0,
                        APPLICATION = 0,
                        PAGEVIEWDAY = 0,
                        APPLICATIONDAY = 0,
                        STARTLEARNINGVOLUME=0,
                        STARTLEARNINGVOLUMETODAY=0,
                        KAIFAQU = 0,
                        ZHIFUQU = 0,
                        FUSHANQU = 0,

                        MUPINGQU = 0,
                        LAISHANQU = 0,
                        LONGKOU = 0,
                        ZHAOYUAN = 0,
                        QIXIA = 0,

                        LAIZHOU = 0,
                        CHANGDAO = 0,
                        HAIYANG = 0,
                        LAIYANG = 0,
                        PENGLAI = 0,

                        OTHER = 0,
                        GAOXINQU = 0,
                    };
                }
                var startoftoday = DateTime.Parse(string.Format("{0}-{1}-{2}", today.Year, today.Month, today.Day));
                var todaynum = new statistics
                {
                    PAGEVIEW=todaydb.PAGEVIEWDAY,
                    APPLICATION = todaydb.APPLICATIONDAY,
                    KAIFAQU = todaydb.KAIFAQU,
                    ZHIFUQU = todaydb.ZHIFUQU,
                    FUSHANQU = todaydb.FUSHANQU,

                    MUPINGQU = todaydb.MUPINGQU,
                    LAISHANQU = todaydb.LAISHANQU,
                    LONGKOU = todaydb.LONGKOU,
                    ZHAOYUAN = todaydb.ZHAOYUAN,
                    QIXIA = todaydb.QIXIA,

                    LAIZHOU = todaydb.LAIZHOU,
                    CHANGDAO = todaydb.CHANGDAO,
                    HAIYANG = todaydb.HAIYANG,
                    LAIYANG = todaydb.LAIYANG,
                    PENGLAI = todaydb.PENGLAI,
                    InspectedVolume = _db1.HISTORY.Where(c => c.PROCESSED == "1" && c.OVERTIME.CompareTo(startoftoday) >=0).Count(),
                    LearningVolume = todaydb.STARTLEARNINGVOLUMETODAY,
                    OTHER = todaydb.OTHER,
                    GAOXINQU = todaydb.GAOXINQU,
                };
                //    var totaldb = _db1.COUNT.Sum(c => c.KAIFAQU);
                var totalnum = new statistics
                {
                    PAGEVIEW = todaydb.PAGEVIEW,
                    APPLICATION = todaydb.APPLICATION,
                    KAIFAQU = _db1.COUNT.Sum(c => c.KAIFAQU),
                    ZHIFUQU = _db1.COUNT.Sum(c => c.ZHIFUQU),
                    FUSHANQU = _db1.COUNT.Sum(c => c.FUSHANQU),

                    MUPINGQU = _db1.COUNT.Sum(c => c.MUPINGQU),
                    LAISHANQU = _db1.COUNT.Sum(c => c.LAISHANQU),
                    LONGKOU = _db1.COUNT.Sum(c => c.LONGKOU),
                    ZHAOYUAN = _db1.COUNT.Sum(c => c.ZHAOYUAN),
                    QIXIA = _db1.COUNT.Sum(c => c.QIXIA),

                    LAIZHOU = _db1.COUNT.Sum(c => c.LAIZHOU),
                    CHANGDAO = _db1.COUNT.Sum(c => c.CHANGDAO),
                    HAIYANG = _db1.COUNT.Sum(c => c.HAIYANG),
                    LAIYANG = _db1.COUNT.Sum(c => c.LAIYANG),
                    PENGLAI = _db1.COUNT.Sum(c => c.PENGLAI),
                    InspectedVolume = _db1.HISTORY.Where(c => c.PROCESSED == "1").Count(),
                    LearningVolume= todaydb.STARTLEARNINGVOLUME,
                    OTHER = _db1.COUNT.Sum(c => c.OTHER),
                    GAOXINQU = _db1.COUNT.Sum(c => c.GAOXINQU),
                };
                return new StatisticsResponse
                {
                    status = 0,today=todaynum,total=totalnum
                };
            }
            catch (Exception ex)
            {
                Log.Error("Statistics,", ex);
                return new StatisticsResponse
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
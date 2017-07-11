using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace six2015.request
{
    enum sixerrors { ok,nouser,invalidrequest,processerror,errorpassword,invalidtoken,unauthorized,passwordisnull,
        invalidpower,invalididentity,invalidstarttime ,invalidendtime}
    public enum SignatureType { Unknown, PhysicalCondition, EducationalRecord }
    public class oneuser
    {
        public string username { get; set; }
        public int power { get; set; }
    }
    public class usersresponse
    {
        public usersresponse()
        {
            users = new List<request.oneuser>();
        }
        public int status { get; set; }
       
        public List<oneuser> users { get; set; }
    }
    public class loginrequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class userrequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public int? power { get; set; }
    }
    public class commonrequest
    {
        public int id { get; set; }
    }
    public class messagerequest
    {
        public int id { get; set; }
        public string content { get; set; }
    }
    public class userresponse
    {
        public int status { get; set; }
    }
    public class picresponse
    {
        public int status { get; set; }
     //   public byte[] pic { get; set; }
        public string pic { get; set; }
    }
    public class loginresponse
    {
        public int status { get; set; }
        public string token { get; set; }
        public int power { get; set; }
    }
    public class message
    {
        public DateTime dateTime { get; set; }
        public string content { get; set; }
    }
    public class StudyRecordsresponse
    {
        public int status { get; set; }
        public List<record> records { get; set; }
    }
    public class record
    {
        public string id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string identity { get; set; }
        public DateTime studyTime { get; set; }
        public string illegal { get; set; }
        public List< message> message { get; set; }
    }
    //"records": [
    //    {
    //        "id": "",                   // 该条记录的唯一标识
    //        "name": "张三",               // 学员姓名
    //        "phone": "",                // 电话号码
    //        "identity": "",             // 身份证号
    //        "studyTime": "",            // 完成学习的时间
    //        "illegal": "",              // 违法状态，从违法库中获取
    //        "message": [                // 向该用户发送过的短信
    //            {
    //                "dateTime": "2017-07-01 09:10:30",     // 发送短信时间
    //                "content": ""                          // 短信内容
    //            }
    //        ]
    //    },

}
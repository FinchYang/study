using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace six2015.request
{
    enum sixerrors { ok,nouser,invalidrequest,processerror,errorpassword,invalidtoken,unauthorized,passwordisnull,
        invalidpower,invalididentity,invalidstarttime ,invalidendtime,illegalunprocessed}
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
    public enum CountyCode
    {
        HaiYang, FuShan, QiXia, LaiShan, ZhiFu,
        MuPing, LongKou, LaiYang, LaiZhou, PengLai,
        ZhaoYuan, ChangDao,
        DaCheng, ShiSuo
    }
    public class messagerequest2
    {
        public int id { get; set; }
        public string message { get; set; }
        public int processed { get; set; }
        public int failure { get; set; }
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
    public class examresponse
    {
        public int status { get; set; }
        public string content { get; set; }
    }
    public class picturesresponse
    {
        public int status { get; set; }
        public List<coursestatus> coursestatus { get; set; }
    }
    public class coursestatus
    {
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public List<coursepics> coursepics { get; set; }
    }
    public class coursepics
    {      
        public string timestamp { get; set; }
        public string pic { get; set; }
    }
    public class picresponse
    {
        public int status { get; set; }
     //   public byte[] pic { get; set; }
        public string pic { get; set; }
    }
    public class signaturepicsresponse
    {
        public int status { get; set; }
        public string EducationalRecord { get; set; }
        public string PhysicalCondition { get; set; }
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
    public  class statistics
    {       
        public decimal PAGEVIEW { get; set; }
        public decimal APPLICATION { get; set; }
        public decimal KAIFAQU { get; set; }
        public decimal ZHIFUQU { get; set; }
        public decimal FUSHANQU { get; set; }
        public decimal MUPINGQU { get; set; }
        public decimal LAISHANQU { get; set; }
        public decimal LONGKOU { get; set; }
        public decimal ZHAOYUAN { get; set; }
        public decimal QIXIA { get; set; }
        public decimal LAIZHOU { get; set; }
        public decimal CHANGDAO { get; set; }
        public decimal HAIYANG { get; set; }
        public decimal LAIYANG { get; set; }
        public decimal PENGLAI { get; set; }
        public decimal GAOXINQU { get; set; }
        public decimal OTHER { get; set; }
    }
    public class StatisticsResponse
    {
        public int status { get; set; }
        public statistics today { get; set; }
        public statistics total { get; set; }
    }
    public class StudyRecordsresponse
    {
        public int status { get; set; }
        public int total { get; set; }
        public List<record> records { get; set; }
    }
    public class record
    {
        public string id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string identity { get; set; }
        public DateTime syyxqz { get; set; }
        public DateTime studyTime { get; set; }
        public string illegal { get; set; }
        public List< message> message { get; set; }
    }    
}
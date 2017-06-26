
using System.Collections.Generic;
namespace study
{
    public static class Global
    {
        private static Dictionary<responseCode, ResponseStatus> status = new Dictionary<responseCode, ResponseStatus>();
       
        static Global()
        {
            status.Add(responseCode.studyOk, new ResponseStatus { StatusCode = "200000", Description = "ok" });
            status.Add(responseCode.ok, new ResponseStatus { StatusCode = "100000", Description = "ok" });
            status.Add(responseCode.studyRequestError, new ResponseStatus { StatusCode = "200002", Description = "请求错误，请检查请求参数！" });
            status.Add(responseCode.RequestError, new ResponseStatus { StatusCode = "100002", Description = "请求错误，请检查请求参数！" });
            status.Add(responseCode.studyNotNecessary, new ResponseStatus { StatusCode = "200004", Description = "您不需要学习！" });
            status.Add(responseCode.InvalidIdentiy, new ResponseStatus { StatusCode = "100004", Description = "无效的身份证！" });
            status.Add(responseCode.studyProgramError, new ResponseStatus { StatusCode = "200003", Description = "处理出错，请稍后再试！" });
            status.Add(responseCode.ProgramError, new ResponseStatus { StatusCode = "100003", Description = "处理出错，请稍后再试！" });
            status.Add(responseCode.studyTokenError, new ResponseStatus { StatusCode = "200001", Description = "无效的 token！" });
            status.Add(responseCode.TokenError, new ResponseStatus { StatusCode = "100001", Description = "无效的 token！" });
        }

        internal static Dictionary<responseCode, ResponseStatus> Status { get => status;  }
       
         internal static string HostIp { get => hostip; set=>hostip=value; }
         private static string hostip="localhost";
         public static string PhotoPath=string.Empty;
    }
}
﻿using System;

namespace cdmcli
{
    class Program
    {
        private const string homeurl = "localhost:8000";
        static void Main(string[] args)
        {
            var aa = new YunYiCdm.YunYiCdm();
            if (args.Length < 1)
            {
                Console.WriteLine("please input method");
                return;
            }
            switch (args[0])
            {
                //case "zip":
                //    var zinput = new BusinessModel { countyCode = "zhifu", businessCategory = "yew1", userName = "user1", password = "pass" ,ID=3032};
                //    var zjson = JsonConvert.SerializeObject(zinput);
                //    Console.WriteLine("input=" + zjson);
                //    var ret = aa.SendRestHttpClientRequest(homeurl, "BusinessesPictureQuery ", zjson);
                //    Console.WriteLine("ResultToFile output = " + aa.ResultToFile(ret, "d:\\aaaaa"));
                // //   Console.WriteLine("output = " + ret);
                //    var bb = JsonConvert.DeserializeObject<ResultModel>(ret);
                //    Console.WriteLine("output = " + bb.StatusCode);
                //    Console.WriteLine("ByteToFile output = " + aa.ByteToFile(bb.BussinessModel.zipFile, "d:\\aaaaa"));
                //    break;
                //case "getordinal":
                //    var input = new BusinessModel { countyCode = "haiyang", businessCategory = "yew1", userName = "user1", password = "pass" };
                //    var json = JsonConvert.SerializeObject(input);
                //    Console.WriteLine("input=" + json);
                //       Console.WriteLine("output = "+aa.RestHttpClientGet(homeurl, "getordinal", json));
                //    break;
                case "getordinal2":
                         Console.WriteLine(aa.RestHttpClientGet(homeurl, "getordinal2", "?code=334&category=0334"));
                    break;
                case "jss":
                    Console.WriteLine(aa.JsonserializeEx("laizhou", "user1", "pass", "haha", 111, 222, @"E:\cdm\CDMservers\CdmCliComNs\bin\Release\CdmCliComNs.zip"));
                    break;
                //case "GET_VERSION":
                //    var haha = JsonConvert.SerializeObject(new BusinessModel { businessCategory = "HE", address = "wolong" });
                //    //    var dict = new Dictionary<string, string>
                //    //{
                //    //    {"UserName","param1. userName"},
                //    //    {"Password"," param1.password"},
                //    //    {"grant_type", "password"}
                //    //};
                //    Console.WriteLine("输入：" + haha + "     输出" + aa.SendRestHttpClientRequest(homeurl, "GET_VERSION", haha));
                //    break;
                default:
                    Console.WriteLine("no this method");
                    break;
            }
            Console.ReadKey();
        }
    }
}

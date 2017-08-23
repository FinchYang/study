using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace exportdb
{
    class Program
    {
        static string dbtofilePath = "/home/inspect/dbtofile";
       // static string dbtofilePath = @"e:\11\22";
        static string exportPath = "/home/inspect/ftp/put";
        static void Main(string[] args)
        {
            Console.WriteLine("{0}, export archived data to file, and zip it, started...", DateTime.Now);
            DbToFileForStatistics();
           DbUserToFile();
           DbHistoryToFile();
           DbRequestToFile();
           DbToFileForExtranetToIntranet();
            Console.WriteLine("{0}, export archived data to file, and zip it, completed.");
        }
        static void DbUserToFile()
        {
            var date = DateTime.Now;
            // var dir = string.Format("{0}-{1}-{2}-{3}-{4}-{5}", date.Year, date.Month.ToString("D2"), date.Day.ToString("D2"),
            // date.Hour.ToString("D2"), date.Minute.ToString("D2"), date.Second.ToString("D2"));
            var dbtofilefname = "user.txt";
            if (!Directory.Exists(dbtofilePath)) Directory.CreateDirectory(dbtofilePath);
            var fname = Path.Combine(dbtofilePath, dbtofilefname);
             Console.WriteLine("{0}, export user data to file,  started...", DateTime.Now);
            using (var db = new studyinContext())
            {
                var tempday = date.AddDays(-1);
              //  var yesterday = DateTime.Parse(string.Format("{0}/{1}/{2}", 2000, tempday.Month, tempday.Day));
                var yesterday = DateTime.Parse(string.Format("{0}/{1}/{2}", tempday.Year, tempday.Month, tempday.Day));
                var theuser = db.User.Where(async => async.Syncdate.CompareTo(yesterday) > 0 &&
                 ( async.Firstsigned=="1"||async.Signed=="1"||!string.IsNullOrEmpty(async.Studylog))  );
                Console.WriteLine("yesterday is {0},today is {1}, {2} users need to  be archived", yesterday, date, theuser.Count());
                foreach (var re in theuser)
                {
                    File.AppendAllText(fname, JsonConvert.SerializeObject(re) + "\r\n");
                      NewMethod(re.Photofile);
                }
            }
             Console.WriteLine("{0}, export usesr data to file,  end...", DateTime.Now);
        }
        static void DbHistoryToFile()
        {
            var date = DateTime.Now;
            var dbtofilefname = "history.txt";
            if (!Directory.Exists(dbtofilePath)) Directory.CreateDirectory(dbtofilePath);
            var fname = Path.Combine(dbtofilePath, dbtofilefname);
              Console.WriteLine("{0}, export history data to file,  started...", DateTime.Now);
            using (var db = new studyinContext())
            {
                var tempday = date.AddDays(-1);
               // var yesterday = DateTime.Parse(string.Format("{0}/{1}/{2}", 2000, tempday.Month, tempday.Day));
                var yesterday = DateTime.Parse(string.Format("{0}/{1}/{2}", tempday.Year, tempday.Month, tempday.Day));
                var theuser = db.History.Where(async => async.Finishdate.CompareTo(yesterday) > 0);
                Console.WriteLine("yesterday is {0},today is {1}, {2} history users need to  be archived", yesterday, date, theuser.Count());
                foreach (var re in theuser)
                {
                    File.AppendAllText(fname, JsonConvert.SerializeObject(re) + "\r\n");
                }
            }
             Console.WriteLine("{0}, export history data to file,  end...", DateTime.Now);
        }
        static void DbRequestToFile()
        {
            var date = DateTime.Now;
            var dbtofilefname = "request.txt";
            if (!Directory.Exists(dbtofilePath)) Directory.CreateDirectory(dbtofilePath);
            var fname = Path.Combine(dbtofilePath, dbtofilefname);
              Console.WriteLine("{0}, export Request data to file,  started...", DateTime.Now);
            using (var db = new studyinContext())
            {
                var tempday = date.AddDays(-1);
              //  var yesterday = DateTime.Parse(string.Format("{0}/{1}/{2}", 2000, tempday.Month, tempday.Day));
                var yesterday = DateTime.Parse(string.Format("{0}/{1}/{2}", tempday.Year, tempday.Month, tempday.Day));
                var theuser = db.Request.Where(async => async.Time.CompareTo(yesterday) > 0);
                Console.WriteLine("yesterday is {0},today is {1}, {2} requests need to  be archived", yesterday, date, theuser.Count());
                foreach (var re in theuser)
                {
                    File.AppendAllText(fname, JsonConvert.SerializeObject(re) + "\r\n");
                }
            }
              Console.WriteLine("{0}, export Request data to file, end...", DateTime.Now);
        }
        static void DbToFileForExtranetToIntranet()
        {
            var date = DateTime.Now;
            var dir = string.Format("{0}-{1}-{2}-{3}-{4}-{5}", date.Year, date.Month.ToString("D2"), date.Day.ToString("D2"),
            date.Hour.ToString("D2"), date.Minute.ToString("D2"), date.Second.ToString("D2"));
            var dbtofilefname = string.Format("{0}-{1}-{2}", date.Year, date.Month.ToString("D2"), date.Day.ToString("D2"))
            + "extranetToIntranet.dat";
            if (!Directory.Exists(dbtofilePath)) Directory.CreateDirectory(dbtofilePath);
            var fname = Path.Combine(dbtofilePath, dbtofilefname);
              Console.WriteLine("{0}, export out-in data to file,  started...", DateTime.Now);
            using (var db = new studyinContext())
            {
                var tempday = date.AddDays(-1);
                var yesterday = DateTime.Parse(string.Format("{0}/{1}/{2}", tempday.Year, tempday.Month, tempday.Day));

                var theuser = db.History.Where(async => async.Finishdate.CompareTo(date) <= 0
                && async.Finishdate.CompareTo(yesterday) > 0
                );
                Console.WriteLine("yesterday is {0},today is {1}, {2} records need to  be archived", yesterday, date, theuser.Count());
                foreach (var re in theuser)
                {
                    File.AppendAllText(fname, JsonConvert.SerializeObject(re) + "\r\n");
                    NewMethod(re.Photofile);
                }
            }
            if (!Directory.Exists(exportPath)) Directory.CreateDirectory(exportPath);
            var zipfname = Path.Combine(exportPath, dir);

            var a = new System.Diagnostics.Process();
            a.StartInfo.UseShellExecute = true;
            a.StartInfo.Arguments =
            string.Format(" {0} {1}/* -r", zipfname, dbtofilePath);
            a.StartInfo.FileName = "zip";
            a.Start();
            a.WaitForExit();
              Console.WriteLine("{0}, export out-in data to file,  end...", DateTime.Now);
        }

        static void DbToFileForStatistics()
        {
            var date = DateTime.Now;
            var dir = string.Format("{0}-{1}-{2}-{3}-{4}-{5}", date.Year, date.Month.ToString("D2"), date.Day.ToString("D2"),
            date.Hour.ToString("D2"), date.Minute.ToString("D2"), date.Second.ToString("D2"));
            var dbtofilefname = string.Format("{0}-{1}-{2}", date.Year, date.Month.ToString("D2"), date.Day.ToString("D2"))
            + "statistics.txt";
            if (!Directory.Exists(dbtofilePath)) Directory.CreateDirectory(dbtofilePath);
            var fname = Path.Combine(dbtofilePath, dbtofilefname);
               Console.WriteLine("{0}, export Statistics data to file,  started...", DateTime.Now);
            using (var db = new studyinContext())
            {
                var temp = string.Format("{0}/{1}/{2}", date.Year, date.Month, date.Day);
                var tempday = DateTime.Parse(temp);

                // var theuser = db.History.Where(async => async.Finishdate.CompareTo(date) <= 0
                // && async.Finishdate.CompareTo(yesterday) > 0
                // );
                Console.WriteLine("tempday is {0},today is {1},", tempday, date);
                var visitorVolumeAll = db.Request.Count();
                var usageAmountAll = db.Request.Where(us => !us.Method.Contains("LoginAndQuery")).Count();

                var visitorVolumeToday = db.Request.Where(us => us.Time.CompareTo(tempday) >= 0).Count();
                var usageAmountToday = db.Request.Where(us => us.Time.CompareTo(tempday) >= 0 && !us.Method.Contains("LoginAndQuery")).Count();

                var startLearningVolume=db.User.Where(bb=>  !string.IsNullOrEmpty(bb.Studylog)).Count();
                var startLearningVolumetoday=db.User.Where(bb=>  !string.IsNullOrEmpty(bb.Studylog) && bb.Startdate>date.AddDays(-1)).Count();

                File.AppendAllText(fname, string.Format("{0},{1},{2},{3},{4},{5},{6}",
                temp, visitorVolumeAll, usageAmountAll, visitorVolumeToday, usageAmountToday, startLearningVolume, startLearningVolumetoday));
            }
              Console.WriteLine("{0}, export Statistics data to file,  end...", DateTime.Now);
        }

        private static void NewMethod(string filebase)
        {
            var a = new System.Diagnostics.Process();
            a.StartInfo.UseShellExecute = true;
            a.StartInfo.Arguments =
            string.Format(" -r /home/inspect/signature/{1}* /home/inspect/logphoto/{1}* {0}/", dbtofilePath, filebase);
            a.StartInfo.FileName = "cp";
            a.Start();
            a.WaitForExit();
        }
    }
}

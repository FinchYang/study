using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace exportdb
{
    class Program
    { static string dbtofilePath = "/home/inspect/dbtofile";
     static string exportPath = "/home/inspect/ftp/put";
        static void Main(string[] args)
        {
            Console.WriteLine("{0}, export archived data to file, and zip it, started...",DateTime.Now);
            DbToFileForExtranetToIntranet();
            Console.WriteLine("{0}, export archived data to file, and zip it, completed.");
        }
          static void DbToFileForExtranetToIntranet()
        {
            var date = DateTime.Today;
            var dir = string.Format("{0}{1}{2}", date.Year, date.Month, date.Day);
            var dbtofilefname = dir + "extranetToIntranet.dat";
            if (!Directory.Exists(dbtofilePath)) Directory.CreateDirectory(dbtofilePath);
            var fname = Path.Combine(dbtofilePath, dbtofilefname);
            using (var db = new studyinContext())
            {
                var tempday = date.AddDays(-1);
                var yesterday = DateTime.Parse(string.Format("{0}/{1}/{2}", tempday.Year, tempday.Month, tempday.Day));
                Console.WriteLine("yesterday is {0},today is {1}", yesterday, date);
                var theuser = db.History.Where(async => async.Finishdate.CompareTo(date) <= 0 &&
                async.Finishdate.CompareTo(yesterday) > 0);
                foreach (var re in theuser)
                {
                    File.AppendAllText(fname, JsonConvert.SerializeObject(re));
                }
            }
            if (!Directory.Exists(exportPath)) Directory.CreateDirectory(exportPath);
            var zipfname = Path.Combine(exportPath, dir);

            var a = new System.Diagnostics.Process();
            a.StartInfo.UseShellExecute = true;
            a.StartInfo.Arguments =
            string.Format(" {0} {2} /home/inspect/signature/{1}/* /home/inspect/logphoto/{1}/*", zipfname, dir, fname);
            a.StartInfo.FileName = "zip";
            a.Start();
        }
    }
}

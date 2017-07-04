using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Threading;
namespace syncdata
{
    class Program
    {

        static string importPath = "/home/inspect/ftp/get";
        static string exportPath = "/home/inspect/ftp/put";
        static string dbtofilePath = "/home/inspect/dbtofile";
        static void Main(string[] args)
        {

            Console.WriteLine("haha");//System.Security.Cryptography
            var t1 = new Thread(new ThreadStart(filetodbthread));
            t1.Name = "import data";
            t1.Start();
            var t2 = new Thread(new ThreadStart(dbtofilethread));
            t2.Name = "export data";
            t2.Start();
            // Console.ReadLine();
            // System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private static void filetodbthread()
        {
            while (true)
            {
                var hour = DateTime.Now.Hour;
                if (hour > 22 && hour <= 23)
                {
                    Console.WriteLine("import data started,{0}", DateTime.Now);
                    FileToDb();
                    Console.WriteLine("import data ok,{0}", DateTime.Now);
                }
                System.Threading.Thread.Sleep(1000 * 60 * 60);
            }
        }
        private static void dbtofilethread()
        {
            while (true)
            {
                var hour = DateTime.Now.Hour;
                //   if (hour > 22 && hour <= 23)
                {
                    Console.WriteLine("export data started,{0}", DateTime.Now);
                    DbToFileForExtranetToIntranet();
                    Console.WriteLine("export data ok,{0}", DateTime.Now);
                }
                System.Threading.Thread.Sleep(1000 * 60 * 60);
            }
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

        static void FileToDb()
        {
            using (var db = new studyinContext())
            {
                var fname = Path.Combine(importPath, "allowToStudy.txt");
                if (File.Exists(fname))
                {
                    var content = File.ReadAllLines(fname);
                    foreach (var line in content)
                    {
                        var fields = line.Split(',');
                        var identity = fields[0];
                        var phone = fields[1];
                        var drivertype = fields[2];
                        DrivingLicenseType enumtype;
                        if (!Enum.TryParse(drivertype, out enumtype))
                        {
                            enumtype = DrivingLicenseType.Unknown;
                        }

                        var drugrelated = fields[3];
                        var pictureok = fields[4];
                        var deductedmarks = fields[5];
                        var licensenumber = fields[6];
                        var photofile = fields[7];
                        var ideducted = 0;
                        if (!int.TryParse(deductedmarks, out ideducted)) ideducted = 1;
                        var theuser = db.User.FirstOrDefault(async => async.Identity == identity);
                        if (theuser == null)
                        {
                            try
                            {
                                db.User.Add(new User
                                {
                                    Identity = identity,
                                    Licensetype = ((int)enumtype).ToString(),
                                    Drugrelated = drugrelated,
                                    Syncphone = phone,
                                    Photostatus = pictureok,
                                    Drivinglicense = licensenumber,
                                    Deductedmarks = ideducted,
                                    Photofile=photofile,
                                    Syncdate = DateTime.Now
                                });
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("user {0} sync error{1}.", identity, ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("user {0} has already existed.", identity);
                        }
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}

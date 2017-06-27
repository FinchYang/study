using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Threading;
namespace syncdata
{
    class Program
    {
        static string importPath = ".";
        static string exportPath = ".";
        static void Main(string[] args)
        {
            Console.WriteLine("haha");
            var t1 = new Thread(new ThreadStart(filetodbthread));
            t1.Name="import data";
            t1.Start();
            var t2 = new Thread(new ThreadStart(dbtofilethread));
            t2.Name="export data";
            t2.Start();
            Console.ReadLine();
        System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private static void filetodbthread()
        {
            while (true)
            {
                var hour = DateTime.Now.Hour;
                if (hour > 22 && hour <= 23)
                { Console.WriteLine("import data started,{0}",DateTime.Now);
                    FileToDb();
                    Console.WriteLine("import data ok,{0}",DateTime.Now);
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
                {Console.WriteLine("export data started,{0}",DateTime.Now);
                    DbToFileForExtranetToIntranet();
                   Console.WriteLine("export data ok,{0}",DateTime.Now);
                }
                System.Threading.Thread.Sleep(1000 * 60 * 60);
            }
        }
        static void DbToFileForExtranetToIntranet()
        {
            using (var db = new studyContext())
            {
                var now = DateTime.Now;
                var yesterday = now.AddDays(-4);
                var theuser = db.History.Where(async => async.Finishdate.CompareTo(now) <= 0 && 
                async.Finishdate.CompareTo(yesterday) > 0);
                var fpath=Path.Combine( exportPath ,DateTime.Today.ToString("yyyyMMdd"));
                if(!Directory.Exists(fpath)) Directory.CreateDirectory(fpath);
                var fname =Path.Combine(fpath, "extranetToIntranet.dat");
                
              //  if(!File.Exists(fname)) File.Create(fname);
                foreach (var re in theuser)
                {
                    File.AppendAllText(fname, JsonConvert.SerializeObject(re));
                }
            }
        }
        static void FileToDb()
        {
            using (var db = new studyContext())
            {
                 var fpath =Path.Combine( exportPath ,DateTime.Today.ToString("yyyyMMdd"));
                DirectoryInfo a = new DirectoryInfo(fpath);
                foreach (var b in a.GetFiles())
                {//b.Attributes.HasFlag()
                    if (b.Name == "users.dat")
                    {
                        var content = File.ReadAllLines(b.FullName);
                        foreach (var line in content)
                        {
                            Console.WriteLine(b.Name + line);
                            var fields = line.Split(',');
                            var identity = fields[0];
                            var name = fields[1];
                            // var syncdate=fields[2];
                            var theuser = db.User.FirstOrDefault(async => async.Identity == identity);
                            if (theuser == null)
                            {
                                db.User.Add(new User
                                {
                                    Identity = identity,
                                    Name = name,
                                    Syncdate = DateTime.Now
                                });
                            }
                            else
                            {
                                Console.WriteLine("user {0} has already existed.",identity);
                            }
                            db.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}

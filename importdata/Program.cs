using System;
using System.IO;
using System.Linq;

namespace importdata
{
    class Program
    {  static string importPath = "/home/inspect/ftp/get";
        static string exportPath = "/home/inspect/ftp/put";
        static string dbtofilePath = "/home/inspect/dbtofile";
        static void Main(string[] args)
        {
            Console.WriteLine("import stated {0}!",DateTime.Now);
             FileToDb();
              Console.WriteLine("import completed {0}!",DateTime.Now);

            //    Console.WriteLine("sms send stated {0}!",DateTime.Now);
            //      var a = new System.Diagnostics.Process();
            // a.StartInfo.FileName = "/home/inspect/bin/sms.sh";
            // a.Start();
            // a.WaitForExit();
            //  Console.WriteLine("sms send completed {0}!",DateTime.Now);
        }
         static void FileToDb()
        {
            using (var db = new studyinContext())
            {
                var filebase="allowToStudy.txt";
                var fname = Path.Combine(importPath, filebase);
                if (!File.Exists(fname)) 
                { 
                    Console.WriteLine("file {0} does  not exist, exit.{1}",fname,DateTime.Now);
                    return;
                }
                    var content = File.ReadAllLines(fname);
                    foreach (var line in content)
                    {
                        var fields = line.Split(',');
                        var identity = fields[0];
                        var phone = fields[1];
                        var drivertype = fields[2].Substring(0,2);
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
                         var status = fields[8];
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
                                    Status=status,
                                    Syncdate = DateTime.Now
                                });
                                 db.SaveChangesAsync();
                                 Console.WriteLine("import user {0} ok {1}", identity,DateTime.Now);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("user {0} sync error{1}.", identity, ex.Message);
                            }
                        }
                        else
                        {
                            if(status!=theuser.Status)                           
                            {
                                theuser.Status=status;                               
                            }
                            if(drugrelated=="1")
                            theuser.Drugrelated=drugrelated;

                             db.SaveChangesAsync();
                             Console.WriteLine("user {0} has already updated.{1}", identity,DateTime.Now);
                        }
                       
                    }
               
            }
        }
    }
}

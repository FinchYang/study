﻿using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace importdata
{
    class Program
    {
        static string importPath = "/home/inspect/ftp/get";
     //   static string importPath = @"e:\11";
        static string exportPath = "/home/inspect/ftp/put";
        static string dbtofilePath = "/home/inspect/dbtofile";
        static void Main(string[] args)
        {
            Console.WriteLine("import stated {0}!", DateTime.Now);
            FileToDb();
            Console.WriteLine("import completed {0}!", DateTime.Now);
        }
        static void FileToDb()
        {
            using (var db = new studyinContext())
            {
                var filebase = "allowToStudy.txt";
                var fname = Path.Combine(importPath, filebase);
                if (!File.Exists(fname))
                {
                    Console.WriteLine("file {0} does  not exist, exit.{1}", fname, DateTime.Now);
                    return;
                }
                var content = File.ReadAllLines(fname);
                foreach (var line in content)
                {
                    var fields = line.Split(',');
                    if(fields.Length<9){
                        Console.WriteLine(" invalid data line {0},{1}",fields.Length,line);
                        continue;
                    }

                    var identity = fields[0];
                     if(identity.Length>44){
                        Console.WriteLine(" invalid data line, identity={0}-{2},{1}",identity.Length,line,identity);
                        continue;
                    }
                    var phone = fields[1];
                    var drivertype = fields[2].Substring(0, 2);
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
                            var today=DateTime.Now;
                            Console.WriteLine("import user {0} , {1},{2},{3},{4},{5},{6},{7},{8},{9}", 
                            identity, ((int)enumtype).ToString(),drugrelated,phone,
                            pictureok,licensenumber,ideducted
                            ,photofile,status,today);
                            db.User.AddAsync(new User
                            {
                                Identity = identity,
                                Licensetype = ((int)enumtype).ToString(),
                                Drugrelated = drugrelated,
                                Syncphone = phone,
                                Photostatus = pictureok,
                                Drivinglicense = licensenumber,
                                Deductedmarks = ideducted,
                                Photofile = photofile,
                                Status = status,
                                Syncdate = today
                            });
                         //   Console.WriteLine("import user {0} before {1}", identity, "db.SaveChanges();");
                             db.SaveChanges();
                            //  db.SaveChangesAsync();
                            Console.WriteLine("import user {0} ok {1}", identity, DateTime.Now);
                        }
                        //              catch (DbUpdateConcurrencyException ex)
                        // {
                        //     foreach (var entry in ex.Entries)
                        //     {
                        //         if (entry.Entity is User)
                        //         {
                        //             // Using a NoTracking query means we get the entity but it is not tracked by the context
                        //             // and will not be merged with existing entities in the context.
                        //             var databaseEntity = db.User.AsNoTracking().Single(p => p.Identity == ((User)entry.Entity).Identity);
                        //             var databaseEntry = db.Entry(databaseEntity);

                        //             foreach (var property in entry.Metadata.GetProperties())
                        //             {
                        //                 var proposedValue = entry.Property(property.Name).CurrentValue;
                        //                 var originalValue = entry.Property(property.Name).OriginalValue;
                        //                 var databaseValue = databaseEntry.Property(property.Name).CurrentValue;

                        //                 // TODO: Logic to decide which value should be written to database
                        //                  entry.Property(property.Name).CurrentValue = proposedValue;

                        //                 // Update original values to
                        //                 entry.Property(property.Name).OriginalValue = databaseEntry.Property(property.Name).CurrentValue;
                        //             }
                        //         }
                        //         else
                        //         {
                        //             throw new NotSupportedException("Don't know how to handle concurrency conflicts for " + entry.Metadata.Name);
                        //         }
                        //     }

                        //     // Retry the save operation
                        //     db.SaveChanges();
                        // }
                        catch (Exception ex)
                        {
                            Console.WriteLine("user {0} sync error{1}.", identity, ex.Message);
                        }
                    }
                    else
                    {
                        if (status != theuser.Status)
                        {
                            theuser.Status = status;
                        }
                        if (drugrelated == "1")
                            theuser.Drugrelated = drugrelated;
                        theuser.Syncdate=DateTime.Now;

                        if(fields.Length>9){
                            switch(fields[9]){
                                case "1":
                                theuser.Inspect=fields[9];
                                break;                                
                                case "0":
                                theuser.Inspect=fields[9];
                                break;
                                default:
                                Console.WriteLine("user {0} ,error permission field, -{1}-", identity, fields[9]);
                                break;
                            }
                        }
                        else{
                             Console.WriteLine("user {0} ,no permission field, -{1}-", identity, fields.Length);
                        }
                         
                        db.SaveChanges();
                        Console.WriteLine("user {0} has already updated.{1}", identity, DateTime.Now);
                    }
                }
              //  db.SaveChangesAsync();
              //  db.Attach
            }
        }
    }
}

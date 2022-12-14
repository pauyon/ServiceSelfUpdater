using Ionic.Zip;
using System;
using System.IO;
using System.Linq;
using System.ServiceProcess;

namespace ServiceUpdater
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Check for an update
            string updateDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Update");

            if (Directory.Exists(updateDirectory) && Directory.GetFileSystemEntries(updateDirectory).Count() > 0)
            {
                foreach (string filepath in Directory.GetFileSystemEntries(updateDirectory))
                {
                    if (Path.GetExtension(filepath) == ".zip")
                    {
                        Console.WriteLine("File is a zip: " + filepath);
                        ServiceController service = new ServiceController("MyTestService");

                        try
                        {
                            // Stop the service
                            if (service.Status != ServiceControllerStatus.Stopped && service.Status != ServiceControllerStatus.StopPending)
                            {
                                service.Stop();
                                service.WaitForStatus(ServiceControllerStatus.Stopped);
                            }

                            // Overwrite files from update zip file
                            using (ZipFile zip = new ZipFile(filepath))
                            {
                                zip.ExtractAll(Path.Combine(Directory.GetCurrentDirectory(), @"E:\Programming\C#\ServiceSelfUpdater\MyWindowsService\bin\Debug"),
                                    ExtractExistingFileAction.OverwriteSilently);
                            }

                            string archiveDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Archive");

                            if (!Directory.Exists(archiveDirectory)) Directory.CreateDirectory(archiveDirectory);
                            if (File.Exists(Path.Combine(archiveDirectory, Path.GetFileName(filepath)))) File.Delete(Path.Combine(archiveDirectory, Path.GetFileName(filepath)));

                            // Move update files into archive folder
                            File.Move(filepath, Path.Combine(archiveDirectory, Path.GetFileName(filepath)));

                            // Start service again
                            if (service.Status== ServiceControllerStatus.Running && service.Status != ServiceControllerStatus.StartPending) 
                            {
                                service.Start();
                                service.WaitForStatus(ServiceControllerStatus.Running);
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}

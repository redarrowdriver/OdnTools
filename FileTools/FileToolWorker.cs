using System;
using System.IO;
using MetadataExtractor;
using MetadataExtractor.Formats;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.Iptc;
using MetadataExtractor.Formats.Jpeg;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileTools
{
    public class FileToolWorker
    {
        public DirectoryInfo srcPath;
        public void structureImageLibrary(DirectoryInfo dirToCheck)
        {
            FileInfo[] files = null;
            DirectoryInfo[] dirs = null;
            try
            {
                files = dirToCheck.GetFiles("*.*");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            if (files != null)
            {
                foreach (FileInfo fi in files)
                {
                    string[] shutterDate = creationDates(fi.FullName);
                    if (shutterDate == null)
                    {
                        Console.WriteLine("Nothing workable found.");
                        Environment.Exit(1);
                    }
                    string year = shutterDate[0];
                    string month = shutterDate[1];
                    string day = shutterDate[2];
                    if (!System.IO.Directory.Exists(srcPath + "\\" + year))
                    {
                        System.IO.Directory.CreateDirectory(srcPath + "\\" + year);
                    }
                    if (!System.IO.Directory.Exists(srcPath + "\\" + year + "\\" + month))
                    {
                        System.IO.Directory.CreateDirectory(srcPath + "\\" + year + "\\" + month);
                    }
                    if (!System.IO.Directory.Exists(srcPath + "\\" + year + "\\" + month + "\\" + day))
                    {
                        System.IO.Directory.CreateDirectory(srcPath + "\\" + year + "\\" + month + "\\" + day);
                    }
                    try
                    {
                        Console.WriteLine("Moving File: " + fi.FullName + " - to: " + srcPath + "\\" + year + "\\" + month + "\\" + day + "\\" + fi.Name);
                        System.IO.File.Move(fi.FullName, srcPath + "\\" + year + "\\" + month + "\\" + day + "\\" + fi.Name);
                    }
                    catch
                    {
                        System.IO.File.Move(fi.FullName, srcPath + "\\" + year + "\\" + month + "\\" + day + "\\" + DateTime.Now.ToString("HH-mm-ss-fff") + "-" + fi.Name);
                    }
                }
            }

            dirs = dirToCheck.GetDirectories();
            foreach (DirectoryInfo di in dirs)
            {
                structureImageLibrary(di);
            }
        }

        public void emptyDirectories(DirectoryInfo dirToCheck)
        {
            try
            {
                if (dirToCheck.Exists)
                {
                    DirectoryInfo[] dirs = dirToCheck.GetDirectories();

                    foreach (DirectoryInfo di in dirs)
                    {
                        if (di.Exists)
                        {
                            if (!System.IO.Directory.EnumerateFileSystemEntries(di.FullName).Any())
                            {
                                deleteDir(di);
                            }
                            emptyDirectories(di);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //ignore the exception
            }
        }

        public string[] creationDates(string dirToCheck)
        {
            try
            {
                IReadOnlyList<MetadataExtractor.Directory> image = ImageMetadataReader.ReadMetadata(dirToCheck);
                //needing to read the exif data only.
                ExifSubIfdDirectory subIfdDirectory = image.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                DateTime? shutterDate = subIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);

                if(shutterDate == null)
                {
                    Console.WriteLine("NULL Shutter Date");
                    shutterDate = System.IO.File.GetLastWriteTime(dirToCheck);
                    string shutterN = shutterDate.Value.ToString("yyyy-MM-dd");
                    string[] shutterSplitN = shutterN.Split('-');
                    return shutterSplitN;
                }
                string shutter = shutterDate.Value.ToString("yyyy-MM-dd");
                string[] shutterSplit = shutter.Split('-');
                return shutterSplit;
            }
            catch
            {
                Console.WriteLine("Unable to read metadata. Assume not image file.");
                //getting the file system modified data.
                DateTime? shutterDate;
                shutterDate = System.IO.File.GetLastWriteTime(dirToCheck);
                string shutter = shutterDate.Value.ToString("yyyy-MM-dd");
                string[] shutterSplit = shutter.Split('-');
                return shutterSplit;
            }
        }

        private void deleteDir(DirectoryInfo dir)
        {
            if (dir.Exists)
            {
                Console.WriteLine("Deleting Empty Dir: " + dir.FullName);
                System.IO.Directory.Delete(dir.FullName);
            }
        }
    }
}

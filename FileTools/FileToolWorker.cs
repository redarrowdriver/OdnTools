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
using System.Threading.Tasks;
//using MetadataExtractor;

namespace FileTools
{
    public class FileToolWorker
    {
        public void emptyDirectories(DirectoryInfo dirToCheck)
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
                    Console.WriteLine(fi.FullName);
                }
            }

            dirs = dirToCheck.GetDirectories();
            foreach (DirectoryInfo di in dirs)
            {
                emptyDirectories(di);
            }

            //okay so this way only returns the length of the directory name
            //need to do it like this
            //get into the directory check if there is a file in the directory if not, the dir is empty
            //this should give us our empty directories
            //need to check for files in each dir



            //string[] dirs = Directory.GetDirectories(dirToCheck, "*", SearchOption.AllDirectories);
            //foreach (string dir in dirs)
            //{
            //    int fileCount = Directory.GetFiles(dir, "*", SearchOption.AllDirectories).Length;
            //    if (fileCount == 0)
            //    {
            //        Console.WriteLine(dir);
            //    }
            //}

            //int fileCount = Directory.GetFiles(dirToCheck, "*", SearchOption.AllDirectories).Length;
            //Console.WriteLine(fileCount.ToString());





            //string[] directories = Directory.GetFiles(dirToCheck, "*", SearchOption.AllDirectories);
            //foreach (string dir in directories)
            //{
            //    Console.WriteLine(dir + "         " + dir.Length.ToString());

            //}

            


            //DirectoryInfo di = new DirectoryInfo(dirToCheck);
            //DirectoryInfo[] subDirs = di.GetDirectories();
            //foreach (DirectoryInfo info in subDirs)
            //{
            //    Console.WriteLine(info.FullName.ToString());
            //}
        }

        public void creationDates(string dirToCheck)
        {
            IReadOnlyList<MetadataExtractor.Directory> image = ImageMetadataReader.ReadMetadata(dirToCheck);
            foreach (MetadataExtractor.Directory dir in image)
            {
                foreach (Tag tag in dir.Tags)
                {
                    //Console.WriteLine($"{dir.Name} - {tag.Name} = {tag.Description}");
                }
            }

            ExifSubIfdDirectory subIfdDirectory = image.OfType<ExifSubIfdDirectory>().FirstOrDefault();
            try
            {
                Nullable<DateTime> dateTime = subIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTime);
                if (dateTime == null)
                {
                    Console.WriteLine("TagDateTime is NULLLLL");
                }
                Console.WriteLine("TagDateTime: " + dateTime.ToString());
            }
            catch (MetadataException e)
            {
                Console.WriteLine(e.ToString());
            }
            Nullable<DateTime> shutterDate = subIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);

            //Console.WriteLine(dateTime.ToString());
            if (shutterDate == null)
            {
                Console.WriteLine("Shutter Date is null");
            }
            else
            {
                Console.WriteLine(shutterDate.ToString());
            }

            //IJpegSegmentMetadataReader[] reader = new IJpegSegmentMetadataReader[]
            //{
            //    new ExifReader(),
            //    new IptcReader()
            //};

            //try
            //{
            //    IReadOnlyList<MetadataExtractor.Directory> dirs = JpegMetadataReader.ReadMetadata(dirToCheck, reader);
            //    Console.WriteLine(dirs.ToString());
            //}
            //catch (JpegProcessingException e)
            //{
            //    Console.WriteLine("JPEG Processing Exp: " + e.ToString());
            //}
            //catch (IOException e)
            //{
            //    Console.WriteLine("IO Exception: " + e.ToString());
            //}

            //DateTime mod = File.GetLastWriteTime(dirToCheck);
            //Console.WriteLine(mod.ToString());

        }
    }
}

using mitoSoft.Common.Media.Contracts;
using mitoSoft.Common.Media.Exceptions;
using mitoSoft.Common.Media.Handler;
using System;
using System.IO;

namespace mitoSoft.Common.Media
{
    public class MediaFileHandler
    {
        public DateTime GetCreationDate(FileInfo file, bool considerSamsungFormat = true)
        {
            if (!File.Exists(file.FullName))
            {
                throw new FileNotFoundException($"File '{file.FullName}' not found.");
            }

            try
            {
                if (file.Extension.ToLower() != ".jpg" &&
                    file.Extension.ToLower() != ".jpeg" &&
                    file.Extension.ToLower() != ".heic" &&
                    file.Extension.ToLower() != ".arw" &&
                    file.Extension.ToLower() != ".mov" &&  //MOV files from I-Phone
                    file.Extension.ToLower() != ".mp4")
                {
                    throw new InvalidExtensionException("invalid file type.");
                }

                if (!considerSamsungFormat)
                {
                    throw new FormatException();
                }

                //*******Im Dateinamen existiert bereits ein DatumsSchlüssel************************

                return this.GateDate(new SamsungHandler(), file);
            }
            catch (FormatException)
            {
                //*******Im Dateinamen existiert kein DatumsSchlüssel*******************************

                if (file.Name.StartsWith("IMG-"))
                {
                    return this.GateDate(new WhatsAppHandler(), file);
                }
                else if (file.Extension.ToLower() == ".jpg")
                {
                    return this.GateDate(new JpegHandler(), file);
                }
                else if (file.Extension.ToLower() == ".heic")
                {
                    return this.GateDate(new HeicHandler(), file);
                }
                else if (file.Extension.ToLower() == ".arw")
                {
                    return this.GateDate(new SonyHandler(), file);
                }
                else if (file.Extension.ToLower() == ".mp4")
                {
                    return this.GateDate(new Mp4Handler(), file);
                }
                else if (file.Extension.ToLower() == ".mov")
                {
                    return this.GateDate(new AppleHandler(), file);
                }
            }

            throw new InvalidOperationException("Invalid pattern matching.");
        }

        private DateTime GateDate(IHandler handler, FileInfo file)
        {
            var date = handler.GetShootingDate(file);

            return date;
        }
    }
}
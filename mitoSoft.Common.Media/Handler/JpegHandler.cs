using mitoSoft.Common.Media.Contracts;
using mitoSoft.Common.Media.Extensions;
using System;
using System.Drawing;
using System.IO;

namespace mitoSoft.Common.Media.Handler
{
    internal class JpegHandler : IHandler
    {
        /// <summary>
        /// Metadaten auslesen -> nach ID 306 suchen , das ist das Datum
        /// </summary>
        /// <returns></returns>
        public DateTime GetShootingDate(FileInfo file)
        {
            //Id=305 steht für Eigenschaften->Aufnahmedatum
            string dateTimeString = GetImageProperty(file, 306);

            //****Wenn Das Bild von Capture One bearbeitet wurde
            //Id=305 steht für Eigenschaften->Ursprung->Programmname
            if (dateTimeString == string.Empty && GetImageProperty(file, 305).Contains("Capture One"))
            {
                dateTimeString = GetImageProperty(file, 36867);
            }

            if (dateTimeString == string.Empty)
            {
                throw new Exception("No datatime found in metadata");
            }

            var date = dateTimeString.Replace("\0", "").ConvertToDateTime("yyyy:MM:dd HH:mm:ss");

            return date;
        }

        private static string GetImageProperty(FileInfo file, int Id)
        {
            System.Drawing.Image image = new Bitmap(file.FullName);

            var propItems = image.PropertyItems;

            var encoding = new System.Text.ASCIIEncoding();

            var resultString = String.Empty;

            foreach (var item in propItems)
            {
                if (item.Id == Id)
                {
                    resultString = encoding.GetString(item.Value!);
                    break;
                }
            }

            image.Dispose();

            return resultString;
        }
    }
}
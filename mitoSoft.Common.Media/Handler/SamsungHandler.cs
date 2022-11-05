using mitoSoft.Common.Media.Contracts;
using mitoSoft.Common.Media.Extensions;
using System;
using System.IO;

namespace mitoSoft.Common.Media.Handler
{
    internal class SamsungHandler : IHandler
    {
        /// <summary>
        /// Falls die Datei berets ein 'SamsungFormat' hat
        /// </summary>
        /// <returns></returns>
        public DateTime GetShootingDate(FileInfo file)
        {
            if (file.Name.Replace(file.Extension, "").Length < 15)
            {
                throw new FormatException("No Samsung format");
            }

            var date = file.Name[..15].ConvertToDateTime("yyyyMMdd_HHmmss");
            return date;
        }
    }
}
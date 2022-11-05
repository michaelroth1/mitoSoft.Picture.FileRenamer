using mitoSoft.Common.Media.Contracts;
using mitoSoft.Common.Media.Extensions;
using System;
using System.IO;

namespace mitoSoft.Common.Media.Handler
{
    internal class WhatsAppHandler : IHandler
    {
        public DateTime GetShootingDate(FileInfo file)
        {
            string fileName = file.Name.Replace("IMG-", "");
            string dateString = fileName.Substring(0, fileName.IndexOf("-"));
            var date = dateString.Trim().ConvertToDateTime("yyyyMMdd");
            return date;
        }
    }
}
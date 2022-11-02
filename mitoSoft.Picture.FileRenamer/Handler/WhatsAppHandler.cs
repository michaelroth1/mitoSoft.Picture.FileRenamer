using mitoSoft.Picture.FileRenamer.Contracts;
using mitoSoft.Picture.FileRenamer.Extensions;

namespace mitoSoft.Picture.FileRenamer.Handler
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
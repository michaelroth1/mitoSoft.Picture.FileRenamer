using mitoSoft.Picture.FileRenamer.Contracts;
using mitoSoft.Picture.FileRenamer.Extensions;

namespace mitoSoft.Picture.FileRenamer.Handler
{
    internal class FileNameHandler : IHandler
    {
        /// <summary>
        /// Falls die Datei berets ein 'SamsungFormat' hat
        /// </summary>
        /// <returns></returns>
        public DateTime GetShootingDate(FileInfo file)
        {
            if (file.Name.Replace(file.Extension, "").Length < 15)
            {
                throw new FormatException("no samsung format");
            }

            var date = file.Name.Substring(0, 15).ConvertToDateTime("yyyyMMdd_HHmmss");
            return date;
        }
    }
}

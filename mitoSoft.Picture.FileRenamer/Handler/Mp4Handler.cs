using mitoSoft.Picture.FileRenamer.Contracts;
using mitoSoft.Picture.FileRenamer.Extensions;

namespace mitoSoft.Picture.FileRenamer.Handler
{
    internal class Mp4Handler : IHandler
    {
        /// <summary>
        /// Metadaten auslesen -> nach "Aufnahmedatum" suchen
        /// </summary>
        /// <returns></returns>
        public DateTime GetShootingDate(FileInfo file)
        {
            var detailString = FileDetailsHandler.GetDetailsOf(file, "Änderungsdatum");
            var date = detailString.Trim().ConvertToDateTime("dd.MM.yyyy HH:mm");
            return date!;
        }
    }
}
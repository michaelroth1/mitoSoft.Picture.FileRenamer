using mitoSoft.Common.Media.Contracts;
using mitoSoft.Common.Media.Extensions;
using mitoSoft.Common.Media.Helper;
using System;
using System.IO;

namespace mitoSoft.Common.Media.Handler
{
    internal class Mp4Handler : IHandler
    {
        /// <summary>
        /// Metadaten auslesen -> nach "Aufnahmedatum" suchen
        /// </summary>
        /// <returns></returns>
        public DateTime GetShootingDate(FileInfo file)
        {
            var detailString = FileDetailsHelper.GetDetailsOf(file, "Änderungsdatum");
            var date = detailString.Trim().ConvertToDateTime("dd.MM.yyyy HH:mm");
            return date!;
        }
    }
}
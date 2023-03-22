using mitoSoft.Common.Media.Contracts;
using mitoSoft.Common.Media.Extensions;
using mitoSoft.Common.Media.Helper;
using System;
using System.IO;
using System.Linq;

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
            var dateString = FileDetailsHelper.GetDetailsOf(file, "Änderungsdatum"); //TODO check for eng. system
            var date = dateString.Trim().CleanUp().ConvertToDateTime("dd.MM.yyyy HH:mm");
            return date!;
        }
    }
}
using mitoSoft.Common.Media.Contracts;
using mitoSoft.Common.Media.Extensions;
using mitoSoft.Common.Media.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace mitoSoft.Common.Media.Handler
{
    internal class HeicHandler : IHandler
    {
        /// <summary>
        /// Metadaten auslesen -> nach "Aufnahmedatum" suchen
        /// </summary>
        /// <returns></returns>
        public DateTime GetShootingDate(FileInfo file)
        {
            var dateString = FileDetailsHelper.GetDetailsOf(file, "Aufnahmedatum"); //TODO check for eng. system
            var date = dateString.Trim().CleanUp().ConvertToDateTime("dd.MM.yyyy HH:mm");
            return date!;
        }
    }
}
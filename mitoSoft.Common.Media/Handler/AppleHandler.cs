﻿using mitoSoft.Common.Media.Contracts;
using mitoSoft.Common.Media.Extensions;
using mitoSoft.Common.Media.Helper;
using System;
using System.IO;

namespace mitoSoft.Common.Media.Handler
{
    internal class AppleHandler : IHandler
    {
        public DateTime GetShootingDate(FileInfo file)
        {
            var dateString = FileDetailsHelper.GetDetailsOf(file, "Änderungsdatum"); //TODO check for eng. system
            var date = dateString.Trim().CleanUp().ConvertToDateTime("dd.MM.yyyy HH:mm");
            return date!;
        }
    }
}
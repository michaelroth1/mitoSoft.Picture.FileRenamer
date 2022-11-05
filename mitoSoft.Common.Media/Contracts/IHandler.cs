using System;
using System.IO;

namespace mitoSoft.Common.Media.Contracts
{
    internal interface IHandler
    {
        DateTime GetShootingDate(FileInfo file);
    }
}
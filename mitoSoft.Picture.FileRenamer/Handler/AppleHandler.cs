using mitoSoft.Picture.FileRenamer.Contracts;
using mitoSoft.Picture.FileRenamer.Extensions;

namespace mitoSoft.Picture.FileRenamer.Handler
{
    internal class AppleHandler : IHandler
    {
        public DateTime GetShootingDate(FileInfo file)
        {
            var date = file.LastAccessTime;
            return date!;
        }
    }
}
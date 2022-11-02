namespace mitoSoft.Picture.FileRenamer.Contracts
{
    internal interface IHandler
    {
        DateTime GetShootingDate(FileInfo file);
    }
}
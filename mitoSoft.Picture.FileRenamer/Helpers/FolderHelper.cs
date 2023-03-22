namespace mitoSoft.Picture.FileRenamer.Helpers
{
    internal static class FolderHelper
    {
        public static string GetLocalFolder()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            folder = System.IO.Path.Combine(folder, "MediaFileRenamer");
            return folder;
        }
    }
}
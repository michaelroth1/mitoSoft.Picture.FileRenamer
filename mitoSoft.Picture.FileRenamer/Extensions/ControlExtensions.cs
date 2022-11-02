namespace mitoSoft.Picture.FileRenamer.Extensions
{
    internal static class ControlExtensions
    {
        public static List<FileInfo> ToFileCollection(this ListBox.ObjectCollection objects)
        {
            var files = new List<FileInfo>();
            foreach (string item in objects)
            {
                files.Add(new FileInfo(item));
            }
            return files;
        }
    }
}
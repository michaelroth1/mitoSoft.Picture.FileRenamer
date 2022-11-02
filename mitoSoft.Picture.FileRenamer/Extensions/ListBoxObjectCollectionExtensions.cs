namespace mitoSoft.Picture.FileRenamer.Extensions
{
    internal static class ListBoxObjectCollectionExtensions
    {
        public static void Refresh(this ListBox.ObjectCollection collection, int i)
        {
            var o = collection[i];
            collection.RemoveAt(i);
            collection.Insert(i, o);
        }
    }
}
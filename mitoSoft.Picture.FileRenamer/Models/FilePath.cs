namespace mitoSoft.Picture.FileRenamer.Models
{
    internal class FilePath
    {
        public string FullName { get; set; } = string.Empty;

        public string Extension
        {
            get
            {
                if (!string.IsNullOrEmpty(FullName))
                {
                    return new FileInfo(FullName).Extension;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(FullName))
                {
                    return new FileInfo(FullName).Name;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string Error { get; set; } = string.Empty;

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Error))
            {
                return FullName;
            }
            else
            {
                return $"{FullName} ({Error})";
            }
        }
    }
}
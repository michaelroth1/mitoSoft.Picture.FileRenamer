namespace mitoSoft.Picture.FileRenamer.Models
{
    internal class FilePath
    {
        public string FullName { get; set; } = string.Empty;

        public string Extension
        {
            get
            {
                if (!string.IsNullOrEmpty(this.FullName))
                {
                    return new FileInfo(this.FullName).Extension;
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
                if (!string.IsNullOrEmpty(this.FullName))
                {
                    return new FileInfo(this.FullName).Name;
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
            if (string.IsNullOrEmpty(this.Error))
            {
                return FullName;
            }
            else
            {
                return $"{this.FullName} ({this.Error})";
            }
        }
    }
}
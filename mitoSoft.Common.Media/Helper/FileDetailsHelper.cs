using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace mitoSoft.Common.Media.Helper
{
    internal class FileDetailsHelper
    {
        /// <summary>
        /// Metadaten auslesen -> nach "Aufnahmedatum" suchen
        /// </summary>
        /// <returns></returns>
        public static string GetDetailsOf(FileInfo file, string detailname)
        {
            var shell = new Shell32.Shell();
            Shell32.Folder folder = shell.NameSpace(Path.GetDirectoryName(file.FullName));
            Shell32.FolderItem item = folder.ParseName(Path.GetFileName(file.FullName));
            var headers = new List<string>();

            for (int i = 0; i < 32000; i++)
            {
                string header = folder.GetDetailsOf(null, i);
                if (string.IsNullOrEmpty(header))
                {
                    break;
                }
                headers.Add(header);
            }

            for (int i = 0; i < headers.Count() - 1; i++)
            {
                if (headers[i].ToLower() == detailname.ToLower())
                {
                    return folder.GetDetailsOf(item, i);
                }
            }

            throw new Exception($"No '{detailname}' tag found in video file {file.Name}");
        }
    }
}
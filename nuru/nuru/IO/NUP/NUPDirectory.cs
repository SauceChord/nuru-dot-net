using System.Collections.Generic;
using System.IO;

namespace nuru.IO.NUP
{
    public class NUPDirectory
    {
        Dictionary<string, NUPFile> nupFiles = new Dictionary<string, NUPFile>();
        public NUPDirectory(string path)       
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException("Directory '" + path + "' not found.");

            var files = Directory.GetFiles(path, "*.nup");
            foreach (var file in files)
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                nupFiles.Add(fileNameWithoutExtension, NUPFile.FromFile(file));
            }
        }

        public NUPFile GetNUPFile(string name)
        {
            return nupFiles[name];
        }
    }
}

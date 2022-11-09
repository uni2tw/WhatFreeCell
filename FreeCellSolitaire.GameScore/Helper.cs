using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace FreeCellSolitaire.Data
{
    public class Helper
    {
        private static string _rootPath;

        public static string MapPath(string relPath)
        {
            if (_rootPath == null)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    _rootPath = Directory.GetCurrentDirectory();
                }
                else
                {
                    _rootPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) +
                        Path.DirectorySeparatorChar;
                }
            }
            string result = Path.Combine(_rootPath, relPath);
            return result;
        }
    }
}

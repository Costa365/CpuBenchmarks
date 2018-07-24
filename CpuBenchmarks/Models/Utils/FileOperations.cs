using System.IO;

namespace CpuBenchmarks.Models.Utils
{
    public class FileOperations : IFileOperations
    {
        public bool Exists(string filename)
        {
            return File.Exists(filename);
        }

        public void WriteAllText(string filename, string text)
        {
            File.WriteAllText(filename, text);
        }

        public string ReadAllText(string filename)
        {
            return File.ReadAllText(filename);
        }
    }
}

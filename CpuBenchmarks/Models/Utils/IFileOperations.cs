namespace CpuBenchmarks.Models.Utils
{
    public interface IFileOperations
    {
        bool Exists(string filename);
        void WriteAllText(string filename, string text);
        string ReadAllText(string filename);
    }
}

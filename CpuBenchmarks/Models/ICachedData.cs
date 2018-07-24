namespace CpuBenchmarks.Models
{
    public interface ICachedData
    {
        bool HasCachedData { get; }
        string FileName { get; }
        string Data { get; set; }
    }
}

using CpuBenchmarks.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpuBenchmarks.Models
{
    class Benchmark
    {
        //http://www.markwithall.com/programming/2013/03/01/worlds-simplest-csharp-wpf-mvvm-example.html
        private Passmark _passMark;

        public Benchmark()
        {
            _passMark = new Passmark(new CachedData(new FileOperations()));
        }

        public List<CpuParam> GetBenchMarkResult(string cpu)
        {
            return _passMark.GetCpuBenchmark(cpu);
        }

        public Task<int> GetBenchmarkData(bool force)
        {
            return _passMark.DownloadData(force);
        }
    }
}

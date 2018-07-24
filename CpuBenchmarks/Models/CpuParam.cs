using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpuBenchmarks.Models
{
    public class CpuParam
    {
        public CpuParam(string param, string value)
        {
            Param = param;
            Value = value;
        }
        public string Param { get; set; }
        public string Value { get; set; }
    }
}

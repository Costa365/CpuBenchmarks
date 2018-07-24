using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CpuBenchmarks.Models.Utils;

namespace CpuBenchmarks.Models
{
    public class CachedData : ICachedData
    {
        private readonly string _fileName = "cache.html";
        private bool _haveCache;
        private IFileOperations _fileOps;

        public CachedData(IFileOperations fileops)
        {
            _fileOps = fileops;
            _haveCache = _fileOps.Exists(_fileName);
        }

        public bool HasCachedData
        {
            get { return _haveCache; }
        }

        public string FileName
        {
            get { return _fileName; }
        }

        public string Data
        {
            set
            {
                _fileOps.WriteAllText(_fileName, value);
                _haveCache = true;
            }

            get
            {
                string data = "";
                if (HasCachedData)
                {
                    data = _fileOps.ReadAllText(_fileName);
                }
                return data;
            }            
        }
    }
}

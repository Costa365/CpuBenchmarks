using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CpuBenchmarks.Models
{
    public class Passmark
    {
        private readonly string _megaPageUrl =
            "https://www.cpubenchmark.net/CPU_mega_page.html";
        private string _benchmarkData = "";
        private ICachedData _cachedData;

        public Passmark(ICachedData cachedData)
        {
            _cachedData = cachedData;
        }

        public string BenchmarkData
        {
            set
            {
                _benchmarkData = value;                
            }
        }

        public async Task<int> DownloadData(bool force)
        {
            if (_cachedData.HasCachedData && !force)
            {
                return await GetCachedData();
            }
            else
            {
                return await GetData();
            }
        }

        private Task<int> GetCachedData()
        {
            var task = Task.Factory.StartNew(() =>
            {
                _benchmarkData = _cachedData.Data;
                return _benchmarkData.Length;
            });
            return task;
        }

        private async Task<int> GetData()
        {
            var client = new WebClient();
            var htmlByte = await client.DownloadDataTaskAsync(new Uri(_megaPageUrl));

            var task = await Task.Factory.StartNew(() =>
            {
                _benchmarkData = Encoding.Default.GetString(htmlByte).Replace("\n", "");
                _cachedData.Data = _benchmarkData;
                return htmlByte.Length;
            });

            return task;
        }

        public List<CpuParam> GetCpuBenchmark(string cpu)
        {
            List<CpuParam> scores = new List<CpuParam>();

            string rx = cpu +
                @".*?>(.*?)</a></td><td>.*?</td><td>(.*?)</td><td>.*?</td><td>(.*?)</td><td>.*?" +
                @"</td><td>(.*?)</td><td>(.*?)</td><td>(.*?)</td><td>.*?</td><td>(.*?)</td></tr>" +
                @".*?No\sof\sCores</span>:\s(.*?)</div>";

            Match match = Regex.Match(_benchmarkData, rx, RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);

            if (match.Success)
            {
                scores.Add(new CpuParam("Full name", match.Groups[1].Value));
                scores.Add(new CpuParam("Cores", match.Groups[8].Value));
                scores.Add(new CpuParam("Type", match.Groups[7].Value));
                scores.Add(new CpuParam("CPU Mark", match.Groups[2].Value));
                scores.Add(new CpuParam("Single Thread Mark", match.Groups[3].Value));
                scores.Add(new CpuParam("TDP (W)", match.Groups[4].Value));
                scores.Add(new CpuParam("Power Perf", match.Groups[5].Value));
                scores.Add(new CpuParam("Release Date" , match.Groups[6].Value));
            }

            return scores;
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using CpuBenchmarks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CpuBenchmarks.Models.Utils;
using NSubstitute;

namespace CpuBenchmarks.Models.Tests
{
    [TestClass()]
    public class PassmarkTests
    {
        private IFileOperations _fileops;
        private ICachedData _cachedData;
        private readonly string _sampledata =
            @"<tr id=""cpu1""><td rowspan=""2""><a class=""toggle collapsed"" href=""#""></a><a href=""cpu_lookup.php?cpu=Intel+Core+i7-2600+%40+3.40GHz&amp;id=1"">Intel Core i7-2600 @ 3.40GHz</a></td><td><a href=""cpu.php?cpu=Intel+Core+i7-2600+%40+3.40GHz&amp;id=1#price"">$385.33</a></td><td>8226</td><td><a href=""cpu.php?cpu=Intel+Core+i7-2600+%40+3.40GHz&amp;id=1#price"">21.35</a></td><td>1919</td><td>4.98</td><td>95</td><td>86</td><td>Dec 2010</td><td>LGA1155</td><td>Desktop</td></tr><tr class=""tablesorter-childRow""><td colspan=""10""><div><span class=""bold"">Clock Speed</span>: 3400</div><div><span class=""bold"">Turbo Speed</span>: 3800</div><div><span class=""bold"">No of Cores</span>: 4 (2 logical cores per physical)</div><div><span class=""bold"">Rank</span>: 259</div><div><span class=""bold"">Samples</span>: 5078</div></td></tr>" +
            @"<tr id=""cpu868""><td rowspan=""2""><a class=""toggle collapsed"" href=""#""></a><a href=""cpu_lookup.php?cpu=Intel+Core+i7-2600K+%40+3.40GHz&amp;id=868"">Intel Core i7-2600K @ 3.40GHz</a></td><td><a href=""cpu.php?cpu=Intel+Core+i7-2600K+%40+3.40GHz&amp;id=868#price"">$289.00</a></td><td>8478</td><td><a href=""cpu.php?cpu=Intel+Core+i7-2600K+%40+3.40GHz&amp;id=868#price"">29.34</a></td><td>1942</td><td>6.72</td><td>95</td><td>89</td><td>Nov 2010</td><td>LGA1155</td><td>Desktop</td></tr><tr class=""tablesorter-childRow""><td colspan=""10""><div><span class=""bold"">Clock Speed</span>: 3400</div><div><span class=""bold"">Turbo Speed</span>: 3800</div><div><span class=""bold"">No of Cores</span>: 4 (2 logical cores per physical)</div><div><span class=""bold"">Rank</span>: 242</div><div><span class=""bold"">Samples</span>: 4788</div></td></tr><tr id=""cpu869""><td rowspan=""2""><a class=""toggle collapsed"" href=""#""></a><a href=""cpu_lookup.php?cpu=Intel+Core+i7-2600S+%40+2.80GHz&amp;id=869"">Intel Core i7-2600S @ 2.80GHz</a></td><td><a href=""cpu.php?cpu=Intel+Core+i7-2600S+%40+2.80GHz&amp;id=869#price"">$258.59</a></td><td>7061</td><td><a href=""cpu.php?cpu=Intel+Core+i7-2600S+%40+2.80GHz&amp;id=869#price"">27.31</a></td><td>1793</td><td>6.93</td><td>65</td><td>108</td><td>Feb 2011</td><td>LGA1155</td><td>Desktop</td></tr><tr class=""tablesorter-childRow""><td colspan=""10""><div><span class=""bold"">Clock Speed</span>: 2800</div><div><span class=""bold"">Turbo Speed</span>: 3800</div><div><span class=""bold"">No of Cores</span>: 4 (2 logical cores per physical)</div><div><span class=""bold"">Rank</span>: 333</div><div><span class=""bold"">Samples</span>: 167</div></td></tr>";
            

        public void PassmarkTest()
        {
            _fileops = Substitute.For<IFileOperations>();
            _cachedData = Substitute.For<ICachedData>();
        }

        [TestMethod()]
        public void GetCpuBenchmarkTest()
        {
            var passmark = new Passmark(_cachedData);
            passmark.BenchmarkData = _sampledata;
            var scores = passmark.GetCpuBenchmark("i7-2600K");
            Assert.AreEqual("Intel Core i7-2600K @ 3.40GHz", scores[0].Value);
            Assert.AreEqual("4 (2 logical cores per physical)", scores[1].Value);
            Assert.AreEqual("Desktop", scores[2].Value);
            Assert.AreEqual("95", scores[5].Value);
            Assert.AreEqual("89", scores[6].Value);
            Assert.AreEqual("Nov 2010", scores[7].Value);
        }

        [TestMethod()]
        public void GetCpuBenchmark2ndCpuTest()
        {
            var passmark = new Passmark(_cachedData);
            passmark.BenchmarkData = _sampledata;
            var scores = passmark.GetCpuBenchmark("i7-2600S");
            Assert.AreEqual("Intel Core i7-2600S @ 2.80GHz", scores[0].Value);
            Assert.AreEqual("4 (2 logical cores per physical)", scores[1].Value);
            Assert.AreEqual("Desktop", scores[2].Value);
            Assert.AreEqual("65", scores[5].Value);
            Assert.AreEqual("108", scores[6].Value);
            Assert.AreEqual("Feb 2011", scores[7].Value);
        }

        [TestMethod()]
        public void GetCpuBenchmarkUnknownTest()
        {
            var passmark = new Passmark(_cachedData);
            passmark.BenchmarkData = _sampledata;
            var scores = passmark.GetCpuBenchmark("i7-2600Z");
            
            Assert.AreEqual(0, scores.Count);
        }
    }
}
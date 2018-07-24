using Microsoft.VisualStudio.TestTools.UnitTesting;
using CpuBenchmarks.Models;
using System;
using System.IO;
using NSubstitute;
using CpuBenchmarks.Models.Utils;

namespace CpuBenchmarks.Models.Tests
{
    [TestClass()]
    public class CachedDataTests
    {
        private IFileOperations _fileops;
        public CachedDataTests()
        {
            _fileops = Substitute.For<IFileOperations>();
        }

        [TestMethod()]
        public void NoCachedDataTest()
        {
            _fileops.Exists("").ReturnsForAnyArgs(false);
            var cachedData = new CachedData(_fileops);                        
            Assert.AreEqual(false, cachedData.HasCachedData);            
        }

        [TestMethod()]
        public void HaveCachedDataTest()
        {
            _fileops.Exists("").ReturnsForAnyArgs(true);
            var cachedData = new CachedData(_fileops);
            Assert.AreEqual(true, cachedData.HasCachedData);
        }
    }
}
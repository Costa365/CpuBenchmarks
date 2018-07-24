using Microsoft.VisualStudio.TestTools.UnitTesting;
using CpuBenchmarks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpuBenchmarks.Models.Tests
{
    [TestClass()]
    public class LogoTests
    {
        [TestMethod()]
        public void GetLogoForNullTest()
        {
            
            Assert.AreEqual("", Logo.GetLogo(null));
        }

        [TestMethod()]
        public void GetLogoForUnknownTest()
        {

            Assert.AreEqual("", Logo.GetLogo("FastChinese"));
        }

        [TestMethod()]
        public void GetLogoForAMD()
        {
            Assert.AreEqual("/Logos/Amd.bmp", Logo.GetLogo("AMD FX3400"));
        }

        [TestMethod()]
        public void GetLogoForInteli5()
        {

            Assert.AreEqual("/Logos/IntelCoreI5.bmp", Logo.GetLogo("Intel Core i5-7600 @ 3.50GHz"));
        }
    }
}
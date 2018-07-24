using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpuBenchmarks.Models
{
    public class Logo
    {
        public static string GetLogo(string result)
        {
            string logo = "";
            if (result != null)
            {
                if (result.Contains("AMD"))
                {
                    return "/Logos/Amd.bmp";
                }

                if (result.Contains("Xeon"))
                {
                    return "/Logos/IntelXeon.bmp";
                }

                if (result.Contains("Atom"))
                {
                    return "/Logos/IntelAtomg.bmp";
                }

                if (result.Contains("Core i3"))
                {
                    return "/Logos/IntelCoreI3.bmp";
                }

                if (result.Contains("Core i5"))
                {
                    return "/Logos/IntelCoreI5.bmp";
                }

                if (result.Contains("Core i7"))
                {
                    return "/Logos/IntelCoreI7.bmp";
                }

                if (result.Contains("Celeron"))
                {
                    return "/Logos/IntelCeleron.bmp";
                }

                if (result.Contains("Intel"))
                {
                    return "/Logos/Intel.bmp";
                }
                
            }
            return logo;
        }
    }
}

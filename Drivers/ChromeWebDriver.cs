using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VoiceManagedShop.Drivers
{
    public class ChromeWebDriver : WebDriver
    {
        public ChromeWebDriver(string baseUrl) : base(baseUrl, new ChromeDriver(@"C:\Users\vadym.matyrin\source\repos\VoiceManagedShop"))
        {
        }
    }
}

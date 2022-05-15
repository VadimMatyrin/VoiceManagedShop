using OpenQA.Selenium;

namespace VoiceManagedShop.Drivers
{
    public abstract class WebDriver : IDisposable
    {
        protected readonly string baseUrl;
        protected OpenQA.Selenium.WebDriver driver;

        public WebDriver(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        protected WebDriver(string baseUrl, OpenQA.Selenium.WebDriver driver) : this(baseUrl)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(baseUrl);
        }

        public bool TryClickBySelector(string selector)
        {
            var element = driver.FindElement(By.CssSelector(selector));
            if (element == null)
            {
                return false;
            }

            element.Click();
            return true;
        }

        public bool DoesElementExistBySelector(string selector)
        {
            var element = driver.FindElement(By.CssSelector(selector));
            return element != null;
        }

        public bool TryClickByXPath(string xPath, int indexToClick = 0)
        {
            try
            {
                var elements = driver.FindElements(By.XPath(xPath));
                if (elements == null || elements.Count == 0)
                {
                    return false;
                }

                elements[indexToClick].Click();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool TryInputById(string id, string input)
        {
            try
            {
                var element = driver.FindElement(By.Id(id));
                if (element == null)
                {
                    return false;
                }

                element.Clear();
                element.SendKeys(input);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool DoesElementExistByXPath(string xPath)
        {
            var element = driver.FindElement(By.XPath(xPath));
            return element != null;
        }

        public void Dispose()
        {
            driver.Dispose();
        }
    }
}
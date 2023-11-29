using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Drawing;

namespace Hoeffner.Specs.Drivers
{
    public class BrowserDriver : IDisposable
    {
        public IWebDriver CurrentWebDriver;
        private bool _disposedValue;
        private const string _url = "https://www.hoeffner.de";

        public BrowserDriver()
        {
            CurrentWebDriver = CreateWebDriver();
        }

        private IWebDriver CreateWebDriver()
        {
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
            ChromeOptions chromeOptions = new ChromeOptions();

            ChromeDriver chromeDriver = new ChromeDriver(chromeDriverService, chromeOptions);
            chromeDriver.Manage().Window.Size = new Size(1920, 1080);
            chromeDriver.Url = _url;

            return chromeDriver;
        }

        public void Dispose()
        {
            if (_disposedValue)
            {
                return;
            }
            CurrentWebDriver.Quit();
            _disposedValue = true;
        }
    }
}

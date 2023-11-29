using Hoeffner.Specs.Drivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Hoeffner.Specs.Helpers
{
    public class CommonHelper
    {
        private readonly BrowserDriver _browserDriver;
        public const int DefaultWaitInSeconds = 1;
        private IWebElement acceptCookieButton => _browserDriver.CurrentWebDriver.FindElement(By.CssSelector(".consentForm__acceptButton"));

        public CommonHelper(BrowserDriver browserDriver)
        {
            _browserDriver = browserDriver;
        }

        public void ClickAcceptCookieButton()
        {
            acceptCookieButton.Click();
        }

        public bool WaitForRedirect(string url, int timeOut)
        {
            var wait = new WebDriverWait(_browserDriver.CurrentWebDriver, TimeSpan.FromSeconds(timeOut));
            bool redirected = wait.Until(condition =>
            {
                try
                {
                    return _browserDriver.CurrentWebDriver.Url == url;
                }
                catch (Exception)
                {
                    return false;
                }
            });
            return redirected;
        }

        public bool WaitForElement(IWebElement webElement, int timeOut)
        {
            var wait = new WebDriverWait(_browserDriver.CurrentWebDriver, TimeSpan.FromSeconds(timeOut));
            bool displayed = wait.Until(condition =>
            {
                try
                {
                    return webElement.Displayed;
                }
                catch (Exception)
                {
                    return false;
                }
            });
            return displayed;
        }
    }
}

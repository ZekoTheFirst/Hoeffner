using Hoeffner.Specs.Drivers;
using OpenQA.Selenium;


namespace Hoeffner.Specs.Helpers
{
    public class LoginPageHelper
    {
        private readonly BrowserDriver _browserDriver;
        private readonly CommonHelper _commonHelper;
        private const string loginPath = "login";

        private IWebElement forgottenPasswordLink => _browserDriver.CurrentWebDriver.FindElement(By.XPath("//a[@class='existingAccount__forgotten']"));
        private IWebElement newsletterInputField => _browserDriver.CurrentWebDriver.FindElement(By.Id("email"));
        private IWebElement submitButton => _browserDriver.CurrentWebDriver.FindElement(By.Id("newsletterFormSubmitBtn"));
        private IWebElement newsletterConfirmationText => _browserDriver.CurrentWebDriver.FindElement(By.CssSelector(".footerNewsletter__confirmation"));

        public LoginPageHelper(BrowserDriver browserDriver, CommonHelper commonHelper)
        {
            _browserDriver = browserDriver;
            _commonHelper = commonHelper;
        }

        public void LoadLoginPage()
        {
            _browserDriver.CurrentWebDriver.Navigate().GoToUrl(_browserDriver.CurrentWebDriver.Url + loginPath);
        }

        public bool WaitForForgottenPasswordLink()
        {
            return _commonHelper.WaitForElement(forgottenPasswordLink, 1);
        }

        public bool WaitForNewsletterSubscriptionInputField()
        {
            return _commonHelper.WaitForElement(newsletterInputField, 1);
        }

        public void ClickForgottenPasswordLink()
        {
            forgottenPasswordLink.Click();
        }

        public void ClickSendButton()
        {
            submitButton.Click();
        }

        public string GetCurrentUrl()
        {
            return _browserDriver.CurrentWebDriver.Url;
        }

        public string getNewsletterConfirmationText()
        {
            string confirmationText = "";
            if (_commonHelper.WaitForElement(newsletterConfirmationText, 5))
            {
                confirmationText = newsletterConfirmationText.Text;
            }
            return confirmationText;
        }

        public void SetEmail(string email)
        {
            newsletterInputField.Clear();
            newsletterInputField.SendKeys(email);
        }
    }
}

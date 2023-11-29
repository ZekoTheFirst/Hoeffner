using Hoeffner.Specs.Drivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace Hoeffner.Specs.Helpers
{
    public class RegisterCustomerHelper
    {
        private readonly BrowserDriver _browserDriver;
        private readonly CommonHelper _commonHelper;
        private const string registerPath = "registrierung";

        public RegisterCustomerHelper(BrowserDriver browserDriver, CommonHelper commonHelper)
        {
            _browserDriver = browserDriver;
            _commonHelper = commonHelper;
        }

        private IWebElement genderDropdownMenu => _browserDriver.CurrentWebDriver.FindElement(By.Id("salutation"));
        private IWebElement firstNameInputField => _browserDriver.CurrentWebDriver.FindElement(By.Id("firstName"));
        private IWebElement lastNameInputField => _browserDriver.CurrentWebDriver.FindElement(By.Id("lastName"));
        private IWebElement emailAddressInputField => _browserDriver.CurrentWebDriver.FindElement(By.Id("email"));
        private IWebElement passwordInputField => _browserDriver.CurrentWebDriver.FindElement(By.Id("password"));
        private IWebElement repeatPasswordInputField => _browserDriver.CurrentWebDriver.FindElement(By.Id("password2"));
        private IWebElement termsAndConditionsCheckbox => _browserDriver.CurrentWebDriver.FindElement(By.XPath("//*[@id=\"register-form\"]/div/div[12]/div/div/span[1]"));
        private IWebElement furtherButton => _browserDriver.CurrentWebDriver.FindElement(By.Id("register-submit"));
        private IWebElement userAccountButton => _browserDriver.CurrentWebDriver.FindElement(By.ClassName("headerElement__link--login"));
        private IWebElement accountWelcomeMessage => _browserDriver.CurrentWebDriver.FindElement(By.ClassName("titleHeadline"));

        public void LoadRegisterPage()
        {
            _browserDriver.CurrentWebDriver.Navigate().GoToUrl(_browserDriver.CurrentWebDriver.Url + registerPath);
        }

        public bool WaitForGenderDropdownMenu()
        {
            return _commonHelper.WaitForElement(genderDropdownMenu, 1);
        }

        public bool WaitForFirstNameInputField()
        {
            return _commonHelper.WaitForElement(firstNameInputField, 1);
        }

        public bool WaitForLastNameInputField()
        {
            return _commonHelper.WaitForElement(lastNameInputField, 1);
        }

        public bool WaitForEmailAddressInputField()
        {
            return _commonHelper.WaitForElement(emailAddressInputField, 1);
        }

        public bool WaitForPasswordInputField()
        {
            return _commonHelper.WaitForElement(passwordInputField, 1);
        }

        public bool WaitForRepeatPasswordInputField()
        {
            return _commonHelper.WaitForElement(repeatPasswordInputField, 1);
        }

        public bool WaitForUserIcon()
        {
            return _commonHelper.WaitForElement(userAccountButton, 5);
        }

        public void SelectGender(string gender)
        {
            if (!String.IsNullOrEmpty(gender))
            {
                var selectElement = new SelectElement(genderDropdownMenu);
                selectElement.SelectByText(gender);
            }
        }

        public void EnterFirstName(string firstName) 
        { 
            firstNameInputField.SendKeys(firstName);
        }

        public void EnterLastName(string lastName)
        {
            lastNameInputField.SendKeys(lastName);  
        }

        public void EnterEmail(string email)
        {
            emailAddressInputField.SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            passwordInputField.SendKeys(password);
        }

        public void EnterRepeatPassword(string repeatPassword)
        {
            repeatPasswordInputField.SendKeys(repeatPassword);
        }

        public void ClickFutherButton()
        {
            furtherButton.Click();
        }

        public void ClickUserAccountButton()
        {
            userAccountButton.Click();
        }

        public void SelectTermsAndConditionsCheckbox()
        {
            termsAndConditionsCheckbox.Click();
        }

        public ReadOnlyCollection<IWebElement> GetAllInputFieldWarnings()
        {
            ReadOnlyCollection<IWebElement> warnings = _browserDriver.CurrentWebDriver.FindElements(By.ClassName("formInput__error"));
            return warnings;
        }

        public bool GetTermsAndConditionsCheckboxState()
        {
            string classes = termsAndConditionsCheckbox.GetAttribute("class");
            bool state = classes.Contains("checkbox__checkbox--checked");
            return state;
        }
        
        public ReadOnlyCollection<IWebElement> GetAllCheckBoxWarnings()
        {
            ReadOnlyCollection<IWebElement> warnings = _browserDriver.CurrentWebDriver.FindElements(By.ClassName("accountNew__agbBoxError"));
            return warnings;
        }

        public string GetWelcomeMessage()
        {
            return accountWelcomeMessage.Text;
        }
        
    }
}

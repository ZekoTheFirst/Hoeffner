using Hoeffner.Specs.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using RandomNameGeneratorNG;
using System.Collections.ObjectModel;

namespace Hoeffner.Specs.StepDefinitions
{
    [Binding]
    public class RegisterCustomerStepDefinitions
    {
        private readonly RegisterCustomerHelper _createNewCustomerHelper;
        private readonly ScenarioContext _scenarioContext;

        public RegisterCustomerStepDefinitions(RegisterCustomerHelper createNewCustomerHelper, ScenarioContext scenarioContext)
        {
            _createNewCustomerHelper = createNewCustomerHelper;
            _scenarioContext = scenarioContext;
        }

        [Given(@"I load the register page")]
        public void GivenILoadTheRegisterPage()
        {
            _createNewCustomerHelper.LoadRegisterPage();
        }

        [Given(@"I see the gender dropdown menu")]
        public void GivenISeeTheGenderDropdownMenu()
        {
            Assert.IsTrue(_createNewCustomerHelper.WaitForGenderDropdownMenu(), "Gender dropdown menu not visible");
        }

        [Given(@"I see the first name input field")]
        public void GivenISeeTheFirstNameInputField()
        {
            Assert.IsTrue(_createNewCustomerHelper.WaitForFirstNameInputField(), "First name input field not visible");
        }

        [Given(@"I see the last name input field")]
        public void GivenISeeTheLastNameInputField()
        {
            Assert.IsTrue(_createNewCustomerHelper.WaitForLastNameInputField(), "Last name input field not visible");
        }

        [Given(@"I see the email address input field")]
        public void GivenISeeTheEmailAddressInputField()
        {
            Assert.IsTrue(_createNewCustomerHelper.WaitForEmailAddressInputField(), "Email address input field not visible");
        }

        [Given(@"I see the password input field")]
        public void GivenISeeThePasswordInputField()
        {
            Assert.IsTrue(_createNewCustomerHelper.WaitForPasswordInputField(), "Password input field not visible");
        }

        [Given(@"I see the repeat password input field")]
        public void GivenISeeTheRepeatPasswordInputField()
        {
            Assert.IsTrue(_createNewCustomerHelper.WaitForRepeatPasswordInputField(), "Repeat Password input field not visible");
        }

        [When(@"I select (.*) in the gender dropdown menu")]
        public void WhenISelectInTheGenderDropdownMenu(string gender)
        {
            _createNewCustomerHelper.SelectGender(gender);
        }

        [When(@"I enter (.*) in the first name input field")]
        public void WhenIEnterInTheFirstNameInputField(string firstName)
        {   if (firstName == "a random firstname")
            {
                var personGenerator = new PersonNameGenerator();
                firstName = personGenerator.GenerateRandomFirstName();
                _scenarioContext.Set(firstName, "FirstName");
            }
            _createNewCustomerHelper.EnterFirstName(firstName);
        }

        [When(@"I enter (.*) in the last name input field")]
        public void WhenIEnterInTheLastNameInputField(string lastName)
        {
            if (lastName == "a random lastname")
            {
                var personGenerator = new PersonNameGenerator();
                lastName = personGenerator.GenerateRandomLastName();
                _scenarioContext.Set(lastName, "LastName");
            }
            _createNewCustomerHelper.EnterLastName(lastName);   
        }

        [When(@"I enter (.*) in the email input field")]
        public void WhenIEnterInTheEmailInputField(string email)
        {
            if (email == "a random email address")
            {
                email = _scenarioContext.Get<string>("FirstName") + "." + _scenarioContext.Get<string>("LastName") + "@mailz.com";
            }
            _createNewCustomerHelper.EnterEmail(email);
        }

        [When(@"I enter (.*) in the password input field")]
        public void WhenIEnterInThePasswordInputField(string password)
        {
            _createNewCustomerHelper.EnterPassword(password);
        }

        [When(@"I enter (.*) in the repeat password input field")]
        public void WhenIEnterInTheRepeatPasswordInputField(string repeatPassword)
        {
            _createNewCustomerHelper.EnterRepeatPassword(repeatPassword);
        }

        [When(@"I click the Further button")]
        public void WhenIClickTheFurtherButton()
        {
            _createNewCustomerHelper.ClickFutherButton();
        }

        [When(@"I dont select to agree with terms and conditions")]
        public void WhenIDontSelectToAgreeWithTermsAndConditions()
        {
            Assert.IsFalse(_createNewCustomerHelper.GetTermsAndConditionsCheckboxState(), "Terms and Conditions checkbox is checked");
        }

        [When(@"I select to agree with terms and conditions")]
        public void WhenISelectToAgreeWithTermsAndConditions()
        {
            _createNewCustomerHelper.SelectTermsAndConditionsCheckbox();
        }

        [When(@"I can see the user account icon")]
        public void WhenICanSeeTheUserAccountIcon()
        {
            _createNewCustomerHelper.WaitForUserIcon();
        }

        [When(@"I click the user account icon")]
        public void WhenIClickTheUserAccountIcon()
        {
            _createNewCustomerHelper.ClickUserAccountButton();
        }

        [Then(@"the following (.*) is presented to the customer")]
        public void ThenTheFollowingIsPresentedToTheCustomer(string expectedWarnings)
        {
            string[] expectedWarningsArray = expectedWarnings.Split(",");

            Dictionary<string, string> allWarningTypesAndText = new Dictionary<string, string>();
            allWarningTypesAndText.Add("salutation-error", "Bitte geben Sie eine Anrede ein");
            allWarningTypesAndText.Add("firstName-error", "Bitte geben Sie Ihren Vornamen ein");
            allWarningTypesAndText.Add("lastName-error", "Bitte geben Sie Ihren Nachnamen ein");
            allWarningTypesAndText.Add("email-error", "Bitte geben Sie eine gültige E-Mail-Adresse ein");
            allWarningTypesAndText.Add("password-error", "Bitte verwenden Sie ein Passwort von mindestes 8 Zeichen mit mindestens einem Kleinbuchstaben, einem Großbuchstaben, einer Zahl und einem Sonderzeichen.");
            allWarningTypesAndText.Add("password2-error", "Die Passwörter stimmen nicht überein");

            ReadOnlyCollection<IWebElement> warnings = _createNewCustomerHelper.GetAllInputFieldWarnings();

            int i = 0;
            foreach (IWebElement item in warnings)
            {
                string warningType = item.GetAttribute("id");
                string warningText = item.Text;
                Assert.AreEqual(expectedWarningsArray[i], warningType, "Incorrect warning type");
                Assert.AreEqual(allWarningTypesAndText[expectedWarningsArray[i]], warningText, "Incorrect warnings text");
                i++;
            }
        }

        [Then(@"I am promted to accept the terms and conditions")]
        public void ThenIAmPromtedToAcceptTheTermsAndConditions()
        {
            ReadOnlyCollection<IWebElement> warnings = _createNewCustomerHelper.GetAllCheckBoxWarnings();
            Assert.AreEqual(1, warnings.Count, "Too many checkbox warnings presented");
            Assert.AreEqual("Bitte akzeptieren Sie die AGB und die Datenschutzbestimmungen", warnings[0].Text, "Incorrect warning text");
        }

        [Then(@"the welcome message contains the newly created user first and lastname")]
        public void ThenTheWelcomeMessageContainsTheNewlyCreatedUserFirstAndLastname()
        {
            string expectedWelcomeMessage = "Hallo, " + _scenarioContext.Get<string>("FirstName") + " " + _scenarioContext.Get<string>("LastName");
            Assert.AreEqual(expectedWelcomeMessage, _createNewCustomerHelper.GetWelcomeMessage(), "User account welcome message not as expected");
        }
    }
}

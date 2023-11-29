using Hoeffner.Specs.Helpers;
using NUnit.Framework;

namespace Hoeffner.Specs.StepDefinitions
{
    [Binding]
    public class LoginPageStepDefinitions
    {
        private readonly LoginPageHelper _loginHelper;

        public LoginPageStepDefinitions(LoginPageHelper loginHelper)
        {
            _loginHelper = loginHelper;
        }

        [Given(@"I load the login page")]
        public void GivenILoadTheLoginPage()
        {
            _loginHelper.LoadLoginPage();
        }

        [Given(@"I can see forgot password link")]
        public void GivenICanSeeForgotPasswordLink()
        {
            Assert.IsTrue(_loginHelper.WaitForForgottenPasswordLink(), "Forgot password link not visible");
        }

        [Given(@"I see the newsletter subscription input field")]
        public void GivenISeeTheNewsletterSubscriptionInputField()
        {
            Assert.IsTrue(_loginHelper.WaitForNewsletterSubscriptionInputField(), "Newsletter subscription input field not visible");
        }

        [Given(@"I enter the email for (.*) in the newsletter subscription input field")]
        public void GivenIEnterTheEmailForInTheNewsletterSubscriptionInputField(string email)
        {
            _loginHelper.SetEmail(email);
        }

        [When(@"I click on a forgot password link")]
        public void WhenIClickOnAForgotPasswordLink()
        {
            _loginHelper.ClickForgottenPasswordLink();
        }

        [When(@"I click on the Send button")]
        public void WhenIClickOnTheSendButton()
        {
            _loginHelper.ClickSendButton();
        }

        [Then(@"I am redirected to forgot password form")]
        public void ThenIAmRedirectedToForgotPasswordForm()
        {
            Assert.AreEqual("https://www.hoeffner.de/passwort/vergessen/login", _loginHelper.GetCurrentUrl(), "Not redirected to Forgot Password Form");
        }

        [Then(@"I see a confirmation message that my newsletter subscription is in progress")]
        public void ThenISeeAConfirmationMessageThatMyNewsletterSubscriptionIsInProgress()
        {
            string expectedText = "Nur noch ein Klick und Sie haben es geschafft!\r\nBitte bestätigen Sie jetzt Ihre Anmeldung über den Klick auf \"Anmeldung bestätigen\" in der E-Mail, die soeben an Sie versandt wurde.";
            Assert.AreEqual(expectedText, _loginHelper.getNewsletterConfirmationText(), "Newsletter confirmation text incorrect");
        }
    }
}

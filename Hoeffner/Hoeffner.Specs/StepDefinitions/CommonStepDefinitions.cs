using Hoeffner.Specs.Helpers;

namespace Hoeffner.Specs.StepDefinitions
{
    [Binding]
    public class CommonStepDefinitions
    {
        private readonly CommonHelper _commonHelper;

        public CommonStepDefinitions(CommonHelper commonHelper)
        {
            _commonHelper = commonHelper;
        }

        [Given(@"I accept cookies on the page")]
        public void GivenIAcceptCookiesOnThePage()
        {
            _commonHelper.ClickAcceptCookieButton();
        }

        [When(@"I am redirected to (.*)")]
        public void WhenIAmRedirectedTo(string url)
        {
            _commonHelper.WaitForRedirect(url, 5);
        }
    }
}

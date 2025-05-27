using EpamTests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace EpamTests.StepDefinitions
{
    [Binding]
    public class CommonSteps
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly EpamHomePage _homePage;

        public CommonSteps(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            _homePage = new EpamHomePage(_driver, _wait);
        }

        [Given(@"I am on the EPAM homepage")]
        public void GivenIAmOnTheEpamHomepage()
        {
            _driver.Navigate().GoToUrl("https://www.epam.com");
            _wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
            _homePage.HandleCookiePopup();
        }
    }
}
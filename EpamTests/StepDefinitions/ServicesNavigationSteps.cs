using EpamTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TechTalk.SpecFlow;

namespace EpamTests.StepDefinitions
{
    [Binding]
    public class ServicesNavigationSteps
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private EpamHomePage _homePage;
        private EpamServicesPage _servicesPage;

        public ServicesNavigationSteps(IWebDriver driver, WebDriverWait wait)
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
            Console.WriteLine($"Navigated to EPAM homepage. Current URL: {_driver.Url}");
            _homePage.HandleCookiePopup();
        }

        [When(@"I navigate to the Services menu")]
        public void WhenINavigateToTheServicesMenu()
        {
            var servicesMenu = _wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Services")));
            Console.WriteLine("Hovering over Services menu...");
            new Actions(_driver).MoveToElement(servicesMenu).Perform();
        }

        [When(@"I select the ""(.*)"" category")]
        public void WhenISelectTheCategory(string serviceCategory)
        {
            var categoryLink = _wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(serviceCategory)));
            Console.WriteLine($"Attempting to click {serviceCategory} category...");
            categoryLink.Click();
            _wait.Until(ExpectedConditions.UrlContains(serviceCategory.Replace(" ", "-").ToLower()));
            Console.WriteLine($"Navigated to {serviceCategory} page. Current URL: {_driver.Url}");
            _servicesPage = new EpamServicesPage(_driver, _wait);
        }

        [Then(@"the page should contain the title ""(.*)""")]
        public void ThenThePageShouldContainTheTitle(string expectedTitle)
        {
            var actualTitle = _servicesPage.GetPageTitle();
            Console.WriteLine($"Validating page title. Expected: {expectedTitle}, Actual: {actualTitle}");
            Assert.AreEqual(expectedTitle, actualTitle, $"Page title should be '{expectedTitle}'.");
        }

        [Then(@"the ""Our Related Expertise"" section should be displayed")]
        public void ThenTheOurRelatedExpertiseSectionShouldBeDisplayed()
        {
            Assert.IsTrue(_servicesPage.IsOurRelatedExpertiseSectionDisplayed(), "Our Related Expertise section should be displayed.");
            Console.WriteLine("Our Related Expertise section is displayed.");
        }
    }
}
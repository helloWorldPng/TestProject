using EpamTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.IO;
using TechTalk.SpecFlow;
using BoDi;

namespace EpamTests.StepDefinitions
{
    [Binding]
    public class DownloadFileSteps
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly string _downloadPath;
        private readonly EpamHomePage _homePage;
        private EpamAboutPage _aboutPage;

        public DownloadFileSteps(IWebDriver driver, WebDriverWait wait, IObjectContainer objectContainer)
        {
            _driver = driver;
            _wait = wait;
            _downloadPath = objectContainer.Resolve<string>();
            _homePage = new EpamHomePage(_driver, _wait);
        }

        [When(@"I navigate to the About page")]
        public void WhenINavigateToTheAboutPage()
        {
            _aboutPage = _homePage.GoToAboutPage();
        }

        [When(@"I scroll to the EPAM at a Glance section")]
        public void WhenIScrollToTheEpamAtAGlanceSection()
        {
            _aboutPage.ScrollToEpamAtAGlance();
        }

        [When(@"I click the download button")]
        public void WhenIClickTheDownloadButton()
        {
            _aboutPage.ClickDownloadButton();
        }

        [Then(@"the file ""(.*)"" should be downloaded")]
        public void ThenTheFileShouldBeDownloaded(string expectedFileName)
        {
            string filePath = Path.Combine(_downloadPath, expectedFileName);
            try
            {
                _wait.Until(_ => File.Exists(filePath));
                Assert.IsTrue(File.Exists(filePath), $"File '{expectedFileName}' should be downloaded.");
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail($"File '{expectedFileName}' was not downloaded within 30 seconds.");
            }
        }
    }
}
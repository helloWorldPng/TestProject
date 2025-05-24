using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using System.Threading;

namespace EpamTests
{
    [TestFixture]
    public class EpamSearchTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private string downloadPath;

        [SetUp]
        public void SetUp()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var options = new ChromeOptions();

            if (bool.TryParse(Environment.GetEnvironmentVariable("HEADLESS"), out bool headless) && headless)
            {
                options.AddArgument("--headless=new");
            }

            // Cross-platform flexible download path inside project folder
            downloadPath = Path.Combine(Directory.GetCurrentDirectory(), "Downloads");

            if (!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }

            options.AddUserProfilePreference("download.default_directory", downloadPath);
            options.AddUserProfilePreference("download.prompt_for_download", false);

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            driver.Navigate().GoToUrl("https://www.epam.com");
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
            Console.WriteLine($"Initial page loaded. URL: {driver.Url}");

            var homePage = new EpamHomePage(driver, wait);
            homePage.HandleCookiePopup();
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver.Dispose();
            driver = null;

            if (Directory.Exists(downloadPath))
            {
                try
                {
                    Directory.Delete(downloadPath, true);
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Could not delete download directory: {ex.Message}");
                }
            }
        }

        [TestCase("AI")]
        [TestCase("Machine Learning")]
        public void SearchOnEpam(string searchTerm)
        {
            var homePage = new EpamHomePage(driver, wait);
            var searchPanel = homePage.OpenSearchPanel();
            searchPanel.PerformSearch(searchTerm);
        }

        [TestCase("EPAM_Systems_Company_Overview.pdf")]
        public void ValidateFileDownload(string expectedFileName)
        {
            var homePage = new EpamHomePage(driver, wait);
            var aboutPage = homePage.GoToAboutPage();
            aboutPage.ScrollToEpamAtAGlance();
            aboutPage.ClickDownloadButton();

            string filePath = Path.Combine(downloadPath, expectedFileName);
            try
            {
                wait.Until(_ => File.Exists(filePath));
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail($"File '{expectedFileName}' was not downloaded within 30 seconds.");
            }

            Assert.IsTrue(File.Exists(filePath), $"File '{expectedFileName}' should be downloaded.");
        }

        [Test]
        public void ValidateCarouselArticleTitle()
        {
            var homePage = new EpamHomePage(driver, wait);
            var insightsPage = homePage.GoToInsightsPage();
            insightsPage.SwipeCarouselTwice();
            Thread.Sleep(500);
            string carouselTitle = insightsPage.GetCarouselArticleTitle();
            var articlePage = insightsPage.ClickReadMore();
            string articleTitle = articlePage.GetArticleTitle();

            Assert.AreEqual(carouselTitle, articleTitle, "Article title should match the carousel title.");
        }
    }
}
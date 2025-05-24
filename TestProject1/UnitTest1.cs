using EpamTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace EpamTests.Tests
{
    public class EpamSearchTests
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private string _downloadPath;

        [SetUp]
        public void Setup()
        {
            _downloadPath = Path.Combine(Directory.GetCurrentDirectory(), "Downloads");
            if (!Directory.Exists(_downloadPath))
            {
                Directory.CreateDirectory(_downloadPath);
            }

            var options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", _downloadPath);
            options.AddUserProfilePreference("download.prompt_for_download", false);

            _driver = new ChromeDriver(options);
            _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60)); // Increased timeout
        }

        [Test]
        public void ValidateFileDownload([Values("EPAM_Systems_Company_Overview.pdf")] string expectedFileName)
        {
            Console.WriteLine("Starting file download test...");
            var homePage = new EpamHomePage(_driver, _wait);
            _driver.Navigate().GoToUrl("https://www.epam.com");
            _wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
            Console.WriteLine("Initial page loaded. URL: " + _driver.Url);
            homePage.HandleCookiePopup();

            var aboutPage = homePage.GoToAboutPage();
            aboutPage.ScrollToEpamAtAGlance();
            Console.WriteLine("Clicking download button...");
            aboutPage.ClickDownloadButton();

            string filePath = Path.Combine(_downloadPath, expectedFileName);
            try
            {
                Console.WriteLine($"Waiting for file: {filePath}");
                _wait.Until(_ => File.Exists(filePath));
                Assert.IsTrue(File.Exists(filePath), $"File '{expectedFileName}' should be downloaded.");
                Console.WriteLine($"File downloaded successfully: {filePath}");
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"File '{expectedFileName}' was not downloaded within 60 seconds.");
                Assert.Fail($"File '{expectedFileName}' was not downloaded within 60 seconds.");
            }
        }

        [TearDown]
        public void TearDown()
        {
            _driver?.Quit();
            if (Directory.Exists(_downloadPath))
            {
                try
                {
                    Directory.Delete(_downloadPath, true);
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Could not delete download directory: {ex.Message}");
                }
            }
        }
    }
}
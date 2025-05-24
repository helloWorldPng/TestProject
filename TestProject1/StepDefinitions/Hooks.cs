using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using TechTalk.SpecFlow;
using EpamTests.Utilities;

namespace EpamTests.StepDefinitions
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private string _downloadPath;

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            bool headless = bool.TryParse(Environment.GetEnvironmentVariable("HEADLESS"), out bool result) && result;
            _driver = WebDriverFactory.CreateDriver(BrowserType.Chrome, headless);

            _downloadPath = Path.Combine(Directory.GetCurrentDirectory(), "Downloads");
            if (!Directory.Exists(_downloadPath))
            {
                Directory.CreateDirectory(_downloadPath);
            }

            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));

            _objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
            _objectContainer.RegisterInstanceAs<WebDriverWait>(_wait);
            _objectContainer.RegisterInstanceAs<string>(_downloadPath);
        }

        [AfterScenario]
        public void AfterScenario()
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
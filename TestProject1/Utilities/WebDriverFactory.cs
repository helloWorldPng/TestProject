using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace EpamTests.Utilities
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateDriver(BrowserType browserType, bool headless = false)
        {
            return browserType switch
            {
                BrowserType.Chrome => CreateChromeDriver(headless),
                BrowserType.Firefox => CreateFirefoxDriver(headless),
                _ => throw new ArgumentOutOfRangeException(nameof(browserType), $"Unsupported browser type: {browserType}")
            };
        }

        private static IWebDriver CreateChromeDriver(bool headless)
        {
            var options = new ChromeOptions();
            if (headless) options.AddArgument("--headless=new");
            options.AddUserProfilePreference("download.default_directory",
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads");
            options.AddUserProfilePreference("download.prompt_for_download", false);
            return new ChromeDriver(options);
        }

        private static IWebDriver CreateFirefoxDriver(bool headless)
        {
            var options = new FirefoxOptions();
            if (headless) options.AddArgument("--headless");
            return new FirefoxDriver(options);
        }
    }

    public enum BrowserType
    {
        Chrome,
        Firefox
    }
}
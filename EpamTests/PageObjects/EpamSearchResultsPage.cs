using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EpamTests.PageObjects
{
    public class EpamSearchResultsPage : BasePage
    {
        private readonly By _resultsContainerLocator = By.CssSelector("[class*='search-results__items']");

        public EpamSearchResultsPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        private IWebElement ResultsContainer
        {
            get
            {
                try
                {
                    Console.WriteLine("Waiting for search results container...");
                    return wait.Until(ExpectedConditions.ElementIsVisible(_resultsContainerLocator));
                }
                catch (WebDriverTimeoutException ex)
                {
                    Console.WriteLine($"Failed to find search results container: {ex.Message}");
                    throw;
                }
            }
        }

        public bool AreResultsVisible()
        {
            try
            {
                Console.WriteLine($"Checking if search results are visible. Current URL: {driver.Url}");
                bool isVisible = ResultsContainer.Displayed && ResultsContainer.FindElements(By.CssSelector("*")).Count > 0;
                Console.WriteLine($"Search results visible: {isVisible}");
                return isVisible;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking search results visibility: {ex.Message}");
                throw;
            }
        }
    }
}
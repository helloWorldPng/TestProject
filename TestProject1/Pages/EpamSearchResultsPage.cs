using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

public class EpamSearchResultsPage : BasePage
{
    public EpamSearchResultsPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

    // Updated selector for results container
    private IWebElement ResultsContainer => wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.search-results, [class*='search-results']")));

    public bool AreResultsVisible()
    {
        Console.WriteLine($"Checking if search results are visible. Current URL: {driver.Url}");
        return ResultsContainer.Displayed;
    }
}
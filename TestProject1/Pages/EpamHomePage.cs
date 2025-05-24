using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class EpamHomePage : BasePage
{
    public EpamHomePage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

    private IWebElement SearchIcon => driver.FindElement(By.CssSelector(".header-search__button"));
    private IWebElement AboutMenu => wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("About")));
    private IWebElement InsightsMenu => wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Insights")));

    public EpamSearchPanel OpenSearchPanel()
        {
            Console.WriteLine("Attempting to click search icon...");
            SearchIcon.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.header-search__panel")));
            Console.WriteLine($"Search panel opened. Current URL: {driver.Url}");
            return new EpamSearchPanel(driver, wait);
        }

    public EpamAboutPage GoToAboutPage()
    {
        Console.WriteLine("Attempting to click About menu...");
        AboutMenu.Click();
        wait.Until(ExpectedConditions.UrlContains("/about"));
        Console.WriteLine($"Navigated to About page. Current URL: {driver.Url}");
        return new EpamAboutPage(driver, wait);
    }

    public EpamInsightsPage GoToInsightsPage()
    {
        Console.WriteLine("Attempting to click Insights menu...");
        InsightsMenu.Click();
        wait.Until(ExpectedConditions.UrlContains("/insights"));
        Console.WriteLine($"Navigated to Insights page. Current URL: {driver.Url}");
        return new EpamInsightsPage(driver, wait);
    }
}
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

public class EpamSearchPanel : BasePage
{
    public EpamSearchPanel(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }


    private IWebElement SearchInput => driver.FindElement(By.CssSelector("#new_form_search"));
    private IWebElement SearchButton => driver.FindElement(By.CssSelector(".custom-search-button"));

    public void EnterSearchText(string searchTerm)
    {
        SearchInput.Clear();
        SearchInput.SendKeys(searchTerm);
        Console.WriteLine($"Entered search term: {searchTerm}");
    }

    public void PerformSearch(string query)
    {
        SearchInput.SendKeys(query);
        SearchButton.Click();
    }
}
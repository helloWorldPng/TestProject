using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace EpamTests.PageObjects
{
    public class EpamSearchPanel : BasePage
    {
        public EpamSearchPanel(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        private IWebElement SearchInput => wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#new_form_search")));
        private IWebElement SearchButton => wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".custom-search-button")));

        public void PerformSearch(string searchTerm)
        {
            Console.WriteLine($"Entered search term: {searchTerm}");
            SearchInput.Clear();
            SearchInput.SendKeys(searchTerm);
            SearchButton.Click();
        }
    }
}
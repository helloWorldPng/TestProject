using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EpamTests.PageObjects
{
    public class EpamArticlePage : BasePage
    {
        public EpamArticlePage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        private IWebElement CarouselArticleTitleElement => wait.Until(ExpectedConditions.ElementIsVisible(
            By.CssSelector("span.font-size-80-33 > span.museo-sans-light")));

        public string GetArticleTitle()
        {
            string title = CarouselArticleTitleElement.Text.Trim();
            Console.WriteLine($"Article page title: {title}");
            return title;
        }
    }
}
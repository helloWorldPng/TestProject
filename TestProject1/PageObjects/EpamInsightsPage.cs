using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EpamTests.PageObjects
{
    public class EpamInsightsPage : BasePage
    {
        private readonly By _carouselNextButtonLocator = By.CssSelector(".slider__right-arrow");
        private readonly By _currentPageIndicatorLocator = By.CssSelector(".slider__pagination--current-page");
        private readonly By _carouselArticleTitleLocator = By.CssSelector(".font-size-60");

        public EpamInsightsPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        private IWebElement CarouselNextButton => driver.FindElement(_carouselNextButtonLocator);
        private IWebElement CurrentPageIndicator => driver.FindElement(_currentPageIndicatorLocator);
        private IWebElement CarouselArticleTitle => driver.FindElement(_carouselArticleTitleLocator);

        public void SwipeCarouselTwice()
        {
            for (int i = 0; i < 2; i++)
            {
                string currentPageText = wait.Until(ExpectedConditions.ElementIsVisible(_currentPageIndicatorLocator)).Text;
                int currentPageNum = int.Parse(currentPageText);
                wait.Until(ExpectedConditions.ElementToBeClickable(_carouselNextButtonLocator)).Click();
                wait.Until(d => int.Parse(d.FindElement(_currentPageIndicatorLocator).Text) > currentPageNum);
            }
        }

        public string GetCarouselArticleTitle()
        {
            try
            {
                wait.Until(driver => driver.FindElements(_carouselArticleTitleLocator).Count > 0);
                var titleElements = driver.FindElements(_carouselArticleTitleLocator);
                foreach (var el in titleElements)
                {
                    Console.WriteLine("Found title candidate: " + el.Text);
                    if (!string.IsNullOrWhiteSpace(el.Text) && el.Displayed)
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", el);
                        string title = el.Text.Trim();
                        Console.WriteLine($"Carousel article title: {title}");
                        return title;
                    }
                }
                throw new NoSuchElementException("No visible carousel title found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to retrieve carousel article title: " + ex.Message);
                throw;
            }
        }

        public EpamArticlePage ClickReadMore()
        {
            try
            {
                Console.WriteLine("Clicking read more button...");
                wait.Until(driver => driver.FindElements(By.CssSelector(".slider-cta-link")).Count > 0);
                var readMoreLinks = driver.FindElements(By.CssSelector(".slider-cta-link"));
                foreach (var link in readMoreLinks)
                {
                    if (link.Displayed)
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", link);
                        wait.Until(driver => link.Enabled && link.Displayed);
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", link);
                        return new EpamArticlePage(driver, wait);
                    }
                }
                throw new NoSuchElementException("No visible 'Read More' link found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Could not find or click the Read More link: " + ex.Message);
                throw;
            }
        }
    }
}
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

public class EpamInsightsPage : BasePage
{
    public EpamInsightsPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

    private By ReadMoreLinkSelector => By.CssSelector(".single-slide__cta-container a.custom-link");
    private IWebElement CarouselNextButton => driver.FindElement(By.CssSelector(".slider__right-arrow"));
    private IWebElement CurrentPageIndicator => driver.FindElement(By.CssSelector(".slider__pagination--current-page"));
    private IWebElement CarouselArticleTitle => driver.FindElement(By.CssSelector(".font-size-60"));



    public void SwipeCarouselTwice()
    {
        for (int i = 0; i < 2; i++)
        {
            // Get the current page number before clicking
            string currentPageText = CurrentPageIndicator.Text;
            int currentPageNum = int.Parse(currentPageText);

            // Click the right arrow button
            CarouselNextButton.Click();

            // Wait for the page number to increment, confirming the slide has changed
            wait.Until(d => int.Parse(d.FindElement(By.CssSelector(".slider__pagination--current-page")).Text) > currentPageNum);
        }
    }

   public string GetCarouselArticleTitle()
   {
       try
       {
           // Find all candidates
           wait.Until(driver => driver.FindElements(By.CssSelector(".font-size-60")).Count > 0);

           var titleElements = driver.FindElements(By.CssSelector(".font-size-60"));
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

                   // Instead of regular click (which fails), use JavaScript click:
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
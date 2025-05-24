using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

public class EpamArticlePage : BasePage
{
    public EpamArticlePage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

    // Updated selector for article title
    private IWebElement CarouselArticleTitleElement => wait.Until(ExpectedConditions.ElementIsVisible(
        By.CssSelector("span.font-size-80-33 > span.museo-sans-light")
    ));

    public string GetArticleTitle()
    {
        string title = CarouselArticleTitleElement.Text.Trim();
        Console.WriteLine($"Carousel article title: {title}");
        return title;
    }
}
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using System;

public class EpamAboutPage : BasePage
{
    public EpamAboutPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

    private IWebElement DownloadButton => wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a.download-link, [href*='.pdf']")));
    private IWebElement EpamAtAGlanceSection => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='colctrl__holder' and .//span[contains(text(), 'EPAM at')]]")));

    public void ScrollToEpamAtAGlance()
    {
        new Actions(driver).ScrollToElement(EpamAtAGlanceSection).Perform();
    }

    public void ClickDownloadButton()
    {
        Console.WriteLine("Clicking download button...");
        ScrollToEpamAtAGlance();
        DownloadButton.Click();
    }
}
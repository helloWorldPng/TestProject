using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EpamTests.PageObjects
{
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
            wait.Until(ExpectedConditions.ElementToBeClickable(DownloadButton)).Click();
        }
    }
}
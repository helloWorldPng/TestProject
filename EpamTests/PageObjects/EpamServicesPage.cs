using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EpamTests.PageObjects
{
    public class EpamServicesPage : BasePage
    {
        public EpamServicesPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        private IWebElement OurRelatedExpertiseSection => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Our Related Expertise')]")));
        private IWebElement PageTitle => wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1")));

        public bool IsOurRelatedExpertiseSectionDisplayed()
        {
            return OurRelatedExpertiseSection.Displayed;
        }

        public string GetPageTitle()
        {
            return PageTitle.Text.Trim();
        }
    }
}
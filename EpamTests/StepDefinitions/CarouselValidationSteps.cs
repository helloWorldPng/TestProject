using EpamTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace EpamTests.StepDefinitions
{
    [Binding]
    public class CarouselValidationSteps
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private EpamHomePage _homePage;
        private EpamInsightsPage _insightsPage;
        private EpamArticlePage _articlePage;
        private string _carouselTitle;

        public CarouselValidationSteps(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            _homePage = new EpamHomePage(_driver, _wait);
        }

        [When(@"I navigate to the Insights page")]
        public void WhenINavigateToTheInsightsPage()
        {
            _insightsPage = _homePage.GoToInsightsPage();
        }

        [When(@"I swipe the carousel twice")]
        public void WhenISwipeTheCarouselTwice()
        {
            _insightsPage.SwipeCarouselTwice();
            System.Threading.Thread.Sleep(500); // Maintain original delay for stability
        }

        [When(@"I retrieve the carousel article title")]
        public void WhenIRetrieveTheCarouselArticleTitle()
        {
            _carouselTitle = _insightsPage.GetCarouselArticleTitle();
        }

        [When(@"I click the Read More link")]
        public void WhenIClickTheReadMoreLink()
        {
            _articlePage = _insightsPage.ClickReadMore();
        }

        [Then(@"the article page title should match the carousel title")]
        public void ThenTheArticlePageTitleShouldMatchTheCarouselTitle()
        {
            string articleTitle = _articlePage.GetArticleTitle();
            Assert.AreEqual(_carouselTitle, articleTitle, "Article title should match the carousel title.");
        }
    }
}
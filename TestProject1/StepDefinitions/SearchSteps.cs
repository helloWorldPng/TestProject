using EpamTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace EpamTests.StepDefinitions
{
    [Binding]
    public class SearchSteps
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private EpamHomePage _homePage;
        private EpamSearchPanel _searchPanel;
        private EpamSearchResultsPage _searchResultsPage;

        public SearchSteps(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            _homePage = new EpamHomePage(_driver, _wait);
        }

        [When(@"I open the search panel")]
        public void WhenIOpenTheSearchPanel()
        {
            _searchPanel = _homePage.OpenSearchPanel();
        }

        [When(@"I search for ""(.*)""")]
        public void WhenISearchFor(string searchTerm)
        {
            _searchPanel.PerformSearch(searchTerm);
            _searchResultsPage = new EpamSearchResultsPage(_driver, _wait);
        }

        [Then(@"I should see search results")]
        public void ThenIShouldSeeSearchResults()
        {
            Assert.IsTrue(_searchResultsPage.AreResultsVisible(), "Search results should be visible.");
        }
    }
}
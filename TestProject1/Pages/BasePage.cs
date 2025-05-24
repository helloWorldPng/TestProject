using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public abstract class BasePage
{
    protected IWebDriver driver;
    protected WebDriverWait wait;

    public BasePage(IWebDriver driver, WebDriverWait wait)
    {
        this.driver = driver;
        this.wait = wait;
    }

    public void HandleCookiePopup()
    {
        try
        {
            // Wait for the cookie accept button (adjust selector if needed)
            var cookieAcceptButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#onetrust-accept-btn-handler")));
            Console.WriteLine($"Cookie button found. IsDisplayed: {cookieAcceptButton.Displayed}, IsEnabled: {cookieAcceptButton.Enabled}");

            // Use JavaScript click to avoid interception
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", cookieAcceptButton);
            Console.WriteLine("Cookie popup clicked via JavaScript.");

            // Wait for the cookie banner to disappear
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("#onetrust-banner-sdk")));
            Console.WriteLine("Cookie popup closed successfully.");
        }
        catch (NoSuchElementException ex)
        {
            Console.WriteLine($"No cookie popup found: {ex.Message}");
        }
        catch (WebDriverTimeoutException ex)
        {
            Console.WriteLine($"Timed out waiting for cookie popup: {ex.Message}");
        }
        catch (ElementClickInterceptedException ex)
        {
            Console.WriteLine($"Cookie popup click intercepted: {ex.Message}");
            // Log page source for debugging
            Console.WriteLine($"Page source: {driver.PageSource.Substring(0, Math.Min(driver.PageSource.Length, 1000))}...");
            throw; // Re-throw to fail the test and inspect the issue
        }
    }
}
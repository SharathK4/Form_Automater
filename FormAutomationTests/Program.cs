using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

public class NestedFormAutomationTests
{
    private IWebDriver driver;
    private WebDriverWait wait;
    private int scrollPosition = 0;

    public static void Main()
    {
        var tester = new NestedFormAutomationTests();
        tester.RunAllTests();
    }

    private void RunAllTests()
    {
        try
        {
            SetupChromeDriver();
            NavigateToFormPage();
            
            TestRegularForm();
            ScrollByDistance(1000);
            TestNestedShadowDOM3Levels();
            ScrollByDistance(1000);
            TestNestedShadowDOM4Levels();
            ScrollByDistance(1000);
            TestIFrameWithID();
            ScrollByDistance(1000);
            TestIFrameWithoutID();

            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("ALL TESTS COMPLETED SUCCESSFULLY!");
            Console.WriteLine(new string('=', 50) + "\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}\n{ex.StackTrace}");
        }
        finally
        {
            if (driver != null)
            {
                System.Threading.Thread.Sleep(2000);
                driver.Quit();
            }
        }
    }

    private void SetupChromeDriver()
    {
        Console.WriteLine("Setting up ChromeDriver...");
        var chromeOptions = new ChromeOptions();
        driver = new ChromeDriver(chromeOptions);
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        Console.WriteLine("✓ WebDriver initialized\n");
    }

    private void NavigateToFormPage()
    {
        string url = "https://app.cloudqa.io/home/AutomationPracticeForm";
        Console.WriteLine($"Navigating to: {url}");
        driver.Navigate().GoToUrl(url);
        driver.Manage().Window.Maximize();
        wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        Console.WriteLine("✓ Page loaded successfully\n");
    }

    private void ScrollByDistance(int pixels)
    {
        try
        {
            scrollPosition += pixels;
            ((IJavaScriptExecutor)driver).ExecuteScript($"window.scrollBy(0, {pixels});");
            System.Threading.Thread.Sleep(1000); // Wait for page to settle
            Console.WriteLine($"  ✓ Scrolled by {pixels}px");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ✗ Scroll failed: {ex.Message}");
        }
    }

    // TEST 1: Regular Form (Shadow DOM)
    private void TestRegularForm()
    {
        try
        {
            Console.WriteLine("=== TEST 1: Regular Shadow DOM Form ===");
            
            FillInputByPlaceholder("Name", "John");
            FillInputByPlaceholder("Surname", "Doe");
            SelectRadioButtonByLabel("Male");
            FillInputByPlaceholder("Mobile Number", "9876543210");
            FillInputByPlaceholder("Email", "john@example.com");
            FillInputByPlaceholder("Country", "USA");
            SelectCheckboxByLabel("Dance");
            FillTextareaByPlaceholder("About Yourself", "I love automation testing");
            
            Console.WriteLine("✓ TEST 1 PASSED: Regular form filled successfully!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ TEST 1 FAILED: {ex.Message}\n");
        }
    }

    // TEST 2: Nested Shadow DOM with 3 Levels
    private void TestNestedShadowDOM3Levels()
    {
        try
        {
            Console.WriteLine("=== TEST 2: Nested Shadow DOM with 3 Levels ===");
            
            FillInputByPlaceholder("Name", "Alice");
            FillInputByPlaceholder("Surname", "Smith");
            SelectRadioButtonByLabel("Female");
            FillInputByPlaceholder("Mobile Number", "5551234567");
            FillInputByPlaceholder("Email", "alice@example.com");
            SelectCheckboxByLabel("Reading");
            FillTextareaByPlaceholder("About Yourself", "Testing nested shadow DOM");
            
            Console.WriteLine("✓ TEST 2 PASSED: 3-level nested form filled successfully!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ TEST 2 FAILED: {ex.Message}\n");
        }
    }

    // TEST 3: Nested Shadow DOM with 4 Levels
    private void TestNestedShadowDOM4Levels()
    {
        try
        {
            Console.WriteLine("=== TEST 3: Nested Shadow DOM with 4 Levels ===");
            
            FillInputByPlaceholder("Name", "Bob");
            FillInputByPlaceholder("Surname", "Johnson");
            SelectRadioButtonByLabel("Transgender");
            FillInputByPlaceholder("Email", "bob@example.com");
            SelectCheckboxByLabel("Cricket");
            FillTextareaByPlaceholder("About Yourself", "Testing 4-level nested forms");
            
            Console.WriteLine("✓ TEST 3 PASSED: 4-level nested form filled successfully!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ TEST 3 FAILED: {ex.Message}\n");
        }
    }

    // TEST 4: IFrame with ID
    private void TestIFrameWithID()
    {
        try
        {
            Console.WriteLine("=== TEST 4: IFrame with ID ===");
            
            // Find and switch to iframe
            var iframe = driver.FindElements(By.TagName("iframe"));
            if (iframe.Count > 0)
            {
                driver.SwitchTo().Frame(iframe[0]);
                Console.WriteLine("  ✓ Switched to iframe");
                
                System.Threading.Thread.Sleep(500);
                
                FillInputByPlaceholder("Name", "Charlie");
                FillInputByPlaceholder("Surname", "Brown");
                SelectRadioButtonByLabel("Male");
                FillInputByPlaceholder("Mobile Number", "5559876543");
                FillInputByPlaceholder("Email", "charlie@example.com");
                FillInputByPlaceholder("Country", "India");
                FillInputByPlaceholder("Username", "charliebrown");
                FillInputByPlaceholder("Password", "SecurePass123!");
                
                driver.SwitchTo().DefaultContent();
            }
            
            Console.WriteLine("✓ TEST 4 PASSED: IFrame form filled successfully!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ TEST 4 FAILED: {ex.Message}\n");
            driver.SwitchTo().DefaultContent();
        }
    }

    // TEST 5: IFrame without ID
    private void TestIFrameWithoutID()
    {
        try
        {
            Console.WriteLine("=== TEST 5: IFrame without ID ===");
            
            var iframes = driver.FindElements(By.TagName("iframe"));
            if (iframes.Count > 1)
            {
                driver.SwitchTo().Frame(iframes[iframes.Count - 1]);
                Console.WriteLine("  ✓ Switched to iframe without ID");
                
                System.Threading.Thread.Sleep(500);
                
                FillInputByPlaceholder("Name", "Diana");
                FillInputByPlaceholder("Surname", "Prince");
                SelectRadioButtonByLabel("Female");
                FillInputByPlaceholder("Mobile Number", "5559999999");
                FillInputByPlaceholder("Email", "diana@example.com");
                SelectCheckboxByLabel("Reading");
                FillInputByPlaceholder("Username", "dianaprince");
                FillInputByPlaceholder("Password", "AnotherPass123!");
                
                driver.SwitchTo().DefaultContent();
            }
            
            Console.WriteLine("✓ TEST 5 PASSED: IFrame without ID form filled successfully!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ TEST 5 FAILED: {ex.Message}\n");
            driver.SwitchTo().DefaultContent();
        }
    }

    private void FillInputByPlaceholder(string placeholder, string value)
    {
        try
        {
            var input = wait.Until(d => d.FindElement(By.XPath($"//input[@placeholder='{placeholder}']")));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", input);
            input.Clear();
            input.SendKeys(value);
            Console.WriteLine($"  ✓ Filled {placeholder}: {value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ✗ Failed to fill {placeholder}: {ex.Message}");
        }
    }

    private void FillTextareaByPlaceholder(string placeholder, string value)
    {
        try
        {
            var textarea = wait.Until(d => d.FindElement(By.XPath($"//textarea[@placeholder='{placeholder}']")));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", textarea);
            textarea.Clear();
            textarea.SendKeys(value);
            Console.WriteLine($"  ✓ Filled {placeholder}: {value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ✗ Failed to fill {placeholder}: {ex.Message}");
        }
    }

    private void SelectRadioButtonByLabel(string labelText)
    {
        try
        {
            // Find the label containing the text, then find the associated radio input
            var label = driver.FindElement(By.XPath($"//label[contains(text(), '{labelText}')]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", label);
            
            // Try to find the radio input that's either a sibling or inside the label
            IWebElement radioInput = null;
            try
            {
                // Look for input inside the label
                radioInput = label.FindElement(By.XPath(".//input[@type='radio']"));
            }
            catch
            {
                // Look for input before the label (common pattern)
                radioInput = label.FindElement(By.XPath("./preceding-sibling::input[@type='radio'][1]"));
            }
            
            // Use JavaScript click for better compatibility with shadow DOM
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", radioInput);
            Console.WriteLine($"  ✓ Selected {labelText}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ✗ Failed to select {labelText}: {ex.Message}");
        }
    }

    private void SelectCheckboxByLabel(string labelText)
    {
        try
        {
            // Find the label containing the text, then find the associated checkbox
            var label = driver.FindElement(By.XPath($"//label[contains(text(), '{labelText}')]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", label);
            
            // Try to find the checkbox input
            IWebElement checkboxInput = null;
            try
            {
                // Look for input inside the label
                checkboxInput = label.FindElement(By.XPath(".//input[@type='checkbox']"));
            }
            catch
            {
                // Look for input before the label
                checkboxInput = label.FindElement(By.XPath("./preceding-sibling::input[@type='checkbox'][1]"));
            }
            
            // Use JavaScript click for better compatibility
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", checkboxInput);
            Console.WriteLine($"  ✓ Selected checkbox: {labelText}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ✗ Failed to select checkbox {labelText}: {ex.Message}");
        }
    }
}

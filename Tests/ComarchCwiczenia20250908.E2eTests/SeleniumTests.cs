using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ComarchCwiczenia20250908.E2eTests;

public class SeleniumTests
{
    private ChromeDriver driver;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        new WebDriverManager.DriverManager().SetUpDriver(new WebDriverManager.DriverConfigs.Impl.ChromeConfig());
        driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        driver.Dispose();
    }

    [Test]
    public void CorrectLoginTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");

        var userNameField = driver.FindElement(By.Id("username"));
        var passwordField = driver.FindElement(By.Id("password"));

        var submitButton = driver.FindElement(By.XPath("//*[@id=\"login\"]/button"));
        var submitButton2 = driver.FindElement(By.CssSelector("#login"));
        var submitButton3 = driver.FindElement(By.CssSelector("button[type='submit']"));
        var submitButton4 = driver.FindElement(By.CssSelector(".fa.fa-2x.fa-sign-in"));
        
        userNameField.SendKeys("tomsmith");
        passwordField.SendKeys("SuperSecretPassword!");

        submitButton.Click();

        var successMessage = driver.FindElement(By.Id("flash"));
        Assert.That(successMessage.Text, Does.Contain("You logged into a secure area!"));
    }

    [Test]
    public void DropDownTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dropdown");

        var dropdown = driver.FindElement(By.Id("dropdown"));
        var selectElement = new SelectElement(dropdown);

        selectElement.SelectByValue("1");
        Assert.That(selectElement.SelectedOption.Text, Is.EqualTo("Option 1"));

        selectElement.SelectByText("Option 2");
        Assert.That(selectElement.SelectedOption.Selected, Is.True);
    }

    [Test]
    public void HandleJavaScriptAlerts()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

        var alertButton = driver.FindElement(By.XPath("//button[@onclick='jsAlert()']"));
        alertButton.Click();

        var alert = driver.SwitchTo().Alert();

        Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"));
        alert.Accept();

        // Sprawdzamy, czy wyświetlił się komunikat o akceptacji
        var resultText = driver.FindElement(By.Id("result"));
        Assert.That(resultText.Text, Is.EqualTo("You successfully clicked an alert"), "Komunikat po zaakceptowaniu alertu jest nieprawidłowy!");

        // Test dla JS Confirm
        var confirmButton = driver.FindElement(By.XPath("//button[text()='Click for JS Confirm']"));
        confirmButton.Click();

        // Przechwytujemy alert confirm
        alert = driver.SwitchTo().Alert();
        Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Tekst confirm jest nieprawidłowy!");

        // Odrzucamy confirm
        alert.Dismiss();

        // Sprawdzamy, czy wyświetlił się komunikat o odrzuceniu
        resultText = driver.FindElement(By.Id("result"));
        Assert.That(resultText.Text, Is.EqualTo("You clicked: Cancel"), "Komunikat po odrzuceniu confirm jest nieprawidłowy!");

        // Test dla JS Prompt
        var promptButton = driver.FindElement(By.XPath("//button[text()='Click for JS Prompt']"));
        promptButton.Click();

        // Przechwytujemy prompt
        alert = driver.SwitchTo().Alert();
        Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "Tekst prompt jest nieprawidłowy!");

        // Wprowadzamy tekst do prompta
        alert.SendKeys("Test Selenium");

        // Akceptujemy prompt
        alert.Accept();

        // Sprawdzamy, czy wyświetlił się komunikat z wpisanym tekstem
        resultText = driver.FindElement(By.Id("result"));
        Assert.That(resultText.Text, Is.EqualTo("You entered: Test Selenium"), "Komunikat po akceptacji prompta jest nieprawidłowy!");
    }
}

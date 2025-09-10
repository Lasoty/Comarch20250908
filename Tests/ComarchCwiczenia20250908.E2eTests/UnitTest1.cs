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
    }
}

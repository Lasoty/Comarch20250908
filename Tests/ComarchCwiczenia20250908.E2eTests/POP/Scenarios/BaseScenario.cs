using OpenQA.Selenium.Chrome;

namespace ComarchCwiczenia20250908.E2eTests.POP.Scenarios;

[TestFixture]
public abstract class BaseScenario
{
    protected ChromeDriver driver;

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        options.AddArgument("headless"); //uruchamianie bez okienka
        options.AddArgument("--disable-gpu");

        new WebDriverManager.DriverManager().SetUpDriver(new WebDriverManager.DriverConfigs.Impl.ChromeConfig());
        driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    [TearDown]
    public void TearDown()
    {
        driver.Close();
        driver.Dispose();
    }
}

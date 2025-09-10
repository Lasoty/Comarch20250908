using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComarchCwiczenia20250908.E2eTests.POP.PageObjects;

namespace ComarchCwiczenia20250908.E2eTests.POP.Scenarios;

public class LoginScenarioTests : BaseScenario
{
    [Test]
    public void LoginWithInvalidCredentials_ShowsErrorMessage()
    {
        LoginPage loginPage = LoginPage.OpenPage(driver);
        loginPage.EnterUserNameAndPassword("invalidUser", "invalidPass");
        loginPage.ClickLogin();

        Assert.That(loginPage.IsErrorDisplayed(), Is.True);
    }
}

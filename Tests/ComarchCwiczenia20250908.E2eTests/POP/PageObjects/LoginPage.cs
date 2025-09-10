using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace ComarchCwiczenia20250908.E2eTests.POP.PageObjects;

public class LoginPage
{
    private readonly IWebDriver driver;

    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    public IWebElement UserNameField => driver.FindElement(By.Id("username"));
    public IWebElement PasswordField => driver.FindElement(By.Id("password"));
    public IWebElement LoginButton => driver.FindElement(By.XPath("//*[@id=\"login\"]/button"));
    public IWebElement ErrorMessage => driver.FindElement(By.Id("flash"));

    public static LoginPage OpenPage(IWebDriver driver)
    {
        LoginPage page = new LoginPage(driver);
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");
        return page;
    }

    public void EnterUserNameAndPassword(string userName, string password)
    {
        UserNameField.SendKeys(userName);
        PasswordField.SendKeys(password);
    }

    public void ClickLogin()
    {
        LoginButton.Click();
    }

    public bool IsErrorDisplayed()
    {
        return ErrorMessage.Displayed && ErrorMessage.Text.Contains("is invalid");

    }
    
}

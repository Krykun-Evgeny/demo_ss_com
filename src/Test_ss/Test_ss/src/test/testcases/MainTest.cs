using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Test_ss.src.test.utilities;
using Test_ss.src.test.constants;

namespace Test_ss.src.test.testcases
{
    public abstract class MainTest : IDisposable
    {
        protected IWebDriver webDriver;
        protected Actions Actions { get; set; }
        protected WebDriverWait Wait { get; set; }
        protected GlobalMethods GlobaMethods { set; get; }
        protected string URL = Constants.init_url;

        protected virtual void SetUp(BrowserType browserType)
        {
            webDriver = WebDriversFactory.Create_Browser(browserType);
            Wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(Constants.timeout));
            Actions = new Actions(webDriver);
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl(URL);
            GlobaMethods = new GlobalMethods(webDriver);

        }

        public void Dispose()
        {
            webDriver.Quit();
        }
    }
}

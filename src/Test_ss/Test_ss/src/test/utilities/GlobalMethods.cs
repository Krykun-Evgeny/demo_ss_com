using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test_ss.src.test.utilities
{
    public class GlobalMethods
    {
        WebDriverWait _wait;
        IWebDriver webDriver;
        public GlobalMethods(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            _wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(constants.Constants.timeout));
        }

        public void ScrollToWebElement(IWebElement element)
        {
            _wait.Until(webDriver => { ((IJavaScriptExecutor)webDriver).ExecuteScript("window.scrollBy(0,500)"); return element.Displayed && element.Enabled; });
        }
    }
}

using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using Xunit;
using System;
using System.Collections.Generic;
using Test_ss.src.test.utilities;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using Test_ss.src.test.constants;

namespace Test_ss.src.test.pages
{
    public class CategoryPage: BasePage
    {

        [FindsBy(How = How.Id, Using = Constants.filter_frm_id)]
        public IWebElement filterFrm { get; set; }

        [FindsBy(How = How.Id, Using = Constants.filter_tbl_id)]
        public IWebElement filterTbl { get; set; }

        public CategoryPage(IWebDriver driver) : base(driver)
        {
            _webDriver = driver;
            PageFactory.InitElements(driver, this);
        }

        public List<IWebElement> GetCategoryLinks()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(Constants.timeout));
            List<IWebElement> subLinks = wait.Until(e => e.FindElements(By.ClassName(Constants.a_category_class)).ToList());
            return subLinks;
        }
    }
}

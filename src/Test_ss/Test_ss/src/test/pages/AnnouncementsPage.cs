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
    class AnnouncementsPage: BasePage
    {

        [FindsBy(How = How.Id, Using = Constants.filter_frm_id)]
        public IWebElement filterFrm { get; set; }

        [FindsBy(How = How.Id, Using = Constants.filter_tbl_id)]
        public IWebElement filterTbl { get; set; }

        [FindsBy(How = How.Id, Using = Constants.head_line_id)]
        public IWebElement headLine { get; set; }

        [FindsBy(How = How.ClassName, Using = Constants.td2_class)]
        public IWebElement pagination { get; set; }

        public AnnouncementsPage(IWebDriver driver) : base(driver)
        {
            _webDriver = driver;
            PageFactory.InitElements(driver, this);
        }

        public List<IWebElement> GetItemLinksFromFirstPage()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(Constants.timeout));
            List<IWebElement> subLinks = wait.Until(e => e.FindElements(By.ClassName(Constants.am_class)).ToList());
            return subLinks;
        }

        public override BasePage ClickFoundCategory(List<IWebElement> links, string substring = Constants.substringForSearching)
        {
            IWebElement foundCategory = SearchByTextContain(links, substring);
            Assert.NotNull(foundCategory);

            foundCategory.Click();

            return this;
        }

        public AnnouncementsPage ClickFoundCategoryByCheckBox(List<IWebElement> links, string substring = Constants.substringForSearching)
        {
            IWebElement foundCategory = SearchByTextContain(links, substring);
            Assert.NotNull(foundCategory);

            string id_foundCategory = foundCategory.GetAttribute(Constants.id_atr).Replace(Constants.dm_substring_href, "");
            IWebElement checkbox = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(Constants.timeout)).
                Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                    By.Id($"{Constants.c_substring_checkbox}{id_foundCategory}")));
            checkbox.Click();
            Assert.True(checkbox.Selected);
            return this;
        }

        public AnnouncementsPage AddToFavorites(int expected_amount)
        {
            IWebElement a_fav_sel = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(Constants.timeout)).
                Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(Constants.a_fav_sel_id)));
            a_fav_sel.Click();
            ValidateAlertAfterAddingToFavorites(Constants.alert_add_favorites_lv, expected_amount);
            return this;
        }
    }
}

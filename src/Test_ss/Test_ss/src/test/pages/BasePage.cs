using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;
using SeleniumExtras.PageObjects;
using Test_ss.src.test.utilities;
using Test_ss.src.test.constants;

namespace Test_ss.src.test.pages
{
    public abstract class BasePage
    {
        public IWebDriver _webDriver;

        public BasePage(IWebDriver driver)
        {
            _webDriver = driver;
            PageFactory.InitElements(driver, this);
        }

        public IWebElement SearchByText(List<IWebElement> links, string category = Constants.categotyName)
        {
            return links.FirstOrDefault(x => x.Text.Equals(category));
        }

        public IWebElement SearchByTextContain(List<IWebElement> links, string substring = Constants.substringForSearching)
        {
            return links.FirstOrDefault(x => x.Text.Contains(substring));
        }

        public IWebElement GetCategory(IWebElement PageMainFull, int tr_number = Constants.first_row_index, 
            string class_str = Constants.main_img_td2)
        {
            List<IWebElement> list_tr = PageMainFull.FindElements(By.TagName(Constants.tag_tr)).ToList();
            if (tr_number < 0 || tr_number > list_tr.Count - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(tr_number), tr_number, null);
            }
            IWebElement mainImageTd = list_tr[tr_number].FindElement(By.ClassName(class_str));
            return mainImageTd;
        }

        public List<IWebElement> GetCategoryLinks(IWebElement mainImageTd)
        {
            IWebElement mainImageTdCategory = mainImageTd.FindElement(By.ClassName(Constants.main_category));
            List<IWebElement> mainImageTdCategoryLinks = mainImageTdCategory.FindElements(By.TagName(Constants.tag_a)).ToList();
            return mainImageTdCategoryLinks;
        }

        public virtual BasePage ClickFoundCategory(List<IWebElement> links, string category = Constants.categotyName)
        {
            IWebElement foundCategory = SearchByText(links, category);
            Assert.NotNull(foundCategory);

            foundCategory.Click();

            return this;
        }


        public BasePage ValidateAlertAfterAddingToFavorites(string expected_alert_msg = Constants.alert_add_favorites_en,
            int expected_amount = Constants.expected_amount_one_favorite)
        {
            IWebElement alert_dv = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(Constants.timeout)).
                Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(Constants.alert_dv_id)));
            IWebElement alert_msg = alert_dv.FindElement(By.Id(Constants.alert_msg_id));
            IWebElement alert_ok = alert_dv.FindElement(By.Id(Constants.alert_ok_id));
            Assert.Equal(expected_alert_msg, alert_msg.Text);
            alert_ok.Click();
            bool alert_dv_invisible = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(Constants.timeout)).
                Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.Id(Constants.alert_dv_id)));
            IWebElement mnu_fav_id = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(Constants.timeout)).
                Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(Constants.mnu_fav_id)));
            int curr_amount = Int32.Parse(Regex.Replace(mnu_fav_id.Text.Trim(), Constants.regex_brackets, ""));
            Assert.Equal(expected_amount, curr_amount);
            return this;
        }
    }
}

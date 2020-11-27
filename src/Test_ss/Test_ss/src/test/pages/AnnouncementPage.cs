using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using Xunit;
using System;
using System.Collections.Generic;
using Test_ss.src.test.utilities;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Test_ss.src.test.constants;

namespace Test_ss.src.test.pages
{
    class AnnouncementPage: BasePage
    {

        [FindsBy(How = How.ClassName, Using = Constants.top_head_msg_class)]
        public IWebElement topHeadMsg { get; set; }

        [FindsBy(How = How.Id, Using = Constants.content_main_div_id)]
        public IWebElement contentMainDiv { get; set; }

        [FindsBy(How = How.Id, Using = Constants.msg_div_msg_id)]
        public IWebElement msgDivMsg { get; set; }


        public AnnouncementPage(IWebDriver driver) : base(driver)
        {
            _webDriver = driver;
            PageFactory.InitElements(driver, this);
        }

        public AnnouncementPage AddToFavorite()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(Constants.timeout));
            IWebElement addFavorite = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                By.Id(Constants.a_fav_id)));
            Assert.True(addFavorite.Displayed && addFavorite.Enabled);
            addFavorite.Click();
            ValidateAlertAfterAddingToFavorites();
            return this;
        }
    }
}

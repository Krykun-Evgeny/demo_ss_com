using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using Xunit;
using System;
using System.Collections.Generic;
using Test_ss.src.test.utilities;
using System.Linq;
using Test_ss.src.test.constants;

namespace Test_ss.src.test.pages
{
    public class MainPage: BasePage
    {

        GlobalMethods _globaMethods;

        [FindsBy(How = How.Id, Using = Constants.main_table_id)]
        public IWebElement MainTable { get; set; }

        [FindsBy(How = How.Id, Using = Constants.page_footer_id)]
        public IWebElement PageFooter { get; set; }

        [FindsBy(How = How.ClassName, Using = Constants.page_header_menu_class)]
        public IWebElement PageHeaderMenu { get; set; }

        [FindsBy(How = How.ClassName, Using = Constants.page_header_logo_class)]
        public IWebElement PageHeaderLogo { get; set; }

        [FindsBy(How = How.Id, Using = Constants.page_main_full_id)]
        public IWebElement PageMainFull { get; set; }



        public MainPage(IWebDriver driver) : base(driver)
        {
            _webDriver = driver;
            _globaMethods = new GlobalMethods(_webDriver);
            PageFactory.InitElements(driver, this);

        }

        public MainPage ValidateInitContent()
        {
            List<IWebElement> tempList = new List<IWebElement>(){ MainTable, PageFooter, PageHeaderMenu, PageHeaderLogo, PageMainFull };

            for (int i = 0; i < tempList.Count; i++) 
            {
                Assert.True(tempList[i].Displayed);
            }

            return this;
        }

        public MainPage ValidateCategory(IWebElement mainImageTd, 
            List<string> listCategoriesStr
            , string expectedTitle = Constants.Transport_title_en)
        {
            IWebElement mainImageTdImg = mainImageTd.FindElement(By.ClassName(Constants.main_images_class));
            Assert.True(mainImageTdImg.Displayed);

            IWebElement mainImageTdHead = mainImageTd.FindElement(By.ClassName(Constants.main_head_class));
            Assert.Equal(expectedTitle, mainImageTdHead.Text);

           
            List<IWebElement> mainImageTdCategoryLinks = GetCategoryLinks(mainImageTd);
            Assert.Equal(listCategoriesStr.Count, mainImageTdCategoryLinks.Count);
            for (int i = 0; i < mainImageTdCategoryLinks.Count; i++)
            {
                Assert.Equal(listCategoriesStr[i], mainImageTdCategoryLinks[i].Text);
            }
            return this;
        }
    }
}

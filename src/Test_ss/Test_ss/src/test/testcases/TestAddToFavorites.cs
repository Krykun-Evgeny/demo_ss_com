using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Test_ss.src.test.pages;
using Xunit;
using Test_ss.src.test.constants;

namespace Test_ss.src.test.testcases
{
    public class TestAddToFavorites: MainTest
    {

        [Theory]
        [InlineData(BrowserType.Chrome, Constants.categotyName, Constants.subCategotyName)]
        public void AddToFavoritesByOneAnnouncementPage(BrowserType browserType,
            string mainCategory, string subCategory)
        {
            MainValidation(browserType, mainCategory, subCategory);

            var announcementsPage = new AnnouncementsPage(webDriver);
            announcementsPage.ClickFoundCategory(announcementsPage.GetItemLinksFromFirstPage(), Constants.substringForSearching);

            var announcementPage = new AnnouncementPage(webDriver);
            announcementPage.AddToFavorite();
        }

        [Theory]
        [InlineData(BrowserType.Chrome, Constants.categotyName, Constants.subCategotyName)]
        public void AddToFavoritesBySeveralAnnouncements(BrowserType browserType,
            string mainCategory, string subCategory)
        {
            MainValidation(browserType, mainCategory, subCategory);

            var announcementsPage = new AnnouncementsPage(webDriver);
            string[] search_sub_string = new string [] { Constants.substringForSearching, Constants.substringForSearchingSecond };
            foreach (string item in search_sub_string)
            {
                announcementsPage.ClickFoundCategoryByCheckBox(announcementsPage.GetItemLinksFromFirstPage(), item);
            }
            announcementsPage.AddToFavorites(search_sub_string.Length);
        }

        void MainValidation(BrowserType browserType, string mainCategory, string subCategory)
        {
            SetUp(browserType);

            var mainPage = new MainPage(webDriver);
            mainPage.ValidateInitContent();

            IWebElement transportCategory = mainPage.GetCategory(mainPage.PageMainFull);
            mainPage.ValidateCategory(transportCategory, Constants.listCategoriesStr);
            mainPage.ClickFoundCategory(mainPage.GetCategoryLinks(transportCategory), mainCategory);

            var categoryPage = new CategoryPage(webDriver);
            categoryPage.ClickFoundCategory(categoryPage.GetCategoryLinks(), subCategory);
        }
    }
}

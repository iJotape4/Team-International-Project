using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Team_International_Project.Pages;

namespace Team_International_Project.Test
{
    public class Tests
    {
        IWebDriver webDriver = new ChromeDriver();

        [SetUp]
        public void Setup()
        {
            webDriver.Navigate().GoToUrl("https://www.teaminternational.com/");
        }

        [Test]
        public void NavigateMainPage()
        {
            HomePage homepage = new HomePage(webDriver);
            Assert.That(homepage.CategoriesListExist, Is.True, "Error. Categories List not Found");
            homepage.NavigatePage();       
        }

        [TearDown]
        public void TearDown() => webDriver.Quit();
    }
}
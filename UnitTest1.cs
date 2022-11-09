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

        [Test]
        public void CheckLabels()
        {
            HomePage homepage = new HomePage(webDriver);
            homepage.CheckLabels();
        }
        
        [Test]
        public void CheckMouseOver()
        {

            HomePage homePage = CreateHomePage();
            homePage.MouseOverActions();
            Assert.That(homePage.buttonBlueExists, Is.True, "Error. Top Gun Lab button isn't visible");
            Assert.That(homePage.buttonGreenExists, Is.True, "Error. Empower Your Carreer button isn't visible");
        }

        [Test]
        public void CheckMouseClick() 
        {
            HomePage homePage = CreateHomePage();
            homePage.ClickMouseActions();
        }

        [Test]
        public void FillOutForm() 
        {
            HomePage homePage = CreateHomePage();
            homePage.FillFormAction();
        }

        public HomePage CreateHomePage() =>  new HomePage(webDriver);

        [TearDown]
        public void TearDown() => webDriver.Quit();
    }
}
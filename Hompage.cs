using System.Collections.Generic;
using OpenQA.Selenium;

namespace Team_International_Project.Pages 
{
	public class HomePage :Page
	{
		public HomePage(IWebDriver webDriver)
		{
			SetWebDriver(webDriver);
		}

		public IWebElement categories => Driver.FindElement(By.Id("fp-nav"));
		private IList<IWebElement> categoriesList => categories.FindElements(By.TagName("li"));

		public bool CategoriesListExist() 
		{
			if (categoriesList.Count ==0)
				return false;

			return true;
		}

		public void NavigatePage()
		{
			foreach (IWebElement IWE in categoriesList)
			{
				IWebElement anchor = IWE.FindElement(By.TagName("a"));
				Driver.Navigate().GoToUrl(anchor.GetAttribute("href")); 
				System.Threading.Thread.Sleep(1500);			
			}
		}
	}

}
		
	


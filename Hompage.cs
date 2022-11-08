using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

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

		public IList<IWebElement> labelsList => Driver.FindElements(By.TagName("label"));
		public bool buttonBlueExists = true;

		//Mouse Over Actions
		private IList<IWebElement> industrialItems => Driver.FindElement(By.ClassName("industrial-row")).FindElements(By.ClassName("industrial-item"));
		private IList<IWebElement> servicesItems => Driver.FindElement(By.ClassName("services-row")).FindElements(By.ClassName("service-item"));
		private IList<IWebElement> logoitems => Driver.FindElement(By.ClassName("logo-row")).FindElements(By.ClassName("logo-item"));
		private IList<IWebElement> locationItems => Driver.FindElement(By.ClassName("slick-track")).FindElements(By.ClassName("location-item"));

		public bool CategoriesListExist() 
		{
			if (categoriesList.Count ==0)
				return false;

			return true;
		}

		public void NavigatePage()
		{
			for (int i=0; i<categoriesList.Count;i++)
				NavigateToCathegory((Categories)i);


		}

		public void CheckLabels()
        {
			foreach (var label in labelsList)
				Console.WriteLine(label);
        }
		public void MouseOverActions()
        {
			Actions action = new Actions(Driver);
			IWebElement leftCorner = Driver.FindElement(By.ClassName("custom-logo-link"));
			PerformMouseOverActions(Categories.Industry, action, leftCorner, industrialItems);
			PerformMouseOverActions(Categories.Services, action, leftCorner, servicesItems);
			PerformMouseOverActions(Categories.Logos, action, leftCorner, logoitems);
			
			PerformMouseOverActionsOnLocation(Categories.Locations, action, leftCorner, locationItems);
			buttonBlueExists= PerformMouseOverActionsOnTopGunLab(Categories.TopGunLab, action, leftCorner, locationItems);

			/*foreach (var it in locationItems)
				Console.WriteLine(it);*/

		}

		#region specific Categories Methods
		private void NavigateToCathegory(Categories CategoryIndex)
		{
			Driver.Navigate().GoToUrl(categoriesList[(int)CategoryIndex].FindElement(By.TagName("a")).GetAttribute("href"));
			System.Threading.Thread.Sleep(1000);
		}

		private void NavigateToCathegory(Categories CategoryIndex, Actions action, IWebElement leftCorner)
        {
			action.MoveToElement(leftCorner).Perform();
			NavigateToCathegory(CategoryIndex);
		}

		private void PerformMouseOverActions(Categories CategoryIndex, Actions action, IWebElement leftCorner, IList<IWebElement> InteractablesList) 
		{
			NavigateToCathegory(CategoryIndex, action, leftCorner);
			foreach (var item in InteractablesList)          
				action.MoveToElement(item).Perform();
            
		}

		private void PerformMouseOverActionsOnLocation(Categories CategoryIndex, Actions action, IWebElement leftCorner, IList<IWebElement> InteractablesList)
		{
			NavigateToCathegory(CategoryIndex, action, leftCorner);
						
			foreach (IWebElement item in InteractablesList)
			{
				if (item.Displayed) 
				{
					var button = item.FindElement(By.CssSelector("a[class='btn btn-green btn-green-full']"));			
					Driver.FindElement(By.ClassName("location-slider")).FindElement(By.CssSelector("img[class='arrow-btn next slick-arrow']")).Click();
					System.Threading.Thread.Sleep(500);
				}
			}
		}

		private bool PerformMouseOverActionsOnTopGunLab(Categories CategoryIndex, Actions action, IWebElement leftCorner, IList<IWebElement> InteractablesList) 
		{
			NavigateToCathegory(CategoryIndex, action, leftCorner);
            IWebElement blueButton = Driver.FindElement(By.CssSelector("a[class='btn blue-hover bnr-career-link']"));

			//Method to show button
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;	
			js.ExecuteScript("arguments[0].style.visibility = 'visible', arguments[0].style.height = '50px'; arguments[0].style.width = '300px'; arguments[0].style.opacity = 1", blueButton);
			System.Threading.Thread.Sleep(2000);
			
			action.MoveToElement(blueButton).Perform();
			return blueButton.Displayed;
		}



		#endregion
	}

	public enum Categories
    {
		Industry, Services, Logos, Locations, TopGunLab, EmpowerYourCarrer, ContactSales, Footer
    }

}
		
	


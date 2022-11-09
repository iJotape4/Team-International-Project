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
		public bool buttonGreenExists = true;

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
			buttonGreenExists= PerformMouseOverActionsOnImpowerYourCarrer(Categories.EmpowerYourCarrer, action, leftCorner, locationItems);

			/*foreach (var it in locationItems)
				Console.WriteLine(it);*/

		}

		public void ClickMouseActions()
        {
			Actions action = new Actions(Driver);
			IWebElement leftCorner = Driver.FindElement(By.ClassName("custom-logo-link"));
			PerformClickOnIndustryItems(Categories.Industry, action, leftCorner);
			PerformClickOnServicesItms(Categories.Services, action, leftCorner);
			PerformClickLogoItems(Categories.Logos, action, leftCorner);
			PerformClickLocationItems(Categories.Locations, action, leftCorner);
			PerformClickOnTopGunLab(Categories.TopGunLab, action, leftCorner);
			PerformClickOnEmpowerYourCarreer(Categories.EmpowerYourCarrer, action, leftCorner);
		}

		public void FillFormAction() 
		{
			Actions action = new Actions(Driver);
			IWebElement leftCorner = Driver.FindElement(By.ClassName("custom-logo-link"));
			FillOutForm(Categories.ContactSales, action, leftCorner);
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
		private bool PerformMouseOverActionsOnImpowerYourCarrer(Categories CategoryIndex, Actions action, IWebElement leftCorner, IList<IWebElement> InteractablesList) 
		{
			NavigateToCathegory(CategoryIndex, action, leftCorner);
            IWebElement greenButton = Driver.FindElement(By.CssSelector("a[class='btn btn-green blue-hover bnr-career-link']"));

			//Method to show button
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;	
			js.ExecuteScript("arguments[0].style.visibility = 'visible', arguments[0].style.height = '50px'; arguments[0].style.width = '300px'; arguments[0].style.opacity = 1", greenButton);
			System.Threading.Thread.Sleep(2000);
			
			action.MoveToElement(greenButton).Perform();
			return greenButton.Displayed;
		}

		private void PerformClickOnIndustryItems(Categories CategoryIndex, Actions action, IWebElement leftCorner)
        {
			NavigateToCathegory(CategoryIndex, action, leftCorner);
			for (int i = 0; i < industrialItems.Count; i++)
			{
				action.Click(industrialItems[i]).Perform();
				WaitAndBackPage();
			}
		}
		private void PerformClickOnServicesItms(Categories CategoryIndex, Actions action, IWebElement leftCorner)
        {
			NavigateToCathegory(CategoryIndex, action, leftCorner);
			for (int i = 0; i < servicesItems.Count; i++)
			{
				action.Click(servicesItems[i]).Perform();
				WaitAndBackPage();
			}
		}
		private void PerformClickLogoItems(Categories CategoryIndex, Actions action, IWebElement leftCorner)
		{
			NavigateToCathegory(CategoryIndex, action, leftCorner);
			for (int i = 0; i < logoitems.Count; i++)
			{
				action.Click(logoitems[i]).Perform();
				WaitAndBackPage();
			}
		}
		private void PerformClickLocationItems(Categories CategoryIndex, Actions action, IWebElement leftCorner)
        {
			NavigateToCathegory(CategoryIndex, action, leftCorner);
			for (int i = 0; i < locationItems.Count; i++)
			{
                if (locationItems[i].Displayed)
                {
					var button = locationItems[i].FindElement(By.CssSelector("a[class='btn btn-green btn-green-full']"));
					action.Click(button);
					if (button.GetAttribute("href") != Driver.Url)
                    {
						Console.WriteLine("Location item {0} button isn't opening their respective WebPage", button.GetAttribute("href").Split("#")[1]);
                    }
                    else
                    {
						WaitAndBackPage();
                    }
					Driver.FindElement(By.ClassName("location-slider")).FindElement(By.CssSelector("img[class='arrow-btn next slick-arrow']")).Click();
					System.Threading.Thread.Sleep(500);
                }
			}
		}
		
		private bool PerformClickOnTopGunLab(Categories CategoryIndex, Actions action, IWebElement leftCorner)
        {
			NavigateToCathegory(CategoryIndex, action, leftCorner);
			for (int i = 0; i < logoitems.Count; i++)
			{
				action.Click(logoitems[i]).Perform();
				WaitAndBackPage();
			}

			NavigateToCathegory(CategoryIndex, action, leftCorner);
			IWebElement blueButton = Driver.FindElement(By.CssSelector("a[class='btn blue-hover bnr-career-link']"));

			//Method to show button
			IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
			js.ExecuteScript("arguments[0].style.visibility = 'visible', arguments[0].style.height = '50px'; arguments[0].style.width = '300px'; arguments[0].style.opacity = 1", blueButton);
			System.Threading.Thread.Sleep(2000);

			action.MoveToElement(blueButton).Click().Perform();			
			WaitAndBackPage();
			return blueButton.Displayed;
		}

		private bool PerformClickOnEmpowerYourCarreer(Categories CategoryIndex, Actions action, IWebElement leftCorner)
        {
			NavigateToCathegory(CategoryIndex, action, leftCorner);
			IWebElement greenButton = Driver.FindElement(By.CssSelector("a[class='btn btn-green blue-hover bnr-career-link']"));

			//Method to show button
			IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
			js.ExecuteScript("arguments[0].style.visibility = 'visible', arguments[0].style.height = '50px'; arguments[0].style.width = '300px'; arguments[0].style.opacity = 1", greenButton);
			System.Threading.Thread.Sleep(2000);

			action.MoveToElement(greenButton).Click().Perform();
			WaitAndBackPage();
			return greenButton.Displayed;
		}

		private void WaitAndBackPage()
        {
			System.Threading.Thread.Sleep(500);
			Driver.Navigate().Back();
			System.Threading.Thread.Sleep(500);
		}
		private void FillOutForm(Categories CategoryIndex, Actions action, IWebElement leftCorner) 
		{
			NavigateToCathegory(CategoryIndex, action, leftCorner);
			System.Threading.Thread.Sleep(2000);
			//IWebElement form = Driver.FindElement(By.CssSelector("div['c-mqlform_mqlform class']"));
			//IList<IWebElement> form = Driver.FindElement(By.ClassName("contact-section")).FindElements(By.TagName("input"));
			IList<IWebElement> form = Driver.FindElement(By.ClassName("contact-section")).FindElement(By.CssSelector("iframe")).FindElements(By.TagName("body"));

			Console.WriteLine(form.Count);
			foreach (var x in form)
				Console.WriteLine(x);
		}



		#endregion
	}

	public enum Categories
    {
		Industry, Services, Logos, Locations, TopGunLab, EmpowerYourCarrer, ContactSales, Footer
    }

}
		
	


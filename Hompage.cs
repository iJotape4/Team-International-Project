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

		public void RunAlltests()
        {
			NavigatePage();
			MouseOverActions();
			ClickMouseActions();
			NavigateToCathegory(Categories.ContactSales);
			FillFormAction();
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
			NavigateToCathegory(Categories.Industry);
			PerformClickOnIndustryItems(Categories.Industry, action, leftCorner);
			NavigateToCathegory(Categories.Services);
			PerformClickOnServicesItms(Categories.Services, action, leftCorner);
			//NavigateToCathegory(Categories.Logos);
			//PerformClickLogoItems(Categories.Logos, action, leftCorner);
			NavigateToCathegory(Categories.Locations);
			PerformClickLocationItems(Categories.Locations, action, leftCorner);
			System.Threading.Thread.Sleep(1500);
			NavigateToCathegory(Categories.TopGunLab);
			buttonBlueExists = PerformClickOnTopGunLab(Categories.TopGunLab, action, leftCorner);
			System.Threading.Thread.Sleep(1500);
			NavigateToCathegory(Categories.EmpowerYourCarrer);
			buttonGreenExists = PerformClickOnEmpowerYourCarreer(Categories.EmpowerYourCarrer, action, leftCorner);
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
			System.Threading.Thread.Sleep(2000);
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
			//NavigateToCathegory(CategoryIndex, action, leftCorner);
			for (int i = 0; i < industrialItems.Count; i++)
			{
				action.Click(industrialItems[i]).Perform();
				WaitAndBackPage(Categories.Industry);
			}
		}
		private void PerformClickOnServicesItms(Categories CategoryIndex, Actions action, IWebElement leftCorner)
        {
		//	NavigateToCathegory(CategoryIndex, action, leftCorner);
			for (int i = 0; i < servicesItems.Count; i++)
			{
				action.Click(servicesItems[i]).Perform();
				WaitAndBackPage(Categories.Services);
			}
		}
		private void PerformClickLogoItems(Categories CategoryIndex, Actions action, IWebElement leftCorner)
		{
			//NavigateToCathegory(CategoryIndex, action, leftCorner);
			for (int i = 0; i < logoitems.Count; i++)
			{
				action.Click(logoitems[i]).Perform();
				WaitAndBackPage(Categories.Logos);
			}
		}
		private void PerformClickLocationItems(Categories CategoryIndex, Actions action, IWebElement leftCorner)
        {
			//NavigateToCathegory(CategoryIndex, action, leftCorner);
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
						WaitAndBackPage(Categories.Locations);
                    }
					Driver.FindElement(By.ClassName("location-slider")).FindElement(By.CssSelector("img[class='arrow-btn next slick-arrow']")).Click();
					System.Threading.Thread.Sleep(500);
                }
			}
		}
		
		private bool PerformClickOnTopGunLab(Categories CategoryIndex, Actions action, IWebElement leftCorner)
        {
			//NavigateToCathegory(CategoryIndex, action, leftCorner);
			IWebElement blueButton = Driver.FindElement(By.CssSelector("a[class='btn blue-hover bnr-career-link']"));

			//Method to show button
			IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
			js.ExecuteScript("arguments[0].style.visibility = 'visible', arguments[0].style.height = '50px'; arguments[0].style.width = '300px'; arguments[0].style.opacity = 1", blueButton);
			System.Threading.Thread.Sleep(2000);

			NavigateToCathegory(Categories.TopGunLab);
			System.Threading.Thread.Sleep(500);
			action.Click(blueButton).Perform();			
			//WaitAndBackPage(Categories.TopGunLab);
			return blueButton.Displayed;
		}

		private bool PerformClickOnEmpowerYourCarreer(Categories CategoryIndex, Actions action, IWebElement leftCorner)
        {
			//NavigateToCathegory(CategoryIndex, action, leftCorner);
			IWebElement greenButton = Driver.FindElement(By.CssSelector("a[class='btn btn-green blue-hover bnr-career-link']"));

			//Method to show button
			IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
			js.ExecuteScript("arguments[0].style.visibility = 'visible', arguments[0].style.height = '50px'; arguments[0].style.width = '300px'; arguments[0].style.opacity = 1", greenButton);
			System.Threading.Thread.Sleep(2000);

			NavigateToCathegory(Categories.TopGunLab);
			System.Threading.Thread.Sleep(500);
			action.Click(greenButton).Perform();
			//WaitAndBackPage(Categories.EmpowerYourCarrer);
			return greenButton.Displayed;
		}

		private void WaitAndBackPage(Categories backpage)
        {
			System.Threading.Thread.Sleep(500);
			Driver.Navigate().Back();		
			System.Threading.Thread.Sleep(1500);
			NavigateToCathegory(backpage);
		}

		private void FillOutForm(Categories CategoryIndex, Actions action, IWebElement leftCorner) 
		{
			NavigateToCathegory(CategoryIndex, action, leftCorner);
			System.Threading.Thread.Sleep(2000);
			//IWebElement form = Driver.FindElement(By.CssSelector("div['c-mqlform_mqlform class']"));
			//IWebElement form = Driver.FindElement(By.XPath("/html/body/webruntime-app/community_byo-scoped-header-and-footer/main/webruntime-router-container/community_layout-slds-flexible-layout/div/community_layout-section/div/div[3]/community_layout-column/div/c-mql-form/lightning-layout/slot/lightning-layout-item[3]/slot/lightning-layout/slot/lightning-layout-item[1]/slot/div/span/input"));
			//IList<IWebElement> form = Driver.FindElements(By.TagName("span"));
			IList<IWebElement> form = Driver.FindElements(By.TagName("span"));
			//IWebElement w= Driver.FindElement(By.XPath("//input[@c-mqlform_mqlform='' @class='form-input' @type='text' @data-id='firstName' @size='255' @aria-required='true' @aria-invalid='false' @style='cursor: auto']"));
			//Console.WriteLine(form.Count);
			
			Console.WriteLine("I'm Tired. I surrender, I cannot find form items");
            
				
		} 



		#endregion
	}

	public enum Categories
    {
		Industry, Services, Logos, Locations, TopGunLab, EmpowerYourCarrer, ContactSales, Footer
    }

}
		
	


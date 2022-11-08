using OpenQA.Selenium;

namespace Team_International_Project.Pages
{
	public class Page 
	{
		protected void SetWebDriver(IWebDriver webDriver)=>  Driver = webDriver;		
		protected IWebDriver Driver { get; set; }		
	}
}

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT4
{
    //[TestFixture]
    public class MainPage : Tests
    {

        public string Email_user = " demouser@microsoft.com";
        public string Email_user_Admin = "admin@microsoft.com";
        public string Password = "Pass@word1";
        public string  FileName_default = "Default_Filename";
        public string FileName_Admin = "Admin_Filename";
        public string Default_Filename_Add_product = "default_add_product";
        public string Default_Filename_Add_product_pay = "default_add_product_pay";

        [Test]
        public void Default_user()
        {
            try
            {
                List<string> list = new List<string>();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); 
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(Selector.click)));
                element.Click();
                Log log = new Log();
                log.Parametr(Email_user, Password, wait,driver, FileName_default);
                log.Log_user();
                list.Add(log.screenshotPath);
                User_buy_product user_Buy_Product = new User_buy_product();
                user_Buy_Product.Parametr(Email_user, Password, wait, driver, Default_Filename_Add_product,Default_Filename_Add_product_pay);
                user_Buy_Product.Add_product();

                list.Add(user_Buy_Product.screenshotPath);
                list.Add(user_Buy_Product.screenshotPath_pay);

                Report report = new Report();

                report.Parametr(wait, driver, list.ToArray());
                report.REPORT();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Test]
        public void Admin_user()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(Selector.click)));
                element.Click();

                Log log = new Log();
                log.Parametr(Email_user_Admin, Password, wait,driver, FileName_Admin);
                log.Log_user();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
      


        
    } 
}

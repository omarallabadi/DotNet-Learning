using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankingAppExercise.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult def()
        {
            return Content("Welcome to the Best Bank");
        }
        [Route("/account-details")]
        public IActionResult details()
        {
            Account account = new Account()
            {
                AccountNumber = 1001,
                AccountHolderName = "Example Name",
                CurrentBalance = 5000
            };  
             return Json(account);
        }
        [Route("/account-statement")]
        public IActionResult statement()
        {
            return File("/test.pdf", "application/pdf");
        }

        [Route("/get-current-balance/{accountNumber?}")]
        public IActionResult balance()
        {


            if( string.IsNullOrEmpty(Convert.ToString(Request.RouteValues["accountNumber"])))
            {
                return NotFound("Account Number should be supplied");
            }


            if (int.TryParse(Convert.ToString(Request.RouteValues["accountNumber"]),out int num))
            {
                if (num != 1001) {
                    return BadRequest("Account Number should be 1001");
                }
                Account account = new Account()
                {
                    AccountNumber = 1001,
                    AccountHolderName = "Example Name",
                    CurrentBalance = 5000
                };
                return Content(account.CurrentBalance.ToString(), "text/plain");

            }
            else
            {
                return StatusCode(400, "The accountNumber should be an int value");

            }

           
        }
    }
}

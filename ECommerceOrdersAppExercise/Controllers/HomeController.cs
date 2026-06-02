using ECommerceOrdersAppExercise.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOrdersAppExercise.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost("/order")]
        public IActionResult Index([Bind(nameof(Order.OrderDate),
          nameof(Order.InvoicePrice),
          nameof(Order.Products))]   Order order)
        {
            if (!ModelState.IsValid)
            {

                string errors=string.Join("\n",ModelState.Values.SelectMany(x=>x.Errors).Select(errors=>errors.ErrorMessage));  
                return BadRequest(errors);
            }

            order.OrderNo = Random.Shared.Next(1, 100000);

            return Json(new {orderno=order.OrderNo});
        }
    }
}

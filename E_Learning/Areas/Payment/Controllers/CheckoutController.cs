using E_Learning.Areas.Payment.Models;
using E_Learning.Helper;
using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using E_Learning.Repository.IReposatories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayPalCheckoutSdk.Orders;

using E_Learning.Areas.Student.Data.Services;
namespace E_Learning.Areas.Payment.Controllers
{
    [Area("Payment")]
    public class CheckoutController : Controller
    {
        public string TotalAmount { get; set; } = null;
        private readonly PaypalClient _paypalClient;
        private readonly IPaymentRepository _payment;
        private readonly IEnrollmentRepository _enrollment;
        private readonly IDataForInstructorRepository _instructor;
		private readonly ICourseRepository _course;
        private readonly ILearningService _studentCourseProgress;
        private readonly ICartRepository _cartRepo;
		public CheckoutController(PaypalClient paypalclient , IEnrollmentRepository enrollment , 
            IPaymentRepository payment,IDataForInstructorRepository _Instructor , ICourseRepository course , ILearningService progress , ICartRepository cartRepo)
        {
            _paypalClient = paypalclient;
            _payment = payment;
            _enrollment = enrollment;
            _instructor = _Instructor;
			_course = course;
            _studentCourseProgress = progress;
            _cartRepo = cartRepo;

        }


        [HttpGet]
        public async Task<IActionResult> Index(CourseSummaryViewModel Csummary)
        {

            ViewBag.ClientId = _paypalClient.ClientId;
            try
            {
                var cart = Csummary;
                ViewBag.cart = cart;
                ViewBag.DollarAmount = cart.total;
               
                if (cart.total > 0)
                {
                    ViewBag.total = ViewBag.DollarAmount;
                    int total = (int)ViewBag.total;
                    TotalAmount = total.ToString();
                    TempData["TotalAmount"] = TotalAmount;
                    ViewBag.CoursesId = Csummary.CourseIdMony.Keys;
                }
                else
                {
                    var routeValues = new RouteValueDictionary
                                                        {
                                                            { "total", Csummary.total },
                                                            { "UserId", Csummary.UserId },
                                                            { "CourseIdMony", string.Join(",", Csummary.CourseIdMony.Select(x => $"{x.Key}:{x.Value}")) } // Convert dictionary to string
                                                        };
                    return RedirectToAction("Success", routeValues);
                }
            }
            catch (Exception x)
            {
                ModelState.AddModelError(string.Empty, x.Message);

            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Order(CancellationToken cancellationToken)
        {
            try
            {
                // set the transaction price and currency
                var price = TempData.Peek("TotalAmount")?.ToString();
                var currency = "USD";

                // "reference" is the transaction key
                var reference = GetRandomInvoiceNumber();// "INV002";

                var response = await _paypalClient.CreateOrder(price!, currency, reference);
                if (response != null) 
                {
                    RedirectToAction("Success", ViewBag.cart);
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                var error = new
                {
                    e.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }
        public async Task<IActionResult> Capture(string orderId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _paypalClient.CaptureOrder(orderId);

                var reference = response.purchase_units[0].reference_id;

                // Put your logic to save the transaction here
                // You can use the "reference" variable as a transaction key

                return Ok(response);
            }
            catch (Exception e)
            {
                var error = new
                {
                    e.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }
        public static string GetRandomInvoiceNumber()
        {
            return new Random().Next(999999).ToString();
        }

        public async Task<IActionResult> Success(double total, string UserId, string CourseIdMony)
        {
            try
            {
                var courseIdMonyDict = CourseIdMony
                                               .Split(',')
                                               .Select(x => x.Split(':'))
                                               .ToDictionary(x => x[0], x => int.Parse(x[1]));

                var proccess = new CourseSummaryViewModel
                {
                    total = total,
                    UserId = UserId,
                    CourseIdMony = courseIdMonyDict
                };

                foreach (var cId in proccess.CourseIdMony)
                {
                    var Id = Guid.NewGuid().ToString();
                    var payment = new E_Learning.Models.Payment()
                    {
                        CourseId = cId.Key,
                        Id = Id,
                        UserId = proccess.UserId,
                        PaymentTime = DateTime.UtcNow,
                        PaymentMethod = "Paypal",
                        PaymentAmount = cId.Value
                    };
                    var Enrollment = new E_Learning.Models.Enrollment()
                    {
                        Id = Id,
                        CourseId = cId.Key,
                        Date = DateTime.UtcNow,
                        CompletionStatus = false,
                        Percentage = 100,
                        UserId = proccess.UserId

                    };

                    await _payment.AddAsync(payment);
                    await _enrollment.AddAsync(Enrollment);
                    var InstructorId = _course.GetById(cId.Key).InstructorId;
                    var oldData = await _instructor.GetInstructorDataByInstructorIdAsync(InstructorId);
                    if (oldData != null)
                    {
                        oldData.Balance += cId.Value;
                        await _instructor.UpdateInstructorDataAsync(oldData);
                    }

                    await _studentCourseProgress.UpdateCourseProgressAsync(proccess.UserId, cId.Key, 0.0);
                    await _cartRepo.DeleteAsync(cId.Key, proccess.UserId);
                }
                if (proccess != null)
                {
                    return View();
                }
                return View("Fail");
            }
            catch
            {
                return View("Fail");

            }
        }




		public IActionResult Processing(string stripeToken, string stripeEmail)
		{
			//var optionCust = new CustomerCreateOptions
			//{
			//    Email = stripeEmail,
			//    Name = "Rizwan Yousaf",
			//    Phone = "338595119"
			//};
			//var serviceCust = new CustomerService();
			//Customer customer = serviceCust.Create(optionCust);
			//var optionsCharge = new ChargeCreateOptions
			//{
			//    Amount = Convert.ToInt64(TempData["TotalAmount"]),
			//    Currency = "USD",
			//    Description="Pet Selling amount",
			//    Source=stripeToken,
			//    ReceiptEmail=stripeEmail

			//};
			//var serviceCharge = new ChargeService();
			//Charge charge = serviceCharge.Create(optionsCharge);
			//if(charge.Status=="successded")
			//{
			//    ViewBag.AmountPaid = charge.Amount;
			//    ViewBag.Customer = customer.Name;
			//}
			return View();


		}

	}
}
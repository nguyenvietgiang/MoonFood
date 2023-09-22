using Microsoft.AspNetCore.Mvc;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;


namespace MoonFood.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/payment")]
    [ApiVersion("1.0")]
    public class PaymentController : ControllerBase
    {
        private readonly string _clientId = "AebdKRRUpQ8ioQSqJ4uGVDB4p5aQvZIBA0ysQCJCoWwH--kaad6Y7lbCvQQuvSU7RrLtQWl23QCQkNj3";
        private readonly string _clientSecret = "EJfS-cL5T3FpQdPCRgUsdDOOLJENYeb77j0MiRaARnNw1Bz8y8Vx69uw3CqcT4k_-yaBYe0_sWPasdt8";

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder()
        {
            try
            {
                // Khởi tạo cấu hình PayPal
                var environment = new SandboxEnvironment(_clientId, _clientSecret);
                var client = new PayPalHttpClient(environment);

                // Tạo đơn hàng
                var request = new OrdersCreateRequest();
                request.Prefer("return=representation");
                request.RequestBody(BuildRequestBody());

                var response = await client.Execute(request);
                var order = response.Result<PayPalCheckoutSdk.Orders.Order>();

                // Lấy liên kết thanh toán từ đơn hàng
                var approveUrl = order.Links.Find(x => x.Rel == "approve").Href;

                return Ok(new { approveUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private OrderRequest BuildRequestBody()
        {
            var order = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>
                {
                    new PurchaseUnitRequest
                    {
                        AmountWithBreakdown = new AmountWithBreakdown
                        {
                           CurrencyCode = "USD",
                           Value = "200.00"
                        }
                    }
                }
            };

            return order;
        }
    }
}


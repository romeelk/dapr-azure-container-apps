using Microsoft.AspNetCore.Mvc;
using Dapr;
using Dapr.Client;

namespace checkout.Controllers;

[ApiController]
[Route("[controller]")]
public class CheckoutServiceController : ControllerBase
{
    private readonly ILogger<CheckoutServiceController> _logger;

    public CheckoutServiceController(ILogger<CheckoutServiceController> logger)
    {
        _logger = logger;
    }

    [Topic("order_pub_sub", "orders")]
    [HttpPost("/checkout")]
    public void Checkout([FromBody]object body)
    {
        _logger.LogInformation(body.ToString());
    }
}

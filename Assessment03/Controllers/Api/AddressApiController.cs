using Assessment03.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Assessment03.Context;
using Microsoft.EntityFrameworkCore;

namespace Assessment03.Controllers.Api;

[ApiController]
[Route("[controller]")]
public class AddressApiController : Controller
{
    private readonly ILogger<AddressApiController> _logger;
    private readonly IServiceProvider _serviceProvider;

    public AddressApiController(ILogger<AddressApiController> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    [HttpGet]
    public async Task<List<Address>> GetAll()
    {
        _logger.LogInformation("GET: Address/Api/");
        ProjectContext context = _serviceProvider.GetRequiredService<ProjectContext>();

        List<Address> addresses = await context.Addresses.ToListAsync();
        
        return addresses;
    }
}

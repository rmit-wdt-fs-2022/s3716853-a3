using Assessment03.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Assessment03.Context;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Assessment03.Controllers;

public class AddressController : Controller
{
    private readonly ILogger<AddressController> _logger;
    private readonly IServiceProvider _serviceProvider;

    public AddressController(ILogger<AddressController> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("GET: Contacts/");
        List<Address> addresses = (await GetQueryApi<List<Address>>("https://localhost:7002/AddressApi"))!;
        return View(addresses);
    }

    // Taken from my assignment 02, if you are wondering why its so complex (and Generic) while only being used once
    private async Task<T?> GetQueryApi<T>(string connectionString)
    {
        HttpClient? httpClient = _serviceProvider.GetService<HttpClient>();

        string response =
            await httpClient?.GetStringAsync($"{connectionString}")!;

        return JsonConvert.DeserializeObject<T>(response);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
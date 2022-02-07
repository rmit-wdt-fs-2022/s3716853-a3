using Assessment03.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Assessment03.Context;
using Assessment03.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Assessment03.Controllers;

public class ContactsController : Controller
{
    private readonly ILogger<ContactsController> _logger;
    protected readonly IServiceProvider _serviceProvider;

    public ContactsController(ILogger<ContactsController> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public IActionResult Index()
    {
        List<Contact> contacts = _serviceProvider.GetRequiredService<ProjectContext>().Contacts.Include(contact => contact.Work).Include(contact => contact.Home).ToList();
        return View(contacts);
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult SubmitCreate(ContactCreateViewModel contactCreateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View("Create", contactCreateViewModel);
        }
        _logger.LogError("Oh would you look at that you submitted!");
        _logger.LogError(contactCreateViewModel.FirstName);
        _logger.LogError(contactCreateViewModel.LastName);
        _logger.LogError(contactCreateViewModel.Email);
        _logger.LogError(contactCreateViewModel.MobilePhone);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
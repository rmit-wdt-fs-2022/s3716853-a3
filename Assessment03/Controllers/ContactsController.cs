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
        _logger.LogInformation("GET: Contacts/");
        List<Contact> contacts = _serviceProvider.GetRequiredService<ProjectContext>().Contacts.Include(contact => contact.Work).Include(contact => contact.Home).ToList();
        return View(contacts);
    }

    public IActionResult Create()
    {
        _logger.LogInformation("GET: Contacts/Create");
        return View();
    }

    public async Task<IActionResult> SubmitCreate(ContactCreateViewModel contactCreateViewModel)
    {
        _logger.LogInformation("GET: Contacts/CreateSubmit");
        if (!ModelState.IsValid)
        {
            return View("Create", contactCreateViewModel);
        }

        ProjectContext context = _serviceProvider.GetRequiredService<ProjectContext>();
        await context.AddAsync(new Contact()
        {
            Email = contactCreateViewModel.Email,
            FirstName = contactCreateViewModel.FirstName,
            LastName = contactCreateViewModel.LastName,
            MobilePhone = contactCreateViewModel.MobilePhone
        });
        await context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
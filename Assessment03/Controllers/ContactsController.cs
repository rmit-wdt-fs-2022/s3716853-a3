using Assessment03.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Assessment03.Context;
using Assessment03.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

    public IActionResult UpdateStepOne()
    {
        List<Contact> contacts = _serviceProvider.GetRequiredService<ProjectContext>().Contacts.Include(contact => contact.Work).Include(contact => contact.Home).ToList();
        ViewBag.Contacts = contacts;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateStepTwo(UpdateStepOneViewModel updateStepOneViewModel)
    {
        _logger.LogInformation("POST: Contacts/UpdateStepTwo");
        if (!ModelState.IsValid)
        {
            return View("UpdateStepOne", updateStepOneViewModel);
        }

        // Supressing nullable as the contact received should never be nullable
        Contact contact = (await _serviceProvider.GetRequiredService<ProjectContext>().Contacts
            .Include(contact => contact.Home).FirstOrDefaultAsync(contact => contact.ContactId == updateStepOneViewModel.ContactId))!;

        UpdateStepTwoViewModel updateStepTwoViewModel;

        if (contact.Home != null)
        {
            updateStepTwoViewModel = new UpdateStepTwoViewModel()
            {
                ContactId = contact.ContactId,
                AddressId = contact.Home.AddressId,
                Street = contact.Home.Street,
                PostCode = contact.Home.PostCode,
                State = contact.Home.State,
                Suburb = contact.Home.Suburb
            };
        }
        else
        {
            updateStepTwoViewModel = new UpdateStepTwoViewModel()
            {
                ContactId = contact.ContactId,
            };
        }
        return View(updateStepTwoViewModel);
    }

    public async Task<IActionResult> UpdateComplete(UpdateStepTwoViewModel updateStepTwoViewModel)
    {
        ProjectContext context = _serviceProvider.GetRequiredService<ProjectContext>();
        if (updateStepTwoViewModel.AddressId != null)
        {
            // Since the address id was set, then the address existed and must be updated
            context.Addresses.Update(new Address()
            {
                AddressId = (int) updateStepTwoViewModel.AddressId,
                PostCode = updateStepTwoViewModel.PostCode,
                State = updateStepTwoViewModel.State,
                Street = updateStepTwoViewModel.Street,
                Suburb = updateStepTwoViewModel.Suburb
            });
        }
        else
        {
            // Since the address id was NOT set, have to make a new address...
            EntityEntry<Address> homeAddressEntity = context.Addresses.Add(new Address()
            {
                PostCode = updateStepTwoViewModel.PostCode,
                State = updateStepTwoViewModel.State,
                Street = updateStepTwoViewModel.Street,
                Suburb = updateStepTwoViewModel.Suburb
            });
            // ... and we also need to link it to the contact 
            Contact contact =
                (await context.Contacts.FirstOrDefaultAsync(contact =>
                    contact.ContactId == updateStepTwoViewModel.ContactId))!; // still using the result from the previous form, so the contact should definetely exist (assuming the request comes from the web app)

            contact.HomeAddressId = homeAddressEntity.Entity.AddressId;
            contact.Home = homeAddressEntity.Entity;

            context.Contacts.Update(contact);
        }

        await context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
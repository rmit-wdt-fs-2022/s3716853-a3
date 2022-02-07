using Assessment03.Context;
using Assessment03.Models;

namespace Assessment03.Utilities;

public static class ProjectContextExtension
{
    public static async Task Seed(this ProjectContext projectContext)
    {
        // Only seed if the DB is empty
        if (projectContext.Addresses.Any() || projectContext.Contacts.Any())
        {
            return;
        }

        Address address1 = new()
        {
            PostCode = "3000",
            State = State.ACT,
            Street = "Street Name",
            Suburb = "Suburb Name"
        };
        Address address2 = new()
        {
            PostCode = "3001",
            State = State.VIC,
            Street = "Street Name",
            Suburb = "Suburb Name"
        };

        // One address should be set to the home address and the other address set to the work address for a single contact. 
        Contact contact1 = new() 
        {
            FirstName = "John",
            LastName = "Smith",
            Email = "john.smith@email.com",
            MobilePhone = "0490015570",
            HomeAddressId = address1.AddressId,
            WorkAddressId = address2.AddressId,
            Home = address1,
            Work = address2
        };

        // The remaining 2 contacts are to have no address related data, meaning HomeAddressID and WorkAddressID should be null. 
        Contact contact2 = new()
        {
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@email.com",
            MobilePhone = "0490015570"
        };
        Contact contact3 = new()
        {
            FirstName = "Bill",
            LastName = "Smith",
            Email = "bill.smith@email.com",
            MobilePhone = "0490015570"
        };

        await projectContext.Addresses.AddAsync(address1);
        await projectContext.Addresses.AddAsync(address2);

        await projectContext.Contacts.AddAsync(contact1);
        await projectContext.Contacts.AddAsync(contact2);
        await projectContext.Contacts.AddAsync(contact3);
        
        await projectContext.SaveChangesAsync();
    }
}


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Assessment03.Models.ViewModels;
public class ContactCreateViewModel
{
    [StringLength(30, MinimumLength = 1)]
    [DisplayName("First Name")]
    [RegularExpression("^[A-Z][a-zA-Z]*$", ErrorMessage = "Must start with capital and only contain letters")]
    public string FirstName { get; set; }

    [StringLength(30, MinimumLength = 1)]
    [DisplayName("Last Name")]
    [RegularExpression("^[A-Z][a-zA-Z]*$", ErrorMessage = "Must start with capital and only contain letters")]
    public string LastName { get; set; }

    [StringLength(320, MinimumLength = 1)]
    [DataType(DataType.EmailAddress)]
    [RegularExpression("^[^@]+@[^@]+$", ErrorMessage = "Must be valid email")]
    public string Email { get; set; }

    [DataType(DataType.PhoneNumber)]
    [StringLength(10, MinimumLength = 10)]
    [DisplayName("Mobile Phone")]
    [RegularExpression("^04\\d{8}$", ErrorMessage = "Must follow 04XXXXXXXX format where X are digits")]
    public string? MobilePhone { get; set; }
}

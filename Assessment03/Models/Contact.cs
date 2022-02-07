using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Assessment03.Utilities;

namespace Assessment03.Models;
public class Contact
{
    [Key]
    public int ContactId { get; set; }

    [StringLength(30, MinimumLength = 1)]
    [RegularExpression(Constants.NameRegex, ErrorMessage = "Must start with capital and only contain letters")]
    public string FirstName { get; set; }

    [StringLength(30, MinimumLength = 1)]
    [RegularExpression(Constants.NameRegex, ErrorMessage = "Must start with capital and only contain letters")]
    public string LastName { get; set; }

    [StringLength(320, MinimumLength = 1)]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(Constants.EmailRegex, ErrorMessage = "Must be valid email")]
    public string Email { get; set; }

    [DataType(DataType.PhoneNumber)]
    [StringLength(10, MinimumLength = 10)]
    [RegularExpression(Constants.PhoneNumberRegex, ErrorMessage = "Must follow 04XXXXXXXX format")]
    public string? MobilePhone { get; set; }

    [ForeignKey(nameof(Address))]
    public string? HomeAddressId { get; set; }
    public Address? HomeAddress { get; set; }

    [ForeignKey(nameof(Address))]
    public string? WorkAddressId { get; set; }
    public Address? WorkAddress { get; set; }
}

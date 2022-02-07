using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Assessment03.Utilities;

namespace Assessment03.Models;
public class Contact
{
    [Key]
    public int ContactId { get; set; }

    [StringLength(30, MinimumLength = 1)]
    [RegularExpression("^[A-Z][a-zA-Z]*$", ErrorMessage = "Must start with capital and only contain letters")]
    public string FirstName { get; set; }

    [StringLength(30, MinimumLength = 1)]
    [RegularExpression("^[A-Z][a-zA-Z]*$", ErrorMessage = "Must start with capital and only contain letters")]
    public string LastName { get; set; }

    [StringLength(320, MinimumLength = 1)]
    [DataType(DataType.EmailAddress)]
    [RegularExpression("^[^@]+@[^@]+$", ErrorMessage = "Must be valid email")]
    public string Email { get; set; }

    [DataType(DataType.PhoneNumber)]
    [StringLength(10, MinimumLength = 10)]
    [RegularExpression("^04\\d{8}$", ErrorMessage = "Must follow 04XXXXXXXX format")]
    public string? MobilePhone { get; set; }

    [ForeignKey(nameof(Address))]
    public int? HomeAddressId { get; set; }
    public Address? Home { get; set; }

    [ForeignKey(nameof(Address))]
    public int? WorkAddressId { get; set; }
    public Address? Work { get; set; }
}

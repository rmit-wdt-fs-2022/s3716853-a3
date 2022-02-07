using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Assessment03.Utilities;

namespace Assessment03.Models.ViewModels;
public class UpdateStepTwoViewModel
{
    public int ContactId { get; set; }
    public int? AddressId { get; set; }

    [StringLength(100, MinimumLength = 1)]
    public string Street { get; set; }

    [StringLength(100, MinimumLength = 1)]
    [RegularExpression("^[A-Z](?:[a-zA-Z]| )*$", ErrorMessage = "Must start with capital and can only contain letters and spaces")]
    public string Suburb { get; set; }

    [StringLength(4, MinimumLength = 4)]
    [RegularExpression("^\\d+$", ErrorMessage = "Post Code only accepts digits")]
    [DataType(DataType.PostalCode)]
    public string PostCode { get; set; }

    public State State { get; set; }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Assessment03.Models.ViewModels;
public class UpdateStepOneViewModel
{
    [DisplayName("Select the contact to update")]
    public int ContactId { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is Required"),
           EmailAddress(ErrorMessage = "The Email Is Invaild")]

        public string Email { get; set; }

    }
}

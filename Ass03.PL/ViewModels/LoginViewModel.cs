using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email is Required"),
		   EmailAddress(ErrorMessage = "The Email Is Invaild")]

		public string Email { get; set; }

		//public string UserName { get; set; }
		[Required(ErrorMessage = "Passeord is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}

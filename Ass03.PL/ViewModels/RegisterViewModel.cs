using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class RegisterViewModel
	{
        [Required]
        public string FName { get; set; }

		[Required]
		public string LName { get; set; }   

        [Required(ErrorMessage ="Email is Required"),
            EmailAddress(ErrorMessage ="The Email Is Invaild")]

        public string Email { get; set; }


        //public string UserName { get; set; }
        [Required (ErrorMessage ="Passeord is Required")]
        [Compare(nameof(ConfirmedPassword),ErrorMessage ="Confired Password does not match with Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ConfirmedPassword { get; set; }

        public bool IsAgree { get; set; }


    }
}

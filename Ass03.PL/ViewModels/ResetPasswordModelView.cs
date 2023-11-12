using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class ResetPasswordModelView
	{
		[Required(ErrorMessage = "Passeord is Required")]
		[Compare(nameof(ConfirmedPassword), ErrorMessage = "Confired Password does not match with Password")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		public string ConfirmedPassword { get; set; }
	}
}

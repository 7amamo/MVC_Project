namespace Demo.presentaton.Layer.ViewModels
{
	public class LoginViewModel
	{
		[Required]
		[EmailAddress(ErrorMessage ="Invaild Email")]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public	bool RememberMe { get; set; }
	}
} 

namespace Demo.presentaton.Layer.ViewModels
{
	public class ResetPasswordViewModel
	{
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Password & Confirm password Doesn't Match")]
		public string ConfirmPassword { get; set; }




	}
}

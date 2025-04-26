namespace Demo.presentaton.Layer.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="First Name is Reqired")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last Name is Reqired")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "User Name is Reqired")]
		public string UserName { get; set; }
        [Required(ErrorMessage = "Email is Reqired")]
        [EmailAddress(ErrorMessage = "Invaild Email")]
		public string Email { get; set; }
		[DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Reqired")]

        public string Password { get; set; }
		[DataType(DataType.Password)]
		[Compare(nameof(Password),ErrorMessage ="Password & Confirm password Doesn't Match")]
		public string ConfirmPassword { get; set; }
		public bool IsAgree { get; set; }
	}
}

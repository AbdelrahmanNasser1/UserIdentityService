namespace UserIdentityService.Model
{
    public class UserModels
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "DisplayName is required.")]
        [StringLength(100, ErrorMessage = "DisplayName cannot be longer than 100 characters.")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Mail is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
    }
}

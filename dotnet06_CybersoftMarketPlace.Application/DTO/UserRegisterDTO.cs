using System.ComponentModel.DataAnnotations;

public class UserRegisterDTO
{
    [Required(ErrorMessage = "Username là bắt buộc.")]
    [StringLength(
        32,
        MinimumLength = 6,
        ErrorMessage = "Username phải từ 6 đến 32 ký tự."
    )]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email là bắt buộc.")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "FullName là bắt buộc.")]
    [RegularExpression(
        @"^(?!.*\d).+$",
        ErrorMessage = "FullName không được chứa số."
    )]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Password là bắt buộc.")]
    [MinLength(
        6,
        ErrorMessage = "Password phải có ít nhất 6 ký tự."
    )]
    public string Password { get; set; }

    [Required(ErrorMessage = "PhoneNumber là bắt buộc.")]
    [Phone(ErrorMessage = "PhoneNumber không đúng định dạng số điện thoại.")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Address là bắt buộc.")]
    public string? Address { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Bazaar.Models.ViewModels;

public class RegisterViewModel
{
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string? Fname{get;set;}
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string? Lname{get;set;}
    [Required]
    [EmailAddress]
    public string? Email{get;set;}
    [Required]
    [RegularExpression("[0-9]{10}")]
    public string? Phone{get;set;}
    [Required]
    [StringLength(20, MinimumLength =8)]
    public string? Password{get;set;}
    [Required]
    [StringLength(20, MinimumLength = 8)]
    [Compare("Password")]
    public string? ConfirmPassword{get;set;}

    [CheckBoxRequired(ErrorMessage = "Must agree the tearms and conditions")]
    public bool Agreement{get; set;}
}

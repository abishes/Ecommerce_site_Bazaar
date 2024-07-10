using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Bazaar.Models.DbModels;

public class User : IdentityUser{
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string? Fname{get;set;}
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string? Lname{get;set;}
    [Required]
    public string? AccoutCreationDate{get; set;}

    [CheckBoxRequired]
    public bool Agreement{get; set;}
}
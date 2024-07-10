using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Bazaar.Models.ViewModels;

public class ProductViewModel{
    [Required]
    [MaxLength(100)]
    public string? Category{get;set;}
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string? Name{get;set;}
    [Required]
    [Precision(16,2)]
    public decimal Price{get;set;}
    [Required]
    [StringLength(100, MinimumLength =3)]
    public string? Description{get;set;}
    [Required]
    [NotNull]
    public IFormFile? ImageFileName{get;set;}
}

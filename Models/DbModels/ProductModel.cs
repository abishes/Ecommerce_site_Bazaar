using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Bazaar.Models.DbModels;

public class Product{
    public Guid Id{get;set;}
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
    [StringLength(100)]
    [NotNull]
    public string? Description{get;set;}
    [Required]
    public string? ImageFileName{get;set;}
    [Required]
    public string? Date{get;set;}
}

using System.ComponentModel.DataAnnotations;

namespace Bazaar.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}


public class CheckBoxRequired: ValidationAttribute{
    public override bool IsValid(object? value)
    {
        if(value == null) return false;
        if(value is bool b) return b;
        throw new InvalidOperationException("Can only be used on boolean properties.");
    }
}
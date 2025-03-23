using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WebApi.Utilities;

namespace WebApi.Models;
public class ApplicationUser : IdentityUser
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }
}

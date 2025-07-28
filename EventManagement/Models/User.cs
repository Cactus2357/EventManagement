using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models;

public partial class User
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required.")]
    [Display(Name = "Password")]
    public string PasswordHash { get; set; } = null!;

    [Required]
    [RegularExpression("admin|organizer|attendee", ErrorMessage = "Role must be 'admin', 'organizer', or 'attendee'.")]
    public string Role { get; set; } = null!;

    [DataType(DataType.DateTime)]
    [Display(Name = "Created At")]
    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}

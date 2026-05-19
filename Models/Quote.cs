using System;
using System.ComponentModel.DataAnnotations;

namespace WisdomWall.Models;

public class Quote
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The quote text is required.")]
    [StringLength(500, ErrorMessage = "The quote cannot be longer than 500 characters.")]
    public string Text { get; set; } = string.Empty;

    [Required(ErrorMessage = "The author is required.")]
    [StringLength(100, ErrorMessage = "The author's name cannot be longer than 100 characters.")]
    public string Author { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

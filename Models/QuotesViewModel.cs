using System.Collections.Generic;

namespace WisdomWall.Models;

public class QuotesViewModel
{
    public List<Quote> Quotes { get; set; } = new();
    
    // For binding the form to add a new quote
    public Quote NewQuote { get; set; } = new();

    // Pagination details
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; } = 5; // Display 5 quotes per page
    public int TotalQuotes { get; set; }

    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}

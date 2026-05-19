using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quotes.com.Data;
using Quotes.com.Models;

namespace Quotes.com.Controllers;

public class HomeController : Controller
{
    private readonly QuotesDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(QuotesDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int page = 1)
    {
        if (page < 1) page = 1;
        
        int pageSize = 5;
        var totalQuotes = await _context.Quotes.CountAsync();
        var totalPages = (int)Math.Ceiling(totalQuotes / (double)pageSize);
        
        if (totalPages > 0 && page > totalPages) page = totalPages;

        var quotes = await _context.Quotes
            .OrderByDescending(q => q.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var model = new QuotesViewModel
        {
            Quotes = quotes,
            CurrentPage = page,
            TotalPages = totalPages == 0 ? 1 : totalPages,
            TotalQuotes = totalQuotes,
            PageSize = pageSize,
            NewQuote = new Quote()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(QuotesViewModel model)
    {
        // Strip NewQuote from model prefix validation if binding is done directly to NewQuote
        // In MVC, when binding QuotesViewModel, ModelState will check validation for model.NewQuote
        // If we clear ModelState errors that are not related to NewQuote, it's safer.
        // But since QuotesViewModel only has NewQuote as an editable property, it should be fine.
        
        // Remove validation errors for properties other than NewQuote
        foreach (var key in ModelState.Keys.ToList())
        {
            if (!key.StartsWith("NewQuote."))
            {
                ModelState.Remove(key);
            }
        }

        if (ModelState.IsValid)
        {
            model.NewQuote.CreatedAt = DateTime.UtcNow;
            _context.Quotes.Add(model.NewQuote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // If validation fails, rebuild the view model to render the page with errors
        int page = 1;
        int pageSize = 5;
        var totalQuotes = await _context.Quotes.CountAsync();
        var totalPages = (int)Math.Ceiling(totalQuotes / (double)pageSize);

        model.Quotes = await _context.Quotes
            .OrderByDescending(q => q.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        model.CurrentPage = page;
        model.TotalPages = totalPages == 0 ? 1 : totalPages;
        model.TotalQuotes = totalQuotes;
        model.PageSize = pageSize;

        return View("Index", model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

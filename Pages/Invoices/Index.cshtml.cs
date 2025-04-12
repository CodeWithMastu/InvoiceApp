using InvoiceApp.Models;
using InvoiceApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceApp.Pages.Invoices
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
		
        public List<Invoice> invoiceList = new();

		[BindProperty(SupportsGet = true)]
		public string? SearchTerm { get; set; }

		[BindProperty(SupportsGet = true)]
		public int PageNumber { get; set; } = 1;

        [BindProperty(SupportsGet =true)]
		public int PageSize { get; set; } = 10;
		public int TotalPages { get; set; }

		public void OnGet()
        {
            invoiceList = _context.Invoices.OrderByDescending(i => i.Id).ToList();

            // For the search bar
            var query = _context.Invoices.AsQueryable();
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(i => i.ClientName != null && i.ClientName.ToLower().Contains(SearchTerm) || 
                i.Number != null && i.Number.ToString().Contains(SearchTerm));
            }
            invoiceList = query.OrderByDescending(i => i.Id).ToList();

			// Pagination
			int totalRecords = query.Count();
			TotalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);

			invoiceList = query
				.OrderByDescending(i => i.Id)
				.Skip((PageNumber - 1) * PageSize)
				.Take(PageSize)
				.ToList();

		}
        public IActionResult OnPostDelete(int id) 
        {
            var invoice = _context.Invoices.FirstOrDefault(i => i.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }
			_context.Remove(invoice);
			_context.SaveChanges();
			return RedirectToPage("/Invoices/Index");
        }

        // For the Details button
        public JsonResult OnGetDetails(int id) 
        {
            var invoice = _context.Invoices.FirstOrDefault(i => i.Id == id);
            if (invoice == null)
            {
                return new JsonResult(new {success = false, message = "Invoice not found"});
            }

            return new JsonResult(new
            { 
                success = true,
                data = new
                {
                    invoice.Id,
                    invoice.Number,
                    invoice.ClientName,
                    invoice.Email,
                    invoice.Phone,
                    invoice.Address,
                    invoice.Status,
                    invoice.Service,
                    invoice.Quantity,
                    invoice.UnitPrice,
                    Total = invoice.Quantity * invoice.UnitPrice,
                    invoice.IssueDate,
                    invoice.DueDate
                }
            });
        }
    }
}

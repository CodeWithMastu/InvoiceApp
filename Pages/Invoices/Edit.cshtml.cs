using InvoiceApp.Models;
using InvoiceApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceApp.Pages.Invoices
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public InvoiceDto InvoiceDto { get; set; } = new InvoiceDto();
        public Invoice Invoice { get; set; } = new();

		private readonly AppDbContext _context;

		public EditModel(AppDbContext context)
        {
			_context = context;
		}
        public IActionResult OnGet(int id)
        {
            var invoice = _context.Invoices.FirstOrDefault(x => x.Id == id);
            if (invoice == null) 
            {
                return RedirectToPage("/Invoices/Index");
            }
            Invoice = invoice;

            InvoiceDto.Number = invoice.Number;
            InvoiceDto.Status = invoice.Status;
            InvoiceDto.IssueDate = invoice.IssueDate;
            InvoiceDto.DueDate = invoice.DueDate;

            InvoiceDto.Service = invoice.Service;
            InvoiceDto.UnitPrice = invoice.UnitPrice;
            InvoiceDto.Quantity = invoice.Quantity;

            InvoiceDto.ClientName = invoice.ClientName;
            InvoiceDto.Address = invoice.Address;
            InvoiceDto.Email = invoice.Email;
            InvoiceDto.Phone = invoice.Phone;

            return Page();
        }
        public string successMessage = "";
		public IActionResult OnPost(int id)
        {
			var invoice = _context.Invoices.FirstOrDefault(x => x.Id == id);
			if (invoice == null)
			{
				return RedirectToPage("/Invoices/Index");
			}
			Invoice = invoice;
            if (!ModelState.IsValid) 
            {
                return Page();
            }
			invoice.Number = InvoiceDto.Number;
			invoice.Status = InvoiceDto.Status;
			invoice.IssueDate = InvoiceDto.IssueDate;
			invoice.DueDate = InvoiceDto.DueDate;

			invoice.Service = InvoiceDto.Service;
			invoice.UnitPrice = InvoiceDto.UnitPrice;
			invoice.Quantity = InvoiceDto.Quantity;

			invoice.ClientName = InvoiceDto.ClientName;
			invoice.Address = InvoiceDto.Address;
			invoice.Email = InvoiceDto.Email;
			invoice.Phone = InvoiceDto.Phone;

			_context.SaveChanges();

            successMessage = "Invoice Updated Successfully";
			return Page();
		}
    }
}

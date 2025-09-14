using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApiSample.Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string CustomerRuc { get; set; }
        public string CustomerName { get; set; }
        public string Currency { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public List<InvoiceDetail> Details { get; set; } = new();
    }
}

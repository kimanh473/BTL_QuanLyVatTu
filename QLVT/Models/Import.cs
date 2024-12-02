using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace QLVT.Models
{
    public class Import
    {
        [Key]
        public int id { get; set; }

        [MaxLength(100)]
        public string doc_id { get; set; } = "";

        public DateTime import_date { get; set; } = DateTime.Now;

        [MaxLength(100)]
        public string product_id { get; set; } = "";

        [MaxLength(100)]
        public string product_name { get; set; } = "";

        [MaxLength(100)]
        public string product_type { get; set; } = "";

        public int quantity { get; set; }

        [MaxLength(100)]
        public string currency { get; set; } = "";
     
        public long import_price { get; set; }
        
        public long total { get; set; }

        [MaxLength(100)]
        public string deliverer { get; set; } = "";

        [MaxLength(100)]
        public string receiver { get; set; } = "";

        [MaxLength(5000)]
        public string note { get; set; } = "";

    }
}

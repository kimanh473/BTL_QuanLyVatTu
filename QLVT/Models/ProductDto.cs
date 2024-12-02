using System.ComponentModel.DataAnnotations;

namespace QLVT.Models
{
    public class ProductDto
    {
        [Display(Name = "Mã chứng từ")]
        [Required(ErrorMessage = "{0} là bắt buộc."), MaxLength(100)]
        public string doc_id { get; set; } = "";

        [Display(Name ="Mã vật tư")]
        [Required(ErrorMessage = "{0} là bắt buộc."), MaxLength(100)]
        public string product_id { get; set; } = "";

        [Display(Name = "Tên vật tư")]
        [Required(ErrorMessage = "{0} là bắt buộc."), MaxLength(100)]
        public string product_name { get; set; } = "";

        [Display(Name = "Loại vật tư")]
        [Required(ErrorMessage = "{0} là bắt buộc."), MaxLength(100)]
        public string product_type { get; set; } = "";

        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "{0} là bắt buộc.")]
        public int quantity { get; set; }

        [Display(Name = "Đơn vị sản phẩm")]
        [Required(ErrorMessage = "{0} là bắt buộc."), MaxLength(100)]
        public string currency { get; set; } = "";

        [Display(Name = "Giá nhập")]
        [Required(ErrorMessage = "{0} là bắt buộc.")]
        public long import_price { get; set; }

        public long total { get; set; }

        [Display(Name = "Người giao")]
        [Required(ErrorMessage = "{0} là bắt buộc."), MaxLength(100)]
        public string deliverer { get; set; } = "";

        [Display(Name = "Người nhận")]
        [Required(ErrorMessage = "{0} là bắt buộc."), MaxLength(100)]
        public string receiver { get; set; } = "";

        [Display(Name = "Mô tả/Ghi chú")]
        [Required(ErrorMessage = "{0} là bắt buộc."), MaxLength(5000)]
        public string note { get; set; } = "";


    }
}

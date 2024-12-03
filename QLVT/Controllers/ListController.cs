using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLVT.Models;
using QLVT.Services;
using System.Text;

namespace QLVT.Controllers
{
    public class ListController : Controller
    {
        public readonly ApplicationDbContext context;
        public ListController(ApplicationDbContext context)
        {
            this.context = context;
        }

        ////Lấy dữ liệu lên view ListProduct kèm bộ lọc Tìm kiếm
        public async Task<IActionResult> ListProduct(string searchString)
        {
            var products = await context.ListProduct.OrderByDescending(p => p.id).ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(n => n.product_name.Contains(searchString)
                    || n.product_id.Contains(searchString)).ToList();
            }
            return View(products);
        }

        //Xuất file excel
        [HttpPost]
        public IActionResult ExportToExcel(string htmlTable)
        {
            if (string.IsNullOrWhiteSpace(htmlTable))
            {
                return BadRequest("Không có dữ liệu để xuất.");
            }

            string excelContent = $@"
            <html xmlns:x=""urn:schemas-microsoft-com:office:excel"">
            <head>
                <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"">
            </head>
            <body>
                {htmlTable}
            </body>
            </html>";

            // Chuyển đổi chuỗi thành byte array
            var byteArray = Encoding.UTF8.GetBytes(excelContent);

            // Trả về file Excel
            return File(byteArray, "application/vnd.ms-excel", "DSTonKho.xls");
        }

        //Lấy dữ liệu lên view ListImport
        public async Task<IActionResult> ListImport()
        {
            var imports = await context.ListImport.OrderByDescending(p => p.id).ToListAsync();
            return View(imports);
        }

        //Lấy dữ liệu lên view ListExport
        public async Task<IActionResult> ListExport()
        {
            var exports = await context.ListExport.OrderByDescending(p => p.id).ToListAsync();
            return View(exports);
        }
        public IActionResult Import()
        {
            return View();
        }

        public async Task<IActionResult> Export()
        {
            var productList = await context.ListProduct
                .Select(p => new { p.product_id })
                .ToListAsync();

            ViewBag.ProductList = productList;

            return View();
        }

    //Lấy dữ liệu từ product_id đã chọn lên Export
        [HttpGet]
        public async Task<IActionResult> GetProductDetails(string product_id_detail)
        {
            if (string.IsNullOrEmpty(product_id_detail))
                return BadRequest("Product ID không hợp lệ.");

            var product = await context.ListProduct
                .Where(p => p.product_id == product_id_detail)
                .Select(p => new
                {
                    p.product_id,
                    p.product_name,
                    p.product_type,
                    p.quantity,
                    p.currency
                })
                .FirstOrDefaultAsync();

            if (product == null)
                return NotFound("Không tìm thấy sản phẩm.");
            
            return Json(product);
        }


        [HttpPost]
        public async Task<IActionResult> Export(ProductDtoex model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProductList = await context.ListProduct.ToListAsync();
                return View(model);
            }
            // Lưu thông tin lịch sử xuất kho vào ListExport
            Export export = new Export()
            {
                doc_id = model.doc_id,
                export_date = DateTime.Now,
                product_id = model.product_id,
                product_name = model.product_name,
                product_type = model.product_type,
                quantity = model.quantity,
                currency = model.currency,
                export_price = model.export_price,
                total = model.export_price * model.quantity, // Tính tổng
                promoter = model.promoter,
                receiver = model.receiver,
                note = model.note
            };

            context.ListExport.Add(export);

            // Cập nhật số lượng và thành tiền cho ListProduct
            var product = await context.ListProduct
                .FirstOrDefaultAsync(p => p.product_id == model.product_id);

            if (product != null)
            {
                
                product.quantity -= model.quantity; // Giảm số lượng trong kho
                product.total = product.import_price * product.quantity; // Cập nhật thành tiền
                context.ListProduct.Update(product);
            }
            else
            {
                ModelState.AddModelError("", "Không tìm thấy sản phẩm.");
                ViewBag.ProductList = await context.ListProduct.ToListAsync();
                return View(model);
            }

            context.SaveChanges();
            return RedirectToAction("ListProduct", "List");
        }


        //Lưu thông tin nhập kho vào 2 bảng ListProduct và ListImport
        [HttpPost]
        public IActionResult Import(ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return View(productDto);
            }
            Product product = new Product()
            {
                import_date = DateTime.Now,
                product_id = productDto.product_id,
                product_name = productDto.product_name,
                product_type = productDto.product_type,
                quantity = productDto.quantity,
                currency = productDto.currency,
                import_price = productDto.import_price,
                total = productDto.total,
            };
            context.ListProduct.Add(product);
            Import import = new Import()
            {
                doc_id = productDto.doc_id,
                import_date = DateTime.Now,
                product_id = productDto.product_id,
                product_name = productDto.product_name,
                product_type = productDto.product_type,
                quantity = productDto.quantity,
                currency = productDto.currency,
                import_price = productDto.import_price,
                total = productDto.total,
                deliverer = productDto.deliverer,
                receiver = productDto.receiver,
                note = productDto.note,
            };
            context.ListImport.Add(import);
            context.SaveChanges();
            return RedirectToAction("ListProduct", "List");
        }

    //Xóa 1sp khỏi ListProduct
        public IActionResult Delete(int id)
        {
            var product = context.ListProduct.Find(id);
            if (product == null)
            {
                return RedirectToAction("ListProduct", "List");
            }
            context.ListProduct.Remove(product);
            context.SaveChanges(true);
            return RedirectToAction("ListProduct", "List");
        }




    }
}

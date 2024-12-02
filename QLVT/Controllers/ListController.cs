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

        public async Task<IActionResult> ListImport()
        {
            var imports = await context.ListImport.OrderByDescending(p => p.id).Take(5).ToListAsync();
            return View(imports);
        }

        public async Task<IActionResult> ListExport()
        {
            var exports = await context.ListExport.OrderByDescending(p => p.id).Take(5).ToListAsync();
            return View(exports);
        }
        public IActionResult Import()
        {
            return View();
        }
        public IActionResult Export()
        {
            return View();
        }
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

        [HttpPost]
        public IActionResult Export(ProductDtoex productDtoex)
        {
            if (!ModelState.IsValid)
            {
                return View(productDtoex);
            }

            Product product = new Product()
            {
                import_date = DateTime.Now,
                product_id = productDtoex.product_id,
                product_name = productDtoex.product_name,
                product_type = productDtoex.product_type,
                quantity = productDtoex.quantity,
                currency = productDtoex.currency,
                import_price = productDtoex.export_price,
                total = productDtoex.total,
            };
            context.ListProduct.Add(product);

            Export export = new Export()
            {
                doc_id = productDtoex.doc_id,
                export_date = DateTime.Now,
                product_id = productDtoex.product_id,
                product_name = productDtoex.product_name,
                product_type = productDtoex.product_type,
                quantity = productDtoex.quantity,
                currency = productDtoex.currency,
                export_price = productDtoex.export_price,
                total = productDtoex.total,
                promoter = productDtoex.promoter,
                receiver = productDtoex.receiver,
                note = productDtoex.note,
            };
            context.ListExport.Add(export);

            context.SaveChanges();

            return RedirectToAction("ListProduct", "List");
        }
    } 
}

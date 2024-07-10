using Bazaar.Models.AllDbContexts;
using Microsoft.AspNetCore.Mvc;
using Bazaar.Models.ViewModels;
using Bazaar.Models.DbModels;
using Microsoft.EntityFrameworkCore;

public class ProductController(ProductDbContext pdb): Controller{
    private readonly ProductDbContext _productDb = pdb;

    [HttpGet]
    public IActionResult AddProduct(){
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductViewModel product){
        if(ModelState.IsValid && (product.ImageFileName !=null)){
            string filename = Guid.NewGuid().ToString()+"_"+product.ImageFileName.FileName;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", filename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await product.ImageFileName.CopyToAsync(stream);
            }

            // string folder = Path.Combine(env.WebRootPath,"productImages");
            // string filePath = Path.Combine(folder, product.ImageFileName.FileName);
            // await product.ImageFileName.CopyToAsync(new FileStream(filePath, FileMode.Create));
            Product newProduct = new(){
                Category=product.Category,
                Name=product.Name,
                Price=product.Price,
                Description=product.Description,
                ImageFileName=filename,
                Date = DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss")
            };
            await _productDb.Products.AddAsync(newProduct);
            await _productDb.SaveChangesAsync();          
            return RedirectToAction("Index", "Home");
        }
        return View(product);
    }
    [HttpGet]
    public async Task<IActionResult> ListProduct(){
        return View(await _productDb.Products.ToListAsync());
    }
    [HttpGet]
    public async Task<IActionResult> EditProduct(Guid id){
        var product =await _productDb.Products.FindAsync(id);
        if(product != null){
            return View(product);
        }
        return RedirectToAction("ListProduct", "Product");
    }

    [HttpPost]
    public async Task<IActionResult> EditProduct(Product product){
        if(ModelState.IsValid){
            var productEdit =await _productDb.Products.FindAsync(product.Id);
            if(productEdit != null){
                productEdit.Category = product.Category;
                productEdit.Name = product.Name;
                productEdit.Price = product.Price;
                productEdit.Description = product.Description;
                await _productDb.SaveChangesAsync();
                return RedirectToAction("ListProduct","Product");
            }
            return RedirectToAction("ListProduct","Product");
        }
        return View(product);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteProduct(Guid Id){
        var product = await _productDb.Products.FindAsync(Id);
        if(product != null){
            _productDb.Products.Remove(product);
            await _productDb.SaveChangesAsync();
            return RedirectToAction("ListProduct","Product");
        }
        return Content("Could not delete the product");
    }

    [HttpGet]
    public async Task<IActionResult> ViewProduct(Guid Id){
        var product = await _productDb.Products.FindAsync(Id);
        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> BuyProduct(Guid Id){
        var product = await _productDb.Products.FindAsync(Id);
        return View(product);
    }
}
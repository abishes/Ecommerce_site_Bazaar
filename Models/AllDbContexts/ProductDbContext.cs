
using Bazaar.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Bazaar.Models.AllDbContexts;

public class ProductDbContext(DbContextOptions options) : DbContext(options){
    public DbSet<Product> Products{get;set;}
}

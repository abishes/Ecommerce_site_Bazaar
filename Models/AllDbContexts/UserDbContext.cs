using Bazaar.Models.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bazaar.Models.AllDbContexts;
public class UserDbContext(DbContextOptions<UserDbContext> options) : IdentityDbContext<User>(options){
}

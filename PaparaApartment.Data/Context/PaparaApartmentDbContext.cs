using PaparaApartment.Core.Entities.Concrete;
using PaparaApartment.Entity.Concrete;
using Microsoft.EntityFrameworkCore;



namespace PaparaApartment.Data.Context
{
    public partial class PaparaApartmentDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=BESTEKARAGOZ;Database=PaparaApartmentDb;Trusted_Connection=True;");
            }
        }

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<ApartmentExpense> ApartmentExpenses { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }

    }
}

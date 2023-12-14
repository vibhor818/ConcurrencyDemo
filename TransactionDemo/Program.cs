using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using (VBContext context = new VBContext())
{
    //var data = context.ProductInfos.FromSqlInterpolated($"exec dbo.VBProc").ToList();


    //foreach (var item in data)
    //{
    //    Console.WriteLine(item.Name);
    //}

    //context.Database.ExecuteSqlRaw(@"CREATE View view_ProductCount AS 
    //                                SELECT p.Name,Count(p.Id) as ProductCount
    //                                FROM ProductInfos p
    //                                GROUP BY p.Name");

    //    var productsToUpdat = new List<ProductInfo>
    //    {
    //        new ProductInfo{Id=3,Name="TV",Price=4000},
    //        new ProductInfo{Id=2,Name="Mobile",Price=000},
    //    };

    //var success = await UpdateTOProducts(productsToUpdat);
    //    if (success)
    //    {
    //        Console.WriteLine("done...");
    //    }
    //    else
    //    {
    //        Console.WriteLine("transaction failed");
    //    }


    //    async Task<bool> UpdateTOProducts(List<ProductInfo> productsToUpdat)
    //    {
    //        using (var transaction =await context.Database.BeginTransactionAsync())
    //        {
    //            try
    //            {
    //                foreach (var product in productsToUpdat)
    //                {
    //                    await context.ProductInfos.AddAsync(product);
    //                }

    //                await context.SaveChangesAsync();
    //                await transaction.CommitAsync();
    //                return true;
    //            }
    //            catch (Exception)
    //            {
    //                await transaction.RollbackAsync();
    //                return false;
    //            }
    //        }
    //    }

    var pCount = context.ProductModelViews.ToList();

    foreach (var item in pCount)
    {
        Console.WriteLine(item.Name+"   "+item.ProductCount);
    }
    context.
       
};
public class VBContext : DbContext
{
    public DbSet<ProductInfo> ProductInfos { get; set; }
    public DbSet<ProductModelView> ProductModelViews { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"data source=(localdb)\mssqllocaldb;database=concurrentdb;trusted_connection=true");

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductModelView>().HasNoKey().ToView("view_ProductCount");
    }
}
public class ProductInfo
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
   

    //nosql
    //[ConcurrencyCheck]
    //public Guid MyProperty { get; set; }
}
public class ProductModelView
{
    public string Name { get; set; }

    public int ProductCount { get; set; }
}
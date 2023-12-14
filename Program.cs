
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;

using(VBContext context=new VBContext())
{
    //store win
    //try
    //{
    //    var pro = await context.Products.FindAsync(1);
    //    pro.Name = "Mobile";
    //    await context.SaveChangesAsync();
    //    Console.WriteLine("Changes saved successfully.");
    //}
    //catch (DbUpdateConcurrencyException ex)
    //{
    //    foreach (var entry in ex.Entries)
    //    {
    //        //var databaseValues = await entry.GetDatabaseValuesAsync();
    //        //var clientValues = entry.CurrentValues.Clone();

    //        //entry.OriginalValues.SetValues(databaseValues);
    //        //await context.SaveChangesAsync();
    //        Console.WriteLine("Server wins, changes done.");
    //    }

    //}
    //client wins

    try
    {
        var pro = await context.Products.FindAsync(1);
        pro.Name = "TV";
        await context.SaveChangesAsync();
        Console.WriteLine("Changes saved successfully.");
    }
    catch (DbUpdateConcurrencyException ex)
    {
        foreach (var entry in ex.Entries)
        {
            var databaseValues = await entry.GetDatabaseValuesAsync();
            if (databaseValues == null)
            {
                Console.WriteLine("record deleted");
            }
            else
            {
                entry.OriginalValues.SetValues(databaseValues);
                await context.SaveChangesAsync();
                Console.WriteLine("Cleint wins, changes done.");
            }
        }

     }
}

public class VBContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"data source=(localdb)\mssqllocaldb;database=concurrentdb;trusted_connection=true");

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.Timestamp)
            .IsRowVersion();
    }
    public DbSet<Product> Products { get; set; }
}
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    [Timestamp]
    public byte[] Timestamp { get; set; }

    //nosql
    //[ConcurrencyCheck]
    //public Guid MyProperty { get; set; }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NapovedaPomoc;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NapovedaPomoc
{
    public class TractebelContext(DbContextOptions options): DbContext(options)
    {
        public virtual DbSet<FluidState> FluidState { get; set; }
        public virtual DbSet<Kolony> Kolony { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<PedfluidHazard> PedfluidHazard { get; set; }
        public virtual DbSet<Umisteni> Umisteni { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if(!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer("Data Source=W10177552\\MARTIN74;Initial Catalog=Tractebel;User ID=www;Password=www");
            }
        }

        //Vztahy mezi tabulkami
        protected override async void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<FluidState>().HasOne(m => m.id).WithMany(x => x.)
            modelBuilder.Entity<Media>().HasOne(m => m.FluidState).WithMany(x => x.ItemCz).HasForeignKey(m => m.id);
            modelBuilder.Entity<Media>().HasOne(m => m.FluidStateId).WithMany(x => x.);
            modelBuilder.Entity<Medie>()
            OnModelCreatingPartial(modelBuilder);
        }

        //pro rozšíření v jiném souboru
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

    //Pro Identity přidat Nuget Microsoft.AspNetCore.Identity.EntityFrameworkCore
    //použití IdentityDbContext pro přístup k tabulkám identity
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
    }

    var IP = Environment.MachineName;
    bool Doma = false;
    if (IP == "MARTIN")
    {
        Doma = true;
    }
    string pattern = "sjhskhladskjgl6sdf5";

    string replacement = string.Empty;
    //Vzor pro hledání (například datum) pokud nechci změnu nesmí být nalezeno tedy např "sjhskhladskjgl6sdf5"
    //co se má hledat ve vzoru.
    if (IP == "W5CD344HXYH")
    {
        replacement = builder.Configuration.GetConnectionString("ZamenaVzoru") ?? string.Empty;
        pattern = builder.Configuration.GetConnectionString("HledanyVzor") ?? string.Empty;
    }

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        var con = builder.Configuration.GetConnectionString(Doma ? "DomaLoginSql" : "PraceLoginSql") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        con = Regex.Replace(con, pattern, replacement);
        options.UseSqlServer(con);
    });

}

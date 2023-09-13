using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NapovedaPomoc
{
    public class TractebelContext : DbContext
    {
        public TractebelContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<FluidState> FluidState { get; set; }
    public virtual DbSet<Kolony> Kolony { get; set; }
    public virtual DbSet<Media> Media { get; set; }
    public virtual DbSet<PedfluidHazard> PedfluidHazard { get; set; }
    public virtual DbSet<Umisteni> Umisteni { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //if (!optionsBuilder.IsConfigured)
    //{
    //    optionsBuilder.UseSqlServer("Data Source=W10177552\\MARTIN74;Initial Catalog=Tractebel;User ID=www;Password=www");
    //}
    //}

    protected override async void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<FluidState>().HasOne(m => m.id).WithMany(x => x.)
        //modelBuilder.Entity<Media>().HasOne(m => m.FluidState).WithMany(x=>x.ItemCz).HasForeignKey(m => m.id);
        //modelBuilder.Entity<Media>().HasOne(m => m.FluidStateId).WithMany(x => x.);
        //modelBuilder.Entity<Medie>()
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

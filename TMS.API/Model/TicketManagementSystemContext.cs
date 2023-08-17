using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TMS.API.Model;

public partial class TicketManagementSystemContext : DbContext
{
    public TicketManagementSystemContext()
    {
    }

    public TicketManagementSystemContext(DbContextOptions<TicketManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<TicketCategory> TicketCategories { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-08FV6VI\\SQLEXPRESS;Initial Catalog=Ticket_Management_System;Integrated Security=True;TrustServerCertificate=True;encrypt=false;").UseLazyLoadingProxies();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B87124540B");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Event__7944C870C2B94B74");

            entity.ToTable("Event");

            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.EventDescription)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EventName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EventTypeId).HasColumnName("EventTypeID");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.VenueId).HasColumnName("VenueID");

            entity.HasOne(d => d.EventType).WithMany(p => p.Events)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_event_event_type");

            entity.HasOne(d => d.Venue).WithMany(p => p.Events)
                .HasForeignKey(d => d.VenueId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_event_venue");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("PK__EventTyp__A9216B1FEC2538D1");

            entity.ToTable("EventType");

            entity.Property(e => e.EventTypeId).HasColumnName("EventTypeID");
            entity.Property(e => e.EventTypeName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF6B00EB59");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.OrderedAt).HasColumnType("datetime");
            entity.Property(e => e.TicketCategoryId).HasColumnName("TicketCategoryID");
            entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_orders_customers");

            entity.HasOne(d => d.TicketCategory).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TicketCategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_orders_ticketCategory");
        });

        modelBuilder.Entity<TicketCategory>(entity =>
        {
            entity.HasKey(e => e.TicketCategoryId).HasName("PK__TicketCa__C84589C657318814");

            entity.ToTable("TicketCategory");

            entity.Property(e => e.TicketCategoryId).HasColumnName("TicketCategoryID");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EventId).HasColumnName("EventID");

            entity.HasOne(d => d.Event).WithMany(p => p.TicketCategories)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ticket_category_event");
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(e => e.VenueId).HasName("PK__Venue__3C57E5D289492BE2");

            entity.ToTable("Venue");

            entity.Property(e => e.VenueId).HasColumnName("VenueID");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}


using MySql.Data.Entity;

namespace JJETS.Pos.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using MySql.Data.MySqlClient;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Migrations;

    public partial class Context : DbContext
    {

        public Context() : base("name=Offline")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>("name=Offline"));
            Database.CreateIfNotExists();
        }

        public Context(string conString) : base(conString)
        {

        }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<TransactionItem> TransactionItems { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }


    }
}

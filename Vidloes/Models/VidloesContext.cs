using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Vidloes.Models
{
    public class VidlyoesContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Genre> Genre { get; set; }

        public VidlyoesContext() : base("name=DBCS")
        {

        }
    }
}
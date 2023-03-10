using diseaseAPI_DotNet6.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace diseaseAPI_DotNet6
{
    public class diseaseDbContext : DbContext
    {

        public diseaseDbContext(DbContextOptions<diseaseDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Disease> Diseases { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Doctor_info> Doctors { get; set; }



        
    }

    

    
    
    

   

}


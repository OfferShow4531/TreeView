using TreeView.Models;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;


namespace TreeView.Data
{
    public class CharacterDbContext : DbContext
    {
        public CharacterDbContext(DbContextOptions<CharacterDbContext> options) : base(options)
        {
        }

        public DbSet<CharacterType> CharacterType { get; set; }
        public DbSet<CharacterSubType> CharacterSubType { get; set; }

        
    }
}

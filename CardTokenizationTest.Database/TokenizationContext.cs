using System;
using Microsoft.EntityFrameworkCore;

namespace CardTokenizationTest.Database
{
    public class TokenizationContext : DbContext
    {
        public TokenizationContext(DbContextOptions<TokenizationContext> options) : base(options)
        {
        }

        public virtual DbSet<CardTokenizationTest.Database.Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}


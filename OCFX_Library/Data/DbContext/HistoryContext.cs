using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCFX.Data.Context
{
    public class HistoryContext : DbContext
    {
        public HistoryContext(DbContextOptions<HistoryContext> options) : base(options)
        {

        }

        public DbSet<HistoryModels.History> Histories { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseContext
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<DBContextClass>
    {
        public DBContextClass CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DBContextClass>();
            string databasePath = Path.Combine("..", "eUcionicaDB.db");
            optionsBuilder.UseSqlite($"Data Source = { databasePath}");

            return new DBContextClass(optionsBuilder.Options);
        }
    }
}

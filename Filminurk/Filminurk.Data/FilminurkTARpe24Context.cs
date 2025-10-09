using Filminurk.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.Data
{
    public class FilminurkTARpe24Context : DbContext
    {
        public DbSet<Movie> Moves { get; set; }
    }
}
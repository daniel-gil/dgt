using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace DGT.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        DgtDbContext Init(DbContextOptions<DgtDbContext> options);
    }
}


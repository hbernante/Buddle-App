using System;
using Buddle.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Buddle.Data;

public class BuddleDbContext : IdentityDbContext<User>
{
    public BuddleDbContext(DbContextOptions options) : base(options)
    {
    }
}

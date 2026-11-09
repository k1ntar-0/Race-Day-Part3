using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Models;

namespace RaceDay.Api.Data
{
    public class RaceDayDbContext : DbContext
    {
        public RaceDayDbContext(DbContextOptions<RaceDayDbContext> options) : base(options) { }

        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<EventCategory> EventCategories => Set<EventCategory>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<EventEnrolment> EventEnrolments => Set<EventEnrolment>();
        public DbSet<Result> Results => Set<Result>();
    }
}
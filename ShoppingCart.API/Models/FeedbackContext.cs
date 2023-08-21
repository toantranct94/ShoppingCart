using Microsoft.EntityFrameworkCore;

namespace ShoppingCart.API;

public class FeedbackContext : DbContext
{
    public FeedbackContext(DbContextOptions<FeedbackContext> options)
        : base(options)
    {
    }

    public DbSet<FeedbackItem> FeedbackItems { get; set; } = null!;
}
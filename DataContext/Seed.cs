
using Microsoft.AspNetCore.Identity;
using sosialClone;


namespace Context
{
    public class Seed
    {

        // with usermanager the api manages users in stores
        public static async Task SeedData(DataContext context, UserManager<user> userManager)
        {
            if (!userManager.Users.Any())
            {

                var u = new user { DisplayName = "Bob", UserName = "bob", Email = "bob@test.com" };
                   

               await userManager.CreateAsync(u, "Pa$$w0rd");
                
            }

            if (context.entities.Any()) return;

            var activities = new List<Entities>
            {
                new Entities
                {
                    Title = "Past Activity 1",
                    Date = DateTime.UtcNow.AddMonths(-2),
                    Description = "Activity 2 months ago",
                    Catagory = "drinks",
                    City = "London",
                    Venue = "Pub",
                },
                new Entities
                {
                    Title = "Past Activity 2",
                    Date = DateTime.UtcNow.AddMonths(-1),
                    Description = "Activity 1 month ago",
                    Catagory = "culture",
                    City = "Paris",
                    Venue = "Louvre",
                },
                new Entities
                {
                    Title = "Future Activity 1",
                    Date = DateTime.UtcNow.AddMonths(1),
                    Description = "Activity 1 month in future",
                    Catagory = "culture",
                    City = "London",
                    Venue = "Natural History Museum",
                },
                new Entities
                {
                    Title = "Future Activity 2",
                    Date = DateTime.UtcNow.AddMonths(2),
                    Description = "Activity 2 months in future",
                    Catagory = "music",
                    City = "London",
                    Venue = "O2 Arena",
                },
                new Entities
                {
                    Title = "Future Activity 3",
                    Date = DateTime.UtcNow.AddMonths(3),
                    Description = "Activity 3 months in future",
                    Catagory = "drinks",
                    City = "London",
                    Venue = "Another pub",
                },
                new Entities
                {
                    Title = "Future Activity 4",
                    Date = DateTime.UtcNow.AddMonths(4),
                    Description = "Activity 4 months in future",
                    Catagory = "drinks",
                    City = "London",
                    Venue = "Yet another pub",
                },
                new Entities
                {
                    Title = "Future Activity 5",
                    Date = DateTime.UtcNow.AddMonths(5),
                    Description = "Activity 5 months in future",
                    Catagory = "drinks",
                    City = "London",
                    Venue = "Just another pub",
                },
                new Entities
                {
                    Title = "Future Activity 6",
                    Date = DateTime.UtcNow.AddMonths(6),
                    Description = "Activity 6 months in future",
                    Catagory = "music",
                    City = "London",
                    Venue = "Roundhouse Camden",
                },
                new Entities
                {
                    Title = "Future Activity 7",
                    Date = DateTime.UtcNow.AddMonths(7),
                    Description = "Activity 2 months ago",
                    Catagory = "travel",
                    City = "London",
                    Venue = "Somewhere on the Thames",
                },
                new Entities
                {
                    Title = "Future Activity 8",
                    Date = DateTime.UtcNow.AddMonths(8),
                    Description = "Activity 8 months in future",
                    Catagory = "film",
                    City = "London",
                    Venue = "Cinema",
                }
            };

            await context.entities.AddRangeAsync(activities);
            await context.SaveChangesAsync();
        }
    }
}
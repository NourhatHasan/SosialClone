
using Microsoft.AspNetCore.Identity;
using sosialClone;


namespace Context
{
    public class Seed
    {

        // with usermanager the api manages users in stores
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any() && !context.entities.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com"
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

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
                        Attendies = new List<EntityUser>
                        {
                            new EntityUser
                            {
                                AppUser = users[0],
                                isHost = true
                            }
                        }
                    },
                    new Entities
                    {
                        Title = "Past Activity 2",
                        Date = DateTime.UtcNow.AddMonths(-1),
                        Description = "Activity 1 month ago",
                        Catagory = "culture",
                        City = "Paris",
                        Venue = "The Louvre",
                        Attendies = new List<EntityUser>
                        {
                            new EntityUser
                            {
                                AppUser = users[0],
                                isHost = true
                            },
                            new EntityUser
                            {
                                AppUser = users[1],
                                isHost = false
                            },
                        }
                    },
                    new Entities
                    {
                        Title = "Future Activity 1",
                        Date = DateTime.UtcNow.AddMonths(1),
                        Description = "Activity 1 month in future",
                        Catagory = "music",
                        City = "London",
                        Venue = "Wembly Stadium",
                        Attendies= new List<EntityUser>
                        {
                            new EntityUser
                            {
                                AppUser = users[2],
                                isHost = true
                            },
                            new EntityUser
                            {
                                AppUser = users[1],
                                isHost = false
                            },
                        }
                    },
                    new Entities
                    {
                        Title = "Future Activity 2",
                        Date = DateTime.UtcNow.AddMonths(2),
                        Description = "Activity 2 months in future",
                       Catagory = "food",
                        City = "London",
                        Venue = "Jamies Italian",
                        Attendies = new List<EntityUser>
                        {
                            new EntityUser
                            {
                                AppUser = users[0],
                                isHost = true
                            },
                            new EntityUser
                            {
                                AppUser = users[2],
                                isHost = false
                            },
                        }
                    },
                    new Entities
                    {
                        Title = "Future Activity 3",
                        Date = DateTime.UtcNow.AddMonths(3),
                        Description = "Activity 3 months in future",
                        Catagory = "drinks",
                        City = "London",
                        Venue = "Pub",
                        Attendies = new List<EntityUser>
                        {
                            new EntityUser
                            {
                                AppUser = users[1],
                                isHost = true
                            },
                            new EntityUser
                            {
                                AppUser = users[0],
                                isHost = false
                            },
                        }
                    },
                    new Entities
                    {
                        Title = "Future Activity 4",
                        Date = DateTime.UtcNow.AddMonths(4),
                        Description = "Activity 4 months in future",
                        Catagory = "culture",
                        City = "London",
                        Venue = "British Museum",
                        Attendies = new List<EntityUser>
                        {
                            new EntityUser
                            {
                                AppUser = users[1],
                                isHost = true
                            }
                        }
                    },
                    new Entities
                    {
                        Title = "Future Activity 5",
                        Date = DateTime.UtcNow.AddMonths(5),
                        Description = "Activity 5 months in future",
                        Catagory = "drinks",
                        City = "London",
                        Venue = "Punch and Judy",
                        Attendies = new List<EntityUser>
                        {
                            new EntityUser
                            {
                                AppUser = users[0],
                                isHost = true
                            },
                            new EntityUser
                            {
                                AppUser = users[1],
                                isHost = false
                            },
                        }
                    },
                    new Entities
                    {
                        Title = "Future Activity 6",
                        Date = DateTime.UtcNow.AddMonths(6),
                        Description = "Activity 6 months in future",
                        Catagory = "music",
                        City = "London",
                        Venue = "O2 Arena",
                       Attendies= new List<EntityUser>
                        {
                            new EntityUser
                            {
                                AppUser = users[2],
                                isHost = true
                            },
                            new EntityUser
                            {
                                AppUser = users[1],
                                isHost = false
                            },
                        }
                    }


                };

                await context.entities.AddRangeAsync(activities);
                await context.SaveChangesAsync();
            }
        }
    }
}

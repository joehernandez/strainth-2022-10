using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Strainth.DataService.Entities.Setup;

namespace Strainth.DataService.Data.SeedData;

public static class PermanentData
{
    public static void SeedPermanentData(this ModelBuilder modelBuilder)
    {
        SeedCategories(modelBuilder);
        SeedExercises(modelBuilder);
    }

    private static void SeedCategories(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasData(
                new Category { Id = 1, Name = "Abs" },
                new Category { Id = 2, Name = "Calves" },
                new Category { Id = 3, Name = "Curl" },
                new Category { Id = 4, Name = "Extend" },
                new Category { Id = 5, Name = "Hinge" },
                new Category { Id = 6, Name = "Press" },
                new Category { Id = 7, Name = "Pull" },
                new Category { Id = 8, Name = "Push" },
                new Category { Id = 9, Name = "Row" },
                new Category { Id = 10, Name = "Shoulders" },
                new Category { Id = 11, Name = "Squat" }
            );
    }

    private static void SeedExercises(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exercise>()
           .HasData(
                new Exercise { Id = 1, Name = "Ab Wheel", CategoryId = 1 },
                new Exercise { Id = 2, Name = "Bicycle Crunch", CategoryId = 1 },

                new Exercise { Id = 3, Name = "Seated Barbell Calf Raise", CategoryId = 2 },
                new Exercise { Id = 4, Name = "Standing Calf Raise", CategoryId = 2 },

                new Exercise { Id = 5, Name = "Incline Dumbbell Curl", CategoryId = 3 },
                new Exercise { Id = 6, Name = "Dumbbell Spider Curl", CategoryId = 3 },

                new Exercise { Id = 7, Name = "Rope Pushdown", CategoryId = 4 },
                new Exercise { Id = 8, Name = "Standing OH Cable Extension", CategoryId = 4 },

                new Exercise { Id = 9, Name = "Deadlift - Sumo", CategoryId = 5 },
                new Exercise { Id = 10, Name = "Back Extension", CategoryId = 5 },
                new Exercise { Id = 11, Name = "Floor Hamstring Curl", CategoryId = 5 },
                new Exercise { Id = 12, Name = "Pull Through", CategoryId = 5 },

                new Exercise { Id = 13, Name = "Seated OH Press", CategoryId = 6 },
                new Exercise { Id = 14, Name = "Arnold Press", CategoryId = 6 },

                new Exercise { Id = 15, Name = "Chinup", CategoryId = 7 },
                new Exercise { Id = 16, Name = "Neutral Grip Pulldown", CategoryId = 7 },
                new Exercise { Id = 17, Name = "Underhand Cable Pullover", CategoryId = 7 },

                new Exercise { Id = 18, Name = "Incline Bench Press", CategoryId = 8 },
                new Exercise { Id = 19, Name = "Incline Dumbbell Press Fly", CategoryId = 8 },
                new Exercise { Id = 20, Name = "Close Grip Bench Press", CategoryId = 8 },

                new Exercise { Id = 21, Name = "Chest-supported Dumbbell Row", CategoryId = 9 },
                new Exercise { Id = 22, Name = "Cable Upright Row", CategoryId = 9 },

                new Exercise { Id = 23, Name = "Lateral Raise", CategoryId = 10 },
                new Exercise { Id = 24, Name = "Prone Rear Delt Raise", CategoryId = 10 },
                new Exercise { Id = 25, Name = "Skiers", CategoryId = 10 },

                new Exercise { Id = 26, Name = "Slantboard Front Squat", CategoryId = 11 },
                new Exercise { Id = 27, Name = "ATG Split Squat", CategoryId = 11 }
            );
    }
}
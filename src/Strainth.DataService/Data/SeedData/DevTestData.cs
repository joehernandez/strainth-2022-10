using Strainth.DataService.Entities.Programming;
using Strainth.DataService.Entities.Setup;

namespace Strainth.DataService.Data.SeedData
{
    public static class DevTestData
    {
        public static void SeedTestData(StrainthContext strainthContext)
        {
            SeedSetupData(strainthContext);
            SeedProgramming(strainthContext);
        }

        private static void SeedProgramming(StrainthContext strainthContext)
        {
            if (strainthContext.ProgramSplits.Any()) return;

            var progSplit = SeedProgramSplit(strainthContext);
            var (progDetail1, progDetail2) = SeedProgramDetails(strainthContext, progSplit.Id);

            SeedProgramExercises(strainthContext, progSplit, progDetail1, progDetail2);
        }

        private static void SeedProgramExercises(StrainthContext strainthContext, ProgramSplit progSplit, ProgramDetail progDetail1, ProgramDetail progDetail2)
        {
            var progExercise1_1 = new ProgramExercise
            {
                ProgramDetailId = progDetail1.Id,
                ExerciseId = 9, // Deadlift sumo
                RepsRangeLower = 5,
                RepsRangeUpper = 10,
                SetsRangeLower = 3,
                SetsRangeUpper = 3,
                RepsThreshold = 20,
                WeightIncrement = 10.0m
            };

            var progExercise1_2 = new ProgramExercise
            {
                ProgramDetailId = progDetail1.Id,
                ExerciseId = 10, // Back Extension
                RepsRangeLower = 10,
                RepsRangeUpper = 15,
                SetsRangeLower = 3,
                SetsRangeUpper = 3,
                RepsThreshold = 30,
                WeightIncrement = 5.0m
            };
            var progExercise2_1 = new ProgramExercise
            {
                ProgramDetailId = progDetail2.Id,
                ExerciseId = 18, // Incline Bench Press
                RepsRangeLower = 5,
                RepsRangeUpper = 10,
                SetsRangeLower = 3,
                SetsRangeUpper = 3,
                RepsThreshold = 22,
                WeightIncrement = 10.0m
            };
            var progExercise2_2 = new ProgramExercise
            {
                ProgramDetailId = progDetail2.Id,
                ExerciseId = 15, // Chinup
                RepsRangeLower = 5,
                RepsRangeUpper = 15,
                SetsRangeLower = 3,
                SetsRangeUpper = 3,
                RepsThreshold = 22,
                WeightIncrement = 10.0m
            };
            strainthContext.AddRange(progExercise1_1, progExercise1_2, progExercise2_1, progExercise2_2);
            strainthContext.SaveChanges();

            progDetail1.ProgramExercises.Add(progExercise1_1);
            progDetail1.ProgramExercises.Add(progExercise1_2);
            progDetail2.ProgramExercises.Add(progExercise2_1);
            progDetail2.ProgramExercises.Add(progExercise2_2);
            strainthContext.SaveChanges();

            progSplit.ProgramDetails.Add(progDetail1);
            progSplit.ProgramDetails.Add(progDetail2);
            strainthContext.SaveChanges();
        }

        private static (ProgramDetail progDetail1, ProgramDetail progDetail2) SeedProgramDetails(StrainthContext strainthContext, int progSplitId)
        {
            var progDetail1 = new ProgramDetail
            {
                Name = "Day 1",
                DayNumber = 1,
                ProgramSplitId = progSplitId
            };
            var progDetail2 = new ProgramDetail
            {
                Name = "Day 2",
                DayNumber = 2,
                ProgramSplitId = progSplitId
            };
            strainthContext.AddRange(progDetail1, progDetail2);
            strainthContext.SaveChanges();

            return (progDetail1, progDetail2);
        }

        private static ProgramSplit SeedProgramSplit(StrainthContext strainthContext)
        {
            var progSplit = new ProgramSplit
            {
                Name = "Test Split 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1),
                IsActive = true
            };
            strainthContext.Add(progSplit);
            strainthContext.SaveChanges();
            return progSplit;
        }

        private static void SeedSetupData(StrainthContext strainthContext)
        {
            if (strainthContext.Exercises.Any()) return;

            var categories = SeedCategoryData(strainthContext);
            SeedExerciseData(strainthContext, categories);
        }

        private static void SeedExerciseData(StrainthContext strainthContext, List<Category> categories)
        {

            var exercises = new List<Exercise>();
            var categoryId = categories.FirstOrDefault(c => c.Name == "Pull").Id;
            exercises.AddRange(new List<Exercise>
            {
                new Exercise { Name = "Chinup", CategoryId = categoryId },
                new Exercise { Name = "Neutral Grip Pulldown", CategoryId = categoryId },
                new Exercise { Name = "Underhand Cable Pullover", CategoryId = categoryId }
            });

            categoryId = categories.FirstOrDefault(c => c.Name == "Calves").Id;
            exercises.AddRange(new List<Exercise>
            {
                new Exercise { Name = "Seated Barbell Calf Raise", CategoryId = categoryId },
                new Exercise { Name = "Standing Calf Raise", CategoryId = categoryId }
            });

            categoryId = categories.FirstOrDefault(c => c.Name == "Abs").Id;
            exercises.AddRange(new List<Exercise>
            {
                new Exercise { Name = "Ab Wheel", CategoryId = categoryId },
                new Exercise { Name = "Bicycle Crunch", CategoryId = categoryId }
            });

            categoryId = categories.FirstOrDefault(c => c.Name == "Curl").Id;
            exercises.AddRange(new List<Exercise>
            {
                new Exercise { Name = "Incline Dumbbell Curl", CategoryId = categoryId },
                new Exercise { Name = "Dumbbell Spider Curl", CategoryId = categoryId }
            });

            categoryId = categories.FirstOrDefault(c => c.Name == "Hinge").Id;
            exercises.AddRange(new List<Exercise>
            {
                new Exercise { Name = "Deadlift - Sumo", CategoryId = categoryId },
                new Exercise { Name = "Back Extension", CategoryId = categoryId },
                new Exercise { Name = "Floor Hamstring Curl", CategoryId = categoryId },
                new Exercise { Name = "Pull Through", CategoryId = categoryId }
            });

            categoryId = categories.FirstOrDefault(c => c.Name == "Press").Id;
            exercises.AddRange(new List<Exercise>
            {
                new Exercise { Name = "Seated OH Press", CategoryId = categoryId },
                new Exercise { Name = "Arnold Press", CategoryId = categoryId }
            });

            categoryId = categories.FirstOrDefault(c => c.Name == "Push").Id;
            exercises.AddRange(new List<Exercise>
            {
                new Exercise { Name = "Incline Bench Press", CategoryId = categoryId },
                new Exercise { Name = "Incline Dumbbell Press Fly", CategoryId = categoryId },
                new Exercise { Name = "Close Grip Bench Press", CategoryId = categoryId }
            });

            categoryId = categories.FirstOrDefault(c => c.Name == "Squat").Id;
            exercises.AddRange(new List<Exercise>
            {
                new Exercise { Name = "Slantboard Front Squat", CategoryId = categoryId },
                new Exercise { Name = "ATG Split Squat", CategoryId = categoryId }
            });

            categoryId = categories.FirstOrDefault(c => c.Name == "Shoulders").Id;
            exercises.AddRange(new List<Exercise>
            {
                new Exercise { Name = "Lateral Raise", CategoryId = categoryId },
                new Exercise { Name = "Prone Rear Delt Raise", CategoryId = categoryId },
                new Exercise { Name = "Skiers", CategoryId = categoryId }
            });

            categoryId = categories.FirstOrDefault(c => c.Name == "Row").Id;
            exercises.AddRange(new List<Exercise>
            {
                new Exercise { Name = "Chest-supported Dumbbell Row", CategoryId = categoryId },
                new Exercise { Name = "Cable Upright Row", CategoryId = categoryId },
            });


            categoryId = categories.FirstOrDefault(c => c.Name == "Extend").Id;
            exercises.AddRange(new List<Exercise>
            {
                new Exercise { Name = "Rope Pushdown", CategoryId = categoryId },
                new Exercise { Name = "Standing OH Cable Extension", CategoryId = categoryId }
            });


            strainthContext.AddRange(exercises);
            strainthContext.SaveChanges();
        }

        private static List<Category> SeedCategoryData(StrainthContext strainthContext)
        {
            // ORDERED: Abs, Calves, Curl, Extend, Hinge, Press, Pull, Push, Row, Shoulders, Squat
            var categories = new List<Category>
            {
                new Category { Name = "Pull" },
                new Category { Name = "Curl" },
                new Category { Name = "Extend" },
                new Category { Name = "Calves" },
                new Category { Name = "Hinge" },
                new Category { Name = "Abs" },
                new Category { Name = "Row" },
                new Category { Name = "Push" },
                new Category { Name = "Press" },
                new Category { Name = "Squat" },
                new Category { Name = "Shoulders" }
            };

            strainthContext.AddRange(categories);
            strainthContext.SaveChanges();

            return categories;
        }
    }
}

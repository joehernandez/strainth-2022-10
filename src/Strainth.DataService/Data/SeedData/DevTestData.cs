using Strainth.DataService.Entities.Programming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strainth.DataService.Data.SeedData
{
    public static class DevTestData
    {
        public static void SeedTestData(StrainthContext strainthContext)
        {
            if (strainthContext.ProgramSplits.Any()) return;

            var progSplit = new ProgramSplit
            {
                Name = "Test Split 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1),
                IsActive = true
            };
            strainthContext.Add(progSplit);

            var progDetails1 = new ProgramDetail
            {
                Name = "Day 1",
                DayNumber = 1,
                ProgramSplit = progSplit
            };
            var progDetails2 = new ProgramDetail
            {
                Name = "Day 2",
                DayNumber = 2,
                ProgramSplit = progSplit
            };
            strainthContext.AddRange(progDetails1, progDetails2);
            strainthContext.SaveChanges();

            var progExercise1_1 = new ProgramExercise
            {
                ProgramDetailId = progDetails1.Id,
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
                ProgramDetailId = progDetails1.Id,
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
                ProgramDetailId = progDetails2.Id,
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
                ProgramDetailId = progDetails2.Id,
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

            progDetails1.ProgramExercises.Add(progExercise1_1);
            progDetails1.ProgramExercises.Add(progExercise1_2);
            progDetails2.ProgramExercises.Add(progExercise2_1);
            progDetails2.ProgramExercises.Add(progExercise2_2);
            strainthContext.SaveChanges();

            progSplit.ProgramDetails.Add(progDetails1);
            progSplit.ProgramDetails.Add(progDetails2);
            strainthContext.SaveChanges();
        }
    }
}

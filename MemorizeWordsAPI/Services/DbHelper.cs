using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MemorizeWordsAPI.Services
{
    public class LearningSchedule
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScheduleID { get; set; }
        public int WordNumberPerDay { get; set; }
        public int NumberOfDay { get; set; }
        public int WordListID { get; set; }
        public int DaysHaveLearned { get; set; }
        public virtual WordList WordListIDNavigation { get; set; }
    }
    public class WordList
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WordListID { get; set; }
        public String WordListName { get; set; }
        public int WordNumber { get; set; }
    }

    public class Word
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WordID { get; set; }
        public String EnglishWord { get; set; }
        public String PhoneticSymbols { get; set; }
        public String ChineseMeaning { get; set; }
        public String WordStatus { get; set; }
        public int WordListID { get; set; }
        public virtual WordList WordListIDNavigation { get; set; }
    }

    public class MWContext : DbContext
    {
        public MWContext(DbContextOptions<MWContext> options) : base(options) { }
        public DbSet<LearningSchedule> LearningSchedules { get; set; }
        public DbSet<WordList> WordLists { get; set; }
        public DbSet<Word> Words { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LearningSchedule>().HasData(
                new { ScheduleID = 1, WordNumberPerDay = 20, NumberOfDay= 5, WordListID =1, DaysHaveLearned = 2 }
            );
            modelBuilder.Entity<WordList>().HasData(
               new { WordListID = 1, WordListName = "A small word list for test", WordNumber = 100 }
);
        }
    }

}

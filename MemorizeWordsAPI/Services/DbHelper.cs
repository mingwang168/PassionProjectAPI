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
        public int times { get; set; }
        public bool time1 { get; set; }
        public bool time2 { get; set; }
        public bool time3 { get; set; }
        public bool time4 { get; set; }
        public bool time5 { get; set; }
        public bool time6 { get; set; }
        public bool time7 { get; set; }
        public bool time8 { get; set; }
        public int WordListID { get; set; }
        public virtual WordList WordListIDNavigation { get; set; }
    }
    public class Task
    {
        public int taskID { get; set; }
        public DateTime date { get; set; }
        public DateTime beginingTime { get; set; }
        public DateTime endingTime { get; set; }
        public int newWordNumber { get; set; }
        public int reviewWordNumber { get; set; }
        public virtual LearningSchedule ScheduleIDNavigation { get; set; }
    }
    public class MWContext : DbContext
    {
        public MWContext(DbContextOptions<MWContext> options) : base(options) { }
        public DbSet<LearningSchedule> LearningSchedules { get; set; }
        public DbSet<WordList> WordLists { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Task> Tasks { get; set; }
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

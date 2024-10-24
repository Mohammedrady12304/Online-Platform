using E_Learning.Models;

namespace E_Learning.FillData
{
    public class Fill
    {
        private readonly ApplicationDbContext dbContext;

        public Fill(ApplicationDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public  Task AddLanguageData()
        {
            dbContext.Languages.AddRange(
                new Language { NameId = "Arabic" },
                new Language { NameId = "Bulgarian" },
                new Language { NameId = "Catalan" },
                new Language { NameId = "Chinese" },
                new Language { NameId = "Croatian" },
                new Language { NameId = "Czech" },
                new Language { NameId = "Danish" },
                new Language { NameId = "Dutch" },
                new Language { NameId = "English" },
                new Language { NameId = "Estonian" },
                new Language { NameId = "Finnish" },
                new Language { NameId = "French" },
                new Language { NameId = "German" },
                new Language { NameId = "Greek" },
                new Language { NameId = "Hungarian" },
                new Language { NameId = "Icelandic" },
                new Language { NameId = "Indonesian" },
                new Language { NameId = "Irish" },
                new Language { NameId = "Italian" },
                new Language { NameId = "Japanese" },
                new Language { NameId = "Korean" },
                new Language { NameId = "Latvian" },
                new Language { NameId = "Lithuanian" },
                new Language { NameId = "Maltese" },
                new Language { NameId = "Norwegian" },
                new Language { NameId = "Polish" },
                new Language { NameId = "Portuguese" },
                new Language { NameId = "Romanian" },
                new Language { NameId = "Russian" },
                new Language { NameId = "Slovak" },
                new Language { NameId = "Slovenian" },
                new Language { NameId = "Spanish" },
                new Language { NameId = "Swedish" },
                new Language { NameId = "Turkish" },
                new Language { NameId = "Ukrainian" },
                new Language { NameId = "Vietnamese" }
            );

            dbContext.SaveChanges();

            return Task.CompletedTask;
        }


    }
}

using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace EntityFrameworkSeedFromJSON
{

    //public class MultiTenantContextInitializer : DropCreateDatabaseAlways<DbLocalContext>
    public class MultiTenantContextInitializer : DropCreateDatabaseAlways<DbLocalContext>
    {

        //https://connect.microsoft.com/VisualStudio/feedback/details/1934385
        // DropCreateDatabaseAlways  CreateDatabaseIfNotExists

        protected override void Seed(DbLocalContext context)
        {
            GetSpeakers(context);
            context.SaveChanges();
        }

        private void GetSpeakers(DbLocalContext context)
        {
            var speakerJsonAll = 
                GetEmbeddedResourceAsString("EntityFrameworkSeedFromJSON.speaker.json");

            JArray jsonValSpeakers = JArray.Parse(speakerJsonAll) as JArray;
            dynamic speakersData = jsonValSpeakers;
            foreach (dynamic speaker in speakersData)
            {
                context.Speakers.Add(new Speaker
                {
                    PictureId = speaker.id,
                    FirstName = speaker.firstName,
                    LastName = speaker.lastName,
                    AllowHtml = speaker.allowHtml,
                    Bio = speaker.bio,
                    WebSite = speaker.webSite
                });

            }
        }

       
        private string GetEmbeddedResourceAsString(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            //var names = assembly.GetManifestResourceNames();

            string result;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}

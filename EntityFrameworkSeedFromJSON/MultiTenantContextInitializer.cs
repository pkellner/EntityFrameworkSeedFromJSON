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
            GetSessions(context);
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

        private void GetSessions(DbLocalContext context)
        {
            var sessionJsonAll = 
                GetEmbeddedResourceAsString("EntityFrameworkSeedFromJSON.session.json");

            JArray jsonValSessions = JArray.Parse(sessionJsonAll) as JArray;
            dynamic sessionsData = jsonValSessions;
            foreach (dynamic session in sessionsData)
            {
                var sessionForAdd = new Session
                {
                    Id = session.id,
                    Description = session.description,
                    DescriptionShort = session.descriptionShort,
                    Title = session.title
                };

                var speakerPictureIds = new List<int>();
                foreach (dynamic speaker in session.speakers)
                {
                    dynamic pictureId = speaker.id;
                    speakerPictureIds.Add((int)pictureId);
                }

                sessionForAdd.Speakers = new List<Speaker>();
                foreach (var speakerPictureId in speakerPictureIds)
                {
                    var speakerForAdd = 
                        context.Speakers.FirstOrDefault(a => a.PictureId == speakerPictureId);
                    sessionForAdd.Speakers.Add(speakerForAdd);
                }

                context.Sessions.Add(sessionForAdd);
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

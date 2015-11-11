using System;
using System.Linq;

namespace EntityFrameworkSeedFromJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new DbLocalContext())
            {

                var sessions = context.Sessions.ToList();
                foreach (var session in sessions)
                {
                    Console.WriteLine("Session: " + session.Title);
                    foreach (var speaker in session.Speakers)
                    {
                        Console.WriteLine("{0} {1} {2}",
                            speaker.FirstName, speaker.LastName, speaker.PictureId);
                    }
                }

                //var speakers = context.Speakers.ToList();
                //foreach (var speaker in speakers)
                //{
                //    Console.WriteLine("{0} {1} {2}", 
                //        speaker.FirstName, speaker.LastName, speaker.PictureId);
                //}
            }

        }
    }
}

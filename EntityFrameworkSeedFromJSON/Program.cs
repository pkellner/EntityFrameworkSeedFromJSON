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
                Console.WriteLine("---SESSIONS---");
                var sessions = context.Sessions.ToList();
                foreach (var session in sessions)
                {
                    Console.WriteLine("Session: " + session.Title);
                    foreach (var speaker in session.Speakers)
                    {
                        Console.WriteLine("---{0} {1} {2}",
                            speaker.FirstName, speaker.LastName, speaker.PictureId);
                    }
                }
                Console.WriteLine("");
                Console.WriteLine("---SPEAKERS---");
                var speakers = context.Speakers.ToList();
                foreach (var speaker in speakers)
                {
                    Console.WriteLine("{0} {1} {2}",
                        speaker.FirstName, speaker.LastName, speaker.PictureId);
                    foreach (var session in speaker.Sessions)
                    {
                        Console.WriteLine("---{0}",
                       session.Title);
                    }
                }

            }

        }
    }
}

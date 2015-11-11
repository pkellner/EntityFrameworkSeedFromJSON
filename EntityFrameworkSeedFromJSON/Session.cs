using System.Collections.Generic;

namespace EntityFrameworkSeedFromJSON
{
    public class Session
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string DescriptionShort { get; set; }

        public string Description { get; set; }

        public virtual List<Speaker> Speakers { get; set; }


    }
}

namespace GraphQL.NetCoreExample.Types
{
    using System.Collections.Generic;
    using System.Linq;
    using GraphQL.NetCoreExample.Models;

    public class CatResolver
    {
        static List<Cat> Cats = new List<Cat>()
        {
            new Cat
            {
                Name = "Blackcat",
                Color = "black"
            },
            new Cat
            {
                Name = "Zeus",
                Color = "white"
            },
        };

        public Cat GetCatByName(string name) {
            return Cats.FirstOrDefault(c => c.Name == name);
        }

        public static Cat GetCatByNameStatic(string name) {
            return Cats.FirstOrDefault(c => c.Name == name);
        }
    }
}
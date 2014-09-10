using System;
using System.Collections.Generic;
using System.Linq;
using ColinGourlay.GenderEncoder.Properties;
using ColinGourlay.GenderEncoder.Utilities;

namespace ColinGourlay.GenderEncoder.Model
{
    internal static class GenderCodingNames
    {
        public static CachedList<Person> AllGenderCodingNames { get; private set; }
        public static CachedList<Person> WildCardNames { get; private set; }

        static GenderCodingNames()
        {
            AllGenderCodingNames = new CachedList<Person>(GetAllGenderCodingNames);
            WildCardNames = new CachedList<Person>(GetWildCardNames);
        }

        private static List<Person> GetWildCardNames()
        {
            return (from n in AllGenderCodingNames where n.FirstName.Contains("+") select n).ToList();
        }

        private static List<Person> GetAllGenderCodingNames()
        {
            var myResult = new List<Person>();
            GetGenderEncoding(myResult);
            return myResult.ToList();
        }




        private static void GetGenderEncoding(ICollection<Person> genderEncoding)
        {
            var content = Resources.GenderCodingNames.Split(new[] {"\n", "\r\n"}, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in content)
            {
                string[] line = s.Split('\t');

                genderEncoding.Add(new Person(line[0], line[1]));
            }
        }
    }
}

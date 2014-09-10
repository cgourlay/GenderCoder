using System;
using System.Collections.Generic;
using System.Linq;

using ColinGourlay.GenderEncoder.Properties;
using ColinGourlay.GenderEncoder.Utilities;

namespace ColinGourlay.GenderEncoder.Model
{
    internal static class GenderEncodedNames
    {
        internal static CachedList<Person> AllGenderEncodedNames { get; private set; }
        internal static CachedList<Person> AllWildCardNames { get; private set; }

        static GenderEncodedNames()
        {
            AllGenderEncodedNames = new CachedList<Person>(GetAllGenderEncodedNames);
            AllWildCardNames = new CachedList<Person>(GetWildCardNames);
        }

        private static List<Person> GetAllGenderEncodedNames()
        {
            return GetGenderEncoding();
        }

        private static List<Person> GetWildCardNames()
        {
            return (from n in AllGenderEncodedNames where n.Forename.Contains("+") select n).ToList();
        }

        




        private static List<Person> GetGenderEncoding()
        {
            var genderEncoding = new List<Person>();

            var content = Resources.GenderMapping.Split(new[] {"\n", "\r\n"}, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in content)
            {
                string[] line = s.Split('\t');

                genderEncoding.Add(new Person(line[0], line[1]));
            }

            return genderEncoding;
        }
    }
}

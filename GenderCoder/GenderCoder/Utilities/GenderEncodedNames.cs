using System;
using System.Collections.Generic;
using System.Linq;

using ColinGourlay.GenderEncoder.Model;
using ColinGourlay.GenderEncoder.Properties;

namespace ColinGourlay.GenderEncoder.Utilities
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
            foreach (string person in Resources.GenderMapping.Split(new[] {"\n", "\r\n"}, StringSplitOptions.RemoveEmptyEntries))
            {
                var line = person.Split('\t');
                genderEncoding.Add(new Person(forename: line[0], genderCode: line[1]));
            }
            return genderEncoding;
        }
    }
}

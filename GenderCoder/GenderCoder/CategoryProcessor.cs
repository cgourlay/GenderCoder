using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

using ColinGourlay.GenderEncoder.Model;
using ColinGourlay.GenderEncoder.Utilities;

namespace ColinGourlay.GenderEncoder
{
    public static class CategoryProcessor
    {
        static CategoryProcessor()
        {
            GenderEncoding.AllGenderEncodedNames.UpdateCache();
            GenderEncoding.AllWildCardNames.UpdateCache();
        }

        public static Gender GetGender(string forename)
        {
            return GetGenderUsingForename(forename);
        }

        public static string AsJson(this Gender gender)
        {
            return JsonConvert.SerializeObject(gender);
        }

        private static Gender GetGenderUsingForename(string firstName)
        {
            var workingFirstName = CleanName(firstName);
            if (workingFirstName.Length < 1) { return Gender.Unknown; }
            workingFirstName = SubstituteSpacesWithWildcard(workingFirstName);
            if (workingFirstName.Contains("+")) { return SearchCachedList(GenderEncoding.AllWildCardNames, workingFirstName); }
            return SearchCachedList(GenderEncoding.AllGenderEncodedNames, workingFirstName);   
        }

        private static Gender SearchCachedList(IEnumerable<Person> cachedList, string forename)
        {
            return (from person in cachedList where string.Equals(forename, person.Forename, StringComparison.OrdinalIgnoreCase) select person.Gender).FirstOrDefault();
        }

        private static string SubstituteSpacesWithWildcard(string workingFirstName)
        {
            workingFirstName = workingFirstName.Trim().Replace(" ", "+").Replace("-", "+");
            return workingFirstName;
        }

        private static string CleanName(string forename)
        {
            var workingFirstName = forename.Trim();

            while (workingFirstName.Contains("."))
            {
                var dotIndex = workingFirstName.IndexOf(".");
                var spaceIndex = dotIndex - 1;
                while (workingFirstName[spaceIndex] != ' ')
                {
                    spaceIndex--;
                    if (spaceIndex == -1) { break; }
                }

                workingFirstName = workingFirstName.Remove(spaceIndex + 1, dotIndex - spaceIndex).Trim();
            }
            return workingFirstName;
        }
    }
}

using System;
using ColinGourlay.GenderEncoder.Model;
using Newtonsoft.Json;

namespace ColinGourlay.GenderEncoder
{
    public class CategoryProcessor
    {
        public CategoryProcessor()
        {
            GenderCodingNames.AllGenderCodingNames.Refresh();
            GenderCodingNames.UnitesStatesNames.Refresh();
            GenderCodingNames.ForeignNames.Refresh();
            GenderCodingNames.WildCardNames.Refresh();
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
            if (workingFirstName.Contains("+"))
            {
                return SearchWildcardNames(workingFirstName);
            }
            
            return SearchAmericanNames(workingFirstName);
            return SearchForeignNames(workingFirstName);
        }

        private static Gender SearchForeignNames(string workingFirstName)
        {
            //If we've still got nothing, try a  deep case-insensitive compare...
            foreach (GenderCodingName name in GenderCodingNames.ForeignNames)
            {
                //Remove wildcard from the gender coding name, if it exists
                string compare = name.FirstName.Contains("+") ? name.FirstName.Replace("+", "") : name.FirstName;

                if (string.Equals(workingFirstName, compare, StringComparison.OrdinalIgnoreCase))
                {
                    {
                        return name.Sex;

                    }
                }
            }
            return Gender.Unknown;
        }

        private static Gender SearchAmericanNames(string workingFirstName)
        {
            foreach (GenderCodingName name in GenderCodingNames.UnitesStatesNames)
            {
                if (string.Equals(workingFirstName, name.FirstName, StringComparison.OrdinalIgnoreCase))
                {
                    {
                        return name.Sex;
                    }
                }
            }
            return Gender.Unknown;
        }

        private static Gender SearchWildcardNames(string workingFirstName)
        {
            foreach (GenderCodingName name in GenderCodingNames.WildCardNames)
            {
                if (string.Equals(workingFirstName, name.FirstName, StringComparison.OrdinalIgnoreCase))
                {
                    {
                        return name.Sex;
                    }
                }
            }
            return Gender.Unknown;
        }

        private static string SubstituteSpacesWithWildcard(string workingFirstName)
        {
            workingFirstName = workingFirstName.Trim().Replace(" ", "+").Replace("-", "+");
            return workingFirstName;
        }

        private static string CleanName(string firstName)
        {
            string workingFirstName = firstName.Trim();

            while (workingFirstName.Contains("."))
            {
                int dotIndex = workingFirstName.IndexOf(".");

                int spaceIndex = dotIndex - 1;

                while (workingFirstName[spaceIndex] != ' ')
                {
                    spaceIndex--;

                    if (spaceIndex == -1)
                    {
                        break;
                    }
                }

                workingFirstName = workingFirstName.Remove(spaceIndex + 1, dotIndex - spaceIndex).Trim();
            }
            return workingFirstName;
        }
    }
}

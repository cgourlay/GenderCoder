using System;

using GenderCoder.Entities;

namespace GenderCoder
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

        private static Gender GetGenderUsingForename(string FirstName)
        {
            var workingFirstName = CleanName(FirstName);

            if (workingFirstName.Length < 1)
            {
                return Gender.Unknown;
            }

            //Trim the string, then replace any spaces or hyphens with the "+" wildcard
            workingFirstName = workingFirstName.Trim().Replace(" ", "+").Replace("-", "+");

            //If the name contains a wildcard, run it against a subset of wildcard-containing names
            if (workingFirstName.Contains("+"))
            {
                foreach (GenderCodingName name in GenderCodingNames.WildCardNames)
                {
                    if (String.Equals(workingFirstName, name.FirstName, StringComparison.OrdinalIgnoreCase))
                    {
                        return name.Sex;
                    }
                }
            }
            else
            {
                //Otherwise, attempt a case-insensitive compare against US-Only names
                foreach (GenderCodingName name in GenderCodingNames.UnitesStatesNames)
                {
                    if (String.Equals(workingFirstName, name.FirstName, StringComparison.OrdinalIgnoreCase))
                    {
                        return name.Sex;
                    }
                }
            }

            //If we've still got nothing, try a  deep case-insensitive compare...
            foreach (GenderCodingName name in GenderCodingNames.ForeignNames)
            {
                //Remove wildcard from the gender coding name, if it exists
                string compare = name.FirstName.Contains("+") ? name.FirstName.Replace("+", "") : name.FirstName;

                if (String.Equals(workingFirstName, compare, StringComparison.OrdinalIgnoreCase))
                {
                    return name.Sex;
                }
            }

            return Gender.Unknown;
        }

        private static string CleanName(string FirstName)
        {
            string workingFirstName = FirstName.Trim();

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

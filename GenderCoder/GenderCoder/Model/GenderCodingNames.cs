using System;
using System.Collections.Generic;
using System.Linq;
using ColinGourlay.GenderEncoder.Properties;
using GenderCoder.Utilities;

namespace GenderCoder.Model
{
    internal static class GenderCodingNames
    {
        public static CachedList<GenderCodingName> AllGenderCodingNames { get; private set; }
        public static CachedList<GenderCodingName> UnitesStatesNames { get; private set; }
        public static CachedList<GenderCodingName> ForeignNames { get; private set; }
        public static CachedList<GenderCodingName> WildCardNames { get; private set; }

        static GenderCodingNames()
        {
            AllGenderCodingNames = new CachedList<GenderCodingName>(GetAllGenderCodingNames);
            UnitesStatesNames = new CachedList<GenderCodingName>(GetUnitedStatesNames);
            ForeignNames = new CachedList<GenderCodingName>(GetForeignNames);
            WildCardNames = new CachedList<GenderCodingName>(GetWildCardNames);
        }

        private static List<GenderCodingName> GetUnitedStatesNames()
        {
            return (from n in AllGenderCodingNames where n.UsPopularity > 0 select n).OrderByDescending(x => x.UsPopularity).ToList();
        }

        private static List<GenderCodingName> GetForeignNames()
        {
            return (from n in AllGenderCodingNames where n.UsPopularity < 1 select n).ToList();
        }

        private static List<GenderCodingName> GetWildCardNames()
        {
            return (from n in AllGenderCodingNames where n.FirstName.Contains("+") select n).OrderByDescending(x => x.UsPopularity).ToList();
        }

        private static List<GenderCodingName> GetAllGenderCodingNames()
        {
            var myResult = new List<GenderCodingName>();

            //Load Gender Coding Names
            var content = Resources.GenderCodingNames.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in content)
            {
                string[] line = s.Split('\t');

                myResult.Add(new GenderCodingName(line[0], line[1], Convert.ToInt32(line[2])));
            }

            //Load Supplemental First Names
            content = Resources.SupplementalFirstNames.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in content)
            {
                string[] line = s.Split('\t');

                myResult.Add(new GenderCodingName(line[0], line[1], Convert.ToInt32(line[2])));
            }

            return myResult.OrderByDescending(x => x.UsPopularity).ToList();
        }
    }
}

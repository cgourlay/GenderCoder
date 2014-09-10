using System;
using System.Collections.Generic;
using System.Linq;
using GenderCoder.Properties;
using GenderCoder.Utilities;

namespace GenderCoder.Entities
{
    internal static class GenderCodingNames
    {
        public static CachedList<GenderCodingName> AllGenderCodingNames { get; set; }
        public static CachedList<GenderCodingName> USNames { get; set; }
        public static CachedList<GenderCodingName> ForeignNames { get; set; }
        public static CachedList<GenderCodingName> WildCardNames { get; set; }

        static GenderCodingNames()
        {
            AllGenderCodingNames = new CachedList<GenderCodingName>(() => { return GetAllGenderCodingNames(); });

            USNames = new CachedList<GenderCodingName>(() => { return GetUSNames(); });

            ForeignNames = new CachedList<GenderCodingName>(() => { return GetForeignNames(); });

            WildCardNames = new CachedList<GenderCodingName>(() => { return GetWildCardNames(); });
        }

        private static List<GenderCodingName> GetUSNames()
        {
            return (from n in AllGenderCodingNames where n.USPopularity > 0 select n).OrderByDescending(x => x.USPopularity).ToList();
        }

        private static List<GenderCodingName> GetForeignNames()
        {
            return (from n in AllGenderCodingNames where n.USPopularity < 1 select n).ToList();
        }

        private static List<GenderCodingName> GetWildCardNames()
        {
            return (from n in AllGenderCodingNames where n.FirstName.Contains("+") select n).OrderByDescending(x => x.USPopularity).ToList();
        }

        private static List<GenderCodingName> GetAllGenderCodingNames()
        {
            List<GenderCodingName> myResult = new List<GenderCodingName>();

            //Load Gender Coding Names
            string[] content = Resources.GenderCodingNames.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in content)
            {
                string[] line = s.Split('\t');

                myResult.Add(new GenderCodingName(line[0], line[1], Convert.ToInt32(line[2])));
            }

            //Load Supplemental First Names
            content = Resources.SupplementalFirstNames.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in content)
            {
                string[] line = s.Split('\t');

                myResult.Add(new GenderCodingName(line[0], line[1], Convert.ToInt32(line[2])));
            }

            return myResult.OrderByDescending(x => x.USPopularity).ToList();
        }
    }
}

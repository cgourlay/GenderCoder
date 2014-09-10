namespace GenderCoder.Entities
{
    internal class GenderCodingName
    {
        public GenderCodingName(string FirstName, string GenderCode, int USPopularity)
        {
            this.FirstName = FirstName;
            this.USPopularity = USPopularity;

            switch (GenderCode)
            {
                case "M":
                    this.Sex = Gender.Male;
                    break;
                case "1M":
                    //this.Gender = Gender.MostlyMale;
                    break;
                case "?M":
                    //this.Gender = Gender.MostlyMale;
                    break;
                case "F":
                    this.Sex = Gender.Female;
                    break;
                case "1F":
                    //this.Gender = Gender.MostlyFemale;
                    break;
                case "?F":
                    //this.Gender = Gender.MostlyFemale;
                    break;
                default:
                    this.Sex = Gender.Unknown;
                    break;
            }
        }

        public string FirstName { get; set; }
        public Gender Sex { get; set; }
        public int USPopularity { get; set; }
    }
}

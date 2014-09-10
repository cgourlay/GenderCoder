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
                    Sex = Gender.Male;
                    break;
                case "F":
                    Sex = Gender.Female;
                    break;
                default:
                    Sex = Gender.Unknown;
                    break;
            }
        }

        public string FirstName { get; set; }
        public Gender Sex { get; set; }
        public int USPopularity { get; set; }
    }
}

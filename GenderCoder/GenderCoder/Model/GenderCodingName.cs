namespace ColinGourlay.GenderEncoder.Model
{
    internal class GenderCodingName
    {
        public GenderCodingName(string firstName, string genderCode)
        {
            FirstName = firstName;
           
            switch (genderCode)
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

        public string FirstName { get; private set; }
        public Gender Sex { get; private set; }
    }
}

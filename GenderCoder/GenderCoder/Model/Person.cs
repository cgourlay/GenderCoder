namespace ColinGourlay.GenderEncoder.Model
{
    internal class Person
    {
        public Person(string forename, string genderCode)
        {
            Forename = forename;

            switch (genderCode.ToUpper())
            {
                case "M":
                    Gender = Gender.Male;
                    break;
                case "F":
                    Gender = Gender.Female;
                    break;
                default:
                    Gender = Gender.Unknown;
                    break;
            }
        }

        public string Forename { get; private set; }
        public Gender Gender { get; private set; }
    }
}

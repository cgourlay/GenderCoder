namespace ColinGourlay.GenderEncoder.Model
{
    internal class Person
    {
        public Person(string firstName, string genderCode)
        {
            FirstName = firstName;

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

        public string FirstName { get; private set; }
        public Gender Gender { get; private set; }
    }
}

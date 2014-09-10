namespace GenderCoder.Entities
{
    internal class GenderCodingResult
    {
        public GenderCodingResult(string firstName, int? row = null, int? id = null, Gender gender = Gender.Unknown)
        {
            Id = id;
            FirstName = firstName;
            Row = row;
            Gender = gender;
        }

        public string FirstName { get; private set; }
        public Gender Gender { get; set; }
        public int? Id { get; set; }
        public int? Row { get; set; }
        public bool Processed { get; set; }
    }
}
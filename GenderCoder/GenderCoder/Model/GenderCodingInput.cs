namespace GenderCoder.Model
{
    internal class GenderCodingInput
    {
        public GenderCodingInput(string firstName, int? uniqueId = null)
        {
            Id = uniqueId; 
            FirstName = firstName;   
        }

        public string FirstName { get; private set; }
        public int? Id { get; private set; }
    }
}

namespace GenderCoder.Entities
{
    public class GenderCodingInput
    {
        public GenderCodingInput(string FirstName, int? UniqueID = null)
        {
            this.FirstName = FirstName;
            this.UniqueID = UniqueID;
        }

        public string FirstName { get; set; }
        
        //Optional ID for record tracking
        public int? UniqueID { get; set; }
    }
}

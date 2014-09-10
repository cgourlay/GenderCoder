namespace GenderCoder.Entities
{
    public class GenderCodingResult
    {
        public GenderCodingResult(string FirstName, int? Row = null, int? UniqueID = null)
        {
            this.FirstName = FirstName;
            this.Row = Row;
            this.UniqueID = UniqueID;

            //Default gender to unknown
            this.Gender = Gender.Unknown;
        }

        public string FirstName { get; set; }
        public Gender Gender { get; set; }

        public int? UniqueID { get; set; }
        public int? Row { get; set; }

        public bool Processed { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenderCoder.Entities
{
    internal class GenderCodingInput
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

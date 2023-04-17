using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserEntities
{
    public class BloodType
    {
        public int Id { get; set; }
        public BloodTypeEnum BloodTypeName { get; set; }
    }
}

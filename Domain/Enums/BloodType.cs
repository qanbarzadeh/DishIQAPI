using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Enums
{
    public enum BloodTypeEnum
    {
        [Display(Name = "A", Description = "Type A")]
        A = 1,

        [Display(Name = "B", Description = "Type B")]
        B = 2,

        [Display(Name = "AB", Description = "Type AB")]
        AB = 3,

        [Display(Name = "O", Description = "Type O")]
        O = 4,
    }
}
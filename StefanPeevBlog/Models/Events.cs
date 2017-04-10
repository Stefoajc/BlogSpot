using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StefanPeevBlog.Models
{
    public class Events : IValidatableObject
    {
        //public Events()
        //{
        //    Coords = new HashSet<Coords>();
        //}

        [Key]
        public int EventId { get; set; }
        [ForeignKey("EventCreator")]
        public string UserId { get; set; }
        public string ImagePath { get; set; }
        [StringLength(40)]
        public string EventTitle { get; set; }
        public string EventBody { get; set; }
        [StringLength(20)]
        public string Lat { get; set; }
        [StringLength(20)]
        public string Lng { get; set; }
        public DateTime PostedOn { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:mm/dd/yyyy H:mm tt")]
        public DateTime Starts { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "0:mm/dd/yyyy H:mm tt")]
        public DateTime? Ends { get; set; }
        public bool? IsSpecial { get; set; }

        public virtual ApplicationUser EventCreator { get; set; }


        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            List<ValidationResult> res = new List<ValidationResult>();
            if (DateTime.Now > Starts)
            {
                ValidationResult mss = new ValidationResult("Start date must be greater than or equal to the current date and time");
                res.Add(mss);
            }
            if (Starts > Ends)
            {
                ValidationResult mss = new ValidationResult("End date must be greater than to the Start date");
                res.Add(mss);
            }
            return res;
        }
        //public virtual ISet<Coords> Coords { get; set; }
    }
}
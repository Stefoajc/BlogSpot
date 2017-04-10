using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StefanPeevBlog.Models
{
    public class Coords
    {
        [Key]
        public int CoordsId { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }

        public virtual Events Event { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Paradise.Models
{
    public class ImageContainer
    {
        [Key]
        public int container_ID { get; set; }
        public int image_ID { get; set; }
    }
}
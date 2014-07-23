using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//add the using for dataannotations
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
  //new to declare a partial class
    [MetadataType(typeof(Postvalidation))]
    public partial class Post 
    {
    }
    
    public class Postvalidation
    {
        
       
       [Required, DataType(DataType.MultilineText)]
        public string Body { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(200), DataType(DataType.ImageUrl)]
        public string ImageURL { get; set; }
        
       
    }
}
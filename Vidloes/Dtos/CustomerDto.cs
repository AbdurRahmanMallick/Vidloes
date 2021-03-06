﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidloes.Models;

namespace Vidloes.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter customer's name.")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubCribedToNewsletter { get; set; }

      

       
        public byte MembershipTypeId { get; set; }

        
      //  [Min18YearsIfAMember]
        public DateTime BirthDate { get; set; }
    }
}
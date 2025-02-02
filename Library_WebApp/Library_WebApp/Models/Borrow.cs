﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_WebApp.Models
{
    public class Borrow
    {
        public int id { set; get; }
        [DataType(DataType.Date)]
        [Display(Name = "Data wypożyczenia")]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public DateTime? borrowDate { set; get; }
        [Display(Name = "Data zwrotu")]
        [DataType(DataType.Date)]
        public DateTime? returnDate { set; get; }
        [Display(Name = "Id woluminu")]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public int? volumeId { set; get; }
        [Display(Name = "Login wypożyczającego")]
        [Required(ErrorMessage = "Pole {0} nie może być puste.")]
        public String userLogin { set; get; }
    }
}

﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BorrowMeAPI.Model
{
    public class City : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
    }
}
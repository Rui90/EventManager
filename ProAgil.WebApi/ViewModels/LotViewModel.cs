using System;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebApi.ViewModels
{
    public class LotViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(512)]
        public string Name { get; set; }

        [Required]
        public string Price { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Quantity  { get; set; }

    }
}
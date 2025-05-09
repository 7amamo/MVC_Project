﻿using System.ComponentModel.DataAnnotations;

namespace Demo.presentaton.Layer.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 5)]
        public string Name { get; set; }
        [Range(19, 60)]
        public int Age { get; set; }

        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal salary { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }

        public bool IsActive { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }


        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }
    }
}

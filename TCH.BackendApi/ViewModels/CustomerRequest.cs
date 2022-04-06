﻿using System.ComponentModel.DataAnnotations;
using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.ViewModels;

public class CustomerRequest
{
    public string? FullName { get; set; }
    [MaxLength(10)]
    [Required]
    [RegularExpression(@"(84|0[3|5|7|8|9])+([0-9]{8})\b",
        ErrorMessage = "Định dạng số điện thoại sai")]
    public string Phone { get; set; }
    public string? Email { get; set; }
    [MaxLength(255)]
    public string Address { get; set; } = "Ngô Quyền, Hải Phòng";
    public Gender Gender { get; set; } = Gender.Female;
    public DateTime DateOfBirth { get; set; } = DateTime.Now;
    public string? ConversionRate { get; set; }
    public IFormFile? File { get; set; }
    public string MemberTypeID { get; set; }
}
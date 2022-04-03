﻿using System.ComponentModel.DataAnnotations;
using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.Entities;

public class Promition
{
    [Key]
    public string ID { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string? Image { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreateDate { get; set; }
    public PromotionType TypePromotion { get; set; }
    public PromotionObject PromotionObject { get; set; }
    public int Status { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public ICollection<PromotionGift> PromotionGifts { get; set; }
}
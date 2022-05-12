﻿using System.ComponentModel.DataAnnotations;

namespace TCH.Data.Entities;

public class Unit
{
    [Key]
    public string ID { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string? UserCreateID { get; set; }
    public string? UserUpdateID { get; set; }
    public virtual ICollection<UnitConversion> SourceUnits { get; set; }
    public virtual ICollection<UnitConversion> DestinationUnits { get; set; }
}
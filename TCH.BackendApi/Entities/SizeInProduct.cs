﻿using System.ComponentModel.DataAnnotations.Schema;

namespace TCH.BackendApi.Entities;

public class SizeInProduct
{
    [ForeignKey("Products")]
    public string ProductID { get; set; }
    [ForeignKey("Sizes")]
    public string SizeID { get; set; }
    public Product Product { get; set; }
    public Size Size { get; set; }
}
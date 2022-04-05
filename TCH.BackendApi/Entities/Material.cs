﻿namespace TCH.BackendApi.Entities;

public class Material
{
    public string ID { get; set; }
    public string Name { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string? Description { get; set; }
    public string MaterialTypeID { get; set; }
    public MaterialType MaterialType { get; set; }
    public ICollection<StockMaterial> StockMaterials { get; set; }
    public ICollection<ExportMaterial> ExportMaterials { get; set; }
    public ICollection<ImportMaterial> ImportMaterials { get; set; }
    public ICollection<LiquidationMaterial> LiquidationMaterials { get; set; }
}

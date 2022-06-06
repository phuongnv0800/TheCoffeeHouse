using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCH.Utilities.Error;
using TCH.Utilities.Searchs;
using TCH.BackendApi.Repositories.DataRepository;
using TCH.Data.Entities;
using TCH.ViewModel.SubModels;
using TCH.Utilities.Roles;

namespace TCH.BackendApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
public class ReportsController : ControllerBase
{
    private readonly IReportRepository _repository;
    private readonly ILogger<RolesController> _logger;

    public ReportsController(IReportRepository roleService, ILogger<RolesController> logger)
    {
        _repository = roleService;
        _logger = logger;
    }

    [HttpGet("import/{branchID}")]
    public async Task<IActionResult> GetAllImportByBranchID(string branchID, [FromQuery] Search search)
    {
        try
        {
            var respond = await _repository.GetAllImportReportByBranchID(branchID, search);
            return Ok(respond);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [HttpGet("excel-export/{branchId}")]
    public async Task<IActionResult> ExcelExportReport(string branchId, [FromQuery] Search search)
    {
        try
        {
            string newPath = await _repository.ExcelExportReport(branchId, search);
            var memory = new MemoryStream();
            using (var stream = new FileStream(newPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                Path.GetFileName(newPath));
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }
    [HttpGet("excel-liquidation/{branchId}")]
    public async Task<IActionResult> ExcelLiquidationReport(string branchId, [FromQuery] Search search)
    {
        try
        {
            string newPath = await _repository.ExcelLiquidationReport(branchId, search);
            var memory = new MemoryStream();
            using (var stream = new FileStream(newPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                Path.GetFileName(newPath));
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }
    [HttpGet("excel-import/{branchId}")]
    public async Task<IActionResult> ExcelImportReport(string branchId, [FromQuery] Search search)
    {
        try
        {
            string newPath = await _repository.ExcelImportReport(branchId, search);
            var memory = new MemoryStream();
            using (var stream = new FileStream(newPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                Path.GetFileName(newPath));
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }
    [HttpGet("excel-import-by-id/{id}")]
    public async Task<IActionResult> ExcelImportReportById(string id)
    {
        try
        {
            string newPath = await _repository.ExcelImportReportById(id);
            if (newPath == null)
            {
                return BadRequest(new {result = -1, message = "Không thấy"});
            }
            var memory = new MemoryStream();
            using (var stream = new FileStream(newPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                Path.GetFileName(newPath));
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }
    [HttpGet("excel-export-by-id/{id}")]
    public async Task<IActionResult> ExcelExportReportById(string id)
    {
        try
        {
            string newPath = await _repository.ExcelExportReportById(id);
            if (newPath == null)
            {
                return BadRequest(new {result = -1, message = "Không thấy"});
            }
            var memory = new MemoryStream();
            using (var stream = new FileStream(newPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                Path.GetFileName(newPath));
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }
    [HttpGet("excel-liquidation-by-id/{id}")]
    public async Task<IActionResult> ExcelLiquidationReportById(string id)
    {
        try
        {
            string newPath = await _repository.ExcelLiquidationReportById(id);
            if (newPath == null)
            {
                return BadRequest(new {result = -1, message = "Không thấy"});
            }
            var memory = new MemoryStream();
            using (var stream = new FileStream(newPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                Path.GetFileName(newPath));
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }
  [HttpGet("excel-export-all")]
    public async Task<IActionResult> ExcelExportAllReport([FromQuery] Search search)
    {
        try
        {
            string newPath = await _repository.ExcelExportAllReport(search);
            var memory = new MemoryStream();
            using (var stream = new FileStream(newPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                Path.GetFileName(newPath));
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }
    [HttpGet("excel-liquidation-all")]
    public async Task<IActionResult> ExcelLiquidationAllReport( [FromQuery] Search search)
    {
        try
        {
            string newPath = await _repository.ExcelLiquidationAllReport( search);
            var memory = new MemoryStream();
            using (var stream = new FileStream(newPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                Path.GetFileName(newPath));
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }
    [HttpGet("excel-import-all")]
    public async Task<IActionResult> ExcelImportAllReport([FromQuery] Search search)
    {
        try
        {
            string newPath = await _repository.ExcelImportAllReport(search);
            var memory = new MemoryStream();
            using (var stream = new FileStream(newPath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                Path.GetFileName(newPath));
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [HttpGet("import")]
    public async Task<IActionResult> GetAllImport([FromQuery] Search search)
    {
        try
        {
            var respond = await _repository.GetAllImportReport(search);
            return Ok(respond);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [HttpGet("export/{branchID}")]
    public async Task<IActionResult> GetAllExport(string branchID, [FromQuery] Search search)
    {
        try
        {
            var respond = await _repository.GetAllExportReportByBranchID(branchID, search);
            return Ok(respond);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [HttpGet("export")]
    public async Task<IActionResult> GetAllExport([FromQuery] Search search)
    {
        try
        {
            var respond = await _repository.GetAllExportReport(search);
            return Ok(respond);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [HttpGet("liquidation/{branchID}")]
    public async Task<IActionResult> GetAllLiquidationByBranchID(string branchID, [FromQuery] Search search)
    {
        try
        {
            var respond = await _repository.GetAllLiquidationReportByBranchID(branchID, search);
            return Ok(respond);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [HttpGet("liquidation")]
    public async Task<IActionResult> GetAllLiquidation([FromQuery] Search search)
    {
        try
        {
            var respond = await _repository.GetAllLiquidationReport(search);
            return Ok(respond);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [HttpGet("import/{id}")]
    public async Task<IActionResult> GetImportByID(string id)
    {
        try
        {
            var respond = await _repository.GetImportReportByID(id);
            return Ok(respond);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [HttpGet("export/{id}")]
    public async Task<IActionResult> GetExportByID(string id)
    {
        try
        {
            var respond = await _repository.GetExportReportByID(id);
            return Ok(respond);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [HttpGet("liquidation/{id}")]
    public async Task<IActionResult> GetLiquidationByID(string id)
    {
        try
        {
            var respond = await _repository.GetLiquidationReportByID(id);
            return Ok(respond);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [HttpPost("import")]
    public async Task<IActionResult> CreateImport([FromBody] ImportRequest search)
    {
        try
        {
            var respond = await _repository.CreateImportReport(search);
            return Ok(respond);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [HttpPost("export")]
    public async Task<IActionResult> CreateExport([FromBody] ExportRequest search)
    {
        try
        {
            var respond = await _repository.CreateExportReport(search);
            return Ok(respond);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [HttpPost("liquidation")]
    public async Task<IActionResult> CreateLiquidation([FromBody] ImportRequest search)
    {
        try
        {
            var respond = await _repository.CreateLiquidationReport(search);
            return Ok(respond);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
    [HttpDelete("import/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.DeleteImportReport(id);
            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
    [HttpDelete("export/{id}")]
    public async Task<IActionResult> DeleteExport(string id)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.DeleteExportReport(id);
            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [Authorize(Roles = Permission.Branch + "," + Permission.Manage)]
    [HttpDelete("liquidation/{id}")]
    public async Task<IActionResult> DeleteLiquidation(string id)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _repository.DeleteLiquidationReport(id);
            return Ok(result);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }
    [HttpGet("get-mass-material-in-day")]
    public async Task<IActionResult> GetMassMaterialInDay([FromQuery] Search search)
    {
        try
        {
            var respond = await _repository.GetMassMaterialInDay(search);
            return Ok(respond);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

    [HttpGet("get-mass-material-in-day-by-branch/{branchID}")]
    public async Task<IActionResult> GetMassMaterialInDayByBranchId(string branchID, [FromQuery] Search search)
    {
        try
        {
            var respond = await _repository.GetMassMaterialInDayByBranchId(branchID, search);
            return Ok(respond);
        }
        catch (CustomException e)
        {
            return BadRequest(new {result = -1, message = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(new {result = -2, message = e.Message});
        }
    }

}
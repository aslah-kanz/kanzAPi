using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Transaction.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class WithdrawalController(
IWithdrawalService withdrawalService, IWithdrawalFilterableService withdrawalFilteredService)
: ControllerBase
{

    IWithdrawalService _withdrawalService = withdrawalService;

    IWithdrawalFilterableService _withdrawalFilteredService = withdrawalFilteredService;

    [HttpPost]
    public ResponseMessage<WithdrawalResponse> Add([FromBody] WithdrawalRequest request)
    {
        WithdrawalResponse data = _withdrawalFilteredService.Add(request);
        return ResponseMessage<WithdrawalResponse>.Success(data);
    }

    [HttpPost("{id}")]
    public ResponseMessage<WithdrawalResponse> Edit(int id, [FromBody] WithdrawalRequest request)
    {
        WithdrawalResponse data = _withdrawalFilteredService.Edit(id, request);
        return ResponseMessage<WithdrawalResponse>.Success(data);
    }

    [HttpPost("{id}/status")]
    public ResponseMessage<WithdrawalResponse> UpdateStatus(int id, [FromBody] WithdrawaUpdateStatusRequest param)
    {
        WithdrawalResponse data = _withdrawalService.UpdateStatus(id, param.Status);
        return ResponseMessage<WithdrawalResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<WithdrawalResponse>> GetAll([FromQuery] WithdrawalPageableParam param)
    {
        PaginatedResponse<WithdrawalResponse> data = _withdrawalFilteredService.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<WithdrawalResponse>>.Success(data);
    }

    [HttpGet("admin")]
    public ResponseMessage<PaginatedResponse<WithdrawalResponse>> GetAllAdmin([FromQuery] WithdrawalPageableParam param)
    {
        PaginatedResponse<WithdrawalResponse> data = _withdrawalService.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<WithdrawalResponse>>.Success(data);
    }

    [HttpGet("AvailableAmount")]
    public ResponseMessage<decimal> GetAmount()
    {
        return ResponseMessage<decimal>.Success(_withdrawalFilteredService.GetAmount());
    }
}

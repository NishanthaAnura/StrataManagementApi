using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Services;
using WebApi.Utilities;

namespace StrataManagementApi.Tests.Controllers;

[TestFixture]
public class MaintenanceRequestControllerTests
{
    private Mock<IMaintenanceRequestService> _service;
    private MaintenanceRequestController _controller;

    [SetUp]
    public void Setup()
    {
        _service = new Mock<IMaintenanceRequestService>();
        _controller = new MaintenanceRequestController(_service.Object);
    }

    [Test]
    public async Task Create_WithValidRequest_ReturnsOkResultWithResponse()
    {
        var request = new MaintenanceUserRequest
        {
            Title = "Leaky faucet",
            Description = "Kitchen faucet is leaking badly",
            Status = MaintenanceStatus.Pending,
            BuildingId = "bld-123",
            UnitNumber = "A101"
        };

        var expectedResponse = new MaintenanceRequestResponse
        {
            Id = "req_id",
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            LastChangedTime = DateTime.UtcNow,
            BuildingName = "Main Building",
            UnitNumber = request.UnitNumber
        };

        _service.Setup(s => s.CreateAsync(It.IsAny<MaintenanceUserRequest>()))
            .ReturnsAsync(Result<MaintenanceRequestResponse>.Success(expectedResponse));

        var result = await _controller.Create(request);

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(expectedResponse));
    }

    [Test]
    public async Task Create_WithInvalidRequest_ReturnsBadRequest()
    {
        var request = new MaintenanceUserRequest
        {
            Title = "Leaky faucet",
            Description = "Kitchen faucet is leaking badly",
            Status = MaintenanceStatus.Pending,
            BuildingId = "bld-123",
            UnitNumber = "A101"
        };

        var errorMessage = "Failed to create request";
        _service.Setup(s => s.CreateAsync(It.IsAny<MaintenanceUserRequest>()))
            .ReturnsAsync(Result<MaintenanceRequestResponse>.Failure(errorMessage));

        var result = await _controller.Create(request);

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        var badRequestResult = result as BadRequestObjectResult;
        Assert.That(badRequestResult.Value, Is.EqualTo(errorMessage));
    }

    [Test]
    public async Task Update_WithValidRequest_ReturnsOkResultWithResponse()
    {
        var id = "req_id";
        var request = new MaintenanceUpdatedRequest
        {
            Status = MaintenanceStatus.InProgress
        };

        var expectedResponse = new MaintenanceRequestResponse
        {
            Id = id,
            Title = "Leaky faucet",
            Description = "Kitchen faucet is leaking badly",
            Status = request.Status,
            LastChangedTime = DateTime.UtcNow,
            BuildingName = "Main Building",
            UnitNumber = "A101"
        };

        _service.Setup(s => s.UpdateAsync(id, request))
            .ReturnsAsync(Result<MaintenanceRequestResponse>.Success(expectedResponse));

        var result = await _controller.Update(id, request);

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(expectedResponse));
    }

    [Test]
    public async Task Update_WithInvalidId_ReturnsBadRequest()
    {
        var id = "invalid-id";
        var request = new MaintenanceUpdatedRequest
        {
            Status = MaintenanceStatus.InProgress
        };

        var errorMessage = "Request not found";
        _service.Setup(s => s.UpdateAsync(id, request))
            .ReturnsAsync(Result<MaintenanceRequestResponse>.Failure(errorMessage));

        var result = await _controller.Update(id, request);

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        var badRequestResult = result as BadRequestObjectResult;
        Assert.That(badRequestResult.Value, Is.EqualTo(errorMessage));
    }

    [Test]
    public async Task Update_WithNullRequest_ReturnsBadRequest()
    {
        var id = "req_id";

        var result = await _controller.Update(id, null);

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        var badRequestResult = result as BadRequestObjectResult;
        Assert.That(badRequestResult.Value, Is.EqualTo("Request is null"));
    }

    [Test]
    public async Task Update_WithEmptyId_ReturnsBadRequest()
    {
        var id = "invalid_id";
        var request = new MaintenanceUpdatedRequest
        {
            Status = MaintenanceStatus.InProgress
        };

        _service.Setup(s => s.UpdateAsync(id, request))
            .ReturnsAsync(Result<MaintenanceRequestResponse>.Failure("Maintenance request not found."));

        var result = await _controller.Update(id, request);

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        var badRequestResult = result as BadRequestObjectResult;
        Assert.That(badRequestResult.Value, Is.EqualTo("Maintenance request not found."));
    }

    [Test]
    public async Task Update_WithValidRequest_ReturnsUpdatedRequest()
    {
        var id = "req_id";
        var request = new MaintenanceUpdatedRequest { Status = MaintenanceStatus.Completed };

        var expectedResponse = new MaintenanceRequestResponse
        {
            Id = id,
            Status = MaintenanceStatus.Completed,
        };

        _service.Setup(s => s.UpdateAsync(id, request))
            .ReturnsAsync(Result<MaintenanceRequestResponse>.Success(expectedResponse));

        var result = await _controller.Update(id, request);

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(expectedResponse));
        Assert.That(((MaintenanceRequestResponse)okResult.Value).Status,
            Is.EqualTo(MaintenanceStatus.Completed));
    }


}

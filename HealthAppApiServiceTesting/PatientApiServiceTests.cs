using HealthAppWebApi.Models;
using HealthAppWebApi.Repositories.Interface;
using HealthAppWebApi.Services.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharedDto.PatientDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class PatientServiceTests
{
    private readonly Mock<IPatientRepository> _repoMock;
    private readonly PatientService _service;

    public PatientServiceTests()
    {
        _repoMock = new Mock<IPatientRepository>();
        _service = new PatientService(_repoMock.Object);
    }

    // ✅ TEST: GetAllPatientsAsync
    [Fact]
    public async Task GetAllPatientsAsync_ReturnsMappedPatients()
    {
        // Arrange
        var patients = new List<Patient>
        {
            new Patient
            {
                PatientId = 1,
                FullName = "John",
                Gender = 1,
                Email = "john@mail.com",
                PhoneNumber = "123",
                CreatedDate = DateTime.UtcNow
            }
        };

        _repoMock.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(patients);

        // Act
        var result = await _service.GetAllPatientsAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("John", result[0].FullName);
    }

    // ✅ TEST: GetPatientByIdAsync
    [Fact]
    public async Task GetPatientByIdAsync_ReturnsPatient()
    {
        var patient = new Patient
        {
            PatientId = 1,
            FullName = "Test User",
            Gender = 1,
            Email = "test@mail.com"
        };

        _repoMock.Setup(r => r.GetByIdAsync(1))
                 .ReturnsAsync(patient);

        var result = await _service.GetPatientByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.PatientId);
    }

    // ✅ TEST: GetPatientByIdAsync (NOT FOUND)
    [Fact]
    public async Task GetPatientByIdAsync_ReturnsNull_WhenNotFound()
    {
        _repoMock.Setup(r => r.GetByIdAsync(1))
                 .ReturnsAsync((Patient)null);

        var result = await _service.GetPatientByIdAsync(1);

        Assert.Null(result);
    }

    // ✅ TEST: RegisterPatientAsync (SUCCESS)
    [Fact]
    public async Task RegisterPatientAsync_AddsPatient()
    {
        var dto = new CreatePatientDto
        {
            FullName = "New User",
            Email = "new@mail.com",
            Gender = "Male",
            DateOfBirth = DateTime.Today.AddYears(-20)
        };

        _repoMock.Setup(r => r.EmailExistsAsync(dto.Email))
                 .ReturnsAsync(false);

        await _service.RegisterPatientAsync(dto);

        _repoMock.Verify(r => r.AddAsync(It.IsAny<Patient>()), Times.Once);
    }

    // ✅ TEST: RegisterPatientAsync (EMAIL EXISTS)
    [Fact]
    public async Task RegisterPatientAsync_Throws_WhenEmailExists()
    {
        var dto = new CreatePatientDto
        {
            Email = "exists@mail.com"
        };

        _repoMock.Setup(r => r.EmailExistsAsync(dto.Email))
                 .ReturnsAsync(true);

        await Assert.ThrowsAsync<Exception>(() =>
            _service.RegisterPatientAsync(dto));
    }

    // ✅ TEST: RegisterPatientAsync (FUTURE DOB)
    [Fact]
    public async Task RegisterPatientAsync_Throws_WhenFutureDOB()
    {
        var dto = new CreatePatientDto
        {
            Email = "test@mail.com",
            DateOfBirth = DateTime.Today.AddDays(1)
        };

        _repoMock.Setup(r => r.EmailExistsAsync(dto.Email))
                 .ReturnsAsync(false);

        await Assert.ThrowsAsync<Exception>(() =>
            _service.RegisterPatientAsync(dto));
    }

    // ✅ TEST: UpdatePatientAsync (SUCCESS)
    [Fact]
    public async Task UpdatePatientAsync_UpdatesPatient()
    {
        var patient = new Patient
        {
            PatientId = 1,
            FullName = "Old Name"
        };

        var dto = new CreatePatientDto
        {
            FullName = "Updated",
            Email = "new@mail.com",
            Gender = "Male",
            DateOfBirth = DateTime.Today.AddYears(-25)
        };

        _repoMock.Setup(r => r.GetByIdAsync(1))
                 .ReturnsAsync(patient);

        await _service.UpdatePatientAsync(1, dto);

        _repoMock.Verify(r => r.UpdateAsync(patient), Times.Once);
        Assert.Equal("Updated", patient.FullName);
    }

    // ✅ TEST: UpdatePatientAsync (NOT FOUND)
    [Fact]
    public async Task UpdatePatientAsync_Throws_WhenNotFound()
    {
        _repoMock.Setup(r => r.GetByIdAsync(1))
                 .ReturnsAsync((Patient)null);

        await Assert.ThrowsAsync<Exception>(() =>
            _service.UpdatePatientAsync(1, new CreatePatientDto()));
    }

    // ✅ TEST: SearchByNameAsync
    [Fact]
    public async Task SearchByNameAsync_ReturnsResults()
    {
        var patients = new List<Patient>
        {
            new Patient { PatientId = 2, FullName = "Aniket" }
        };

        _repoMock.Setup(r => r.SearchByNameAsync("Ani"))
                 .ReturnsAsync(patients);

        var result = await _service.SearchByNameAsync("Ani");

        Assert.Single(result);
        Assert.Equal("Aniket", result[0].FullName);
    }

    // ✅ TEST: GetAppointmentCountAsync
    [Fact]
    public async Task GetAppointmentCountAsync_ReturnsCount()
    {
        _repoMock.Setup(r => r.GetAppointmentCountAsync(1))
                 .ReturnsAsync(5);

        var result = await _service.GetAppointmentCountAsync(1);

        Assert.Equal(5, result);
    }
}
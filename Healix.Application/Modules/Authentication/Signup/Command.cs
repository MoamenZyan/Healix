using Healix.Domain.Entities;
using Healix.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Healix.Application.Modules;

public class UserSignupCommand : IRequest<(ApplicationUserWrapper, string)>
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? UserType { get; set; }
    public Location? Location { get; set; }
    public IFormFile? ProfilePhoto { get; set; }
    public string? PhoneNumber { get; set; }
    public int? YearsOfExperience { get; set; }
    public string? ProfessionalTitle { get; set; }
    public string? NationalId { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public BloodType? BloodType { get; set; }
    public string? Height { get; set; }
    public string? Weight { get; set; }
    public Gender Gender { get; set; }
    public DoctorSpeciality? DoctorSpeciality { get; set; }
    public IFormFile? LicensePractice { get; set; }
}

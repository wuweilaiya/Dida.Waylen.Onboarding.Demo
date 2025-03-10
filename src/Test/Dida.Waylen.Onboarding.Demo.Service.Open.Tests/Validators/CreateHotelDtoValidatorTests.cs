using Dida.Waylen.Onboarding.Demo.Service.Open.Dtos;
using Dida.Waylen.Onboarding.Demo.Service.Open.Infrastructure.Validators;
using Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos;
using Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos.Hotels;

namespace Dida.Waylen.Onboarding.Demo.Service.Open.Tests.Validators;

public class CreateHotelDtoValidatorTests
{
    private readonly CreateHotelValidator _validator;

    public CreateHotelDtoValidatorTests()
    {
        _validator = new CreateHotelValidator(
            new AddressValidator(),
            new ContactValidator(),
            new ImageValidator()
        );
    }

    [Fact]
    public async Task Validate_WithValidData_ShouldPass()
    {
        // Arrange
        var dto = new CreateHotelDto
        {
            Name = "Test Hotel",
            Address = new AddressDto
            {
                Latitude = 1,
                Longitude = 1,
                Detail = "Test Address"
            },
            HotelStarRating = HotelStarRatingEnum.FiveStar,
            Image = new ImageDto
            {
                Url = "https://test.com/test.jpg"
            },
            Contact = new ContactDto
            {
                PhoneNumber = "15168440402"
            }
        };

        // Act
        var result = await _validator.ValidateAsync(dto);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Validate_WithInvalidName_ShouldFail(string name)
    {
        // Arrange
        var dto = new CreateHotelDto
        {
            Name = name,
            Address = new AddressDto
            {
                Detail = "Test Address"
            },
            Contact = new ContactDto()
        };

        // Act
        var result = await _validator.ValidateAsync(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateHotelDto.Name));
    }
}

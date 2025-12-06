using FluentValidation.TestHelper;
using TaskManagement.Models;
using Xunit;

public class TaskCreateDtoValidatorTests
{
    private readonly TaskCreateDtoValidator _validator;

    public TaskCreateDtoValidatorTests()
    {
        _validator = new TaskCreateDtoValidator();
    }

    [Fact]
    public void Valid_model_should_pass_validation()
    {
        // Arrange
        var dto = new TaskCreateDto
        {
            Title = "Geçerli Başlık",
            Description = "Bu geçerli bir açıklamadır.",
            IsCompleted = false
        };

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Empty_title_should_have_error()
    {
        var dto = new TaskCreateDto
        {
            Title = "",
            Description = "Geçerli açıklama",
            IsCompleted = false
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Short_title_should_have_error()
    {
        var dto = new TaskCreateDto
        {
            Title = "ab", // 2 karakter, kural min 3
            Description = "Geçerli açıklama",
            IsCompleted = false
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Empty_description_should_have_error()
    {
        var dto = new TaskCreateDto
        {
            Title = "Geçerli Başlık",
            Description = "",
            IsCompleted = false
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public void Short_description_should_have_error()
    {
        var dto = new TaskCreateDto
        {
            Title = "Geçerli Başlık",
            Description = "kısa", // 4 karakter, kural min 5
            IsCompleted = false
        };

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
}


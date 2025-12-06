using FluentValidation;
using TaskManagement.Models;

public class TaskCreateDtoValidator : AbstractValidator<TaskCreateDto>
{
    public TaskCreateDtoValidator()
    {
        RuleFor(x => x.Title)
        .NotEmpty().WithMessage("Title boş olamaz")
        .MinimumLength(3).WithMessage("Title en az 3 karakter olmalı");


    RuleFor(x => x.Description)
        .NotEmpty().WithMessage("Description zorunludur")
        .MinimumLength(5).WithMessage("Description en az 5 karakter olmalı");
    }


}


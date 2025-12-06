using FluentValidation;
using TaskManagement.Models;

public class TaskUpdateDtoValidator : AbstractValidator<TaskUpdateDto>
{
    public TaskUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
        .GreaterThan(0).WithMessage("Id 0'dan büyük olmalı");


    RuleFor(x => x.Title)
        .NotEmpty().WithMessage("Title boş olamaz");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description boş olamaz");
    }


}


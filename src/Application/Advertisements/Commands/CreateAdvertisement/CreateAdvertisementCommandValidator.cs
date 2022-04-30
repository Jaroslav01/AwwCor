using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Advertisements.Commands.CreateAdvertisement;

public class CreateAdvertisementCommandValidator : AbstractValidator<CreateAdvertisementCommand>
{
    public CreateAdvertisementCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Description)
            .MaximumLength(2000)
            .NotEmpty();

        RuleFor(v => v.Amount)
            .GreaterThan(-1L);

        RuleFor(v => v.ImagesUrl)
            .Must(list => list.Count <= 3)
            .WithMessage($"The length of 'ImagesUrl' array must be 3 elements or fewer.")
            .NotEmpty();
    }
}
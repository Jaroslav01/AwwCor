using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Advertisements.Queries.GetAdvertisementByIdQuery;

public class GetAdvertisementByIdQueryValidator : AbstractValidator<GetAdvertisementByIdQuery>
{
    public GetAdvertisementByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.AllFields)
            .NotNull();
    }
}

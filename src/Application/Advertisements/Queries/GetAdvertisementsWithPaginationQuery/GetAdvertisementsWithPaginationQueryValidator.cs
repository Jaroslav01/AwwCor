using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitecture.Application.Advertisements.Queries.GetAdvertisementsWithPagination;

public class GetAdvertisementsWithPaginationQueryValidator : AbstractValidator<GetAdvertisementsWithPaginationQuery>
{
    public GetAdvertisementsWithPaginationQueryValidator()
    {
        //RuleFor(x => x.ListId)
            //.NotEmpty().WithMessage("ListId is required.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");

        RuleFor(x => x.OrderBy)
            .IsInEnum();

        RuleFor(x => x.OrderType)
            .IsInEnum();
    }
}

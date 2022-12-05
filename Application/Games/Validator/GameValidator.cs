using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Types;
using FluentValidation;

namespace Application.Games.Validator
{
  public class GameValidator : AbstractValidator<Game>
  {
    public GameValidator()
    {
      RuleFor(x => x.Title).NotEmpty();
      RuleFor(x => x.Description).NotEmpty();
      RuleFor(x => x.Category).NotEmpty();
      RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
      RuleFor(x => x.Stock).NotEmpty().GreaterThanOrEqualTo(0);
    }
  }
}
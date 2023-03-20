using FluentValidation;
using sosialClone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryAplication.Tools
{
    public class Validation : AbstractValidator<Entities>
    {
        public Validation()
        {
            RuleFor(x=>x.Title).MinimumLength(6);
            RuleFor(x=>x.Venue).NotEmpty();
            RuleFor(x=>x.City).NotEmpty();
            RuleFor(x=>x.Description).MaximumLength(50);
            RuleFor(x=>x.Date).NotEmpty();
            RuleFor(x=>x.Catagory).NotEmpty();
           
        }
    }
}
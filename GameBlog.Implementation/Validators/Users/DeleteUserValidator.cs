using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Implementation.Validators.Users
{
    public class DeleteUserValidator : AbstractValidator<int>
    {
        public DeleteUserValidator()
        {
            RuleFor(x => x).NotEmpty();
        }
    }
}

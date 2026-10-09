using FluentValidation;
using SocialMedia.Core.DTOs;

namespace SocialMedia.Services.Validators
{
    public class PostDtoValidator:AbstractValidator<PostDto>
    {
        public PostDtoValidator()
        {
            //validacion del UserID
            RuleFor(x => x.UserId)
                    .NotEmpty().WithMessage("El IdUser es requerido ")
                    .GreaterThan(0).WithMessage("El id User debe ser mayor  0 ");

            RuleFor(x=> x.Description).MinimumLength(5).WithMessage("Minimo 5 caracteres ")
                .MaximumLength(20).WithMessage("maximo 20 caracteres ");
        }

    }
}

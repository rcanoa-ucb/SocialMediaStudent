using System.Globalization;
using FluentValidation;
using SocialMedia.Core.DTOs;
namespace SocialMedia.Services.Validators;
public class PostDtoValidator : AbstractValidator<PostDto>
{
    public PostDtoValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.Description).NotEmpty().MinimumLength(5).MaximumLength(500);
        RuleFor(x => x.Date).Must(BeValidDate).WithMessage("La fecha debe ser yyyy-MM-dd o ISO 8601.");
        RuleFor(x => x.Image).Must(x => string.IsNullOrWhiteSpace(x) ||
            (Uri.TryCreate(x, UriKind.Absolute, out var uri) &&
             (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)))
            .WithMessage("La imagen debe tener URL HTTP/HTTPS.");
    }
    private static bool BeValidDate(string? input) =>
        DateTime.TryParse(input, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
}

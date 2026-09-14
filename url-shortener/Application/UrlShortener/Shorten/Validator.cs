using System.ComponentModel.DataAnnotations;

namespace Application.UrlShortener.Shorten
{
    public static class UrlShortenDtoValidator
    {
        public static string[] Validate(UrlShortenerDto dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(
                dto,
                validationContext,
                validationResults,
                validateAllProperties: true);

            if (isValid)
            {
                return [];
            }

            return validationResults
                .Where(result => !string.IsNullOrWhiteSpace(result.ErrorMessage))
                .Select(result => result.ErrorMessage)
                .ToArray();
        }
    }
}

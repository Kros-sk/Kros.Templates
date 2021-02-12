using FluentValidation;

namespace Kros.CqrsTemplate.Application.Commands
{
    /// <summary>
    /// Validator for <see cref="UpdateRRREntityNameRRR_Command"/>.
    /// </summary>
    public class UpdateRRREntityNameRRR_CommandValidator : AbstractValidator<UpdateRRREntityNameRRR_Command>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public UpdateRRREntityNameRRR_CommandValidator()
        {
        }
    }
}

using Kros.AspNetCore;
using Kros.AspNetCore.Authorization;
using Kros.CqrsTemplate.Application.Commands;
using Kros.CqrsTemplate.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kros.CqrsTemplate.Application.Controllers
{
    /// <summary>
    /// RRREntityNameRRR_Plural_ controller
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtAuthorizationHelper.JwtSchemeName)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class RRREntityNameRRR_Plural_Controller : ApiBaseController
    {
        /// <summary>
        /// Get RRREntityNameRRR_Plural_.
        /// <response code="200">Ok.</response>
        /// <response code="403">
        /// Forbidden when user don't have permission for RRREntityNameRRR_Plural_.
        /// </response>
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllRRREntityNameRRR_Plural_Query.RRREntityNameRRR_>))]
        public async Task<IEnumerable<GetAllRRREntityNameRRR_Plural_Query.RRREntityNameRRR_>> Get()
            => await this.SendRequest(new GetAllRRREntityNameRRR_Plural_Query());

        /// <summary>
        /// Get RRREntityNameRRR_ by id.
        /// </summary>
        /// <response code="200">Ok.</response>
        /// <response code="403">
        /// Forbidden when user don't have permission for RRREntityNameRRR_ with <paramref name="id"/>.
        /// </response>
        /// <response code="404">If RRREntityNameRRR_ with id <paramref name="id"/> doesn't exist.</response>
        [HttpGet("{id}", Name = nameof(GetRRREntityNameRRR_))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetRRREntityNameRRR_Query.RRREntityNameRRR_))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<GetRRREntityNameRRR_Query.RRREntityNameRRR_> GetRRREntityNameRRR_(long id)
            => await this.SendRequest(new GetRRREntityNameRRR_Query(id));

        /// <summary>
        /// Create new RRREntityNameRRR_.
        /// </summary>
        /// <param name="command">Data for creating RRREntityNameRRR_.</param>
        /// <response code="201">Created. RRREntityNameRRR_ id in body.</response>
        /// <response code="403">
        /// Forbidden when user don't have permission for creating RRREntityNameRRR_.
        /// </response>
        /// <returns>
        /// RRREntityNameRRR_ id.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateRRREntityNameRRR_(CreateRRREntityNameRRR_Command command)
            => await this.SendCreateCommand(command, nameof(GetRRREntityNameRRR_));

        /// <summary>
        /// Update RRREntityNameRRR_.
        /// </summary>
        /// <param name="id">RRREntityNameRRR_ id.</param>
        /// <param name="command">Data for creating RRREntityNameRRR_.</param>
        /// <response code="200">Updated.</response>
        /// <response code="403">Forbidden when user don't have permission for RRREntityNameRRR_ with <paramref name="id"/>.</response>
        /// <response code="404">If RRREntityNameRRR_ with id <paramref name="id"/> doesn't exist.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateRRREntityNameRRR_(long id, UpdateRRREntityNameRRR_Command command)
        {
            command.Id = id;

            await this.SendRequest(command);

            return Ok();
        }

        /// <summary>
        /// Delete RRREntityNameRRR_.
        /// </summary>
        /// <param name="id">RRREntityNameRRR_ id.</param>
        /// <response code="204">Deleted.</response>
        /// <response code="403">Forbidden when user don't have permission for RRREntityNameRRR_ with <paramref name="id"/>.</response>
        /// <response code="404">If RRREntityNameRRR_ with id <paramref name="id"/> doesn't exist.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteRRREntityNameRRR_(long id)
        {
            await this.SendRequest(new DeleteRRREntityNameRRR_Command(id));

            return NoContent();
        }
    }
}

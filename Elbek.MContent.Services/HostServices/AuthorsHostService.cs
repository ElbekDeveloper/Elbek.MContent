using Elbek.MContent.Services.CoreServices;
using Elbek.MContent.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Elbek.MContent.Services.HostServices
{
    [ApiController]
    [Route("/api/Authors")]
    public class AuthorsHostService : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorsHostService(IAuthorService service)
        {
            _service = service;
        }
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "All Authors", Type = typeof(MContentResult<IEnumerable<AuthorDto>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<MContentResult<AuthorDto>>> GetAuthors()
        {
            return Ok(await _service.GetAuthorsAsync());
        }
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Author by Id", Type = typeof(MContentResult<IEnumerable<AuthorDto>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<MContentResult<AuthorDto>>> GetAuthorById([FromRoute]Guid id)
        {
            return Ok(await _service.GetAuthorByIdAsync(id));
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Author added ", Type = typeof(MContentResult<AuthorDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<MContentResult<AuthorDto>>> AddAuthor([FromBody] AuthorDto authorDto)
        {
            return Ok(await _service.AddAuthorAsync(authorDto));
        }
    }
}

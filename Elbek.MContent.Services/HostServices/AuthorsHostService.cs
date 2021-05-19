using Elbek.MContent.Services.CoreServices;
using Elbek.MContent.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public async Task<MContentResult<IList<AuthorDto>>> GetAuthors()
        {
            return await _service.GetAuthorsAsync();
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Author by Id", Type = typeof(MContentResult<IEnumerable<AuthorDto>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<MContentResult<AuthorDto>> GetAuthorById([FromRoute]Guid id)
        {
            return await _service.GetAuthorByIdAsync(id);
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Author added ", Type = typeof(MContentResult<AuthorDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<MContentResult<AuthorDto>> AddAuthor([FromBody] AuthorDto authorDto)
        {
            return await _service.AddAuthorAsync(authorDto);
        }


        /// TODO 2 ActionResult и Ok убрать
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Author Updated ", Type = typeof(MContentResult<AuthorDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<MContentResult<AuthorDto>>> UpdateAuthor([FromRoute][Required] Guid id, [FromBody][Required] AuthorDto authorDto)
        {
            return Ok(await _service.UpdateAuthorAsync(id, authorDto));
        }
    }
}

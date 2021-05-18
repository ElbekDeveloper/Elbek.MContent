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

        /// Todo 2.3 все эти методы не должны возвращать ActionResult.
        /// методы должны возвращать Task<MContentResult<AuthorDto>>

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
        public async Task<ActionResult<MContentResult<AuthorDto>>> GetAuthorById([FromRoute]Guid id)
        {
            /// todo 2.4 Ok(), а что если такого автора нет ? OK() все равно вернет код 200
            /// методы сервиса IAuthorService должны возвращать <MContentResult<AuthorDto>
            /// не нужно использвать тут  Ok() или BadRequest()
            /// это касается всех методов этого класса
            return Ok(await _service.GetAuthorByIdAsync(id));
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Author added ", Type = typeof(MContentResult<AuthorDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<MContentResult<AuthorDto>>> AddAuthor([FromBody] AuthorDto authorDto)
        {
            return Ok(await _service.AddAuthorAsync(authorDto));
        }

        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Author Updated ", Type = typeof(MContentResult<AuthorDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<MContentResult<AuthorDto>>> UpdateAuthor([FromRoute][Required] Guid id, [FromBody][Required] AuthorDto authorDto)
        {
            if (id != authorDto.Id)
            {
                return BadRequest();
            }
            return Ok(await _service.UpdateAuthorAsync(authorDto));
        }
    }
}

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
    [Route("/api/Contents")]
    public class ContentsHostService : ControllerBase
    {
        private readonly IContentService _service;

        public ContentsHostService(IContentService service)
        {
            _service = service;
        }
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "All Content", Type = typeof(MContentResult<IList<ContentDto>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<MContentResult<IList<ContentDto>>> GetContents()
        {
            return await _service.GetAllAsync();
        }

        [HttpGet]
        [Route("{type:int}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "All Content by Type", Type = typeof(MContentResult<IList<ContentDto>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<MContentResult<IList<ContentDto>>> GetContents([FromRoute]int type)
        {
            return await _service.GetByType(type);
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Content added ", Type = typeof(MContentResult<ContentDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<MContentResult<ContentDto>> AddContent([FromBody] ContentDto contentDto)
        {
            return await _service.AddAsync(contentDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Content by Id", Type = typeof(MContentResult<ContentDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<MContentResult<ContentDto>> GetContentById([FromRoute] Guid id)
        {
            return await _service.GetByIdAsync(id);
        }
    }
}

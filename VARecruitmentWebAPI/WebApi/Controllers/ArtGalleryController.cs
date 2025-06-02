using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VAArtGalleryWebAPI.Application.Commands;
using VAArtGalleryWebAPI.Application.Queries;
using VAArtGalleryWebAPI.Domain.Entities;
using VAArtGalleryWebAPI.WebApi.Models;

namespace VAArtGalleryWebAPI.WebApi.Controllers
{
    [Route("api/art-galleries")]
    [ApiController]
    public class ArtGalleryController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<GetAllArtGalleriesResult>>> GetAllGalleries()
        {
            var galleries = await mediator.Send(new GetAllArtGalleriesQuery());

            var result = galleries.Select(g => new GetAllArtGalleriesResult(g.Id, g.Name, g.City, g.Manager, g.ArtWorksOnDisplay?.Count ?? 0)).ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("{galleryId}")]
        public async Task<ActionResult<GetArtGalleryResult>> GetGalleryById([FromRoute] Guid galleryId)
        {
            var gallery = await mediator.Send(new GetArtGalleryByIdQuery(galleryId));

            if (gallery == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, null);
            }

            var result = new GetArtGalleryResult(gallery.Id, 
                gallery.Name,
                gallery.City,
                gallery.Manager,
                gallery.ArtWorksOnDisplay?
                .Select(a => new GetArtGalleryArtWorksResult(a.Id,a.Name, a.Author, a.CreationYear, a.AskPrice)).ToList());

            return Ok(result);
        }

        [HttpGet]
        [Route("{galleryId}/art-works")]
        public async Task<ActionResult<List<GetArtGalleryArtWorksResult>>> GetAllGalleryArtWorks([FromRoute] Guid galleryId)
        {
            var artWorks = await mediator.Send(new GetArtGalleryArtWorksQuery(galleryId));

            var result = artWorks.Select(g => new GetArtGalleryArtWorksResult(g.Id, g.Name, g.Author, g.CreationYear, g.AskPrice)).ToList();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreateArtGalleryResult>> CreateArtGallery([FromBody] CreateArtGalleryRequest request)
        {
            var artGallery = await mediator.Send(request.ToCommand());

            var result = new CreateArtGalleryResult(artGallery.Id);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut]
        [Route("{galleryId}")]
        public async Task<ActionResult<CreateArtGalleryResult>> UpdateArtGallery([FromBody] UpdateArtGalleryRequest request)
        {
            var artGallery = await mediator.Send(request.ToCommand());

            var result = new CreateArtGalleryResult(artGallery.Id);

            return StatusCode(StatusCodes.Status201Created, result);
        }


        [HttpDelete]
        [Route("{galleryId}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute] Guid galleryId)
        {
            var resultSuccess = await mediator.Send(new DeleteArtGalleryCommand(galleryId));

            var status = resultSuccess ? StatusCodes.Status200OK : StatusCodes.Status404NotFound;

            return StatusCode(status, resultSuccess);
        }

    }
}

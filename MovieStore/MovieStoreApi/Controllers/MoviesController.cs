using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Api.Movies.Commands;
using MovieStore.Api.Movies.Queries;
using MovieStore.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieStore.Api.Controllers
{
    //[Authorize()]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<Movie>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<Movie>>> GetAllMovies()
        {
            var movies = await _mediator.Send(new GetAllMovies.Query());

            if (movies is null || movies.Count == 0)
                return NotFound();

            return Ok(movies);

        }
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Movie>> GetMovieById([FromRoute] GetMovieById.Query query)
        {
            var movie = await _mediator.Send(query);

            return movie is null ? NotFound() : Ok(movie);
        }

        [HttpGet("{PageNumber}/{PageSize}")]
        [ProducesResponseType(typeof(IList<Movie>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<Movie>>> GetMoviePagination([FromRoute] GetMoviesPagination.Query query)
        {
            var movies = await _mediator.Send(query);

            if (movies is null || movies.Count == 0)
                return NotFound();

            return Ok(movies);
        }
        [HttpGet("/movieCount")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetMovieCount()
        {
            var movieCount = await _mediator.Send(new GetMovieCount.Query());

            return Ok(movieCount);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateMovie(CreateMovie.Command command)
        {
            await _mediator.Send(command);
            return Ok();
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateMovie(UpdateMovie.Command command)
        {
            var result = await _mediator.Send(command);

            return result ? Ok() : NotFound();
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteMovieById(DeleteMovieById.Command command)
        {
            var result = await _mediator.Send(command);

            return result ? Ok() : NotFound();
        }
    }
}

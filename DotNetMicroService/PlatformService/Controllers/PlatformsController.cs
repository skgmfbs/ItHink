using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;

        public PlatformsController(IPlatformRepo repository, IMapper mapper, ICommandDataClient commandDataClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAll()
        {
            var platforms = _repository.GetAll();
            var readDtos = _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);

            return Ok(readDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<PlatformReadDto> GetById(int id)
        {
            var platform = _repository.Get(id);
            if (platform == null)
            {
                return NotFound($"'{id}' not found.");
            }

            var readDto = _mapper.Map<PlatformReadDto>(platform);

            return Ok(readDto);
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> Create(PlatformCreateDto createDto)
        {
            var platform = _mapper.Map<Platform>(createDto);
            _repository.Create(platform);
            if (!_repository.SaveChanges())
            {
                return BadRequest();
            }

            var readDto = _mapper.Map<PlatformReadDto>(platform);

            try
            {
                await _commandDataClient.SendPlatformToCommand(readDto);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"SendPlatformToCommand failed: {ex.Message}");
            }

            return CreatedAtRoute(string.Empty, new { id = readDto.Id }, readDto);
        }
    }
}
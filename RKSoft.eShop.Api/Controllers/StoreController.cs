using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RKSoft.eShop.App.DTOs;
using RKSoft.eShop.App.Interfaces;
using RKSoft.eShop.Model.Entities;

namespace RKSoft.eShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly IMapper _mapper;
        public StoreController(IStoreService storeService, IMapper mapper)
        {
            _storeService = storeService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all", Name = "GetAllStores")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllStores()
        {
            var stores = await _storeService.GetAllStoreAsync();
            var storesDTOdata = _mapper.Map<List<StoreDTO>>(stores);

            return Ok(storesDTOdata);
        }

        [HttpGet]
        [Route ("{id:int}", Name = "GetStoreById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetStoreById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var store = await _storeService.GetStoreByIdAsync(store => store.Id == id);
            if (store == null) return NotFound($"The store with id {id} not found");

            var StoreDTOdata = _mapper.Map<StoreDTO>(store);
            return Ok(StoreDTOdata);
        }
        
        [HttpPost]
        [Route("add", Name = "CreateStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateStore([FromBody] StoreDTO dto)
        {
            if (dto == null) return BadRequest();

            EStore store = _mapper.Map<EStore>(dto);
            var storeAfterCreation = await _storeService.CreateStoreAsync(store);

            dto.Id = storeAfterCreation.Id;
            return CreatedAtRoute(nameof(GetStoreById), new { id = dto.Id }, dto);
        }

        [HttpPut]
        [Route("update", Name = "UpdatStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdatStore(StoreDTO dto)
        {
            if (dto == null) return BadRequest();

            var existingStoreData = await _storeService.GetStoreByIdAsync(store => store.Id == dto.Id);
            if (existingStoreData == null)
                return NotFound();

            var newRecord = _mapper.Map<EStore>(dto);

            await _storeService.UpdateStoreAsync(newRecord);
            return Ok(newRecord);
        }

        [HttpDelete]
        [Route("{id:int}/delete", Name = "DeleteStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteStore(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var store = await _storeService.GetStoreByIdAsync(store => store.Id == id);
            if (store == null)
                return BadRequest($"The store with Id {id} not found");
            var result = await _storeService.DeleteStoreAsync(store);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

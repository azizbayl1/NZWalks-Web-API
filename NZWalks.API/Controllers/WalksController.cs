using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        #region CREATE A NEW WALK
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            //Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDTO);

            await walkRepository.CreateAsync(walkDomainModel);

            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }
        #endregion

        #region GET ALL WALKS
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await walkRepository.GetAllAsync();

            return Ok(mapper.Map<List<WalkDTO>>(walksDomainModel));
        }
        #endregion

        #region GET A SINGLE WALK
        [HttpGet]
        [Route("Get{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }
        #endregion

        #region UPDATE A WALK
        [HttpPut]
        [Route("Update{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            //Map DTO to Domain model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDTO);

            walkDomainModel= await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }
        #endregion

        #region DELTE A WALK
        [HttpDelete]
        [Route("Delete{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkdomainModel = await walkRepository.DeleteAsync(id);

            if(deletedWalkdomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDTO>(deletedWalkdomainModel));
        }
        #endregion
    }
}

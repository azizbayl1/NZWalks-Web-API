﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        #region GET ALL REGIONS

        [HttpGet]
        //[Route("GetAll")]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy = null, [FromQuery] bool isAscending = true,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 25)
        {
            _logger.LogInformation("GetAll regions action method invoked");

            //Get Data From Database (Domain models)
            var regionsDomain = await _regionRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);

            _logger.LogInformation($"GetAll regions action method completed: {JsonSerializer.Serialize(regionsDomain)}");

            return Ok(_mapper.Map<List<RegionDTO>>(regionsDomain));
        }

        #endregion


        #region GET A SINGLE REGION (by Id)

        [HttpGet("{id:guid}")]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            //Get Region Domain Model from Database
            var regionDomain = await _regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionDTO>(regionDomain));
        }

        #endregion


        #region CREATE A NEW REGION

        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDTO addRegionRequestDto)
        {
            //Convert DTO to Domain Model
            var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);

            //Use Domain Model to create region
            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

            //Map Domain model back to DTO
            var regionDto = _mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(_mapper.Map<RegionDTO>(regionDomainModel));
        }

        #endregion


        #region UPDATE REGION (by Id)

        [HttpPut("{id:guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            //Map DTO to Domain Model
            var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDTO);

            //Checking region is exists or not
            regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionDTO>(regionDomainModel));
        }

        #endregion


        #region DELETE REGION (by Id)

        [HttpDelete("{id:guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await _regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Convert Domain Model to DTO to return Deleted region back
            return Ok(_mapper.Map<RegionDTO>(regionDomainModel));
        }

        #endregion
    }
}

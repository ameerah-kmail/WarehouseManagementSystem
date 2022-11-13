﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using WarehouseManagement.Entits;
using WarehouseManagement.Models;
using WarehouseManagement.Services;

namespace WarehouseManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ContainersController : ControllerBase
    {
        private readonly IContainerRepository _repository;
        private readonly IMapper _mapper;
        public ContainersController(IContainerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ContainerDto>>> GetAllContainers()
        {
            var containerEntities = await _repository.GetAllContainers();
            var containers = _mapper.Map<List<ContainerDto>>(containerEntities);
            return Ok(containers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContainerDto>> get(int id)
        {
            var containerEntity = await _repository.GetContainer(id);
            if (containerEntity == null)
                return NotFound();
            return Ok(_mapper.Map<ContainerDto>(containerEntity));
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int _containerId)
        {
            var containerEntity = await _repository.GetContainer(_containerId);
            if (containerEntity == null)
                return NotFound();
            //_mapper.Map<Containerr>(containerEntity);
            _repository.DeleteContainer(_containerId);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> update(ContainerDto _containerDto)
        {
            var container = await _repository.GetContainer(_containerDto.ContainerId);
            
            var containerEntity= _mapper.Map<Containerr>(container);
            if (containerEntity == null)
                return NotFound();
             _repository.UpdateContainer(containerEntity);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> create(ContainerDto _containerDto)
        {
           
            var containerEntity = _mapper.Map<Containerr>(_containerDto);
            await _repository.AddContainer(containerEntity);
            return Ok();

        }
    }
}

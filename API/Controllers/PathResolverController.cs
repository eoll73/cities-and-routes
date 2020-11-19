﻿using DataAccess.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service.PathResolver;
using Service.Services.Interfaces;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("{controller}")]
    [ApiController]
    public class PathResolverController : ControllerBase
    {
        private readonly IAlgorithmService _algorithmService;

        public PathResolverController(IAlgorithmService AlgorithmService)
        {
            _algorithmService = AlgorithmService;
        }

        [HttpPost]
        [Route("FindShortestPath")]
        public IActionResult FindShortestPath([FromBody] PathResolverDTO Dto)
        {
            return Ok(_algorithmService.FindShortestPath(Dto.MapId, Dto.CityFromId, Dto.CityToId));
        }

        [HttpPost]
        [Route("solve-travel-salesman-annealing")]
        public async Task<IActionResult> SolveTravelSalesmanAnnealing([FromBody] TravelSalesmanRequest BodyRequest)
        {
            var response = await _algorithmService.SolveAnnealingTravelSalesman(BodyRequest);
            if (response == default) return BadRequest();
            return Ok(response);

        }
        [HttpPost]
        [Route("solve-travel-salesman-nearest")]
        public async Task<IActionResult> SolveTravelSalesmanNearest([FromBody] TravelSalesmanRequest BodyRequest)
        {
            var response = await _algorithmService.SolveNearestNeghborTravelSalesman(BodyRequest);
            if (response == default) return BadRequest();
            return Ok(response);
        }
        [HttpPost]
        [Route("solve-travel-salesman-quickest")]
        public IActionResult Experiment([FromBody] TravelSalesmanRequest BodyRequest)
        {
            var taskArr = new Task<TravelSalesmanResponse>[]
            {
                _algorithmService.SolveNearestNeghborTravelSalesman(BodyRequest),
                _algorithmService.SolveAnnealingTravelSalesman(BodyRequest)
            };
            
            var task = Task.WhenAny(taskArr).Result;
            var response = task.Result;
            return Ok(response);
        }


    }
}

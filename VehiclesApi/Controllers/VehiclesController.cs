using Microsoft.AspNetCore.Mvc;
using VehiclesApi.Interfaces;
using VehiclesApi.Models;

namespace VehiclesApi.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class VehiclesController : ControllerBase
{
    private readonly IVehicle _vehicleService;

    public VehiclesController(IVehicle vehicleService)
    {
        _vehicleService = vehicleService;
    }
    
    // GET: api/<VehiclesController>
    [HttpGet]
    public async Task<IEnumerable<Vehicle>> Get()
    {
        return await _vehicleService?.GetAllVehicles();
    }
    
    // GET: api/<VehiclesController>/5
    [HttpGet("{id}")]
    public async Task<Vehicle?> Get(int id)
    {
        return await _vehicleService.GetVehicleById(id);
    }
    
    // POST: api/<VehiclesController>
    [HttpPost]
    public async Task AddVehicle([FromBody] Vehicle vehicle)
    {
        await _vehicleService.AddVehicle(vehicle);
    }
    
    // PUT: api/<VehiclesController>/5
    [HttpPut("{id}")]
    public async Task UpdateVehicle(int id, [FromBody] Vehicle vehicle)
    {
        await _vehicleService.UpdateVehicle(id, vehicle);
    }
    
    // DELETE: api/<VehiclesController>/5
    [HttpDelete("{id}")]
    public async Task DeleteVehicle(int id)
    {
        await _vehicleService.DeleteVehicle(id);
    }
}
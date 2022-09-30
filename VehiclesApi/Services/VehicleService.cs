using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using VehiclesApi.Data;
using VehiclesApi.Interfaces;
using VehiclesApi.Models;

namespace VehiclesApi.Services;

public class VehicleService : IVehicle
{
    private readonly ApiDbContext _dbContext;

    public VehicleService(ApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Vehicle>> GetAllVehicles()
    {
        Debug.Assert(_dbContext.Vehicles != null, "dbContext.Vehicles != null");
        return await _dbContext.Vehicles.ToListAsync();
    }

    public async Task<Vehicle?> GetVehicleById(int id)
    {
        Debug.Assert(_dbContext.Vehicles != null, "dbContext.Vehicles != null");
        return await _dbContext.Vehicles.FindAsync(id);
    }

    public async Task AddVehicle(Vehicle vehicle)
    {
        Debug.Assert(_dbContext.Vehicles != null, "dbContext.Vehicles != null");
        await _dbContext.Vehicles.AddAsync(vehicle);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateVehicle(int id, Vehicle vehicle)
    {
        _dbContext.Entry(await GetVehicleById(id))
            .CurrentValues.SetValues(vehicle);
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteVehicle(int id)
    {
        Debug.Assert(_dbContext.Vehicles != null, "dbContext.Vehicles != null");
        _dbContext.Vehicles.Remove(await _dbContext.Vehicles.SingleAsync(v => v.Id == id));
    }
}
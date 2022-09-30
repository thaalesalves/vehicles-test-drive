using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using VehiclesApi.Data;
using VehiclesApi.Interfaces;
using VehiclesApi.Models;

namespace VehiclesApi.Services;

public class VehicleService : IVehicle
{
    private ApiDbContext dbContext;

    public VehicleService()
    {
        dbContext = new ApiDbContext();
    }
    
    public async Task<List<Vehicle>> GetAllVehicles()
    {
        Debug.Assert(dbContext.Vehicles != null, "dbContext.Vehicles != null");
        return await dbContext.Vehicles.ToListAsync();
    }

    public async Task<Vehicle?> GetVehicleById(int id)
    {
        Debug.Assert(dbContext.Vehicles != null, "dbContext.Vehicles != null");
        return await dbContext.Vehicles.FindAsync(id);
    }

    public async Task AddVehicle(Vehicle vehicle)
    {
        Debug.Assert(dbContext.Vehicles != null, "dbContext.Vehicles != null");
        await dbContext.Vehicles.AddAsync(vehicle);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateVehicle(int id, Vehicle vehicle)
    {
        dbContext.Entry(await GetVehicleById(id))
            .CurrentValues.SetValues(vehicle);
        
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteVehicle(int id)
    {
        Debug.Assert(dbContext.Vehicles != null, "dbContext.Vehicles != null");
        dbContext.Vehicles.Remove(await dbContext.Vehicles.SingleAsync(v => v.Id == id));
    }
}
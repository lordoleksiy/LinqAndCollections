using System;
using CollectionsAndLinq.BL.Services;
using CollectionsAndLinq.BLL.Interfaces;
using CollectionsAndLinq.BLL.Services;
using CollectionsAndLinq.DAL.DB;
using CollectionsAndLinq.DAL.Interfaces;
using CollectionsAndLinq.DAL.Repositories;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;

namespace CollectionsAndLinq.WebApi.Infrastructure;

public static class ConfigServices
{
    private static IConfiguration Configuration;
    public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
    {
        Configuration = config;

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static IServiceCollection AddMyDependencyGroup(
            this IServiceCollection services)
    {
        services.AddDbContext<DataContext>(options => options
        .UseSqlServer(Configuration.GetConnectionString("DbContext")), ServiceLifetime.Scoped);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAutoMapper, MyAutoMapper>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IDataProcessingService, DataProcessingService>();
        return services;
    }
}

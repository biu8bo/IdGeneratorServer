using IdGeneratorServer.Application;
using Microsoft.AspNetCore.Cors;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace IdGeneratorServer.Web;
[DependsOn(   typeof(AbpAutofacModule),
    typeof(ApplicationModule),
    typeof(AbpAspNetCoreMvcModule) ,//自动api控制器
    typeof(AbpAspNetCoreSerilogModule),//日志系统
    typeof(AbpSwashbuckleModule))]
public class WebModule:AbpModule
{
    private const string DefaultCorsPolicyName = "Default";

    public override Task ConfigureServicesAsync(ServiceConfigurationContext context)
    { 
        var configuration = context.Services.GetConfiguration();
// Add services to the container.

        context.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        context.Services.AddEndpointsApiExplorer();
        context.Services.AddSwaggerGen();
 
        //配置自动Api控制器
        Configure<AbpAspNetCoreMvcOptions>(option =>
            //开启自动API控制器
            // //http://localhost:5233/api/app/abp-domain-exercise/on-completed   
            option.ConventionalControllers.Create(typeof(ApplicationModule).Assembly));
        //跨域
        context.Services.AddCors(options =>
        {
            options.AddPolicy(DefaultCorsPolicyName, builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]!
                            .Split(";", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return Task.CompletedTask;
    }

    public override   Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var app =     context.GetApplicationBuilder();
        
        app.UseRouting();

        //跨域
        app.UseCors(DefaultCorsPolicyName);
// Configure the HTTP request pipeline.
        if (context.GetEnvironment() .IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        //终节点
        app.UseConfiguredEndpoints();
 
         return Task.CompletedTask;
    }

 
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Internal;
using Amazon.DynamoDBv2;
using Members.Core.Repositories.Abstract;
using Members.Core.Repositories.Implementation;
using Members.Infrastructure.Mappers;
using Members.Infrastructure.Services.Abstract;
using Members.Infrastructure.Services.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Amazon;
using Members.Infrastructure.Services.RandomUser;
using Swashbuckle.AspNetCore.Swagger;

namespace Members.API
{
    public class Startup
    {
        public const string AppS3BucketKey = "AppS3Bucket";
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            if (_env.IsDevelopment())
            {
                services.AddSingleton<IAmazonDynamoDB>(cc =>
                {
                    var clientConfig = new AmazonDynamoDBConfig { ServiceURL = "http://localhost:5001" };
                    return new AmazonDynamoDBClient(clientConfig);
                });
            }
            else
            {
                services.AddAWSService<IAmazonDynamoDB>();
                services.AddDefaultAWSOptions(
                    new AWSOptions
                    {
                        Region = RegionEndpoint.GetBySystemName("us-east-2")
                    });
            }


            services.AddAWSService<Amazon.S3.IAmazonS3>();

            services.AddScoped<IMapper, Mapper>();
            services.AddScoped<IMembersService, MembersService>();
            services.AddScoped<IMembersRepository, MembersRepository>();
            services.AddHttpClient<IRandomUserApiClient, RandomUserApiClient>();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Title = "Members RandomUserApi (Orange Theory Fitness)",
                    Version = "v1",
                    Description = "CRUD API using AWS Lambda and Dynamodb",
                    Contact = new Contact
                    {
                        Name = "Ronald Johnson",
                        Email = "ronjohn@outlook.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //... and tell Swagger to use those XML comments.
                s.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(s => s.SwaggerEndpoint("Prod/swagger/v1/swagger.json", "Members RandomUserApi"));


            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

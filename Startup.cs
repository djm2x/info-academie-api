using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Services;
using Newtonsoft.Json;
using Services;
using System.IO;
using Models;
using System.Threading.Tasks;
using Hubs;
using Providers;
using Extensions;
using System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using System.Security.Cryptography;
using Microsoft.OpenApi.Models;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Provide a secret key to Encrypt and Decrypt the Token
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();



            services.AddDbContext<MyContext>(options =>
            {
                // options.UseSqlServer(Configuration.GetConnectionString("hicham"));
                options.UseSqlServer(Configuration.GetConnectionString("docker"));
                // options.UseSqlite(Configuration.GetConnectionString("sqlite"));
                // options.EnableDetailedErrors();
            });

            services.AddScoped<TokkenHandler>();
            services.AddScoped<Crypto>();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddScoped<EmailService>();
            services.AddScoped<AccountService>();
            services.AddScoped<HtmlService>();

            services.AddDataProtection()
                .UseCustomCryptographicAlgorithms(new ManagedAuthenticatedEncryptorConfiguration()
                {
                    // A type that subclasses SymmetricAlgorithm
                    EncryptionAlgorithmType = typeof(Aes),

                    // Specified in bits
                    EncryptionAlgorithmKeySize = 256,

                    // A type that subclasses KeyedHashAlgorithm
                    ValidationAlgorithmType = typeof(HMACSHA256)
                });

            services.AddHttpClient("ActivationServer", c =>
            {
                c.BaseAddress = new Uri(appSettings.ActivationServer);
            });

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               //    options.Events = new JwtBearerEvents
               //    {
               //        OnTokenValidated = context =>
               //        {
               //                  var route = context.HttpContext.Request.RouteValues;
               //                 //  var userId = int.Parse(context.Principal.Identity.Name);
               //                 //  var user = userService.GetById(userId);
               //                 //  if (user == null)
               //                 //  {
               //                 //      // return unauthorized if user no longer exists
               //                 //      context.Fail("Unauthorized");
               //                 //  }
               //               return Task.CompletedTask;
               //        }
               //    };

               options.Events = new JwtBearerEvents
               {
                   OnAuthenticationFailed = context =>
                   {
                       if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                       {
                           context.Response.Headers.Add("Token-Expired", "true");
                       }
                       return Task.CompletedTask;
                   }
               };
               //    /**
               //    * this just for the sake of signalR
               //    */
               //    options.Events = new JwtBearerEvents
               //    {
               //        OnMessageReceived = context =>
               //        {
               //            var accessToken = context.Request.Query["access_token"];
               //            if (string.IsNullOrEmpty(accessToken) == false)
               //            {
               //                context.Token = accessToken;
               //            }
               //            return Task.CompletedTask;
               //        }
               //    };

               options.RequireHttpsMetadata = false;
               options.SaveToken = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });
            //auth

            // services.AddCors(options =>
            // {
            //     options.AddPolicy("CorsPolicy", builder =>
            //     {
            //         builder
            //        .WithOrigins(
            //            "http://localhost:4201",
            //            "http://localhost:4200",
            //            "http://192.168.1.25:4202",
            //            "http://192.168.43.238:4202"
            //            )
            //                // .AllowAnyOrigin()
            //                .AllowAnyMethod()
            //                .AllowAnyHeader()
            //                .AllowCredentials();
            //     });
            // });


            // services.AddControllersWithViews()

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Info Academie", Version = "v1", Description = "Info Academie ASP.NET Core Web API" });

                // article thta hepled me in this one
                // https://codeburst.io/api-security-in-swagger-f2afff82fb8e
                // add JWT Authentication
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new string[] { } }
                });
            });

            // services.AddSignalR(o =>
            // {
            //     o.EnableDetailedErrors = true;
            //     o.MaximumReceiveMessageSize = 10240; // bytes
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }
            else
            {
                app.Use(async (context, next) =>
                {
                    await next();

                    var reqLength = context.Request.Path.Value.Length;

                    if (context.Response.StatusCode == 404 && (reqLength > 150 || !Path.HasExtension(context.Request.Path.Value)))
                    {
                        context.Request.Path = "/index.html";
                        await next();
                    }
                });

            }

            // app.UseCors("CorsPolicy");
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Info-academie");
                c.RoutePrefix = String.Empty;
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // else
            // {
            //     app.Use(async (context, next) =>
            //     {
            //         await next();

            //         if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
            //         {
            //             context.Request.Path = "/index.html";
            //             await next();
            //         }
            //     });

            // }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseMiddleware<ErrorHandler>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            // var provider = new FileExtensionContentTypeProvider();
            // provider.Mappings.Add(".exe", "application/octect-stream");
            // app.UseStaticFiles(new StaticFileOptions
            // {
            //     ServeUnknownFileTypes = true, //allow unkown file types also to be served
            //     // DefaultContentType = "Whatver you want eg: plain/text" //content type to returned if fileType is not known.
            //     ContentTypeProvider = provider
            // });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // endpoints.MapControllerRoute( name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                // endpoints.MapHub<ChatHub>("/ChatHub");

                // endpoints.MapFallbackToFile("/index.html");
            });
        }
    }
}


// <modules runAllManagedModulesForAllRequests="false">
//     <remove name="WebDAVModule" />
//   </modules>

// https://stackoverflow.com/questions/48188895/asp-net-core-with-iis-http-verb-not-allowed
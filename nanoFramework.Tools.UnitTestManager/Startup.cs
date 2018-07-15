using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using nanoFramework.Tools.UnitTestManager.Models;

namespace nanoFramework.Tools.UnitTestManager
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
			services.AddSingleton<ITestService>(
				new TestService(Configuration.GetSection("TestEngine").GetSection("ConnectedDevices").Get<List<ConnectedDevice>>(),
				Configuration.GetSection("TestEngine").GetValue<string>("JobDirectory"),
				Configuration.GetSection("TestEngine").GetValue<string>("ResultsDirectory"),
				Configuration.GetSection("TestEngine").GetValue<string>("WakeOnLanMacAddress")));
			services.AddMvc();
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}

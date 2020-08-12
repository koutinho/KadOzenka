using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Platform.LongProcessManagment;
using Serilog;
using Serilog.Context;

namespace KadOzenka.LongProcessService
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Log.Information("Configure called");
            LogContext.PushProperty("ApplicationName", env.ApplicationName);
            app.UseMiddleware<SerilogMiddleware>();
            app.UseSerilogRequestLogging();
            LongProcessManagementServiceWeb.LongProcessServiceInit(app, env);
        }
    }
}

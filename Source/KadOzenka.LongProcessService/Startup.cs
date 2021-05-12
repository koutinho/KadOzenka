using KadOzenka.Dal.CommonFunctions.ExistFolderChecker;
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
            app.UseMiddleware<SerilogMiddleware>();
            app.UseSerilogRequestLogging();
            ExistFolderCheckerService.Run();
            LongProcessManagementServiceWeb.LongProcessServiceInit(app, env);
        }
    }
}

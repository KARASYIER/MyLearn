using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SerliLogLearn
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //只註冊運行 Controller/ Razor Pages 必要的核心服務，確保 Pipeline 程序可動作，其餘如像 Data Annotation Model Validation、身分驗證等服務要自己加掛，除有特殊客製需求，一般不太常用。
            //services.AddMvcCore();


            /*  包含 AddMvcCore() 所做的動作外，再加上：
                身分驗證服務
                Swagger / Open API 等 API 文件動態產生功能
                Data Annotation -支援 Attribute 資料檢核及 IValidateObject
                Formatter Mapping - 依 Request 需求提供不同格式(JSON/ XML)內容
                CORS - 支援跨網域整合
                要用 Controller 但不會用到 View，例如純 WebAPI，可以選這個。
            */
            //services.AddControllers();


            /*包含 AddMvcCore() 所有功能，再加上：

                Razor Pages 功能
                身分驗證服務
                Data Annotation -支援 Attribute 資料檢核及 IValidateObject
                Cache Tag Helper*/
            //services.AddRazorPages()



            /*包含 AddControllers() 的所有項目，再加上：
            cshtml Razor View
            Cache Tag Helper
            要使用標準 Model +View + Controller 架構，通常就選這個。*/
            //services.AddControllersWithViews();



            /*等於 AddControllersWithViews() 加 AddRazorPages()，不想漏掉功能發揮 ASP.NET Core 最大威力，選這個就對了。*/

            services.AddMvc();



            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapDefaultControllerRoute();

            });

        }
    }
}

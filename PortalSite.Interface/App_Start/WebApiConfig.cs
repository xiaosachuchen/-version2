using PortalSite.Interface.Filter;
using PortalSite.Interface.Globle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;

namespace PortalSite.Interface
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{Action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            GlobleSetting.subjects = BLL.BaseBLL.GetDicOptions("subject");
            GlobleSetting.press= BLL.BaseBLL.GetDicOptions("version");
            GlobleSetting.grades= BLL.BaseBLL.GetDicOptions("subject");
            GlobleSetting.subjects = BLL.BaseBLL.GetDicOptions("press");
            GlobleSetting.press = BLL.BaseBLL.GetDicOptions("coursetype");
            config.Filters.Add(new RequestInfoFilter());
        }
    }
}

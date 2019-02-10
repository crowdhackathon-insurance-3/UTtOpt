using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace UtOpt
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {


            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Index",
                url: "Home/Index",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "login",
                url: "Account/Login",
                defaults: new { controller = "Account", action = "Login" });

            routes.MapRoute(
                name: "exceloperators",
                url: "Excel_Operations/sh",
                defaults: new { controller = "Excel_Operations", action = "sh" });
            routes.MapRoute(
               name: "exceloperators2",
               url: "Excel_Operations/xlfill",
               defaults: new { controller = "Excel_Operations", action = "xlfill" });

           
            routes.MapRoute(
                name: "logout",
                url: "Account/Logout",
                defaults: new { controller = "Account", action = "Logout" });

            routes.MapRoute(
                name: "reports",
                url: "Home/Reports",
                defaults: new { controller = "Home", action = "Reports" });

            routes.MapRoute(
                name: "rules",
                url: "Home/Rules",
                defaults: new { controller = "Home", action = "Rules" });
            routes.MapRoute(
                name: "table",
                url: "Home/table",
                 defaults: new { controller = "Home", action = "table" });

            routes.MapRoute(
                name: "validation",
                url: "Home/Validation",
                defaults: new { controller = "Home", action = "Validation" });

            routes.MapRoute(
                name: "reconciliation",
                url: "Home/Reconciliation",
                defaults: new { controller = "Home", action = "Reconciliation" });


            routes.MapRoute(
                name: "adjustments",
                url: "Home/Adjustments",
                defaults: new { controller = "Home", action = "Adjustments" });

            routes.MapRoute(
                name: "admin",
                url: "Home/Admin",
                defaults: new { controller = "Home", action = "Admin" });


            routes.MapRoute(
                name: "data",
                url: "Home/Data",
                defaults: new { controller = "Home", action = "Data" });

            routes.MapHttpRoute(
                name: "GetTablesColumns",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "tablecolumns" }
                );

            routes.MapHttpRoute(
                name: "GetColumnFieldValue",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "field-value" }
                );

            routes.MapHttpRoute(
                name: "GetTablesColumnsList",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "tablecolumnsbyid" }
                );

            routes.MapHttpRoute(
                name: "GetTablesList",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "tables" }
                );

            routes.MapHttpRoute(
                name: "PerformMergeQuery",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "merge-query" }
                );

            routes.MapHttpRoute(
                name: "PerformCopyRules",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "copy-rules" }
                );

            routes.MapHttpRoute(
                name: "PerformGenerateValue",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "generate-value" }
                );

            routes.MapHttpRoute(
                name: "GetReports_Lists",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "reports-list" }
                );

            routes.MapHttpRoute(
                name: "GetDistinctReportNames",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "reports-names" }
                );

            routes.MapHttpRoute(
                name: "JoinData",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "join-data" }
                );

            routes.MapHttpRoute(
                name: "GenericQuery",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "generic-query" }
                );

            routes.MapHttpRoute(
                name: "GenericFilter",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "generic-filter" }
                );
            
                routes.MapHttpRoute(
                name: "Getreports",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "getreportssimplelist" }
                );
            routes.MapHttpRoute(
                name: "apiwithid",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            routes.MapRoute(
                    name: "Default",
                    url: "{*url}",
                    defaults: new { controller = "Account", action = "Login" });

        

            routes.MapHttpRoute("apiwithaction", "api/{controller}/{action}");
        }
    }
}

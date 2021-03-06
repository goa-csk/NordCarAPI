﻿using FluentValidation.WebApi;
using NordCar.WebAPI.Filter;
using NordCar.WebAPI.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace NordCar.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {


            // New code
            //config.EnableSystemDiagnosticsTracing();


            // New code http://www.asp.net/web-api/overview/security/enabling-cross-origin-requests-in-web-api
            //config.EnableCors();
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            config.Filters.Add(new ValidateModelStateFilter());
            config.MessageHandlers.Add(new ResponseWrappingHandler());

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            FluentValidationModelValidatorProvider.Configure(config);
        }
    }
}

﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RM.Architecture.Identity.Application.Interfaces;
using RM.Architecture.Identity.Application.Services;
using RM.Architecture.Identity.Domain.Interfaces.Repository;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Configuration;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Context;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;
using RM.Architecture.Identity.Infra.Data.Repository;
using SimpleInjector;

namespace RM.Architecture.Identity.Infra.CrossCuting.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            // Identity
            container.Register<ApplicationDbContext>(Lifestyle.Scoped);
            container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new ApplicationDbContext()), Lifestyle.Scoped);
            container.Register<IRoleStore<IdentityRole, string>>(() => new RoleStore<IdentityRole>(), Lifestyle.Scoped);
            container.Register<ApplicationRoleManager>(Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);

            // APP
            container.Register<IUsuarioAppService, UsuarioAppService>(Lifestyle.Scoped);
            container.Register<ILoginAppService, LoginAppService>(Lifestyle.Scoped);
            container.Register<IAuthorizationAppService, AuthorizationAppService>(Lifestyle.Scoped);
            
            // Data
            container.Register<IUsuarioRepository, UsuarioRepository>(Lifestyle.Scoped);
            container.Register<IClaimsRepository, ClaimsRepository>(Lifestyle.Scoped);
        }
    }
}
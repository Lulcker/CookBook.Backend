using Autofac;
using CookBook.Backend.App.Contracts;
using CookBook.Backend.Infrastructure.Providers;

namespace CookBook.Backend.Modules;

public class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<UserInfoProvider>()
            .As<IUserInfoProvider>()
            .InstancePerLifetimeScope();
        
        builder
            .RegisterType<AccessRightProvider>()
            .As<IAccessRightProvider>()
            .InstancePerLifetimeScope();
    }
}
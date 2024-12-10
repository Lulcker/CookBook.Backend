using Autofac;
using CookBook.Backend.Persistence;

namespace CookBook.Backend.Modules;

public class PersistenceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterGeneric(typeof(Repository<>))
            .As(typeof(IRepository<>))
            .InstancePerLifetimeScope();
        
        builder
            .RegisterType<ChangesSaver>()
            .As<IChangesSaver>()
            .InstancePerLifetimeScope();
    }
}
using Autofac;
using CookBook.Backend.App.Commands.Categories;
using CookBook.Backend.App.Mappings;

namespace CookBook.Backend.Modules;

public class AppModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterAssemblyTypes(typeof(CreateCategoryCommand).Assembly)
            .Where(x => x.Name.EndsWith("Command") || x.Name.EndsWith("Query") || x.Name.EndsWith("Rule"))
            .AsImplementedInterfaces()
            .AsSelf()
            .InstancePerLifetimeScope();
    }
}
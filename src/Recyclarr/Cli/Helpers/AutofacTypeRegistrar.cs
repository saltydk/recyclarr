using Autofac;
using Spectre.Console.Cli;

namespace Recyclarr.Cli.Helpers;

internal class AutofacTypeRegistrar : ITypeRegistrar
{
    private readonly ContainerBuilder _builder;

    public AutofacTypeRegistrar(ContainerBuilder builder)
    {
        _builder = builder;
    }

    public void Register(Type service, Type implementation)
    {
        _builder.RegisterType(implementation).As(service);
    }

    public void RegisterInstance(Type service, object implementation)
    {
        _builder.RegisterInstance(implementation).As(service);
    }

    public void RegisterLazy(Type service, Func<object> factory)
    {
        _builder.Register(_ => factory()).As(service);
    }

    public ITypeResolver Build()
    {
        return new AutofacTypeResolver(_builder.Build());
    }
}

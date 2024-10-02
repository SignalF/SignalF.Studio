using Autofac;
using Blazor.Diagrams;
using SignalF.Studio.Designer.Model;

namespace SignalF.Studio.Designer.Module;

public class DiagramModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<DocumentManager>()
               .InstancePerLifetimeScope();

        builder.RegisterType<DiagramModel>()
               .InstancePerDependency();

        builder.RegisterType<BlazorDiagram>()
               .InstancePerDependency();
    }
}

using Autofac;
using Scotec.Blazor.Diagrams;
using Scotec.Blazor.Diagrams.Components;
using Scotec.Blazor.Diagrams.Core.Layer;
using Scotec.Blazor.Diagrams.Core.Models;
using SignalF.Studio.Designer.Components;
using SignalF.Studio.Designer.Models;
using SignalF.Studio.Designer.Widgets;

namespace SignalF.Studio.Designer.Module;

public class DiagramModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<DocumentManager>()
               .InstancePerLifetimeScope();

        builder.RegisterType<DesignerDiagramModel>()
               .InstancePerLifetimeScope();

        builder.RegisterType<ConfigurationLayerModel>()
               .As<LayerModel>()
               .InstancePerLifetimeScope();

        builder.RegisterType<ComponentMapping<ConfigurationLayerModel, ConfigurationLayer>>()
               .As<IComponentMapping>()
               .SingleInstance();

        builder.RegisterType<ComponentMapping<SignalProcessorNodeModel, SignalProcessor>>()
               .As<IComponentMapping>()
               .SingleInstance();

    }
}

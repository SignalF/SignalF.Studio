using Autofac;
using Scotec.Blazor.Diagrams;
using Scotec.Blazor.Diagrams.Components;
using Scotec.Blazor.Diagrams.Core.Layer;
using Scotec.Blazor.Diagrams.Core.Models;
using Scotec.Blazor.Diagrams.Widgets;
using SignalF.Studio.Designer.Components;
using SignalF.Studio.Designer.Models;
using SignalF.Studio.Designer.Widgets;

namespace SignalF.Studio.Designer.Module;

public class DiagramModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<DataContext>()
               .InstancePerLifetimeScope();

        builder.RegisterType<DesignerDiagramModel>()
               .InstancePerDependency();

        builder.RegisterType<ConfigurationLayerModel>()
               .As<LayerModel>()
               .InstancePerDependency();

        builder.RegisterType<ComponentMapping<ConfigurationLayerModel, ConfigurationLayer>>()
               .As<IComponentMapping>()
               .SingleInstance();

        builder.RegisterType<ComponentMapping<SignalProcessorNodeModel, SignalProcessorNode>>()
               .As<IComponentMapping>()
               .SingleInstance();

        builder.RegisterType<ComponentMapping<SignalProcessorPortModel, SignalProcessorPort>>()
               .As<IComponentMapping>()
               .SingleInstance();

        builder.RegisterType<ComponentMapping<SignalProcessorLinkModel, SignalProcessorLink>>()
               .As<IComponentMapping>()
               .SingleInstance();

        builder.RegisterType<SignalProcessorNodeModel>()
               .InstancePerDependency();

        builder.RegisterType<SignalProcessorPortModel>()
               .InstancePerDependency();

        builder.RegisterType<SignalProcessorLinkModel>()
               .InstancePerDependency();

    }
}

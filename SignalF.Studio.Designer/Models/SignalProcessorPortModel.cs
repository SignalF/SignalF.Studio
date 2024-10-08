﻿using Scotec.Blazor.Diagrams.Core.Geometry;
using Scotec.Blazor.Diagrams.Core.Models;
using SignalF.Datamodel.Signals;

namespace SignalF.Studio.Designer.Models;

public class SignalProcessorPortModel : PortModel
{
    public ISignalConfiguration SignalConfiguration { get; }

    public SignalProcessorPortModel(ISignalConfiguration signalConfiguration, SignalProcessorNodeModel parent, PortAlignment alignment,
                                    Point position, Size size) 
        : this(signalConfiguration, signalConfiguration.Id.ToString("D"), parent, alignment, position, size)
    {
    }

    public SignalProcessorPortModel(ISignalConfiguration signalConfiguration, string id, SignalProcessorNodeModel parent, PortAlignment alignment, Point position, Size size) 
        : base(id, parent, alignment, position, size)
    {
        SignalConfiguration = signalConfiguration;
    }

    public string DefinitionName => SignalConfiguration.Definition.Name;

    public string Name
    {
        get => SignalConfiguration.Name;
        set => SignalConfiguration.Name = value;
    }

    public PortType Type => SignalConfiguration is ISignalSourceConfiguration ? PortType.SignalSource : PortType.SignalSink;


    //public override bool CanAttachTo(ILinkable other)
    //{
    //    if (other is not SignalProcessorPortModel port || port.Id == Id)
    //    {
    //        return false;
    //    }
        
    //    return Type != port.Type;
    //}
}

public enum PortType
{
    SignalSink,

    SignalSource
}

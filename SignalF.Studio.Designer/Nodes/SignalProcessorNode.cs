using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using SignalF.Datamodel.Signals;

namespace SignalF.Studio.Designer.Nodes;

public class SignalProcessorNode : NodeModel
{
    private readonly ISignalProcessorConfiguration _configuration;

    public SignalProcessorNode(ISignalProcessorConfiguration configuration, Point position, Size size)
        : base(configuration.Id.ToString("D"), position)
    {
        _configuration = configuration;
        //var x = _configuration.SignalSinks.First();
        Size = size;
    }

    public string? Name
    {
        get => _configuration.Name;
        set => _configuration.Name = value;
    }

    public string? DefinitionName => _configuration.Definition.Name ?? _configuration.Definition.Template.Name;
}

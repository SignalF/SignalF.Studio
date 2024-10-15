using Scotec.Blazor.Diagrams.Core.Behaviours;
using Scotec.Blazor.Diagrams.Core.Layer;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams;

public class BlazorDiagramModel : DiagramModel
{
    public BlazorDiagramModel(Func<IEnumerable<LayerModel>> layerFactory, Func<DiagramModel, IEnumerable<IDiagramBehaviour>> behavioursFactory) 
        : base(layerFactory, behavioursFactory)
    {
    }
}

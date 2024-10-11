using Scotec.Blazor.Diagrams.Core.Layer;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams;

public class BlazorDiagramModel : DiagramModel
{
    public BlazorDiagramModel(Func<IEnumerable<LayerModel>> layerFactory) 
        : base(layerFactory)
    {
    }
}

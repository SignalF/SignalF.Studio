using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Core.Behaviours;

public abstract class DiagramBehaviour : IDiagramBehaviour
{
    protected DiagramBehaviour(DiagramModel diagramModel)
    {
        DiagramModel = diagramModel;
    }

    protected DiagramModel DiagramModel { get; }
}

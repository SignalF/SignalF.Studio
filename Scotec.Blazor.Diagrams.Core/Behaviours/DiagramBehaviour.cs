using Scotec.Blazor.Diagrams.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scotec.Blazor.Diagrams.Core.Behaviours
{
    public abstract class DiagramBehaviour : IDiagramBehaviour
    {
        protected  DiagramModel DiagramModel { get; }

        protected DiagramBehaviour(DiagramModel diagramModel)
        {
            DiagramModel = diagramModel;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scotec.Blazor.Diagrams.Core.Models;
using SignalF.Datamodel.Designer;

namespace SignalF.Studio.Designer.Models
{
    public class SignalProcessorLinkModel : LinkModel
    {
        public SignalProcessorLinkModel(ILinkElement linkElement)
        : base(linkElement.Id.ToString("D"), linkElement.Vertices.Select(vertex => vertex.ToPoint()).ToArray())
        {
            
        }
    }
}

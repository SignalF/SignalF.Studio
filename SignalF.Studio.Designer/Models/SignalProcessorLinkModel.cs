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
        public delegate SignalProcessorLinkModel Factory(ILinkElement linkElement, AnchorModel source, AnchorModel target);

        public SignalProcessorLinkModel(ILinkElement linkElement, AnchorModel source, AnchorModel target)
        : base(linkElement.Id.ToString("D"), source, target)
        {
            
        }
        internal SignalProcessorLinkModel(AnchorModel source, AnchorModel target)
        : base(Guid.NewGuid().ToString("D"), source, target)
        {
            
        }
    }
}

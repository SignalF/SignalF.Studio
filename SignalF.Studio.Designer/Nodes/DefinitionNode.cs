using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using SignalF.Datamodel.Signals;

namespace SignalF.Studio.Designer.Nodes
{
    internal abstract class DefinitionNode
    {
        protected DefinitionNode(Guid id, string typeName)
        {
            Id = id;
            TypeName = typeName;
        }

        public Guid Id { get; }
        public string TypeName { get; }

        public ISignalProcessorConfiguration CreateItem(Point position)
        {
            return OnCreateItem(position);
        }

        protected abstract ISignalProcessorConfiguration OnCreateItem(Point position);
    }
}

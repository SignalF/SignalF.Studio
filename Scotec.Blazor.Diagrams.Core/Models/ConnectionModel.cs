using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scotec.Blazor.Diagrams.Core.Geometry;

namespace Scotec.Blazor.Diagrams.Core.Models
{
    public class ConnectionModel : Model
    {
        protected ConnectionModel(Point[] points = default) : base()
        {
        }


        protected ConnectionModel(string id, Point[] points = default) : base(id)
        {
        }

    }
}

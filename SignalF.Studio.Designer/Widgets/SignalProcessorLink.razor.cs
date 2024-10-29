using Microsoft.AspNetCore.Components;
using SignalF.Studio.Designer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scotec.Blazor.Diagrams;
using Scotec.Blazor.Diagrams.Core.Geometry;

namespace SignalF.Studio.Designer.Widgets
{
    public partial class SignalProcessorLink
    {
        private SignalProcessorLinkModel _link;

        [Parameter]
        public SignalProcessorLinkModel Link
        {
            get => _link;
            set
            {
                _link = value;
                BuildSvgPath();
            }
        }

        private void BuildSvgPath()
        {
            if (Link is null || Link.Vertices.Length <= 1)
            {
                return;
            }


            var builder = new StringBuilder();

            var first = Link.Vertices.First();
            var last = Link.Vertices.Last();

            builder.Append($"M{first.X.ToInvariantString()} {first.Y.ToInvariantString()} ");

            foreach (var vertex in Link.Vertices.Skip(1).SkipLast(1))
            {
                //builder.Append($"L{(vertex.X - before.X).ToInvariantString()} {(vertex.Y - before.Y).ToInvariantString()} ");
                builder.Append($"L{(vertex.X).ToInvariantString()} {(vertex.Y).ToInvariantString()} ");
                builder.Append($"M{vertex.X.ToInvariantString()} {vertex.Y.ToInvariantString()} ");
            }

            builder.Append($"L{(last.X).ToInvariantString()} {(last.Y).ToInvariantString()} ");
            builder.Append("Z");

            SvgPath = builder.ToString();
            //SvgPath = GetSvgPath();
        }

        private string GetSvgPath()
        {

            return "M150 5 L75 200 L225 200 Z";
        }

        [Parameter]
        public string SvgPath { get; set; }


    }
}

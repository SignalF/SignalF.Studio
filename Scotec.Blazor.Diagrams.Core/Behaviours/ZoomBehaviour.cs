using Scotec.Blazor.Diagrams.Core.EventArgs;
using Scotec.Blazor.Diagrams.Core.Geometry;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Core.Behaviours;

public class ZoomBehaviour : DiagramBehaviour
{
    private const double ZoomMin = 0.1;
    private const double ZoomMax = 2;

    public ZoomBehaviour(DiagramModel diagramModel) : base(diagramModel)
    {
        DiagramModel.Wheel += OnWheel;
    }

    private void OnWheel(WheelEventArgs args)
    {
        if (args.DeltaY == 0)
        {
            return;
        }

        var zoomables = DiagramModel.Layers.OfType<IZoomable>().ToList();
        if (!zoomables.Any())
        {
            return;
        }

        var oldZoom = DiagramModel.Zoom;
        var factor = args.DeltaY >= 0 ? 1.0 / 1.05 : 1.05;
        var newZoom = oldZoom * factor;
        newZoom = Math.Clamp(newZoom, ZoomMin, ZoomMax);

        var clientWidth = DiagramModel.Bounds.Width;
        var clientHeight = DiagramModel.Bounds.Height;
        var widthDiff = clientWidth * newZoom - clientWidth * oldZoom;
        var heightDiff = clientHeight * newZoom - clientHeight * oldZoom;
        var clientX = args.ClientX - DiagramModel.Bounds.Left;
        var clientY = args.ClientY - DiagramModel.Bounds.Top;
        var xFactor = (clientX - DiagramModel.Pan.X) / oldZoom / clientWidth;
        var yFactor = (clientY - DiagramModel.Pan.Y) / oldZoom / clientHeight;
        var newPanX = DiagramModel.Pan.X - widthDiff * xFactor;
        var newPanY = DiagramModel.Pan.Y - heightDiff * yFactor;

        DiagramModel.Pan = new Point(newPanX, newPanY);
        DiagramModel.Zoom = newZoom;
        foreach (var zoomable in zoomables)
        {
            zoomable.Zoom = newZoom;
            if (zoomable is IMovable movable)
            {
                movable.SetPosition(newPanX, newPanY);
            }
        }
    }
}

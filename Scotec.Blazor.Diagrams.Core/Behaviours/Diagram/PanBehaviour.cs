using Scotec.Blazor.Diagrams.Core.EventArgs;
using Scotec.Blazor.Diagrams.Core.Geometry;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Core.Behaviours.Diagram;

public class PanBehaviour : DiagramBehaviour
{
    private bool _buttonDown;
    private double _lastClientX;
    private double _lastClientY;
    private List<IMovable> _movables = [];

    public PanBehaviour(DiagramModel diagramModel) : base(diagramModel)
    {
        DiagramModel.PointerDown += OnPointerDown;
        DiagramModel.PointerUp += OnPointerUp;
        DiagramModel.PointerMove += OnPointerMove;
    }

    private void OnPointerMove(Model? model, PointerEventArgs args)
    {
        if (!_buttonDown)
        {
            return;
        }

        var differenceX = args.ClientX - _lastClientX;
        var differenceY = args.ClientY - _lastClientY;

        var x = DiagramModel.Pan.X + differenceX;
        var y = DiagramModel.Pan.Y + differenceY;

        foreach (var movable in _movables)
        {
            movable.SetPosition(x, y);
        }

        DiagramModel.Pan = new Point(x, y);

        _lastClientX = args.ClientX;
        _lastClientY = args.ClientY;
    }

    private void OnPointerUp(Model? arg1, PointerEventArgs args)
    {
        _buttonDown = false;
        _movables.Clear();
    }

    private void OnPointerDown(Model? model, PointerEventArgs args)
    {
        if (model is not null)
        {
            return;
        }

        _buttonDown = true;
        _lastClientX = args.ClientX;
        _lastClientY = args.ClientY;

        // ReSharper disable once SuspiciousTypeConversion.Global
        _movables = DiagramModel.Layers.OfType<IMovable>().ToList();
    }
}

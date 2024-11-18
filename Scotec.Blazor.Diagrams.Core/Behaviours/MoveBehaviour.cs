using System.Collections.Immutable;
using Scotec.Blazor.Diagrams.Core.EventArgs;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Core.Behaviours;

public class MoveBehaviour : DiagramBehaviour
{
    private bool _buttonDown;
    private bool _firstMove;
    private double _lastClientX;
    private double _lastClientY;
    private List<IMovable> _movables = [];
    private IReadOnlyList<AnchorModel>? _anchors;

    public MoveBehaviour(DiagramModel diagramModel) : base(diagramModel)
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

        if (_firstMove)
        {
            _firstMove = false;
            _movables.ForEach(movable => movable.IsMoving = true);
        }

        SetPosition(args);

        _lastClientX = args.ClientX;
        _lastClientY = args.ClientY;
    }

    private void SetPosition(PointerEventArgs args)
    {
        var differenceX = (args.ClientX - _lastClientX) / DiagramModel.Zoom;
        var differenceY = (args.ClientY - _lastClientY) / DiagramModel.Zoom;

        foreach (var movable in _movables)
        {
            var x = movable.Position.X + differenceX;
            var y = movable.Position.Y + differenceY;

            movable.SetPosition(x, y);
        }
    }

    private void OnPointerUp(Model? arg1, PointerEventArgs args)
    {
        if (_buttonDown)
        {
            _buttonDown = false;
            _firstMove = false;
            _movables.ForEach(movable => movable.IsMoving = false);
            SetPosition(args);
            _movables.Clear();
        }
    }

    private void OnPointerDown(Model? model, PointerEventArgs args)
    {
        if (model is not IMovable movable)
        {
            return;
        }

        _buttonDown = true;
        _firstMove = true;
        _lastClientX = args.ClientX;
        _lastClientY = args.ClientY;

        _movables = DiagramModel.Layers
                                .SelectMany(layer => layer.GetModels<Model>()
                                                          .OfType<ISelectable>()
                                                          .Where(m => m.IsSelected)
                                                          .OfType<IMovable>())
                                .ToList();

        if (!_movables.Contains(movable))
        {
            _movables.Clear();
            _movables.Add(movable);
        }

        _anchors = _movables.OfType<NodeModel>().SelectMany(node => node.GetPorts<PortModel>())
                            .Select(port => port.Anchor)
                            .ToImmutableList();
    }
}

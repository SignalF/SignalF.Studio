using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scotec.Blazor.Diagrams.Core.EventArgs;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Core.Behaviours
{
    public class MoveBehaviour : DiagramBehaviour
    {
        private List<IMovable> _movables = [];
        private double _lastClientX = 0.0;
        private double _lastClientY = 0.0;
        private bool _buttonDown;

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

            var differenceX = args.ClientX - _lastClientX;
            var differenceY = args.ClientY - _lastClientY;

            foreach (var movable in _movables)
            {
                var x = movable.Position.X + differenceX;
                var y = movable.Position.Y + differenceY;

                movable.SetPosition(x, y);
            }
            _lastClientX = args.ClientX;
            _lastClientY = args.ClientY;

            //DiagramModel.Refresh();
        }

        private void OnPointerUp(Model? arg1, PointerEventArgs args)
        {
            _buttonDown = false;
        }

        private void OnPointerDown(Model? model, PointerEventArgs args)
        {
            if (model is not IMovable movable)
            {
                return;
            }

            _buttonDown = true;
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
        }
    }
}

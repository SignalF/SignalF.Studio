using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scotec.Blazor.Diagrams.Core.EventArgs;
using Scotec.Blazor.Diagrams.Core.Geometry;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Core.Behaviours
{
    public class ConnectBehaviour : DiagramBehaviour
    {
        private List<IMovable> _movables = [];
        private double _lastClientX = 0.0;
        private double _lastClientY = 0.0;
        private bool _firstMove;
        private IConnectable? _firstConnectable;

        public ConnectBehaviour(DiagramModel diagramModel) : base(diagramModel)
        {
            DiagramModel.PointerDown += OnPointerDown;
            DiagramModel.PointerUp += OnPointerUp;
            DiagramModel.PointerMove += OnPointerMove;
        }

        private void OnPointerMove(Model? model, PointerEventArgs args)
        {
            if (_firstConnectable is null)
            {
                return;
            }

            if (_firstMove)
            {
                _firstMove = false;
            }

            SetPosition(args);

            _lastClientX = args.ClientX;
            _lastClientY = args.ClientY;
        }

        private void SetPosition(PointerEventArgs args)
        {
            var differenceX = (args.ClientX - _lastClientX) / DiagramModel.Zoom ;
            var differenceY = (args.ClientY - _lastClientY) / DiagramModel.Zoom;

            //foreach (var movable in _movables)
            //{
            //    var x = movable.Position.X + differenceX;
            //    var y = movable.Position.Y + differenceY;

            //    movable.SetPosition(x, y);
            //}
        }

        private void OnPointerUp(Model? model, PointerEventArgs args)
        {
            if (_firstConnectable is not null)
            {
                _firstConnectable = null;
                _firstMove = false;
            }
        }

        private void OnPointerDown(Model? model, PointerEventArgs args)
        {
            if (model is not IConnectable connectable)
            {
                return;
            }

            _firstMove = true;
            _firstConnectable = connectable;
            _lastClientX = args.ClientX;
            _lastClientY = args.ClientY;

        }
    }
}

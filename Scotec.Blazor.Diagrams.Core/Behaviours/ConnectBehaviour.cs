using Scotec.Blazor.Diagrams.Core.EventArgs;
using Scotec.Blazor.Diagrams.Core.Geometry;
using Scotec.Blazor.Diagrams.Core.Layer;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Core.Behaviours
{
    public class ConnectBehaviour : LayerBehaviour<NodeLayerModel>, INodeLayerBehaviour
    {
        private List<IMovable> _movables = [];
        private double _lastClientX = 0.0;
        private double _lastClientY = 0.0;
        private bool _firstMove;
        private IConnectable? _source;
        private AnchorModel? _sourceAnchor;
        private AnchorModel? _targetAnchor;
        private LinkModel? _link;

        public ConnectBehaviour(NodeLayerModel layerModel) : base(layerModel)
        {
            LayerModel.PointerDown += OnPointerDown;
            LayerModel.PointerUp += OnPointerUp;
            LayerModel.PointerMove += OnPointerMove;
        }

        private void OnPointerMove(Model? model, PointerEventArgs args)
        {
            if (_source is null)
            {
                return;
            }

            if (model is IConnectable target && _source != target)
            {

            }
            if (_firstMove)
            {
                _firstMove = false;
                return;
            }

            var diagramPoint = LayerModel.Diagram.GetDiagramCanvasMousePoint(args.ClientX, args.ClientY);

            SetPosition(diagramPoint);

            _lastClientX = diagramPoint.X;
            _lastClientY = diagramPoint.Y;
        }

        private void SetPosition(Point point)
        {
            var differenceX = (point.X - _lastClientX);// / DiagramModel.Zoom;
            var differenceY = (point.Y - _lastClientY);// / DiagramModel.Zoom;

            _link.Target = new AnchorModel(_link.Target.AnchorPoint + new Point( differenceX, differenceY));
            _link.Refresh();

        }

        private void OnPointerUp(Model? model, PointerEventArgs args)
        {
            if (_source is null)
            {
                return;
            }


            LayerModel.RemoveLink(_link);
            _source = null;
            _firstMove = false;
            _sourceAnchor = null;
            _targetAnchor = null;
            _link = null;
        }

        private void OnPointerDown(Model? model, PointerEventArgs args)
        {
            if (model is not IConnectable connectable)
            {
                return;
            }

            _firstMove = true;
            _source = connectable;

            var diagramPoint = LayerModel.Diagram.GetDiagramCanvasMousePoint(args.ClientX, args.ClientY);


            _sourceAnchor = connectable.Anchor;
            _targetAnchor = new AnchorModel(diagramPoint);
            _link = LayerModel.CreateLink(_sourceAnchor, _targetAnchor);

            LayerModel.AddLink(_link);

            _lastClientX = diagramPoint.X;
            _lastClientY = diagramPoint.Y;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}

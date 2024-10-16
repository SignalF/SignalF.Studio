using System.Data.SqlTypes;
using Scotec.Blazor.Diagrams.Core.EventArgs;
using Scotec.Blazor.Diagrams.Core.Geometry;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Core.Behaviours;

public class SelectionBehaviour : DiagramBehaviour
{
    private readonly IList<Model> _selected = new List<Model>();

    public SelectionBehaviour(DiagramModel diagramModel) : base(diagramModel)
    {
        DiagramModel.PointerDown += OnPointerDown;
        DiagramModel.PointerUp += OnPointerUp;
    }

    private Point _clickPosition;

    private void OnPointerDown(Model? model, PointerEventArgs args)
    {
        _clickPosition = args.GetClientPoint();
    }

    private void OnPointerUp(Model? model, PointerEventArgs args)
    {
        if (_clickPosition != args.GetClientPoint())
        {
            // Selected objects or drawing area have been moved. In that case we want to keep the current selection.
            return;
        }

        if (model is null)
        {
            DeselectAll();
            //DiagramModel.Refresh();
            return;
        }

        var isCtrlKeyPressed = args.CtrlKey;
        var isModelSelected = model.IsSelected;

        if (!isCtrlKeyPressed)
        {
            DeselectAll();
            if (!isModelSelected)
            {
                model.IsSelected = true;
                _selected.Add(model);
            }
        }
        else
        {
            if (isModelSelected)
            {
                model.IsSelected = false;
                _selected.Remove(model);
            }
            else
            {
                model.IsSelected = true;
                _selected.Add(model);
            }
        }

        DiagramModel.Refresh();
    }

    private void DeselectAll()
    {
        foreach (var model in _selected)
        {
            model.IsSelected = false;
        }

        _selected.Clear();
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}

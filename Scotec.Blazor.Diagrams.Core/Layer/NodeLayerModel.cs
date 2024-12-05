using Scotec.Blazor.Diagrams.Core.Behaviours;
using Scotec.Blazor.Diagrams.Core.Behaviours.Layer;
using Scotec.Blazor.Diagrams.Core.Geometry;
using Scotec.Blazor.Diagrams.Core.Models;

namespace Scotec.Blazor.Diagrams.Core.Layer;

public abstract class NodeLayerModel : LayerModel, IMovable, IZoomable
{
    private List<INodeLayerBehaviour>? _behaviours;
    private readonly Func<NodeLayerModel, IEnumerable<INodeLayerBehaviour>> _behavioursFactory;
    private readonly INodeLayerBehaviour.Factory _testFactory;

    protected NodeLayerModel(DiagramModel diagramModel, Func<NodeLayerModel, IEnumerable<INodeLayerBehaviour>> behavioursFactory, INodeLayerBehaviour.Factory testFactory) 
        : base(diagramModel)
    {
        _behavioursFactory = behavioursFactory;
        _testFactory = testFactory;
    }

    public override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _behaviours = _behavioursFactory(this).ToList();
       
        //var x = _testFactory(this);
    }

    public Point Position { get; private set; }

    public virtual void SetPosition(double x, double y)
    {
        Position = new Point(x, y);
    }

    public bool IsMoving { get; set; }

    public double Zoom { get; set; } = 1.5;

    public void AddNode(NodeModel node)
    {
        AddModel(node);
    }

    public virtual void AddNodes(NodeModel node)
    {
        AddModel(node);
    }

    public virtual void AddNodes(IEnumerable<NodeModel> nodes)
    {
        AddModels(nodes);
    }

    public virtual void RemoveNode(NodeModel node)
    {
        RemoveModel(node);
    }

    public virtual void RemoveNodes(IEnumerable<NodeModel> nodes)
    {
        RemoveModels(nodes);
    }

    public virtual void AddLink(LinkModel link)
    {
        AddModel(link);
    }

    public virtual void AddLinks(IEnumerable<LinkModel> links)
    {
        AddModels(links);
    }

    public virtual void RemoveLink(LinkModel link)
    {
        RemoveModel(link);
    }

    public virtual void RemoveLinks(IEnumerable<LinkModel> links)
    {
        RemoveModels(links);
    }

    public IReadOnlyList<NodeModel> GetNodes()
    {
        return GetModels<NodeModel>().ToList();
    }

    public IReadOnlyList<LinkModel> GetLinks()
    {
        return GetModels<LinkModel>().ToList();
    }

    public abstract LinkModel CreateDraftLink(AnchorModel source, AnchorModel target);

    public abstract void CreateLink(PortModel source, PortModel target);

}

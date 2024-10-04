namespace Scotec.Blazor.Diagrams.Core.Geometry;

public record struct Size
{
    public Size()
    {
    }

    public Size(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public double Width { get; init; }
    public double Height { get; init; }
}

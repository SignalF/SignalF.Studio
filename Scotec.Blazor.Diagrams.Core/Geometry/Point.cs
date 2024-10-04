namespace Scotec.Blazor.Diagrams.Core.Geometry;

public record struct Point
{
    public Point()
    {
    }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; init; }
    public double Y { get; init; }
}


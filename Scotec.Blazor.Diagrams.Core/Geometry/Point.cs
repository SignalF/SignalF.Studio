using Microsoft.VisualBasic.CompilerServices;

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

    public static Point operator +(Point p1, Point p2)
    {
        return new Point(p1.X + p2.X, p1.Y + p2.Y);
    }
}


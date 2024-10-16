using System.Text.Json.Serialization;

namespace Scotec.Blazor.Diagrams.Core.Geometry;

public class Rectangle
{
    public Rectangle()
    {
        Position = new Point(0, 0);
        Size = new Size(0, 0);
    }

    [JsonConstructor]
    public Rectangle(double x, double y, double width, double height/*, double top, double right, double bottom, double left*/)
    {
        Position = new Point(x, y);
        Size = new Size(width, height);
    }

    public double X => Position.X;
    public double Y => Position.Y;
    public double Width => Size.Width;
    public double Height => Size.Height;
    public double Top => Position.Y;
    public double Right => Left + Size.Width;
    public double Bottom => Top + Size.Height;
    public double Left => Position.X;
    

    [JsonIgnore]
    public Size Size { get; private set; }

    [JsonIgnore]
    public Point Position { get; private set; }
}

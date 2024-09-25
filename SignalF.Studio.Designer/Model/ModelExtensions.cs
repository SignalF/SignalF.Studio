using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazor.Diagrams.Core.Geometry;

namespace SignalF.Studio.Designer.Model
{
    internal static class ModelExtensions
    {
        public static string GetX(this Point point)
        {
            return point.X.ToString(CultureInfo.InvariantCulture);
        }

        public static string GetY(this Point point)
        {
            return point.Y.ToString(CultureInfo.InvariantCulture);
        }

        public static string GetWidth(this Size size)
        {
            return size.Width.ToString(CultureInfo.InvariantCulture);
        }

        public static string GetHeight(this Size size)
        {
            return size.Height.ToString(CultureInfo.InvariantCulture);
        }
    }
}

using System.Collections.ObjectModel;

namespace Scotec.Blazor.Diagrams.Core.Models;

public class ModelCollection : KeyedCollection<string, Model>
{
    protected override string GetKeyForItem(Model item)
    {
        return item.Id;
    }
}

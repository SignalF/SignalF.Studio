using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scotec.Blazor.Diagrams.Core.Models
{
    public abstract class Model
    {
        protected Model() : this(Guid.NewGuid().ToString("D"))
        {
        }

        protected Model(string id)
        {
            Id = id;
        }

        public string Id { get; }

        public string Title { get; set; } = string.Empty;

        public bool IsVisible { get; set; } = true;

        public bool IsLocked { get; set; } = false;

        public bool IsSelected { get; set; } = false;

        public virtual Task OnInitializedAsync()
        {
            return Task.CompletedTask;
        }

    }
}

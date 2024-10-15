using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Scotec.Blazor.Diagrams.Core.Behaviours;

namespace Scotec.Blazor.Diagrams.Core.Models
{
    public abstract class Model : ISelectable
    {
        private bool _isSelected = false;

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

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if(_isSelected != value) 
                {
                    _isSelected = value;
                    Refresh();
                }
            }
        }

        public virtual Task OnInitializedAsync()
        {
            return Task.CompletedTask;
        }

        public event Action<Model>? Changed;

        public virtual void Refresh() => Changed?.Invoke(this);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalF.Studio.Designer.Models
{
    public class DesignerModel
    {
        private readonly DocumentManager _documentManager;

        public DesignerModel(DocumentManager documentManager)
        {
            _documentManager = documentManager;
        }
    }
}

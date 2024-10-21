using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalF.Studio.Designer.Models
{
    public class DesignerModel
    {
        private readonly DataContext _dataContext;

        public DesignerModel(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
    }
}

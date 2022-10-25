using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holding.Base.Infrastructure.Domain
{
    public interface IMigrationExecuter
    {
        void Start();
    }
}

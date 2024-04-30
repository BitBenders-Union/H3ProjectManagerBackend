using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Interfaces
{
    public interface IValidationService
    {
        public bool WhiteSpaceValidation<TValidation>(TValidation model);
    }
}

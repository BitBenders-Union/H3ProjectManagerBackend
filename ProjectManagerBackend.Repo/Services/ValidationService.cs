using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Services
{
    public class ValidationService
    {

        public bool WhiteSpaceValidation<TValidation>(TValidation model)
        {
            if(model == null)
                throw new ArgumentNullException("model");

            // get the properties and see if they are whitespace

            var properties = typeof(TValidation).GetProperties();

            foreach(var prop in properties)
            {
                if(prop.PropertyType == typeof(string))
                {
                    var value = (string)prop.GetValue(model); // should always be string but cast anyway
                    
                    if(value.Any(char.IsWhiteSpace))
                    {
                        return false;
                    }
                }
                
            }
            return true;
        }
    }
}

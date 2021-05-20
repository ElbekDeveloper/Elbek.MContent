using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elbek.MContent.Services.ValidationServices.ContentValidator
{
    public interface IContentValidationRules: IGenericValidationRules
    {
    }
    public class ContentValidationRules : GenericValidationRules, IContentValidationRules
    {
    }
}

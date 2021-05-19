using System.Collections.Generic;
using System.Linq;

namespace Elbek.MContent.Services.Models
{
public class MContentValidationResult
{
    public bool IsValid
    {
        get
        {
            return !Errors.Any();
        }
        set {  }
    }
    public int StatusCode {
        get;
        set;
    }
    public List<string> Errors {
        get;
        set;
    } = new List<string>();

}
}

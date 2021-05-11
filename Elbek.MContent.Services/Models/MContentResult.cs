using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elbek.MContent.Services.Models
{
    public class MContentResult<T>
    {
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}

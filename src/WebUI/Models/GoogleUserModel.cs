using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Models
{
    public class GoogleUserModel
    {
        public string ResourceName { get; set; }
        public IList<Photo> Photos { get; set; }
    }

    public class Photo
    {
        public string Url { get; set; }
    }


}

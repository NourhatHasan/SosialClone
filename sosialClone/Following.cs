using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sosialClone
{
    public class Following
    {
        public string observerId {  get; set; }
        public AppUser observer { get; set; }
        public string targetId { get; set; }
        public AppUser target { get; set; }
    }
}

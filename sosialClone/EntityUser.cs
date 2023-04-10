using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sosialClone
{
    public class EntityUser
    {
        public string userId { get; set; }
        public user user { get; set; }
        public Guid EntityId { get; set; }
        public Entities entities { get; set; }
        public bool isHost { get; set; }
    }
}

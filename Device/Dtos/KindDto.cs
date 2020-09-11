using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Device.Dtos
{
    public class KindDto
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public DateTime Created { get; internal set; }
    }
}

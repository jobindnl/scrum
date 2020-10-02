using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angular.Web.Validations
{
    public class QueryOptions
    {
        public int Top { get; set; }
        public int Skip { get; set; }
        public bool Count { get; set; }
        public string Select { get; set; }
        public string OrderBy { get; set; }
        public int SortOrder { get; set; }

    }
}

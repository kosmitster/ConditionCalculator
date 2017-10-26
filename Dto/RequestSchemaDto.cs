using System;
using System.Collections.Generic;

namespace Dto
{
    public class RequestSchemaDto
    {
        public Guid Uid { get; set; }
        public List<KeyValuePair<string, string>> Conditions { get; set; }
        public List<KeyValuePair<string, decimal>> Costs { get; set; }
    }
}

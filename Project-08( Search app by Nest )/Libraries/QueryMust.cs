using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries {
    internal class QueryMust : ElasticSearchQuery {
        public override Func<QueryContainerDescriptor<Dictionary<string, object>>, QueryContainer>[] Query => throw new NotImplementedException();
    }
}

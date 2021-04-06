using Nest;

namespace Libraries {
    public class ElasticResponseValidator {
        private readonly ResponseBase response;
        public bool IsValid { get { return response.IsValid; } }
        public string DebugInformation { get { return response.DebugInformation; } }
        public ElasticResponseValidator(ResponseBase response) {
            this.response = response;
        }


    }
}

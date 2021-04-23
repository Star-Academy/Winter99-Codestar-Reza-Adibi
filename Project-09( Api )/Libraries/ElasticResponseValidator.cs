using Nest;
using System;
using System.Linq;

namespace Libraries {
    public static class ElasticResponseValidator {
        public static T Validate<T>(this T response) where T : IResponse {
            if (response == null)
                throw new Exception("Unknown Error!");
            if (!response.IsValid) {
                if (response.OriginalException != null)
                    throw response.OriginalException;
                if (response.ServerError != null)
                    throw new Exception(
                        "Status: " + response.ServerError.Status +
                        "\n Message: " + response.ServerError?.Error?.RootCause?.FirstOrDefault()?.Reason
                    );
                throw new Exception("Unknown Error!");
            }
            return response;
        }
    }
}
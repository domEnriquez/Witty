using System.Collections.Generic;
using Witty.Models;

namespace Witty.Tests.Builders
{
    public class ResponseListBuilder
    {
        private readonly List<Response> response = new List<Response>();
        private string[] responseCategories;
        private string[] responseStrings;

        public static ResponseListBuilder Default()
        {
            return new ResponseListBuilder();
        }

        public ResponseListBuilder WithResponseCategories(params string[] responseCategories)
        {
            this.responseCategories = responseCategories;

            return this;
        }

        public ResponseListBuilder WithResponseStrings(params string[] responseStrings)
        {
            this.responseStrings = responseStrings;

            return this;
        }

        public List<Response> Build()
        {
            for(int i = 0; i < responseCategories.Length; i++)
            {
                response.Add(new Response
                {
                    ResponseCategory = responseCategories[i],
                    ResponseString = responseStrings[i]
                });
            }

            return response;
        }
    }
}

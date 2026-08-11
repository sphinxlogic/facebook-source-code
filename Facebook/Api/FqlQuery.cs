using System;

namespace Facebook.Api
{
    /// <summary>Encapsulates all the objects required to execute an API request within a <see cref="Batch" />.</summary>
    /// <typeparam name="TReturnType">The return type of the API method.</typeparam>
    [Serializable]
    public struct FqlQuery<TReturnType> : IFqlQuery
    {
        /// <summary>Intializes an instance of <see cref="BatchItem{TReturnType}" /> with the specified objects.</summary>
        /// <param name="request">A <see cref="FacebookRequest" /> object containing information about the request, such as the method and any parameters to be passed in.</param>
        /// <param name="response">An <see cref="IFacebookResponse" /> object representing the unpopulated response of the request.</param>
        public FqlQuery(FqlQueryReturnType returnType, String name, String query)
            : this()
        {
            this.ReturnType = returnType;
            this.Name = name;
            this.Query = query;
        }

        public String Name { get; set; }

        public String Query { get; set; }

        public FqlQueryReturnType ReturnType { get; set; }
    }
}

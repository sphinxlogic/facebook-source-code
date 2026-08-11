using System;
using System.Collections.Generic;
using Facebook.Api;
using System.Collections.Specialized;
using System.Xml.Linq;

namespace Facebook
{
    /// <summary>Defines a contract that the API client implements to provide programmatic access to the Facebook API.</summary>
    public interface IFacebookContext
    {
        /// <summary>Gets the API key that identifies the Facebook application.</summary>
        String ApiKey { get; }

        /// <summary>Gets the application secret that corresponds to the <see cref="ApiKey" /> used to authenticate the application.</summary>
        String Secret { get; }

        /// <summary>Gets the version of the API targeted by the context instance.</summary>
        String Version { get; }

        /// <summary>Gets a value representing whether an authentication session has been established between the user and the appliation.</summary>
        Boolean HasSession { get; }

        /// <summary>Gets a reference to a <see cref="ISessionInfo" /> object containing data about the current session.</summary>
        ISessionInfo Session { get; }

        FacebookCache Cache { get; }

        ObjectCache ObjectCache { get; }

        /// <summary>Initializes the <see cref="IFacebookContext" /> implementation's <see cref="Session" /> property with values
        /// from the specified <see cref="ISessionInfo" /> object.</summary>
        /// <param name="sessionInfo"></param>
        void InitSession(ISessionInfo sessionInfo);

        /// <summary>Gets the <see cref="ApplicationType" /> of the context instance.</summary>
        ApplicationType ApplicationType { get; }

        /// <summary>Executes a <see cref="FacebookRequest" /> for <paramref name="methodName" /> with the specified arguments.</summary>
        /// <typeparam name="TValue">The expected return type of the method call.</typeparam>
        /// <param name="methodName">The name of the API method.</param>
        /// <param name="args">An <see cref="IDictionary{String,Object} " /> of arguments that will be passed into the API call.</param>
        /// <param name="excludedArgs">An array of <see cref="String" /> values specifying arguments that should be left out of the API method call.</param>
        /// <returns>A reference to a <see cref="FacebookResponse{TValue}" /> object that will contain the value of the response.</returns>
        /// <remarks>
        /// If a <see cref="Batch" /> is currently active, the <see cref="FacebookResponse{TValue}" /> returned by this method will not contain
        /// a value until <see cref="Api.Batch.Complete" /> is called.
        /// </remarks>
        FacebookResponse<TValue> ExecuteRequest<TValue>(String methodName, IDictionary<String, Object> args, params String[] excludedArgs);

        /// <summary>Gets or sets the current batch, if any, for the implementation of <see cref="IFacebookContext" />.</summary>
        Batch Batch { get; set; }

        /// <summary>Gets or sets the queue, if any, which allows requests to be queued to be executed later.</summary>
        Queue Queue { get; set; }

        /// <summary>Queues a request so that it may be either executed later.</summary>
        /// <typeparam name="TReturnType">The type of value that will be returned from <paramref name="response" />.</typeparam>
        /// <param name="request">A <see cref="FacebookRequest" /> object containing the information for the API request.</param>
        /// <param name="response">A <see cref="IFacebookResponse" /> object that will be populated with the response of the request when it is executed.</param>
        void QueueRequest<TReturnType>(FacebookRequest request, IFacebookResponse response);

        /// <summary>Gets the number of items in the queue.</summary>
        Int32 QueueLength { get; }

        /// <summary>Removes at most the specified number of requests from the <see cref="IFacebookContext.Queue" /> and returns them as an <see cref="ICollection{IQueueItem}" />.</summary>
        /// <param name="count">The maximum number of requests to remove and return.</param>
        /// <returns>an instance of <see cref="ICollection{IQueueItem}" /> referencing</returns>
        ICollection<IQueueItem> DequeueRequests(Int32 count);

        /// <summary>Executes each request in the queue using one or more <see cref="Batch" /> requests.</summary>
        void ExecuteQueue();

        /// <summary>Gets or sets the collection of <see cref="IQueueItem" /> objects that are queued to be executed.</summary>
        ICollection<IQueueItem> QueueItems { get; set; }

        /// <summary>Gets or sets the <see cref="RequestMode" />, which determines the default functionality for API requests.</summary>
        RequestMode RequestMode { get; set; }
    }
}
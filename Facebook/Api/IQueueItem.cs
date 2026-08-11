using System;
using System.Xml.Serialization;

namespace Facebook.Api
{
    /// <summary>Defines a contract for an object that encapsulates all the objects required to execute an API request
    /// within a <see cref="Batch" />.</summary>
    public interface IQueueItem
    {
        /// <summary>Gets the <see cref="FacebookRequest" /> object containing information about the request, such as the method and any parameters to be passed in.</summary>
        FacebookRequest Request { get; }

        /// <summary>Gets a reference to the <see cref="IFacebookResponse" /> object representing the unpopulated response of the request.</summary>
        IFacebookResponse Response { get; }

        /// <summary>Gets or sets a reference to a <see cref="ISessionInfo" /> object containing information about the session the request will be made under.</summary>
        ISessionInfo Session { get; set; }
    }
}
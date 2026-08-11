using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Facebook.Serialization;

namespace Facebook.Api
{
    /// <summary>Encapsulates all the objects required to execute an API request within a <see cref="Queue" /> or <see cref="Batch"/>.</summary>
    /// <typeparam name="TReturnType">The return type of the API method.</typeparam>
    [Serializable]
    [XmlRoot("queueItem")]
    public struct QueueItem<TReturnType> : IQueueItem
    {
        /// <summary>Intializes an instance of <see cref="QueueItem{TReturnType}" /> with the specified objects.</summary>
        /// <param name="request">A <see cref="FacebookRequest" /> object containing information about the request, such as the method and any parameters to be passed in.</param>
        /// <param name="response">An <see cref="IFacebookResponse" /> object representing the unpopulated response of the request.</param>
        internal QueueItem(FacebookRequest request, IFacebookResponse response)
            : this()
        {
            this.Request = request;
            this.Response = (FacebookResponse<TReturnType>)response;      
        }

        /// <summary>Gets or sets the <see cref="FacebookRequest" /> object containing information about the request, such as the method and any parameters to be passed in.</summary>
        [XmlElement("request")]
        public FacebookRequest Request { get; set; }

        /// <summary>Gets or sets a reference to the <see cref="FacebookResponse{TReturnType}" /> object representing the unpopulated response of the request.</summary>
        [XmlElement("response")]
        public FacebookResponse<TReturnType> Response { get; set; }

        /// <summary>Gets or sets a reference to a <see cref="ISessionInfo" /> object containing information about the session the request will be made under.</summary>
        [XmlElement("session")]
        public ISessionInfo Session { get; set; }

        /// <summary>Gets or sets a reference to the <see cref="IFacebookResponse" /> object representing the unpopulated response of the request.</summary>
        [XmlIgnore]
        IFacebookResponse IQueueItem.Response { get { return this.Response; } }
    }
}
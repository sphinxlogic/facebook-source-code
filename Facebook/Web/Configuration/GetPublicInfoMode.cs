namespace Facebook.Web.Configuration
{
    /// <summary>Describes if and when an automatic call to <c>Application.GetPublicInfo</c> is made by the client framework.</summary>    
    /// <seealso cref="FacebookWebSection.Mode" />
    public enum GetPublicInfoMode
    {
        /// <summary>A request to <c>Application.GetPublicInfo</c> is made when the ASP.NET application is first initialized.</summary>
        ApplicationInitialization,

        /// <summary>A the request is made the first time a value is needed.</summary>
        FirstUse,

        /// <summaryThe request will never be made automatically, and must be manually invoked by the client application if information is needed.></summary>
        Never
    }
}
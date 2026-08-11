using System;
using System.Collections.Generic;

namespace Facebook.Api.Controllers
{
    public partial class EventsController
    {        /// <summary>Returns all visible events according to the filters specified.</summary>
        /// <param name="uid">Filter by events associated with a user with this <code>uid</code>.</param>
        /// <param name="startTime">Filter with this UTC as lower bound.</param>
        /// <param name="endTime">Filter with this UTC as upper bound.</param>
        public FacebookResponse<FacebookList<Event>> Get(Int64 uid, DateTime startTime, DateTime endTime)
        {
            var args = new Dictionary<String, Object>();
            args.Add("uid", uid);
            args.Add("start_time", (Int64)startTime.ToUnixDateTime().Value);
            args.Add("end_time", (Int64)endTime.ToUnixDateTime().Value);
            var response = this.ExecuteRequest<FacebookList<Event>>("Events.get", args);
            return response;
        }

        /// <summary>Returns all visible events according to the filters specified.</summary>
        /// <param name="uid">Filter by events associated with a user with this <code>uid</code>.</param>
        /// <param name="rsvpStatus">Filter by this RSVP status. The RSVP status should be one of the following strings:
        ///<ul><li> attending
        ///</li><li> unsure
        ///</li><li> declined
        ///</li><li> not_replied</param>
        /// <param name="startTime">Filter with this UTC as lower bound.</param>
        /// <param name="endTime">Filter with this UTC as upper bound.</param>
        public FacebookResponse<FacebookList<Event>> Get(Int64 uid, String rsvpStatus, DateTime startTime, DateTime endTime)
        {
            var args = new Dictionary<String, Object>();
            args.Add("uid", uid);
            args.Add("rsvp_status", rsvpStatus);
            args.Add("start_time", (Int64)startTime.ToUnixDateTime().Value);
            args.Add("end_time", (Int64)endTime.ToUnixDateTime().Value);
            var response = this.ExecuteRequest<FacebookList<Event>>("Events.get", args);
            return response;
        }

        /// <summary>Returns all visible events according to the filters specified.</summary>
        /// <param name="uid">Filter by events associated with a user with this <code>uid</code>.</param>
        /// <param name="eids">Filter by this list of event IDs. This is a comma-separated list of event IDs.</param>
        /// <param name="startTime">Filter with this UTC as lower bound. A missing or zero parameter indicates no lower bound.</param>
        /// <param name="endTime">Filter with this UTC as upper bound. A missing or zero parameter indicates no upper bound.</param>
        public FacebookResponse<FacebookList<Event>> Get(Int64 uid, String[] eids, DateTime startTime, DateTime endTime)
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("uid", uid);
            args.Add("eids", eids);
            args.Add("start_time", (Int64)startTime.ToUnixDateTime().Value);
            args.Add("end_time", (Int64)endTime.ToUnixDateTime().Value);
            var response = this.ExecuteRequest<FacebookList<Event>>("Events.get", args);
            return response;
        }

        /// <summary>Returns all visible events according to the filters specified.</summary>
        /// <param name="uid">Filter by events associated with a user with this <code>uid</code>.</param>
        /// <param name="eids">Filter by this list of event IDs. This is a comma-separated list of event IDs.</param>
        /// <param name="rsvpStatus">Filter by this RSVP status. The RSVP status should be one of the following strings:
        ///<ul><li> attending
        ///</li><li> unsure
        ///</li><li> declined
        ///</li><li> not_replied</param>
        /// <param name="startTime">Filter with this UTC as lower bound. A missing or zero parameter indicates no lower bound.</param>
        /// <param name="endTime">Filter with this UTC as upper bound. A missing or zero parameter indicates no upper bound.</param>
        public FacebookResponse<FacebookList<Event>> Get(Int64 uid, String[] eids, String rsvpStatus, DateTime startTime, DateTime endTime)
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("uid", uid);
            args.Add("eids", eids);
            args.Add("rsvp_status", rsvpStatus);
            args.Add("start_time", (Int64)startTime.ToUnixDateTime().Value);
            args.Add("end_time", (Int64)endTime.ToUnixDateTime().Value);
            var response = this.ExecuteRequest<FacebookList<Event>>("Events.get", args);
            return response;
        }

        /// <summary>Returns all visible events according to the filters specified.</summary>
        /// <param name="eids">Filter by this list of event IDs. This is a comma-separated list of event IDs.</param>
        /// <param name="startTime">Filter with this UTC as lower bound. A missing or zero parameter indicates no lower bound.</param>
        /// <param name="endTime">Filter with this UTC as upper bound. A missing or zero parameter indicates no upper bound.</param>
        public FacebookResponse<FacebookList<Event>> Get(String[] eids, DateTime startTime, DateTime endTime)
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("eids", eids);
            args.Add("start_time", (Int64)startTime.ToUnixDateTime().Value);
            args.Add("end_time", (Int64)endTime.ToUnixDateTime().Value);
            var response = this.ExecuteRequest<FacebookList<Event>>("Events.get", args);
            return response;
        }

        /// <summary>Returns all visible events according to the filters specified.</summary>
        /// <param name="eids">Filter by this list of event IDs. This is a comma-separated list of event IDs.</param>
        /// <param name="rsvpStatus">Filter by this RSVP status. The RSVP status should be one of the following strings:
        ///<ul><li> attending
        ///</li><li> unsure
        ///</li><li> declined
        ///</li><li> not_replied</param>
        /// <param name="startTime">Filter with this UTC as lower bound. A missing or zero parameter indicates no lower bound.</param>
        /// <param name="endTime">Filter with this UTC as upper bound. A missing or zero parameter indicates no upper bound.</param>
        public FacebookResponse<FacebookList<Event>> Get(String[] eids, String rsvpStatus, DateTime startTime, DateTime endTime)
        {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("eids", eids);
            args.Add("rsvp_status", rsvpStatus);
            args.Add("start_time", (Int64)startTime.ToUnixDateTime().Value);
            args.Add("end_time", (Int64)endTime.ToUnixDateTime().Value);
            var response = this.ExecuteRequest<FacebookList<Event>>("Events.get", args);
            return response;
        }
    }
}
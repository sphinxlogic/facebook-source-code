using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Facebook.Api
{
    [Serializable]
    public class Queue : IDisposable
    {
        /// <summary>Initializes an instance of <see cref="Batch" />.</summary>
        /// <param name="context">A reference to the <see cref="IFacebookContext" /> object that will own the batch.</param>
        internal Queue(IFacebookContext context)
        {
            this.Context = context;
        }

        public static Queue Start(IFacebookContext context)
        {
            if (context.Queue == null)
            {
                context.Queue = new Queue(context);
                return context.Queue;
            }
            else throw new InvalidOperationException("Queueing is already active.");
        }

        /// <summary>Gets a reference to the <see cref="IFacebookContext" /> object that owns the batch.</summary>
        internal IFacebookContext Context { get; private set; }

        /// <summary>Discards the queue and all requests that have been queued into it.</summary>
        public void Discard()
        {
            this.Context.QueueItems = null;
            ((IDisposable)this).Dispose();
        }

        #region [ IDisposable Members ]

        void IDisposable.Dispose()
        {
            this.Context.Queue = null;
        }

        #endregion
    }
}
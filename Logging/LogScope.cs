//
using System;
//
using log4net;


// Initializes log4net, reads from the configuration file of the running assembly
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace us.kristjansson.Logging
{

    /// <summary>
    /// Log scope object, that is returned from the LogExstension.GetScope methods
    /// </summary>
    public class LogScope : IDisposable
    {

        // The active scope
        private IDisposable scope = null;

        /// <summary>
        /// Creates new log scope
        /// </summary>
        public LogScope()
        {
            scope = log4net.NDC.Push( Guid.NewGuid().ToString() );
        }

        /// <summary>
        /// Creates new log scope
        /// </summary>
        /// <param name="scopeId">The identifier of the new scope</param>
        public LogScope( string scopeId )
        {
            scope = log4net.NDC.Push( scopeId );
        }

        /// <summary>
        /// Stops the scope if actively in scope
        /// </summary>
        public void StopScope()
        {
            if (this.scope != null)
                this.scope.Dispose();
        }

        /// <summary>
        /// Deconstruct.
        /// </summary>
        public void Dispose()
        {
            // Remove scope if still in use
            if (this.scope != null)
                this.scope.Dispose();
        }

    }  // EOC
}

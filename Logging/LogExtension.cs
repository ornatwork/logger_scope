//
using System;
//
using log4net;


namespace us.kristjansson.Logging
{

    /// <summary>
    /// ILog extension that makes it convinient to set scope while logging 
    /// </summary>
    public static class LogExtension
    {
        /// <summary>
        /// Starts a scope and returns the scope object, this method will assign
        /// random Guid as identifier for the scope
        /// </summary>
        /// <returns>The scope object</returns>
        public static IDisposable GetScope( this ILog logger )
        {
            // new scope, random Guid
            return new LogScope();
        }

        /// <summary>
        /// Starts a scope and returns the scope object
        /// </summary>
        /// <param name="scopeId">The scope Id to use</param>
        /// <returns>The scope object</returns>
        public static IDisposable GetScope(this ILog logger, string scopeId)
        {
            // New scope scopeId
            return new LogScope(scopeId);
        }
                

    }  // EOC
}

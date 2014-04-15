//
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//
using log4net;
//
using us.kristjansson.Logging;


// Initializes log4net, reads from the configuration file of the running assembly
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace us.kristjansson.LoggingTest
{
    [TestClass]
    public class LoggerTest
    {

        [TestMethod]
        public void BasicSmokeTest()
        {
            ILog logger = LogManager.GetLogger("Smoke UnitTest");
            
            // Make sure scope is returned
            IDisposable scope = logger.GetScope();
            Assert.IsNotNull(scope);
        }

        
        [TestMethod]
        public void TwoScopes()
        {
            ILog log = LogManager.GetLogger("Testing1");
            ILog log2 = LogManager.GetLogger("Testing2");

            // using takes care of clean up
            using( log.GetScope("Scope 1"))
            {
                log.Debug("scope one ");
                log2.Debug("in scope one as well ");
            }

            //
            log.Debug("after scope 1");


            // Calling explicit start and end 
            IDisposable scope = log.GetScope("Scope 2");
                log.Debug("in Scope two ");
            scope.Dispose();

            //
            log.Debug("after scope 2");
            
        }


        [TestMethod]
        public void InnerScope()
        {
            ILog log = LogManager.GetLogger("Testing1");
            ILog log2 = LogManager.GetLogger("Testing2");
            
            // First scope
            using (log.GetScope("Scope 1"))
            {
                log.Debug("scope one ");
                using (log.GetScope("Scope 1.1"))
                {
                    log.Debug("Inner scope ");
                }

                log2.Debug("in scope one as well ");
            }

            //
            log.Debug("after scopes");
        }


        [TestMethod]
        public void AutoScopeId()
        {
            ILog log = LogManager.GetLogger("Testing1");
            ILog log2 = LogManager.GetLogger("Testing2");

            // First scope
            using (log.GetScope())
            {
                log.Debug("scope one ");
                log2.Debug("in scope one as well ");
            }

            //
            log.Debug("after scope one");


            // Second scope
            using (log.GetScope())
            {
                log.Debug("second scope 1");
                log.Debug("second scope 2");
            }

            //
            log.Debug("after second scope");

        }
        

    }  //  EOC
}

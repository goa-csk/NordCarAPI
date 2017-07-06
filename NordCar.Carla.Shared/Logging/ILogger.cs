using NordCar.Carla.Shared.Logging.LoggingMessage;

namespace NordCar.Carla.Shared.Logging
{
    public interface ILogger
    {
        /// <summary>
        /// Log debug information.
        /// 
        /// <para>Use</para>
        /// Use this method when you want to add very specific information (and possibly so much that it impacts performance) about a specific scenario that you are investigating.
        /// It is allowed to add a lot of different log messages using this level, and also information that cannot stand on its own, 
        /// as it must be expected that the person that initiates and reads the debug log is aware of where the information comes from.
        /// 
        /// <para>Examples</para>
        /// - Loop indexes and class state
        /// - class instantiated
        /// - method enterered/exited
        /// </summary>
        void LogDebug(LogMessage message);

        /// <summary>
        /// Log info information
        /// 
        /// <para>Use</para>
        /// Use this method if you want to log generally relevant information - things that occur in normal scenarios - NEVER things that are not expected.
        /// 
        /// <para>Examples</para>
        /// - Server started
        /// - database sync job started
        /// </summary>
        void LogInfo(LogMessage message);

        /// <summary>
        /// Log Warning information
        /// 
        /// <para>Use</para>
        /// Use this method to indicate that something out of the ordinary has happened. 
        /// What happened must not be expected to stop the program/server or cause errors if the user continues.
        /// 
        /// <para>Examples</para>
        /// - Unexpected concurrency issue - user can try again
        /// - resource shortage (user unable to get a connection to X)
        /// </summary>
        void LogWarning(LogMessage message);

        /// <summary>
        /// Log Error information
        /// 
        /// <para>Use</para>
        /// Use this method to indicate that an error has occured - continueing may have negative sideeffects.
        /// 
        /// <para>Examples</para>
        /// - An exeption occured - but was handled
        /// </summary>
        void LogError(LogMessage message);

        /// <summary>
        /// Log Error information
        /// 
        /// <para>Use</para>
        /// Use this method to indicate that a serious error has occured - continueing will have negative effects if at all possible.
        /// 
        /// <para>Examples</para>
        /// - Program/server will have to shutdown - unable to continue.
        /// </summary>
        void LogFatal(LogMessage message);
    }
}

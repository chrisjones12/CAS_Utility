// ------------------------------------------------------------------------
// <filename>LogEntry.cs</filename>
// <projectName>CasTrace Logfile Search Utility</projectName>
// <copyright file="LogEntry.cs" company="Rural Sourcing Inc.">
//      Custom company copyright tag.
// </copyright>
// <description>Contains structure of a log entry found in a CasTrace log file</description>
// <history>
//      <revision author="Chris Jones" date="10-19-2012" 
//            A description of the change made to the file
//      </revision> 
// </history>
// ------------------------------------------------------------------------

#region Includes
using System;
using System.Collections.Generic;
#endregion

namespace CasTraceLog
{
  /// <summary>
  /// Class that represents log entries within a CasTrace log file
  /// </summary>
  public class LogEntry
  {
    #region Variables and Constants
    private string time;         // Time of log entry
    private string sessionId;    // SessionID logged for an entry
    private string logLevel;     // Level used to show for different types of entries
    private string user;         // User name that was logged during an operation
    private string duration;     // Time duration of a lprocess/operation being logged
    private string objectClass;  // Class name being logged that was part of an operation
    private string method;       // Method name that was being executed at time of log entry
    private string message;      // Information about the operation being logged
    #endregion

    #region Constructors
    /// <summary>
    /// Empty Constructor for LogEntry objects
    /// </summary>
    public LogEntry()
    {
      time = String.Empty;
      sessionId = String.Empty;
      logLevel = String.Empty;
      user = String.Empty;
      duration = String.Empty;
      objectClass = String.Empty;
      method = String.Empty;
      message = String.Empty;
    }
    
    /// <summary>
    /// Constructor that initializes the fields representing a log entry
    /// </summary>
    /// <param name="time">Time of log entry</param>
    /// <param name="sessionId">SessionID logged for an entry</param>
    /// <param name="logLevel">Level used to show for different types of entries</param>
    /// <param name="user">User name that was logged during an operation</param>
    /// <param name="duration">Time duration of a lprocess/operation being logged</param>
    /// <param name="objectClass">Class name being logged that was part of an operation</param>
    /// <param name="method">Method name that was being executed at time of log entry</param>
    /// <param name="message">Information about the operation being logged</param>
    public LogEntry(string time, string sessionId, string logLevel, string user, string duration, string objectClass, string method, string message)
    {
      this.time = time;
      this.sessionId = sessionId;
      this.logLevel = logLevel;
      this.user = user;
      this.duration = duration;
      this.objectClass = objectClass;
      this.method = method;
      this.message = message;
    }
    #endregion

    #region Accessor Methods
    /// <summary>
    /// Access method for an entry's time
    /// </summary>
    /// <returns>String value storing the time field of a LogEntry object</returns>
    public string GetTime()
    {
      return this.time;
    }
    
    /// <summary>
    /// Access method for an entry's session id
    /// </summary>
    /// <returns>String value storing the session id field of a LogEntry object</returns>
    public string GetSessionId()
    {
      return this.sessionId;
    }

    /// <summary>
    /// Access method for an entry's log level
    /// </summary>
    /// <returns>String value storing the log level field of a LogEntry object</returns>
    public string GetLogLevel()
    {
      return this.logLevel;
    }

    /// <summary>
    /// Access method for an entry's user
    /// </summary>
    /// <returns>String value storing the user field of a LogEntry object</returns>
    public string GetUser()
    {
      return this.user;
    }

    /// <summary>
    /// Access method for an entry's duration
    /// </summary>
    /// <returns>String value storing the duration field of a LogEntry object</returns>    
    public string GetDuration()
    {
      return this.duration;
    }

    /// <summary>
    /// Access method for an entry's class
    /// </summary>
    /// <returns>String value storing the objectClass field of a LogEntry object</returns>    
    public string GetObjectClass()
    {
      return this.objectClass;
    }

    /// <summary>
    /// Access method for an entry's method
    /// </summary>
    /// <returns>String value storing the method field of a LogEntry object</returns>    
    public string GetMethod()
    {
      return this.method;
    }

    /// <summary>
    /// Access method for an entry's message
    /// </summary>
    /// <returns>String value storing the message field of a LogEntry object</returns>    
    public string GetMessage()
    {
      return this.message;
    }
    #endregion
  }
}

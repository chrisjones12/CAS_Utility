// ------------------------------------------------------------------------
// <filename>Parser.cs</filename>
// <projectName>CasTrace Logfile Search Utility</projectName>
// <copyright file="Parser.cs" company="Rural Sourcing Inc.">
//      Custom company copyright tag.
// </copyright>
// <description>Contains methods to search documents for keywords and other criteria</description>
// <history>
//      <revision author="Chris Jones" date="10-17-2012" 
//				method="find(string searchText, string filePath)">
//            A description of the change made to the file
//      </revision> 
// </history>
// ------------------------------------------------------------------------

#region Includes
using System;
using System.Collections.Generic;
using System.IO;
using CasTraceLog;
#endregion

namespace CAS_File_Search_Utility
{
  /// <summary>
  /// Class containing methods to search for criteria/filter by log level(s) within the content of the tracelog file
  /// </summary>
  public class Parser
  {
    #region Variables and Constants
    // List to store the log entries that are read in from the trace log file
    private List<LogEntry> logEntries;

    // String storing the path to the file to be read/searched
    private string filePath;
    #endregion

    #region Constructors
    /// <summary>
    /// Empty constructor for Parser objects
    /// </summary>
    public Parser()
    {
      this.logEntries = new List<LogEntry>();
      this.filePath = String.Empty;
    }

    /// <summary>
    /// Constructor for Parser objects with a file path
    /// </summary>
    /// <param name="filePath">
    /// Stores the path to the file to be read/searched
    /// </param>
    public Parser(string filePath)
    {
      this.logEntries = new List<LogEntry>();
      this.filePath = filePath;
    }
    #endregion

    #region Read CasTrace Log File
    /// <summary>
    /// Stores log entries into a list of LogEntry objects that are read from the trace log file
    /// </summary>
    /// <param name="filePath">
    ///  Path to the file name needed to read
    /// </param>
    public void ReadCasTraceLogFile()
    {
      try
      {
        using (StreamReader sr = new StreamReader(filePath))
        {
          // Stores each line that is read from the file
          string line = null;

          // Read a single line at a time and create a LogEntry object to store the entry
          while ((line = sr.ReadLine()) != null)
          {
            // List that stores the individual strings separated by the tab character
            List<String> entry = new List<String>(line.Split('\t'));
            
            // Each log entry contains eight fields, but if both the user and duration fields are empty, an extra empty string will be added to the list
            // By removing the 5th element from the list, the list will have two empty elements in place for the user and duration fields
            if (entry.Count == 9)
            {
              entry.RemoveAt(5);
            }
            
            // If a line is terminated by a newline character, the next line(s) are part of the current log entry
            // Therefore, appending the line(s) to the end of the message field will allow them to be maintained within the current entry
            bool isDone = false;
            while (sr.Peek() == ' ' && !isDone)
            { 
              line = sr.ReadLine();    // read the next line in the file
              entry[7] += line;        // append that line to the current entry's message field
              if (sr.Peek() != ' ')    // stop when the next line starts with a date/new entry
              {
                isDone = true;
              }
            }

            // Create a LogEntry object to store each of the fields and add that object to the list of entries
            logEntries.Add(new LogEntry(entry[0], entry[1], entry[2], entry[3], entry[4], entry[5], entry[6], entry[7]));
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("The file could not be read.");
        Console.WriteLine(e.Message);
      }
    }
    #endregion
    
    #region Search CasTrace Log File
    /// <summary>
    /// Performs a search on the list of log entries based on log level and/or search criteria
    /// </summary>
    /// <param name="logLevels">
    /// List of log levels that user wants to filter into the results
    /// </param>
    /// <param name="searchStrings">
    /// List of search criteria entered as text by a user
    /// </param>
    /// <returns>List of search results of log entries</returns>
    public List<LogEntry> SearchCasTraceLogFile(List<String> logLevels, List<String> searchStrings)
    {
      // Stores list of search results
      List<LogEntry> searchResults = new List<LogEntry>();

      if (logLevels.Count > 0 && searchStrings.Count > 0)                // if user has selected both log level(s) and search criteria
      {                                                                  // for each log entry with selected log level
        foreach (LogEntry entry in GetLogEntriesByLogLevel(logLevels))   // check each search criteria for matches
        {
          foreach (string keyword in searchStrings)  
          {
            if (entry.GetMessage().Contains(keyword))                    // if the message contains the search criteria and log level, add it to the resultset
            {
              searchResults.Add(entry);
            }
          }
        }
      }
      else if (logLevels.Count > 0 && searchStrings.Count == 0)          // if user has selected log level(s) but no search criteria
      {                                                                  // add each entry that has the selected log level
        foreach (LogEntry entry in GetLogEntriesByLogLevel(logLevels))  
        {
          searchResults.Add(entry);
        }
      }
      else if (logLevels.Count == 0 && searchStrings.Count > 0)  // if user didn't select log level but entered search criteria
      {                                                          // for each entry
        foreach (LogEntry entry in logEntries)                   // check each search criteria for matches
        {
          foreach (string keyword in searchStrings)  
          {
            if (entry.GetMessage().Contains(keyword))            // if the message contains the search criteria, add it to the resultset
            {
              searchResults.Add(entry);
            }
          }
        }
      }
      else  // if neither log level selected nor search criteria entered, set search results to all entries
      {
        searchResults = this.logEntries;
      }

      return searchResults;
    }
    #endregion

    #region Accessor Methods
    /// <summary>
    /// Returns the set of all log entries of the trace file
    /// </summary>
    /// <returns>Set of all LogEntry objects read from the trace file</returns>
    public List<LogEntry> GetAllLogEntries()
    {
      return this.logEntries;
    }

    /// <summary>
    /// Returns a set of log entries based on what log level the user wants to filter
    /// </summary>
    /// <param name="logLevels">
    /// List of log levels that the user would like to include
    /// </param>
    /// <returns>List of LogEntry objects that have been included by user input for log levels</returns>
    public List<LogEntry> GetLogEntriesByLogLevel(List<String> logLevels)
    {
      // List to store the result set of log entries
      List<LogEntry> entries = new List<LogEntry>();
      
      // Check each entry to add to the filtered set based on the log levels requested
      foreach (LogEntry entry in logEntries)
      {
        if (logLevels.Contains(entry.GetLogLevel()))
        {
          entries.Add(entry);
        }
      }
      return entries;
    }
    #endregion
  }
}

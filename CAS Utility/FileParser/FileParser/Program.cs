#region Includes
using System;
using System.Collections.Generic;
using System.IO;
using CAS_File_Search_Utility;
using CasTraceLog;
#endregion

namespace FileParser
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        string filePath = args[0];
        if (File.Exists(filePath))
        {
          List<String> searchStrings = new List<String>();
          searchStrings.Add("Prm");
          searchStrings.Add("Field Sales Activity");
          
          Parser parser = new Parser(filePath);
          parser.ReadCasTraceLogFile();
          
          List<String> logLevels = new List<String>();
          //logLevels.Add("Info(8)");
          //logLevels.Add("SQL(6)");
          logLevels.Add("Error(9)");
          //logLevels.Add("Performance(5)");
          
          foreach (LogEntry entry in parser.SearchCasTraceLogFile(logLevels, searchStrings))
          {
            Console.WriteLine(entry.GetTime());
            Console.WriteLine(entry.GetSessionId());
            Console.WriteLine(entry.GetLogLevel());
            Console.WriteLine(entry.GetUser());
            Console.WriteLine(entry.GetDuration());
            Console.WriteLine(entry.GetObjectClass());
            Console.WriteLine(entry.GetMethod());
            Console.WriteLine(entry.GetMessage());
            Console.WriteLine("\n-------------------------------------------------------------------------\n");
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
    }
  }
}

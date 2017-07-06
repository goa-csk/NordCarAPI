//#define SHOW_MILLISEC

using System;
using System.IO;
using System.Text;


namespace NordCar.Carla.Data.Infrastructure
{
	public class FileLog
	{
		/// <remarks>
		/// All levels equal or lower are logged. Use power of 2 for level
		/// </remarks>
		public enum Level { NONE = 0, SYSTEM = 1, ERROR = 2, WARNING = 4, VERBOSE = 8, DEBUG = 16, ALL = 64 };
		static string strPath = null;
		static string iso8859 = "iso-8859-1";

		//TODO: set to none after tests
		static private Level logLevel = Level.DEBUG; //Level.NONE;

		static private string serverProcId = string.Empty;		//Server process id

		/// <summary>
		/// 18/5/10 Show server process id in log
		/// </summary>
		static public string ServerProcId
		{
			set { serverProcId = value; }
			get { return serverProcId; }
		}

		static  FileLog()
		{
		}
		
		/// <summary>
		/// Set logfile path and name
		/// </summary>
		/// <param name="logPath"></param>
		/// <returns></returns>
		static public bool OpenLog( string logPath)
		{
			strPath = logPath;
			// If more than 2 MB log, delete it at start
			try {
				FileInfo fi = new FileInfo( strPath );
				if( fi.Exists  && fi.Length > (1024 * 1024) *2 ) {
					fi.Delete();
				}
				fi = null;
			} catch( Exception ) {}
			return true;
		}
		
		/// <summary>
		/// Write a line of info. Open, write, close to allow other access
		/// Set to use iso8859-1 encoding
		/// </summary>
		/// <param name="level">Log level</param>
		/// <param name="data">
		/// <see cref="System.String"/>
		/// </param>
		static public void WriteLog( Level level, string data )
		{
			if( logLevel == Level.NONE || logLevel < level )
				return;
			StreamWriter sw = null;
			try {
				sw = new StreamWriter( strPath, true, System.Text.Encoding.GetEncoding(iso8859) ); //Always append
			} catch( Exception  )
			{
				return;
			}

			//Only one access at a time allowed
			lock(sw) {
				// 12/5/10 Use string builder and add instance if > 0 
				StringBuilder sbstr = new StringBuilder();
				string strLevel = "";
				int lineLength = 95;
				if( serverProcId != null && serverProcId.Length > 0 ) {
					sbstr.Append(serverProcId);
					sbstr.Append(" ");
				}
				if( (level & Level.DEBUG) == level )
					sbstr.Append("dbg ");
				else if( ( level & Level.ERROR ) == level )
					sbstr.Append("FEJL ");
				else if( ( level & Level.WARNING ) == level )
					sbstr.Append("Advarsel ");
				else if( ( level & Level.VERBOSE ) == level )
					sbstr.Append("Info ");
				else if( ( level & Level.SYSTEM ) == level )
					sbstr.Append("SysInfo: ");

				strLevel = sbstr.ToString();
				StringBuilder logstrb = new StringBuilder(data.Replace("\n", "[NL]"));
				//string logstr = data.Replace("\n", "[NL]");
				//data = logstr.Replace("\x1c", "[028]");
				//logstr = data.Replace("\x1f", "[031]");
				logstrb = logstrb.Replace( "\x1c", "[028]" );
				logstrb = logstrb.Replace( "\x1f", "[031]" );
				string logstr = logstrb.ToString();
				
				int start = 0;
				DateTime dt = DateTime.Now;
				if( logstr.Length > lineLength ) {
#if SHOW_MILLISEC
					sw.WriteLine( dt + ":{0}: {1}{2}", dt.Millisecond, strLevel, logstr.Substring( start, lineLength ) );
#else
					sw.WriteLine( dt + ": {0}{1}", strLevel, logstr.Substring( start, lineLength ) ); //+ sw.NewLine);
#endif
					start += lineLength;
					while( start < logstr.Length ) {
						if( start + lineLength > logstr.Length )
							lineLength = logstr.Length - start;
						sw.WriteLine("                      {0}", logstr.Substring(start,lineLength) ); //+ sw.NewLine);
						start += lineLength;
					}
				} else {
#if SHOW_MILLISEC
					sw.WriteLine( dt + ":{0}: {1}{2}", dt.Millisecond, strLevel, logstr ); 
#else
					sw.WriteLine(dt + ": {0}{1}", strLevel, logstr); // + sw.NewLine);
#endif
				}
				sw.Flush();
				sw.Close();
				sw.Dispose();
			}
		}
		
		/// <summary>
		/// Call open with overwrite, close. Set to use iso8859-1 encoding
		/// </summary>
		static public void ClearLog()
		{
			StreamWriter sw = null;
			try {
				sw = new StreamWriter( strPath, false, System.Text.Encoding.GetEncoding( iso8859 ) );
			} catch( Exception ) {
				return;
			}
			sw.Close();
			sw.Dispose();
		}


		/// <summary>
		/// Set/get current loglevels
		/// </summary>
		static public Level LogValue
		{
		    get { return logLevel; }
			set { logLevel = value; }
		}

        ///// <summary>
        ///// Show snapshot of log file in Wordpad
        ///// </summary>
        //static public bool ShowLogFile()
        //{
        //    try {
        //        //NOTE: Extra inverted commas (") for XP spaces in path
        //        System.Diagnostics.Process.Start( "WordPad", "\"" + strPath  + "\"" );
        //        return true;
        //    } catch( Exception e ) {
        //        FileLog.WriteLog( FileLog.Level.ERROR, "Program start fejl: WordPad " + e.Message );
        //        EgeMessageBox.Show( null, "Program start fejl: WordPad " + e.Message, "Fejl" );
        //    }
        //    return false;
        //}
	}
}

//#define DEBUG_SOLO

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  public class Program {
#if DEBUG_SOLO
    public static bool Enemy = false;
    public static Process OtherEnd = new Process();

#endif

    static void Main( string[] args ) {
      Trace.AutoFlush = true;


#if DEBUG_SOLO
      for ( int i = 0; i < args.Length; i++ ) {
        if ( args[i] == "enemy" ) {
          //Trace.Listeners.Add( new TextWriterTraceListener( new StreamWriter( "TranspositifonTable_Output_enemy.txt", false ) ) );
          Trace.WriteLine( "Playing as the opponent" );
          Enemy = true;
          Winboard.Enemy = true;
        }
      }

      if ( !Enemy ) {
        Trace.Listeners.Add( new ConsoleTraceListener() );
        //Trace.Listeners.Add( new TextWriterTraceListener( new StreamWriter( "TranspositifonTable_Output_main.txt", false ) ) );
        //Debug.Listeners.Add( new TextWriterTraceListener( new StreamWriter( "chessboard_main.txt", false ) ) );
        Trace.WriteLine( "Playing as the main process" );
        Winboard.OtherProcess = OtherEnd;
        OtherEnd.StartInfo.FileName = "IntelliChess.exe";
        OtherEnd.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
        OtherEnd.StartInfo.Arguments = "enemy ";
        OtherEnd.StartInfo.UseShellExecute = false; //Needs to be false to redirect output
        OtherEnd.StartInfo.RedirectStandardOutput = true;
        OtherEnd.StartInfo.RedirectStandardInput = true;
        OtherEnd.Start();
      }
#endif 
#if DEBUG_SOLO
      Winboard winboard = new Winboard();


      if ( !Enemy ) {
        winboard.Handler( "new" );
        winboard.Handler( "go" );
        while ( true ) {
          string inputString = OtherEnd.StandardOutput.ReadLine();
          Trace.WriteLine( "Input: " + inputString );
          winboard.Handler( inputString );
        }
      } else {
        winboard.Handler( "new" );
        while ( true ) {
          string inputString = Console.ReadLine();
          Trace.WriteLine( "Input: " + inputString );
          winboard.Handler( inputString );
        }
      }
#else 
        Winboard winboard = new Winboard();
        while ( true ) {
          string inputString = Console.ReadLine();
          using ( StreamWriter outputFromWin = new StreamWriter( "OutputFromWinboard.txt", true ) ) {
            outputFromWin.WriteLine( inputString );
          }
          winboard.Handler( inputString );
        }
#endif
        
    }

  }
}

//#define DEBUG_SOLO
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  public class Logger {
    private static StreamWriter MovesWriter = null;
    private static StreamWriter TimeWriter = null;
    private static bool doWriteTime = true;
    private static bool doWrite = true;

    public static void OutputMove( string move ) {
#if DEBUG_SOLO
      if ( !Winboard.Enemy ) {
#endif
      if ( doWrite ) {
        if ( MovesWriter == null ) {
          try {
            MovesWriter = new StreamWriter( "Moves.acr", false );
            MovesWriter.AutoFlush = true;
          } catch ( Exception e ) {
            doWrite = false;
          }
        }
        if ( doWrite )
          MovesWriter.WriteLine( move );
      }
#if DEBUG_SOLO
      }
#endif

    }


    public static void OutputTime( string time ) {
#if DEBUG_SOLO
        if (!Winboard.Enemy)
        {
#endif
      if ( doWriteTime ) {
        if ( TimeWriter == null ) {
          try {
            TimeWriter = new StreamWriter( "TimePerMove.fe", false );
            TimeWriter.AutoFlush = true;
          } catch ( Exception e ) {
            doWriteTime = false;
          }
        }
        if ( doWriteTime )
          TimeWriter.WriteLine( time );
      }
#if DEBUG_SOLO
        }
#endif
    }

  }

  public class Winboard {
#if DEBUG_SOLO
    public static bool Enemy = false;
    public static Process OtherProcess = null;
#endif
    public ChessBoard chessBoard { get; private set; }
    public int protover { get; private set; }
    public int movesPerMinutes { get; private set; }
    public int minutes { get; private set; }
    public bool isAcceptedDraw { get; private set; }
    public bool isAcceptedColors { get; private set; }
    public bool isAcceptedTime { get; private set; }
    public ChessPieceColors winboardColor { get; private set; }
    public ChessPieceColors engineColor { get; set; }
    public bool whiteTurn { get; private set; }
    public bool force { get; private set; }
    public bool isFirstGo { get; private set; }
    private StringBitboardConverter _winboardConverter;
    private StringBitboardConverter _engineConverter;
    private static Regex STRINGMOVE_FORMAT = new Regex( "[a-h][1-8][a-h][1-8][qrbn]?" );
    public int depth;

    public Winboard() {
      chessBoard = new ChessBoard();
      protover = 0;
      movesPerMinutes = 0;
      minutes = 0;
      whiteTurn = true;
      force = false;
      isFirstGo = true;
      depth = 2;
      winboardColor = ChessPieceColors.White;
      engineColor = ChessPieceColors.Black;
      _winboardConverter = new StringBitboardConverter( chessBoard, winboardColor );
      _engineConverter = new StringBitboardConverter( chessBoard, engineColor );
    }


    public void Handler( string inputString ) {
      Trace.WriteLine( "Handler() input: " + inputString );
      string[] input = inputString.Split( ' ' );
      switch ( input[0] ) {

        case "protover":
          protover = Convert.ToInt32( input[1] );
          if ( protover == 2 ) {
            Console.WriteLine( "feature colors=0 done=1" );
          }
          break;

        case "new":
          chessBoard.InitializeGame();
          Bayes.HandleEvalConfig( out depth, Path.GetFullPath( "config.txt" ) );
          if ( Bayes.TuneEvalValues )
            Bayes.ModifyValues( Bayes.RandomizeEvalConfiguration( Bayes.CreateOutputString(chessBoard) ), chessBoard);
          else {
            Bayes.ModifyValues( Bayes.FindBestEvalConfiguration( Path.GetFullPath( "log.txt" ) ), chessBoard );
          }
          isFirstGo = true;
          break;

        case "level":
          movesPerMinutes = Convert.ToInt32( input[1] );
          minutes = Convert.ToInt32( input[2] );
          break;

        case "accepted":
          switch ( input[1] ) {
            case "colors":
              isAcceptedColors = true;
              break;

            case "time":
              isAcceptedTime = true;
              break;
          }
          break;

        case "quit":
          System.Environment.Exit( 0 );
          break;

        case "force":
          force = true;
          break;

        case "rejected":
          throw new Exception( "Feature '" + input[1] + "' was not accepted." );

        case "result":

          if (Bayes.TuneEvalValues) {
              Bayes.RepetetionPerEvalConfiguration--;
              if (Bayes.RepetetionPerEvalConfiguration == 0) {
                  Bayes.Seed++;
                  Bayes.RepetetionPerEvalConfiguration = 10;
              }
          }

          Bayes.WriteToEvalConfig(Path.GetFullPath("config.txt"), depth);

          switch ( input[1] ) {
            case "1-0":
              chessBoard.State = ChessBoardGameState.WhiteMate;
              Bayes.WriteLineToTxtFile( Path.GetFullPath( "log.txt" ), Bayes.CreateEvalConfigurationStringLogFormat( true, Bayes.CreateOutputString(chessBoard) ) );
              break;

            case "0-1":
              chessBoard.State = ChessBoardGameState.BlackMate;
              Bayes.WriteLineToTxtFile(Path.GetFullPath("log.txt"), Bayes.CreateEvalConfigurationStringLogFormat(false, Bayes.CreateOutputString(chessBoard)));
              break;

            case "1/2-1/2":
              chessBoard.State = ChessBoardGameState.Draw;
              Bayes.WriteLineToTxtFile(Path.GetFullPath("log.txt"), Bayes.CreateEvalConfigurationStringLogFormat(false, Bayes.CreateOutputString(chessBoard)));
              break;

            case "*":
              Console.WriteLine( "Error (unknown command): " + input[0] );
              break;
          }
          break;

        case "go":
          if ( isFirstGo == true ) {
            force = false;
            DoMove();
            isFirstGo = false;
          } else {
            Console.WriteLine( "Error (Not handled): go" );
          }
          break;

        default:
          if ( STRINGMOVE_FORMAT.IsMatch( input[0] ) ) {
            Logger.OutputMove( input[0] );
            ColoredBitBoard bitBoardMoveRecived = _winboardConverter.ConvertStringMoveToBitBoard( input[0] );
            chessBoard.Update( bitBoardMoveRecived );
            whiteTurn = !whiteTurn;
            if ( force == false ) {
              DoMove();
            }


          } else {
            Console.WriteLine( "Error (unknown command): " + input[0] );
          }
          break;
      }
    }

    private void DoMove() {
      if ( whiteTurn && isFirstGo ) {
        winboardColor = ChessPieceColors.Black;
        engineColor = ChessPieceColors.White;
        _winboardConverter = new StringBitboardConverter( chessBoard, winboardColor );
        _engineConverter = new StringBitboardConverter( chessBoard, engineColor );
      }
#if DEBUG
      Stopwatch sw = new Stopwatch();
      sw.Start();
#endif
      ColoredBitBoard bitBoardMoveTaken = NegaMax.GetBestMove( chessBoard, depth, engineColor, MoveGen.GenerateLegalMoves, Eval.EvaluateState );
#if DEBUG
      sw.Stop();
      Logger.OutputTime( sw.ElapsedMilliseconds.ToString() );
      sw.Reset();
#endif

      if ( ( bitBoardMoveTaken != null ) && ( chessBoard.State != ChessBoardGameState.Draw ) ) {
        string stringMoveTaken = _engineConverter.ConvertBitBoardMoveToString( bitBoardMoveTaken );

        Debug.Assert( STRINGMOVE_FORMAT.IsMatch( stringMoveTaken ) );
        
        chessBoard.Update( bitBoardMoveTaken );
        Logger.OutputMove( stringMoveTaken );


#if DEBUG_SOLO
        if ( Enemy ) {
          Console.WriteLine( stringMoveTaken );
        } else {
          OtherProcess.StandardInput.WriteLine( stringMoveTaken );
        }
#else
        Console.WriteLine( "move " + stringMoveTaken );
#endif

        whiteTurn = !whiteTurn;
      } else {
        if ( chessBoard.WhiteKing.IsChecked ) {
          chessBoard.State = ChessBoardGameState.BlackMate;
          Console.WriteLine( "result 0-1 {Black Wins by Checkmate}" );
        } else if ( chessBoard.BlackKing.IsChecked ) {
          chessBoard.State = ChessBoardGameState.WhiteMate;
          Console.WriteLine( "result 1-0 {White Wins by Checkmate}" );
        } else {
          chessBoard.State = ChessBoardGameState.Draw;
          Console.WriteLine( "result 1/2-1/2 {Draw}" );
        }
      }
    }
  }
}

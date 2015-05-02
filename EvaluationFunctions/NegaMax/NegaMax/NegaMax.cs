using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  public class NegaMax {


    public delegate List<ColoredBitBoard> GenerateMoveDelegate( ChessBoard board, ChessPieceColors color );
    public delegate double GetMaterialDelegate( ChessBoard board );

    public enum TerminalConditions { Win, Draw, NotTerminal }




    static NegaMax() {
      BinaryFormatter bf = new BinaryFormatter();
      if ( File.Exists( "TranspositionTable_serialize.obj" ) ) {
        var stream = File.Open( "TranspositionTable_serialize.obj", FileMode.Open );

        Trace.Write( "Loading TranspositionTable_serialize.obj" );
        TranspositionTable.TranspositionCache = (TranspositionTable)bf.Deserialize( stream );
      } else {
        Trace.Write( "Creating new Transposition table" );
        TranspositionTable.TranspositionCache = new TranspositionTable();
      }
    }


    public static TerminalConditions IsTerminal_Debug( ChessBoard board, ChessPieceColors color, GenerateMoveDelegate moveGenerator ) {
      return IsTerminal( board, color, moveGenerator );
    }
    public static double GetMaterialValue_Debug( ChessBoard board ) {
      return Eval.EvaluateState( board );
    }

    /// <summary>
    /// Decides the best move for the player given by the color parameter, with a search
    /// of the given depth. 
    /// </summary>
    public static ColoredBitBoard GetBestMove( ChessBoard board, int depth, ChessPieceColors color, GenerateMoveDelegate GenerateMoves, GetMaterialDelegate GetMaterialValue ) {
      Debug.Assert( board != null );
      Debug.Assert( depth > 0 );
      Debug.Assert( GenerateMoves != null );
      Debug.Assert( GetMaterialValue != null );

      //Alpha, have to add one to change symbol later otherwise overflow.
      double a = int.MinValue + 1;
      //Beta value.
      double b = int.MaxValue;
      double val = 0;
      ColoredBitBoard bestMove = null;
      List<ColoredBitBoard> moves = GenerateMoves( board, color );
      var ttEntry = new TranspositionEntry( board.BoardHash.Key, depth, val, false, EntryType.Exact );
      foreach ( ColoredBitBoard move in moves ) {
        board.Update( move );
        if ( color == ChessPieceColors.Black ) {
          val = -NegaMaxAlgorithm( board, depth - 1, -b, -a, ChessPieceColors.White, GenerateMoves, GetMaterialValue );
        } else if ( color == ChessPieceColors.White ) {
          val = -NegaMaxAlgorithm( board, depth - 1, -b, -a, ChessPieceColors.Black, GenerateMoves, GetMaterialValue );
        }
        if ( val > a ) {
          a = val;
          bestMove = move;
          ttEntry.BestMove = move;
          ttEntry.Score = a;
        }

        board.Undo();
      }
      TranspositionTable.TranspositionCache.Add( ttEntry );
      return bestMove;
    }

    /// <summary>
    /// Implementation of the NegaMax algorithm.
    /// <param name="board"></param>
    /// <param name="depth"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="color"></param>
    /// <param name="GenerateMoves"></param>
    /// <param name="GetMaterialValue"></param>
    /// <returns></returns>
    public static double NegaMaxAlgorithm(
        ChessBoard board,
        int depth, double a, double b,
        ChessPieceColors color,
        GenerateMoveDelegate GenerateMoves,
        GetMaterialDelegate GetMaterialValue
        ) {
      ColoredBitBoard bestMove = null;
      var hashBeforeTermCondition = board.BoardHash.Key;
      var termCondition = IsTerminal( board, color, GenerateMoves );
      Debug.Assert( hashBeforeTermCondition == board.BoardHash.Key );
      if ( termCondition == TerminalConditions.Win ||
           termCondition == TerminalConditions.Draw ||
           depth == 0 )

        return (int)color * ( GetMaterialValue( board ) );
      else {
        bool usingTT = false;
        ulong hash_beforeMove = board.BoardHash.Key;
        var ttEntry = TranspositionTable.TranspositionCache[board.BoardHash.Key];
        if ( ttEntry != null &&
             ttEntry.Hash == board.BoardHash.Key &&
             ttEntry.Depth >= depth
          ) {
          if ( ttEntry.NodeType == EntryType.Alpha && ttEntry.Score <= a ) {
            return a;
          }

          if ( ttEntry.NodeType == EntryType.Beta && ttEntry.Score >= b ) {
            return b;
          }
          if ( ttEntry.NodeType == EntryType.Exact )
            return ttEntry.Score;
          usingTT = true;
        }

        TranspositionEntry newEntry = new TranspositionEntry( board.BoardHash.Key, depth, 0, false, EntryType.Alpha );

        List<ColoredBitBoard> moves = new List<ColoredBitBoard>();
        moves = GenerateMoves( board, color );

        foreach ( ColoredBitBoard move in moves ) {
          double val = 0;
          board.Update( move );
          var hashBeforeRec = board.BoardHash.Key;
          if ( color == ChessPieceColors.White ) {
            val = -NegaMaxAlgorithm( board, depth - 1, -b, -a, ChessPieceColors.Black, GenerateMoves, GetMaterialValue );
          } else if ( color == ChessPieceColors.Black ) {
            val = -NegaMaxAlgorithm( board, depth - 1, -b, -a, ChessPieceColors.White, GenerateMoves, GetMaterialValue );
          }
          Debug.Assert( hashBeforeRec == board.BoardHash.Key );
          board.Undo();
          usingTT = false;
          if ( val >= b ) {
            if ( !usingTT ) {
              newEntry.NodeType = EntryType.Beta;
              newEntry.Score = val;
              TranspositionTable.TranspositionCache.Add( newEntry );
              Debug.Assert( hash_beforeMove == board.BoardHash.Key, "Board hash changed during update/undo" );
            }
            return val;
          }
          if ( val >= a ) {
            a = val;
            bestMove = move;
            newEntry.NodeType = EntryType.Exact;
          }

          Debug.Assert( hash_beforeMove == board.BoardHash.Key, "Board hash changed during update/undo" );
        }
        if ( !usingTT ) {
          if ( newEntry.NodeType == EntryType.Exact ) {
            newEntry.BestMove = bestMove;
          }
          newEntry.Score = a;
          TranspositionTable.TranspositionCache.Add( newEntry );
        }
        return a;
      }
    }
    /// <summary>
    /// Determines whether the given board is in a terminal state, for the
    /// player given by the color parameter.
    /// </summary>
    /// <param name="board"></param>
    /// <param name="color"></param>
    /// <param name="moveDelegate"></param>
    /// <returns></returns>
    private static TerminalConditions IsTerminal( ChessBoard board, ChessPieceColors color, GenerateMoveDelegate moveGenerator ) {
      List<ColoredBitBoard> legalMoves = new List<ColoredBitBoard>();
      legalMoves = moveGenerator( board, color );
      if ( color == ChessPieceColors.White ) {
        if ( legalMoves.Count == 0 && board.WhiteKing.IsChecked ) {
          return TerminalConditions.Win;
        } else if ( legalMoves.Count == 0 && board.WhiteKing.IsChecked == false ) {
          return TerminalConditions.Draw;
        } else
          return TerminalConditions.NotTerminal;
      }
      if ( color == ChessPieceColors.Black ) {
        if ( legalMoves.Count == 0 && board.BlackKing.IsChecked ) {
          return TerminalConditions.Win;
        } else if ( legalMoves.Count == 0 && board.BlackKing.IsChecked == false ) {
          return TerminalConditions.Draw;
        } else
          return TerminalConditions.NotTerminal;
      }
      throw new Exception( "Color should be either White or Black" );
    }
  }
}
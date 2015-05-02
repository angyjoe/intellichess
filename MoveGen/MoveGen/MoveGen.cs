using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  public class MoveGen {   

    private static List<ColoredBitBoard> GeneratePseudoLegalSlidingMoves( ChessBoard inputChessBoard, ChessPieceColors color, EmptyBitBoard blackPieces, EmptyBitBoard whitePieces, EmptyBitBoard allPieces ) {
      List<ColoredBitBoard> result = new List<ColoredBitBoard>();
      result.AddRange( RookMoveGen.RookBitBoardResults( inputChessBoard, color, blackPieces, whitePieces, allPieces ) );
      result.AddRange( BishopMoveGen.BishopBitBoardResults( inputChessBoard, color, blackPieces, whitePieces, allPieces ) );
      result.AddRange( QueenMoveGen.QueenBitBoardResults( inputChessBoard, color, blackPieces, whitePieces, allPieces ) );
      return result;
    }
    private static List<ColoredBitBoard> GeneratePseudoLegalNonSlidingMoves( ChessBoard inputChessBoard, ChessPieceColors color, EmptyBitBoard blackPieces, EmptyBitBoard whitePieces, EmptyBitBoard allPieces ) {
      List<ColoredBitBoard> result = new List<ColoredBitBoard>();

      result.AddRange( PawnMoveGen.PawnBitBoardResults( inputChessBoard, color, blackPieces, whitePieces, allPieces ) );
      result.AddRange( KingMoveGen.KingBitBoardResults( inputChessBoard, color, blackPieces, whitePieces, allPieces ) );
      result.AddRange( KnightMoveGen.KnightBitBoardResults( inputChessBoard, color, blackPieces, whitePieces, allPieces ) );
      return result;
    }
    public static List<ColoredBitBoard> GenerateLegalMoves( ChessBoard inputChessBoard, ChessPieceColors color ) {
      List<ColoredBitBoard> result = new List<ColoredBitBoard>();
      EmptyBitBoard blackPieces = MoveGenUtils.SetBlackBoard( inputChessBoard );
      EmptyBitBoard whitePieces = MoveGenUtils.SetWhiteBoard( inputChessBoard );
      EmptyBitBoard allPieces = MoveGenUtils.SetWholeBoard( inputChessBoard );

      result.AddRange( GeneratePseudoLegalNonSlidingMoves( inputChessBoard, color, blackPieces, whitePieces, allPieces ) );
      result.AddRange(  GeneratePseudoLegalSlidingMoves( inputChessBoard, color, blackPieces, whitePieces, allPieces ) );

      result = RemoveOwnKingChecks( inputChessBoard, result, color );
      result = SetDoesCheck( inputChessBoard, result, color );

      var sortedMoves = MoveGenUtils.SortMoves( result, inputChessBoard, blackPieces, whitePieces );
      return sortedMoves;
    }

    private static bool isOwnKingAttackable( ChessBoard cb, ColoredBitBoard legalMove, ChessPieceColors color ) {
      List<ColoredBitBoard> legalMoves = new List<ColoredBitBoard>();
      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();

      cb.Update( legalMove );

      blackPieces = MoveGenUtils.SetBlackBoard( cb );
      whitePieces = MoveGenUtils.SetWhiteBoard( cb );
      allPieces = MoveGenUtils.SetWholeBoard( cb );

      legalMoves.AddRange( GeneratePseudoLegalNonSlidingMoves( cb, color, blackPieces, whitePieces, allPieces ) );
      legalMoves.AddRange( GeneratePseudoLegalSlidingMoves( cb, color, blackPieces, whitePieces, allPieces ) );

      foreach ( ColoredBitBoard cbb in legalMoves ) {
        if ( color == ChessPieceColors.White && ( cb.BlackKing.Bits & cbb.Bits ) != 0 ) {
          cb.Undo();
          return true;
        } else if ( color == ChessPieceColors.Black && ( cb.WhiteKing.Bits & cbb.Bits ) != 0 ) {
          cb.Undo();
          return true;
        }
      }

      cb.Undo();
      return false;
    }

    private static bool isEnemyKingAttackable( ChessBoard cb, ColoredBitBoard legalMove, ChessPieceColors color ) {
      List<ColoredBitBoard> legalMoves = new List<ColoredBitBoard>();
      EmptyBitBoard blackPieces = new EmptyBitBoard();
      EmptyBitBoard whitePieces = new EmptyBitBoard();
      EmptyBitBoard allPieces = new EmptyBitBoard();

      cb.Update( legalMove );

      blackPieces = MoveGenUtils.SetBlackBoard( cb );
      whitePieces = MoveGenUtils.SetWhiteBoard( cb );
      allPieces = MoveGenUtils.SetWholeBoard( cb );

      if ( legalMove is PawnBitBoard ) {
        legalMoves.AddRange( PawnMoveGen.PawnBitBoardResults( cb, color, blackPieces, whitePieces, allPieces ) );
      } else if ( legalMove is BishopBitBoard ) {
        legalMoves.AddRange( BishopMoveGen.BishopBitBoardResults( cb, color, blackPieces, whitePieces, allPieces ) );
      } else if ( legalMove is KingBitBoard ) {
        legalMoves.AddRange( KingMoveGen.KingBitBoardResults( cb, color, blackPieces, whitePieces, allPieces ) );
      } else if ( legalMove is KnightBitBoard ) {
        legalMoves.AddRange( KnightMoveGen.KnightBitBoardResults( cb, color, blackPieces, whitePieces, allPieces ) );
      } else if ( legalMove is QueenBitBoard ) {
        legalMoves.AddRange( QueenMoveGen.QueenBitBoardResults( cb, color, blackPieces, whitePieces, allPieces ) );
      } else if ( legalMove is RookBitBoard ) {
        legalMoves.AddRange( RookMoveGen.RookBitBoardResults( cb, color, blackPieces, whitePieces, allPieces ) );
      }

      foreach ( ColoredBitBoard cbb in legalMoves ) {
        if ( color == ChessPieceColors.White && ( cb.BlackKing.Bits & cbb.Bits ) != 0 ) {
          cb.Undo();
          return true;
        } else if ( color == ChessPieceColors.Black && ( cb.WhiteKing.Bits & cbb.Bits ) != 0 ) {
          cb.Undo();
          return true;
        }
      }

      cb.Undo();
      return false;
    }

    private static List<ColoredBitBoard> RemoveOwnKingChecks( ChessBoard inputChessBoard, List<ColoredBitBoard> inputPseudoLegalMoves, ChessPieceColors color ) {
      List<ColoredBitBoard> legalMovesResult = inputPseudoLegalMoves.Select( p => p ).ToList();

      for ( int i = 0; i < inputPseudoLegalMoves.Count; i++ ) {
        if ( color == ChessPieceColors.White ) {
          if ( isOwnKingAttackable( inputChessBoard, inputPseudoLegalMoves[i], ChessPieceColors.Black ) ) {
            legalMovesResult.RemoveAt( legalMovesResult.IndexOf( inputPseudoLegalMoves[i] ) );
          }
        } else {
          if ( isOwnKingAttackable( inputChessBoard, inputPseudoLegalMoves[i], ChessPieceColors.White ) ) {
            legalMovesResult.RemoveAt( legalMovesResult.IndexOf( inputPseudoLegalMoves[i] ) );
          }
        }
      }
      return legalMovesResult;
    }

    private static List<ColoredBitBoard> SetDoesCheck( ChessBoard inputChessBoard, List<ColoredBitBoard> inputLegalMoves, ChessPieceColors color ) {
      foreach ( ColoredBitBoard legalMove in inputLegalMoves ) {
        if ( color == ChessPieceColors.White ) {
          if ( isEnemyKingAttackable( inputChessBoard, legalMove, color ) ) {
            legalMove.DoesCheck = true;
          }
        } else {
          if ( isEnemyKingAttackable( inputChessBoard, legalMove, color ) ) {
            legalMove.DoesCheck = true;
          }
        }
      }
      return inputLegalMoves;
    }



#if DEBUG
    public static bool isOwnKingAttackable_TEST( ChessBoard cb, ColoredBitBoard legalMove, ChessPieceColors color ) {
      bool flag;
      flag = isOwnKingAttackable( cb, legalMove, color );
      return flag;
    }

    public static bool isEnemyKingAttackable_TEST( ChessBoard cb, ColoredBitBoard legalMove, ChessPieceColors color ) {
      return isEnemyKingAttackable( cb, legalMove, color );
    }

    public static List<ColoredBitBoard> RemoveOwnKingChecks_TEST( ChessBoard inputChessBoard, List<ColoredBitBoard> inputPseudoLegalMoves, ChessPieceColors color ) {
      return RemoveOwnKingChecks( inputChessBoard, inputPseudoLegalMoves, color );
    }

    public static List<ColoredBitBoard> SetDoesCheck_TEST( ChessBoard inputChessBoard, List<ColoredBitBoard> inputLegalMoves, ChessPieceColors color ) {
      return SetDoesCheck( inputChessBoard, inputLegalMoves, color );
    }
#endif

  }
}

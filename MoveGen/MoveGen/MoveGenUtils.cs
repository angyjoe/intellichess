using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  public static class MoveGenUtils {
    public const ulong FILE_A = (ulong)( BoardSquare.A1 | BoardSquare.A2 | BoardSquare.A3 | BoardSquare.A4 | BoardSquare.A5 | BoardSquare.A6 | BoardSquare.A7 | BoardSquare.A8 );
    public const ulong FILE_H = (ulong)( BoardSquare.H1 | BoardSquare.H2 | BoardSquare.H3 | BoardSquare.H4 | BoardSquare.H5 | BoardSquare.H6 | BoardSquare.H7 | BoardSquare.H8 );//72340172838076673;
    public const ulong FILE_B = (ulong)( BoardSquare.B1 | BoardSquare.B2 | BoardSquare.B3 | BoardSquare.B4 | BoardSquare.B5 | BoardSquare.B6 | BoardSquare.B7 | BoardSquare.B8 );
    public const ulong FILE_G = (ulong)( BoardSquare.G1 | BoardSquare.G2 | BoardSquare.G3 | BoardSquare.G4 | BoardSquare.G5 | BoardSquare.G6 | BoardSquare.G7 | BoardSquare.G8 );
    public const ulong RANK_1 = (ulong)( BoardSquare.A1 | BoardSquare.B1 | BoardSquare.C1 | BoardSquare.D1 | BoardSquare.E1 | BoardSquare.F1 | BoardSquare.G1 | BoardSquare.H1 );
    public const ulong RANK_2 = (ulong)( BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2 );
    public const ulong RANK_3 = (ulong)( BoardSquare.A3 | BoardSquare.B3 | BoardSquare.C3 | BoardSquare.D3 | BoardSquare.E3 | BoardSquare.F3 | BoardSquare.G3 | BoardSquare.H3 );
    public const ulong RANK_4 = (ulong)( BoardSquare.A4 | BoardSquare.B4 | BoardSquare.C4 | BoardSquare.D4 | BoardSquare.E4 | BoardSquare.F4 | BoardSquare.G4 | BoardSquare.H4 );
    public const ulong RANK_5 = (ulong)( BoardSquare.A5 | BoardSquare.B5 | BoardSquare.C5 | BoardSquare.D5 | BoardSquare.E5 | BoardSquare.F5 | BoardSquare.G5 | BoardSquare.H5 );
    public const ulong RANK_6 = (ulong)( BoardSquare.A6 | BoardSquare.B6 | BoardSquare.C6 | BoardSquare.D6 | BoardSquare.E6 | BoardSquare.F6 | BoardSquare.G6 | BoardSquare.H6 );
    public const ulong RANK_7 = (ulong)( BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7 );
    public const ulong RANK_8 = (ulong)( BoardSquare.A8 | BoardSquare.B8 | BoardSquare.C8 | BoardSquare.D8 | BoardSquare.E8 | BoardSquare.F8 | BoardSquare.G8 | BoardSquare.H8 );


    public static EmptyBitBoard SetWhiteBoard( ChessBoard cb ) {
      EmptyBitBoard result = new EmptyBitBoard();
      result.Bits = ( cb.WhiteBishop.Bits | cb.WhiteKing.Bits | cb.WhiteKnight.Bits | cb.WhitePawn.Bits | cb.WhiteQueen.Bits | cb.WhiteRook.Bits );
      return result;
    }

    public static EmptyBitBoard SetBlackBoard( ChessBoard cb ) {
      EmptyBitBoard result = new EmptyBitBoard();
      result.Bits = ( cb.BlackBishop.Bits | cb.BlackKing.Bits | cb.BlackKnight.Bits | cb.BlackPawn.Bits | cb.BlackQueen.Bits | cb.BlackRook.Bits );
      return result;
    }
    
    public static EmptyBitBoard SetWholeBoard( ChessBoard cb ) {
      EmptyBitBoard result = new EmptyBitBoard();
      result.Bits = ( cb.WhiteBishop.Bits | cb.WhiteKing.Bits | cb.WhiteKnight.Bits | cb.WhitePawn.Bits | cb.WhiteQueen.Bits | cb.WhiteRook.Bits | cb.BlackBishop.Bits | cb.BlackKing.Bits | cb.BlackKnight.Bits | cb.BlackPawn.Bits | cb.BlackQueen.Bits | cb.BlackRook.Bits );
      return result;
    }

    public static List<ColoredBitBoard> SortMoves( List<ColoredBitBoard> legalMoves, ChessBoard inputChessBoard, BitBoard blackMoves, BitBoard whiteMoves ) {
      legalMoves = SortMovesByCapturing( legalMoves, inputChessBoard, blackMoves, whiteMoves );
      legalMoves = SortMovesByPV( legalMoves, inputChessBoard, blackMoves, whiteMoves );

      return legalMoves;
    }

    public static List<ColoredBitBoard> SortMovesByPV( List<ColoredBitBoard> legalMoves, ChessBoard inputChessBoard, BitBoard blackMoves, BitBoard whiteMoves ) {
      TranspositionEntry entry = TranspositionTable.TranspositionCache[inputChessBoard.BoardHash.Key];
      if (entry == null || entry.BestMove == null)
      {
        return legalMoves;
      }
      ColoredBitBoard bestmove = entry.BestMove;
      if (bestmove is PawnBitBoard && ((PawnBitBoard)bestmove).Color == ChessPieceColors.White && legalMoves[0].Color == ChessPieceColors.White)
      {
        Debug.Assert(bestmove.Bits != inputChessBoard.WhitePawn.Bits);
      }
      ColoredBitBoard bestmoveInLegalMoves = legalMoves.Find(p => p.Equals(bestmove));
      if (bestmoveInLegalMoves != null)
      {
        legalMoves.Remove(bestmoveInLegalMoves);
        legalMoves.Insert(0, bestmove);
      }
      return legalMoves;
    }

    public static List<ColoredBitBoard> SortMovesByCapturing( List<ColoredBitBoard> legalMoves, ChessBoard inputChessBoard, BitBoard blackMoves, BitBoard whiteMoves ) {
      List<ColoredBitBoard> capturing = new List<ColoredBitBoard>();
      List<ColoredBitBoard> nonCapturing = new List<ColoredBitBoard>();
      List<ColoredBitBoard> result = new List<ColoredBitBoard>();

      //Sets the IsCapturing flag on all moves that is a capturing move
      SetIsCapturing( legalMoves, blackMoves, whiteMoves );

      foreach ( ColoredBitBoard cbb in legalMoves ) {
        if ( cbb.IsCapturing ) {
          capturing.Add( cbb );
        } else {
          nonCapturing.Add( cbb );
        }
      }
      result.AddRange( MoveGenUtils.SortCapturingMoves( capturing, inputChessBoard ) );
      result.AddRange( nonCapturing );
      return result;
    }

    public static List<ColoredBitBoard> SortCapturingMoves( List<ColoredBitBoard> legalMoves, ChessBoard inputChessBoard ) {
      switch ( inputChessBoard.Stage ) {
        case ChessBoardGameStage.Early:
          legalMoves.Sort( ( first, second ) => {
            Dictionary<Type, Func<int>> @switch = new Dictionary<Type, Func<int>>() {
              { typeof(KingBitBoard), () => { return int.MaxValue; } },
              { typeof(QueenBitBoard), () => { return 10; } }, 
              { typeof(RookBitBoard), () => { return 8; } }, 
              { typeof(KnightBitBoard), () => { return 5; } }, 
              { typeof(BishopBitBoard), () => { return 4; } }, 
              { typeof(PawnBitBoard), () => { return 3; } }, 
            };

            int valFirst, valSecond;

            if ( first.Bits == BoardSquare.Empty )
              valFirst = 0;
            else 
              valFirst = @switch[inputChessBoard.GetBitBoardFromSquare( first.Bits, first.Color == ChessPieceColors.White ? ChessPieceColors.Black : ChessPieceColors.White ).GetType()]();
                                      
            if ( second.Bits == BoardSquare.Empty )
              valSecond = 0;
            else
              valSecond = @switch[inputChessBoard.GetBitBoardFromSquare( second.Bits, second.Color == ChessPieceColors.White ? ChessPieceColors.Black : ChessPieceColors.White ).GetType()]();

            return valSecond - valFirst;
          } );
          break;
        case ChessBoardGameStage.Middle:
        case ChessBoardGameStage.Late:
          legalMoves.Sort( ( first, second ) => {
            Dictionary<Type, Func<int>> @switch = new Dictionary<Type, Func<int>>() {
              { typeof(KingBitBoard), () => { return int.MaxValue; } },  
              { typeof(QueenBitBoard), () => { return 10; } }, 
              { typeof(RookBitBoard), () => { return 8; } }, 
              { typeof(KnightBitBoard), () => { return 4; } }, 
              { typeof(BishopBitBoard), () => { return 5; } }, 
              { typeof(PawnBitBoard), () => { return 3; } }, 
            };

            int valFirst;
            if ( first.Bits == BoardSquare.Empty )
              valFirst = 0;
            else
              valFirst = @switch[inputChessBoard.GetBitBoardFromSquare( first.Bits, first.Color == ChessPieceColors.White ? ChessPieceColors.Black : ChessPieceColors.White ).GetType()]();

            int valSecond;
            if ( second.Bits == BoardSquare.Empty )
              valSecond = 0;
            else
              valSecond = @switch[inputChessBoard.GetBitBoardFromSquare( second.Bits, second.Color == ChessPieceColors.White ? ChessPieceColors.Black : ChessPieceColors.White ).GetType()]();

            return valSecond - valFirst;
          } );
          break;
      }

      return legalMoves;
    }
    public static List<ColoredBitBoard> SetIsCapturing( List<ColoredBitBoard> legalMoves, BitBoard blackPieces, BitBoard whitePieces ) {
      foreach ( ColoredBitBoard cbb in legalMoves ) {
        if ( ( cbb.Bits & ( cbb.Color == ChessPieceColors.White ? blackPieces.Bits : whitePieces.Bits ) ) != 0 ) {
          cbb.IsCapturing = true;
        }
      }
      return legalMoves;
    }
  }
}

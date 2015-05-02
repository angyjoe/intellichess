using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Author: Sari Haj Hussein
 */
namespace P5
{
  public static class KingMoveGen
  {
      //Static variables for evaluate function
      public static double NumberOfWhiteKingMoves { get { return _numberOfWhiteKingMoves; } }
      private static double _numberOfWhiteKingMoves;
      public static double NumberOfBlackKingMoves { get { return _numberOfBlackKingMoves; } }
      private static double _numberOfBlackKingMoves;
#if DEBUG
    public static KingBitBoard Test_ComputeWhiteKing(KingBitBoard inputKingBB, ChessBoard inputChessBoard, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      return ComputeWhiteKing(inputKingBB, inputChessBoard, blackPieces, whitePieces, allPieces);
    }
    
    public static KingBitBoard Test_ComputeBlackKing(KingBitBoard inputKingBB, ChessBoard inputChessBoard, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      return ComputeBlackKing(inputKingBB, inputChessBoard, blackPieces, whitePieces, allPieces);
    }

    public static List<KingBitBoard> Test_KingBitBoardResults(ChessBoard inputChessBoard, ChessPieceColors color, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      return KingBitBoardResults(inputChessBoard, color, blackPieces, whitePieces, allPieces);
    }
#endif
    private static KingBitBoard ComputeWhiteKing(KingBitBoard inputKingBB, ChessBoard inputChessBoard, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      KingBitBoard result = new KingBitBoard(ChessPieceColors.White);
      EmptyBitBoard kingClipFILE_H = new EmptyBitBoard();
      EmptyBitBoard kingClipFILE_A = new EmptyBitBoard();

      EmptyBitBoard spot1 = new EmptyBitBoard();
      EmptyBitBoard spot2 = new EmptyBitBoard();
      EmptyBitBoard spot3 = new EmptyBitBoard();
      EmptyBitBoard spot4 = new EmptyBitBoard();
      EmptyBitBoard spot5 = new EmptyBitBoard();
      EmptyBitBoard spot6 = new EmptyBitBoard();
      EmptyBitBoard spot7 = new EmptyBitBoard();
      EmptyBitBoard spot8 = new EmptyBitBoard();
      EmptyBitBoard castling = new EmptyBitBoard();

      kingClipFILE_A.Bits = (BoardSquare)((ulong)inputKingBB.Bits & ~MoveGenUtils.FILE_A);
      kingClipFILE_H.Bits = (BoardSquare)( (ulong)inputKingBB.Bits & ~MoveGenUtils.FILE_H );

      spot1.Bits = (BoardSquare)((ulong)kingClipFILE_A.Bits << 9);
      spot2.Bits = (BoardSquare)((ulong)inputKingBB.Bits << 8);
      spot3.Bits = (BoardSquare)((ulong)kingClipFILE_H.Bits << 7);
      spot4.Bits = (BoardSquare)((ulong)kingClipFILE_H.Bits >> 1);
      spot5.Bits = (BoardSquare)((ulong)kingClipFILE_H.Bits >> 9);
      spot6.Bits = (BoardSquare)((ulong)inputKingBB.Bits >> 8);
      spot7.Bits = (BoardSquare)((ulong)kingClipFILE_A.Bits >> 7);
      spot8.Bits = (BoardSquare)((ulong)kingClipFILE_A.Bits << 1);

      if ( !inputKingBB.HasMoved ) {
        if ( !inputChessBoard.WhiteRook.HasMovedKingSide && ((ulong)(BoardSquare.F1 | BoardSquare.G1) & (ulong)whitePieces.Bits) == 0){
          castling.Bits = (BoardSquare)((ulong)inputKingBB.Bits >> 2);
        }
        else if (!inputChessBoard.WhiteRook.HasMovedQueenSide && ((ulong)(BoardSquare.D1 | BoardSquare.C1 |BoardSquare.B1) & (ulong)whitePieces.Bits) == 0){
          castling.Bits = (BoardSquare)((ulong)inputKingBB.Bits << 2);
        }
      }

      result.Bits = spot1.Bits | spot2.Bits | spot3.Bits | spot4.Bits | spot5.Bits | spot6.Bits | spot7.Bits | spot8.Bits | castling.Bits;
      result.Bits = result.Bits & ~whitePieces.Bits;

      return result;
    }
    private static KingBitBoard ComputeBlackKing(KingBitBoard inputKingBB, ChessBoard inputChessBoard, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      KingBitBoard result = new KingBitBoard(ChessPieceColors.Black);
      EmptyBitBoard kingClipFILE_H = new EmptyBitBoard();
      EmptyBitBoard kingClipFILE_A = new EmptyBitBoard();

      EmptyBitBoard spot1 = new EmptyBitBoard();
      EmptyBitBoard spot2 = new EmptyBitBoard();
      EmptyBitBoard spot3 = new EmptyBitBoard();
      EmptyBitBoard spot4 = new EmptyBitBoard();
      EmptyBitBoard spot5 = new EmptyBitBoard();
      EmptyBitBoard spot6 = new EmptyBitBoard();
      EmptyBitBoard spot7 = new EmptyBitBoard();
      EmptyBitBoard spot8 = new EmptyBitBoard();
      EmptyBitBoard castling = new EmptyBitBoard();

      kingClipFILE_A.Bits = (BoardSquare)((ulong)inputKingBB.Bits & ~MoveGenUtils.FILE_A);
      kingClipFILE_H.Bits = (BoardSquare)((ulong)inputKingBB.Bits & ~MoveGenUtils.FILE_H);

      spot1.Bits = (BoardSquare)((ulong)kingClipFILE_A.Bits << 9);
      spot2.Bits = (BoardSquare)((ulong)inputKingBB.Bits    << 8);
      spot3.Bits = (BoardSquare)((ulong)kingClipFILE_H.Bits << 7);
      spot4.Bits = (BoardSquare)((ulong)kingClipFILE_H.Bits >> 1);
      spot5.Bits = (BoardSquare)((ulong)kingClipFILE_H.Bits >> 9);
      spot6.Bits = (BoardSquare)((ulong)inputKingBB.Bits    >> 8);
      spot7.Bits = (BoardSquare)((ulong)kingClipFILE_A.Bits >> 7);
      spot8.Bits = (BoardSquare)((ulong)kingClipFILE_A.Bits << 1);

      if ( !inputKingBB.HasMoved ) {
        if ( !inputChessBoard.BlackRook.HasMovedKingSide && ( (ulong)( BoardSquare.F8 | BoardSquare.G8 ) & (ulong)blackPieces.Bits ) == 0 ) {
          castling.Bits = (BoardSquare)( (ulong)inputKingBB.Bits >> 2 );
        } else if ( !inputChessBoard.BlackRook.HasMovedQueenSide && ( (ulong)( BoardSquare.D8 | BoardSquare.C8 | BoardSquare.B8 ) & (ulong)blackPieces.Bits ) == 0 ) {
          castling.Bits = (BoardSquare)( (ulong)inputKingBB.Bits << 2 );
        }
      }

      result.Bits = spot1.Bits | spot2.Bits | spot3.Bits | spot4.Bits | spot5.Bits | spot6.Bits | spot7.Bits | spot8.Bits | castling.Bits;
      result.Bits = result.Bits & ~blackPieces.Bits;

      return result;
    }
    public static List<KingBitBoard> KingBitBoardResults(ChessBoard inputChessBoard, ChessPieceColors color, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      List<KingBitBoard> result = new List<KingBitBoard>();
      KingBitBoard legalWhiteKingMoves = new KingBitBoard(ChessPieceColors.White);
      KingBitBoard legalBlackKingMoves = new KingBitBoard(ChessPieceColors.Black);
      List<KingBitBoard> sepKingsOutput = new List<KingBitBoard>();

      if (color == ChessPieceColors.White)
      {
        legalWhiteKingMoves = ComputeWhiteKing(inputChessBoard.WhiteKing, inputChessBoard, blackPieces, whitePieces, allPieces);
        sepKingsOutput = ColoredBitBoard.SplitBitBoard(legalWhiteKingMoves).ToList();
        foreach (KingBitBoard sepKBB in sepKingsOutput)
        {
          result.Add(sepKBB);
        }
        _numberOfWhiteKingMoves = result.Count;
      }
      else
      {
        legalBlackKingMoves = ComputeBlackKing(inputChessBoard.BlackKing, inputChessBoard, blackPieces, whitePieces, allPieces);
        sepKingsOutput = ColoredBitBoard.SplitBitBoard(legalBlackKingMoves).ToList();
        foreach (KingBitBoard sepKBB in sepKingsOutput)
        {
          result.Add(sepKBB);
        }
        _numberOfBlackKingMoves = result.Count;
      }

      return result;
    }
  }
}

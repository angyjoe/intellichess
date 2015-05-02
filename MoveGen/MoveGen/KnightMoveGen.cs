using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Author: Sari Haj Hussein
 */
namespace P5
{
  public static class KnightMoveGen
  {
#if DEBUG
    public static KnightBitBoard ComputeWhiteKnight_Test(ChessBoard inputChessBoard, BitBoard inputKnightBB, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      return ComputeWhiteKnight(inputChessBoard, inputKnightBB, blackPieces, whitePieces, allPieces);
    }
    public static KnightBitBoard ComputeBlackKnight_Test(ChessBoard inputChessBoard, BitBoard inputKnightBB, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      return ComputeBlackKnight(inputChessBoard, inputKnightBB, blackPieces, whitePieces, allPieces);
    }
#endif
    private static KnightBitBoard ComputeWhiteKnight(ChessBoard inputChessBoard, BitBoard inputKnightBB, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      KnightBitBoard result = new KnightBitBoard(ChessPieceColors.White);
      EmptyBitBoard spot1_clip = new EmptyBitBoard();
      EmptyBitBoard spot2_clip = new EmptyBitBoard();
      EmptyBitBoard spot3_clip = new EmptyBitBoard();
      EmptyBitBoard spot4_clip = new EmptyBitBoard();
      EmptyBitBoard spot5_clip = new EmptyBitBoard();
      EmptyBitBoard spot6_clip = new EmptyBitBoard();
      EmptyBitBoard spot7_clip = new EmptyBitBoard();
      EmptyBitBoard spot8_clip = new EmptyBitBoard();


      EmptyBitBoard spot1 = new EmptyBitBoard();
      EmptyBitBoard spot2 = new EmptyBitBoard();
      EmptyBitBoard spot3 = new EmptyBitBoard();
      EmptyBitBoard spot4 = new EmptyBitBoard();
      EmptyBitBoard spot5 = new EmptyBitBoard();
      EmptyBitBoard spot6 = new EmptyBitBoard();
      EmptyBitBoard spot7 = new EmptyBitBoard();
      EmptyBitBoard spot8 = new EmptyBitBoard();

      spot1_clip.Bits = (BoardSquare)((ulong)inputKnightBB.Bits & ~MoveGenUtils.FILE_A);
      spot2_clip.Bits = (BoardSquare)((ulong)inputKnightBB.Bits & ~MoveGenUtils.FILE_H);
      spot3_clip.Bits = (BoardSquare)((ulong)inputKnightBB.Bits & ~MoveGenUtils.FILE_G & ~MoveGenUtils.FILE_H);
      spot4_clip.Bits = (BoardSquare)((ulong)inputKnightBB.Bits & ~MoveGenUtils.FILE_G & ~MoveGenUtils.FILE_H);
      spot5_clip.Bits = (BoardSquare)((ulong)inputKnightBB.Bits & ~MoveGenUtils.FILE_H);
      spot6_clip.Bits = (BoardSquare)((ulong)inputKnightBB.Bits & ~MoveGenUtils.FILE_A);
      spot7_clip.Bits = (BoardSquare)((ulong)inputKnightBB.Bits & ~MoveGenUtils.FILE_A & ~MoveGenUtils.FILE_B);
      spot8_clip.Bits = (BoardSquare)((ulong)inputKnightBB.Bits & ~MoveGenUtils.FILE_A & ~MoveGenUtils.FILE_B);

      spot1.Bits = (BoardSquare)((ulong)spot1_clip.Bits << 17);
      spot2.Bits = (BoardSquare)((ulong)spot2_clip.Bits << 15);
      spot3.Bits = (BoardSquare)((ulong)spot3_clip.Bits << 6);
      spot4.Bits = (BoardSquare)((ulong)spot4_clip.Bits >> 10);
      spot5.Bits = (BoardSquare)((ulong)spot5_clip.Bits >> 17);
      spot6.Bits = (BoardSquare)((ulong)spot6_clip.Bits >> 15);
      spot7.Bits = (BoardSquare)((ulong)spot7_clip.Bits >> 6);
      spot8.Bits = (BoardSquare)((ulong)spot8_clip.Bits << 10);

      result.Bits = spot1.Bits | spot2.Bits | spot3.Bits | spot4.Bits | spot5.Bits | spot6.Bits | spot7.Bits | spot8.Bits;
      result.Bits = result.Bits & ~whitePieces.Bits;

      return result;
    }
    private static KnightBitBoard ComputeBlackKnight(ChessBoard inputChessBoard, BitBoard inputKnightBB, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      KnightBitBoard result = new KnightBitBoard(ChessPieceColors.Black);
      EmptyBitBoard spot1_clip = new EmptyBitBoard();
      EmptyBitBoard spot2_clip = new EmptyBitBoard();
      EmptyBitBoard spot3_clip = new EmptyBitBoard();
      EmptyBitBoard spot4_clip = new EmptyBitBoard();
      EmptyBitBoard spot5_clip = new EmptyBitBoard();
      EmptyBitBoard spot6_clip = new EmptyBitBoard();
      EmptyBitBoard spot7_clip = new EmptyBitBoard();
      EmptyBitBoard spot8_clip = new EmptyBitBoard();


      EmptyBitBoard spot1 = new EmptyBitBoard();
      EmptyBitBoard spot2 = new EmptyBitBoard();
      EmptyBitBoard spot3 = new EmptyBitBoard();
      EmptyBitBoard spot4 = new EmptyBitBoard();
      EmptyBitBoard spot5 = new EmptyBitBoard();
      EmptyBitBoard spot6 = new EmptyBitBoard();
      EmptyBitBoard spot7 = new EmptyBitBoard();
      EmptyBitBoard spot8 = new EmptyBitBoard();

      spot1_clip.Bits = (BoardSquare)((ulong)inputKnightBB.Bits & ~MoveGenUtils.FILE_A);
      spot2_clip.Bits = (BoardSquare)((ulong)inputKnightBB.Bits & ~MoveGenUtils.FILE_H);
      spot3_clip.Bits = (BoardSquare)((ulong)inputKnightBB.Bits & ~MoveGenUtils.FILE_G & ~MoveGenUtils.FILE_H);
      spot4_clip.Bits = (BoardSquare)((ulong)inputKnightBB.Bits & ~MoveGenUtils.FILE_G & ~MoveGenUtils.FILE_H);
      spot5_clip.Bits = (BoardSquare)((ulong)inputKnightBB.Bits & ~MoveGenUtils.FILE_H);
      spot6_clip.Bits = (BoardSquare)((ulong)inputKnightBB.Bits & ~MoveGenUtils.FILE_A);
      spot7_clip.Bits = (BoardSquare)((ulong)inputKnightBB.Bits & ~MoveGenUtils.FILE_A & ~MoveGenUtils.FILE_B);
      spot8_clip.Bits = (BoardSquare)((ulong)inputKnightBB.Bits & ~MoveGenUtils.FILE_A & ~MoveGenUtils.FILE_B);

      spot1.Bits = (BoardSquare)((ulong)spot1_clip.Bits << 17);
      spot2.Bits = (BoardSquare)((ulong)spot2_clip.Bits << 15);
      spot3.Bits = (BoardSquare)((ulong)spot3_clip.Bits << 6);
      spot4.Bits = (BoardSquare)((ulong)spot4_clip.Bits >> 10);
      spot5.Bits = (BoardSquare)((ulong)spot5_clip.Bits >> 17);
      spot6.Bits = (BoardSquare)((ulong)spot6_clip.Bits >> 15);
      spot7.Bits = (BoardSquare)((ulong)spot7_clip.Bits >> 6);
      spot8.Bits = (BoardSquare)((ulong)spot8_clip.Bits << 10);

      result.Bits = spot1.Bits | spot2.Bits | spot3.Bits | spot4.Bits | spot5.Bits | spot6.Bits | spot7.Bits | spot8.Bits;
      result.Bits = result.Bits & ~blackPieces.Bits;

      return result;
    }
    public static List<KnightBitBoard> KnightBitBoardResults(ChessBoard inputChessBoard, ChessPieceColors color, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      List<KnightBitBoard> result = new List<KnightBitBoard>();
      List<Tuple<KnightBitBoard, KnightBitBoard>> legalKnightMoves = new List<Tuple<KnightBitBoard, KnightBitBoard>>();
      List<KnightBitBoard> sepKnightsInput = new List<KnightBitBoard>();
      List<KnightBitBoard> sepKnightsOutput = new List<KnightBitBoard>();

      if (color == ChessPieceColors.White)
      {
        sepKnightsInput = ColoredBitBoard.SplitBitBoard(inputChessBoard.WhiteKnight).ToList();
        foreach (KnightBitBoard sepKnightBB in sepKnightsInput)
        {
          legalKnightMoves.Add(new Tuple<KnightBitBoard,KnightBitBoard>(sepKnightBB, ComputeWhiteKnight(inputChessBoard, sepKnightBB, blackPieces, whitePieces, allPieces)));
        }
        for (int i = 0; i < legalKnightMoves.Count; i++)
        {
          sepKnightsOutput = ColoredBitBoard.SplitBitBoard(legalKnightMoves[i].Item2).ToList();
          foreach (KnightBitBoard sepKnightOutBB in sepKnightsOutput)
          {
            KnightBitBoard boardResult = new KnightBitBoard(color);
            boardResult.Bits = (inputChessBoard.WhiteKnight.Bits ^ legalKnightMoves[i].Item1.Bits) | sepKnightOutBB.Bits;
            result.Add(boardResult);
          }
        }
      }
      else
      {
        sepKnightsInput = ColoredBitBoard.SplitBitBoard(inputChessBoard.BlackKnight).ToList();
        foreach (KnightBitBoard sepKnightBB in sepKnightsInput)
        {
          legalKnightMoves.Add(new Tuple<KnightBitBoard, KnightBitBoard>(sepKnightBB, ComputeBlackKnight(inputChessBoard, sepKnightBB, blackPieces, whitePieces, allPieces)));
        }
        for (int i = 0; i < legalKnightMoves.Count; i++)
        {
          sepKnightsOutput = ColoredBitBoard.SplitBitBoard(legalKnightMoves[i].Item2).ToList();
          foreach (KnightBitBoard sepKnightOutBB in sepKnightsOutput)
          {
            KnightBitBoard boardResult = new KnightBitBoard(color);
            boardResult.Bits = (inputChessBoard.BlackKnight.Bits ^ legalKnightMoves[i].Item1.Bits) | sepKnightOutBB.Bits;
            result.Add(boardResult);
          }
        }
      }
      return result;
    }
  }
}

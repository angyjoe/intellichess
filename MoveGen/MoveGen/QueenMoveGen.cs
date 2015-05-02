using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Author: Sari Haj Hussein
 */
namespace P5
{
  public static class QueenMoveGen
  {
#if DEBUG
    public static QueenBitBoard ComputeWhiteQueen_Test(QueenBitBoard inputQueenBB, BitBoard whitePieces, BitBoard allPieces)
    {
      return ComputeWhiteQueen(inputQueenBB, whitePieces, allPieces);
    }
    public static QueenBitBoard ComputeBlackQueen_Test(QueenBitBoard inputQueenBB, BitBoard blackPieces, BitBoard allPieces)
    {
      return ComputeBlackQueen(inputQueenBB, blackPieces, allPieces);
    }
#endif
    private static QueenBitBoard ComputeWhiteQueen(QueenBitBoard inputQueenBB, BitBoard whitePieces, BitBoard allPieces)
    {
      QueenBitBoard result = new QueenBitBoard(ChessPieceColors.White);
      result.Bits = Magic.getMagicMoves(inputQueenBB.Bits, allPieces.Bits, true);
      result.Bits |= Magic.getMagicMoves(inputQueenBB.Bits, allPieces.Bits, false);
      result.Bits &= ~whitePieces.Bits;
      return result;
    }
    private static QueenBitBoard ComputeBlackQueen(QueenBitBoard inputQueenBB, BitBoard blackPieces, BitBoard allPieces)
    {
      QueenBitBoard result = new QueenBitBoard(ChessPieceColors.Black);
      result.Bits = Magic.getMagicMoves(inputQueenBB.Bits, allPieces.Bits, true);
      result.Bits |= Magic.getMagicMoves(inputQueenBB.Bits, allPieces.Bits, false);
      result.Bits &= ~blackPieces.Bits;
      return result;
    }
    public static List<QueenBitBoard> QueenBitBoardResults(ChessBoard inputChessBoard, ChessPieceColors color, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      List<QueenBitBoard> result = new List<QueenBitBoard>();
      List<Tuple<QueenBitBoard, QueenBitBoard>> legalQueenMoves = new List<Tuple<QueenBitBoard, QueenBitBoard>>();
      List<QueenBitBoard> sepQueensInput = new List<QueenBitBoard>();
      List<QueenBitBoard> sepQueensOutput = new List<QueenBitBoard>();

      if (color == ChessPieceColors.White)
      {
        sepQueensInput = ColoredBitBoard.SplitBitBoard(inputChessBoard.WhiteQueen).ToList();
        foreach (QueenBitBoard sepQueenBB in sepQueensInput)
        {
          legalQueenMoves.Add(new Tuple<QueenBitBoard, QueenBitBoard>(sepQueenBB, ComputeWhiteQueen(sepQueenBB, whitePieces, allPieces)));
        }
        for (int i = 0; i < legalQueenMoves.Count; i++)
        {
          sepQueensOutput = ColoredBitBoard.SplitBitBoard(legalQueenMoves[i].Item2).ToList();
          foreach (QueenBitBoard sepQueenOutBB in sepQueensOutput)
          {
            QueenBitBoard boardResult = new QueenBitBoard(color);
            boardResult.Bits = (inputChessBoard.WhiteQueen.Bits ^ legalQueenMoves[i].Item1.Bits) | sepQueenOutBB.Bits;
            result.Add(boardResult);
          }
        }
      }
      else
      {
        sepQueensInput = ColoredBitBoard.SplitBitBoard(inputChessBoard.BlackQueen).ToList();
        foreach (QueenBitBoard sepQueenBB in sepQueensInput)
        {
          legalQueenMoves.Add(new Tuple<QueenBitBoard, QueenBitBoard>(sepQueenBB, ComputeBlackQueen(sepQueenBB, blackPieces, allPieces)));
        }
        for (int i = 0; i < legalQueenMoves.Count; i++)
        {
          sepQueensOutput = ColoredBitBoard.SplitBitBoard(legalQueenMoves[i].Item2).ToList();
          foreach (QueenBitBoard sepQueenOutBB in sepQueensOutput)
          {
            QueenBitBoard boardResult = new QueenBitBoard(color);
            boardResult.Bits = (inputChessBoard.BlackQueen.Bits ^ legalQueenMoves[i].Item1.Bits) | sepQueenOutBB.Bits;
            result.Add(boardResult);
          }
        }
      }
      return result;
    }
  }
}

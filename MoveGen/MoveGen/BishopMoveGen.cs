using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Author: Sari Haj Hussein
 */
namespace P5
{
  public static class BishopMoveGen
  {
#if DEBUG
    public static BishopBitBoard ComputeWhiteBishop_Test(BishopBitBoard inputBishopBB, ChessBoard inputChessBoard, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      return ComputeWhiteBishop(inputBishopBB, whitePieces, allPieces);
    }
    public static BishopBitBoard ComputeBlackBishop_Test(BishopBitBoard inputBishopBB, ChessBoard inputChessBoard, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      return ComputeBlackBishop(inputBishopBB, blackPieces, allPieces);
    }
#endif
    private static BishopBitBoard ComputeWhiteBishop(BishopBitBoard inputBishopBB, BitBoard whitePieces, BitBoard allPieces)
    {
      BishopBitBoard result = new BishopBitBoard(ChessPieceColors.White);
      result.Bits = Magic.getMagicMoves(inputBishopBB.Bits, allPieces.Bits, false);
      result.Bits &= ~whitePieces.Bits;
      return result;
    }
    private static BishopBitBoard ComputeBlackBishop(BishopBitBoard inputBishopBB, BitBoard blackPieces, BitBoard allPieces)
    {
      BishopBitBoard result = new BishopBitBoard(ChessPieceColors.Black);
      result.Bits = Magic.getMagicMoves(inputBishopBB.Bits, allPieces.Bits, false);
      result.Bits &= ~blackPieces.Bits;
      return result;
    }
    public static List<BishopBitBoard> BishopBitBoardResults(ChessBoard inputChessBoard, ChessPieceColors color, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {

      List<BishopBitBoard> result = new List<BishopBitBoard>();
      List<Tuple<BishopBitBoard, BishopBitBoard>> legalBishopMoves = new List<Tuple<BishopBitBoard, BishopBitBoard>>();
      List<BishopBitBoard> sepBishopsInput = new List<BishopBitBoard>();
      List<BishopBitBoard> sepBishopsOutput = new List<BishopBitBoard>();

      if (color == ChessPieceColors.White)
      {
        sepBishopsInput = ColoredBitBoard.SplitBitBoard(inputChessBoard.WhiteBishop).ToList();
        foreach (BishopBitBoard sepBishopBB in sepBishopsInput)
        {
          legalBishopMoves.Add(new Tuple<BishopBitBoard, BishopBitBoard>(sepBishopBB, ComputeWhiteBishop(sepBishopBB, whitePieces, allPieces)));
        }
        for (int i = 0; i < legalBishopMoves.Count; i++)
        {
          sepBishopsOutput = ColoredBitBoard.SplitBitBoard(legalBishopMoves[i].Item2).ToList();
          foreach (BishopBitBoard sepBishopOutBB in sepBishopsOutput)
          {
            BishopBitBoard boardResult = new BishopBitBoard(color);
            boardResult.Bits = (inputChessBoard.WhiteBishop.Bits ^ legalBishopMoves[i].Item1.Bits) | sepBishopOutBB.Bits;
            result.Add(boardResult);
          }
        }
      }
      else
      {
        sepBishopsInput = ColoredBitBoard.SplitBitBoard(inputChessBoard.BlackBishop).ToList();
        foreach (BishopBitBoard sepBishopBB in sepBishopsInput)
        {
          legalBishopMoves.Add(new Tuple<BishopBitBoard, BishopBitBoard>(sepBishopBB, ComputeBlackBishop(sepBishopBB, blackPieces, allPieces)));
        }
        for (int i = 0; i < legalBishopMoves.Count; i++)
        {
          sepBishopsOutput = ColoredBitBoard.SplitBitBoard(legalBishopMoves[i].Item2).ToList();
          foreach (BishopBitBoard sepBishopOutBB in sepBishopsOutput)
          {
            BishopBitBoard boardResult = new BishopBitBoard(color);
            boardResult.Bits = (inputChessBoard.BlackBishop.Bits ^ legalBishopMoves[i].Item1.Bits) | sepBishopOutBB.Bits;
            result.Add(boardResult);
          }
        }
      }
      return result;
    }
  }
}

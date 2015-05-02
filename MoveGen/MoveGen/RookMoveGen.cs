using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Author: Sari Haj Hussein
 */
namespace P5
{
  public static class RookMoveGen
  {
#if DEBUG
    public static RookBitBoard ComputeWhiteRook_Test(RookBitBoard inputRookBB, ChessBoard inputChessBoard, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      return ComputeWhiteRook(inputRookBB, whitePieces, allPieces);
    }
    public static RookBitBoard ComputeBlackRook_Test(RookBitBoard inputRookBB, ChessBoard inputChessBoard, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      return ComputeBlackRook(inputRookBB, blackPieces, allPieces);
    }
#endif
    private static RookBitBoard ComputeWhiteRook(RookBitBoard inputRookBB, BitBoard whitePieces, BitBoard allPieces)
    {
      RookBitBoard result = new RookBitBoard(ChessPieceColors.White);
      result.Bits = Magic.getMagicMoves(inputRookBB.Bits, allPieces.Bits, true);
      result.Bits &= ~whitePieces.Bits;
      return result;
    }
    private static RookBitBoard ComputeBlackRook(RookBitBoard inputRookBB, BitBoard blackPieces, BitBoard allPieces)
    {
      RookBitBoard result = new RookBitBoard(ChessPieceColors.Black);
      result.Bits = Magic.getMagicMoves(inputRookBB.Bits, allPieces.Bits, true);
      result.Bits &= ~blackPieces.Bits;
      return result;
    }
    public static List<RookBitBoard> RookBitBoardResults(ChessBoard inputChessBoard, ChessPieceColors color, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      List<RookBitBoard> result = new List<RookBitBoard>();
      List<Tuple<RookBitBoard, RookBitBoard>> legalRookMoves = new List<Tuple<RookBitBoard, RookBitBoard>>();
      List<RookBitBoard> sepRooksInput = new List<RookBitBoard>();
      List<RookBitBoard> sepRooksOutput = new List<RookBitBoard>();

      if (color == ChessPieceColors.White)
      {
        sepRooksInput = ColoredBitBoard.SplitBitBoard(inputChessBoard.WhiteRook).ToList();
        foreach (RookBitBoard sepRookBB in sepRooksInput)
        {
          legalRookMoves.Add(new Tuple<RookBitBoard, RookBitBoard>(sepRookBB, ComputeWhiteRook(sepRookBB, whitePieces, allPieces)));
        }
        for (int i = 0; i < legalRookMoves.Count; i++)
        {
          sepRooksOutput = ColoredBitBoard.SplitBitBoard(legalRookMoves[i].Item2).ToList();
          foreach (RookBitBoard sepRookOutBB in sepRooksOutput)
          {
            RookBitBoard boardResult = new RookBitBoard(color);
            boardResult.Bits = (inputChessBoard.WhiteRook.Bits ^ legalRookMoves[i].Item1.Bits) | sepRookOutBB.Bits;
            result.Add(boardResult);
          }
        }
      }
      else
      {
        sepRooksInput = ColoredBitBoard.SplitBitBoard(inputChessBoard.BlackRook).ToList();
        foreach (RookBitBoard sepRookBB in sepRooksInput)
        {
          legalRookMoves.Add(new Tuple<RookBitBoard, RookBitBoard>(sepRookBB, ComputeBlackRook(sepRookBB, blackPieces, allPieces)));
        }
        for (int i = 0; i < legalRookMoves.Count; i++)
        {
          sepRooksOutput = ColoredBitBoard.SplitBitBoard(legalRookMoves[i].Item2).ToList();
          foreach (RookBitBoard sepRookOutBB in sepRooksOutput)
          {
            RookBitBoard boardResult = new RookBitBoard(color);
            boardResult.Bits = (inputChessBoard.BlackRook.Bits ^ legalRookMoves[i].Item1.Bits) | sepRookOutBB.Bits;
            result.Add(boardResult);
          }
        }
      }
      return result;
    }
  }
}

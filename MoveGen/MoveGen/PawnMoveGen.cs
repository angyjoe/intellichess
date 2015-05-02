using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
/*
 * Author: Sari Haj Hussein
 */
namespace P5
{
  public static class PawnMoveGen
  {
    #region tests
#if DEBUG

    public static PawnBitBoard Test_ComputeBlackPawn(PawnBitBoard pawnsBB, ChessBoard inputChessBoard, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      return ComputeBlackPawn(pawnsBB, inputChessBoard, blackPieces, whitePieces, allPieces);
    }

    public static PawnBitBoard Test_ComputeWhitePawn(PawnBitBoard pawnsBB, ChessBoard inputChessBoard, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      return ComputeWhitePawn(pawnsBB, inputChessBoard, blackPieces, whitePieces, allPieces);
    }
#endif
    #endregion

    private static PawnBitBoard ComputeBlackPawn(PawnBitBoard inputPawnBB, ChessBoard inputChessBoard, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      PawnBitBoard result = new PawnBitBoard(ChessPieceColors.Black);

      //moves     
      PawnBitBoard pawnOneStep = new PawnBitBoard(ChessPieceColors.Black);
      pawnOneStep.Bits = (BoardSquare)((ulong)inputPawnBB.Bits >> 8) & ~allPieces.Bits;
      PawnBitBoard pawnTwoSteps = new PawnBitBoard(ChessPieceColors.Black);

      if (((ulong)inputPawnBB.Bits 
        & MoveGenUtils.RANK_7) != 0)
      {
        pawnTwoSteps.Bits = (BoardSquare)((ulong)pawnOneStep.Bits >> 8) & ~allPieces.Bits;
      }


      //attacks
      PawnBitBoard enPassantBB = new PawnBitBoard(ChessPieceColors.Black);
      PawnBitBoard pawnLeftAttack = new PawnBitBoard(ChessPieceColors.Black);
      PawnBitBoard pawnRightAttack = new PawnBitBoard(ChessPieceColors.Black);

      EmptyBitBoard pawnClipFILE_A = new EmptyBitBoard();
      EmptyBitBoard pawnClipFILE_H = new EmptyBitBoard();

      pawnClipFILE_A.Bits = (BoardSquare)( (ulong)inputPawnBB.Bits & ~MoveGenUtils.FILE_A );
      pawnClipFILE_H.Bits = (BoardSquare)( (ulong)inputPawnBB.Bits & ~MoveGenUtils.FILE_H );

      pawnLeftAttack.Bits = (BoardSquare)( ((ulong)pawnClipFILE_A.Bits >> 7) & (ulong)whitePieces.Bits );
      pawnRightAttack.Bits = (BoardSquare)( ((ulong)pawnClipFILE_H.Bits >> 9) & (ulong)whitePieces.Bits );

      if (inputChessBoard.WhitePawn.MovedTwoSquares != BoardSquare.Empty)
      {
        BoardSquare enemyPawn = new BoardSquare();
        enemyPawn = inputChessBoard.WhitePawn.MovedTwoSquares;

        if ((((ulong)inputPawnBB.Bits << 1) == (ulong)enemyPawn) && (((ulong)enemyPawn & MoveGenUtils.FILE_H) == 0))
        {
          enPassantBB.Bits = (BoardSquare)((ulong)inputPawnBB.Bits >> 7);
        }
        if ((((ulong)inputPawnBB.Bits >> 1) == (ulong)enemyPawn) && (((ulong)enemyPawn & MoveGenUtils.FILE_A) == 0))
        {
          enPassantBB.Bits = (BoardSquare)((ulong)inputPawnBB.Bits >> 9);
        }
      }

      result.Bits = pawnOneStep.Bits | pawnTwoSteps.Bits | pawnLeftAttack.Bits | pawnRightAttack.Bits | enPassantBB.Bits;
      return result;
    }

    private static PawnBitBoard ComputeWhitePawn(PawnBitBoard inputPawnBB, ChessBoard inputChessBoard, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      PawnBitBoard result = new PawnBitBoard(ChessPieceColors.White);
    
      //moves     
      PawnBitBoard pawnOneStep = new PawnBitBoard(ChessPieceColors.White);
      pawnOneStep.Bits = (BoardSquare)((ulong)inputPawnBB.Bits << 8) & ~allPieces.Bits;
      PawnBitBoard pawnTwoSteps = new PawnBitBoard(ChessPieceColors.White);

      if (((ulong)inputPawnBB.Bits & MoveGenUtils.RANK_2) != 0)
      {
        pawnTwoSteps.Bits = (BoardSquare)((ulong)pawnOneStep.Bits << 8) & ~allPieces.Bits;
      }    


      //attacks
      PawnBitBoard enPassantBB = new PawnBitBoard(ChessPieceColors.White);
      PawnBitBoard pawnLeftAttack = new PawnBitBoard(ChessPieceColors.White);
      PawnBitBoard pawnRightAttack = new PawnBitBoard(ChessPieceColors.White);

      EmptyBitBoard pawnClipFILE_A = new EmptyBitBoard();
      EmptyBitBoard pawnClipFILE_H = new EmptyBitBoard();

      pawnClipFILE_A.Bits = (BoardSquare)( (ulong)inputPawnBB.Bits & ~MoveGenUtils.FILE_A );
      pawnClipFILE_H.Bits = (BoardSquare)( (ulong)inputPawnBB.Bits & ~MoveGenUtils.FILE_H );

      pawnLeftAttack.Bits = (BoardSquare)( ( (ulong)pawnClipFILE_A.Bits << 9) & (ulong)blackPieces.Bits );
      pawnRightAttack.Bits = (BoardSquare)( ( (ulong)pawnClipFILE_H.Bits << 7 ) & (ulong)blackPieces.Bits );


      //enPassantBB = PawnBitBoard.EnPassant();
      if (inputChessBoard.BlackPawn.MovedTwoSquares != BoardSquare.Empty)
      {
        BoardSquare enemyPawn = new BoardSquare();
        enemyPawn = inputChessBoard.BlackPawn.MovedTwoSquares;

        if ((((ulong)inputPawnBB.Bits << 1) == (ulong)enemyPawn) && (((ulong)enemyPawn & MoveGenUtils.FILE_H) == 0))
        {
          enPassantBB.Bits = (BoardSquare)((ulong)inputPawnBB.Bits << 9);
        }
        if ((((ulong)inputPawnBB.Bits >> 1) == (ulong)enemyPawn) && (((ulong)enemyPawn & MoveGenUtils.FILE_A) == 0))
        {
          enPassantBB.Bits = (BoardSquare)((ulong)inputPawnBB.Bits << 7);
        }
      }
      result.Bits = pawnOneStep.Bits | pawnTwoSteps.Bits | pawnLeftAttack.Bits | pawnRightAttack.Bits | enPassantBB.Bits;
      return result;
    }

    public static List<PawnBitBoard> PawnBitBoardResults(ChessBoard inputChessBoard, ChessPieceColors color, BitBoard blackPieces, BitBoard whitePieces, BitBoard allPieces)
    {
      List<PawnBitBoard> result = new List<PawnBitBoard>();
      List<Tuple<PawnBitBoard, PawnBitBoard>> legalPawnMoves = new List<Tuple<PawnBitBoard, PawnBitBoard>>();
      List<PawnBitBoard> sepPawnsInput = new List<PawnBitBoard>();
      List<PawnBitBoard> sepPawnsOutput = new List<PawnBitBoard>();
      if (color == ChessPieceColors.White)
      {
        sepPawnsInput = ColoredBitBoard.SplitBitBoard( inputChessBoard.WhitePawn ).ToList();
        foreach (PawnBitBoard spb in sepPawnsInput)
        {
         
          var newMove = new Tuple<PawnBitBoard, PawnBitBoard>( spb, ComputeWhitePawn( spb, inputChessBoard, blackPieces, whitePieces, allPieces ) );
          
          legalPawnMoves.Add(newMove); 

        }
        for (int i = 0; i < legalPawnMoves.Count; i++)
        {
          sepPawnsOutput = ColoredBitBoard.SplitBitBoard(legalPawnMoves[i].Item2).ToList();
          foreach (PawnBitBoard sepPBB in sepPawnsOutput)
          {
            if ( ( sepPBB.Bits & (BoardSquare)MoveGenUtils.RANK_8 ) != BoardSquare.Empty ) {
              PawnBitBoard boardResult_B = new PawnBitBoard( color );
              PawnBitBoard boardResult_R = new PawnBitBoard( color );
              PawnBitBoard boardResult_N = new PawnBitBoard( color );
              PawnBitBoard boardResult_Q = new PawnBitBoard( color );
              boardResult_B.Bits = sepPBB.Bits;
              boardResult_R.Bits = sepPBB.Bits;
              boardResult_N.Bits = sepPBB.Bits;
              boardResult_Q.Bits = sepPBB.Bits;
              boardResult_B.Promote( PawnBitBoard.PromotionPiece.Bishop );
              boardResult_R.Promote( PawnBitBoard.PromotionPiece.Rook );
              boardResult_N.Promote( PawnBitBoard.PromotionPiece.Knight );
              boardResult_Q.Promote( PawnBitBoard.PromotionPiece.Queen );
              boardResult_B.Bits = ( inputChessBoard.WhitePawn.Bits ^ legalPawnMoves[i].Item1.Bits ) | sepPBB.Bits;
              boardResult_R.Bits = ( inputChessBoard.WhitePawn.Bits ^ legalPawnMoves[i].Item1.Bits ) | sepPBB.Bits;
              boardResult_N.Bits = ( inputChessBoard.WhitePawn.Bits ^ legalPawnMoves[i].Item1.Bits ) | sepPBB.Bits;
              boardResult_Q.Bits = ( inputChessBoard.WhitePawn.Bits ^ legalPawnMoves[i].Item1.Bits ) | sepPBB.Bits;
              result.Add( boardResult_B );
              result.Add( boardResult_R );
              result.Add( boardResult_N );
              result.Add( boardResult_Q );
            } else {
              PawnBitBoard boardResult = new PawnBitBoard( color );
              boardResult.Bits = ( inputChessBoard.WhitePawn.Bits ^ legalPawnMoves[i].Item1.Bits ) | sepPBB.Bits;

              var oldBoard = inputChessBoard.GetOldBitBoardFromBitBoard( inputChessBoard.WhitePawn );
              BoardSquare bitsDiff = (BoardSquare)( (ulong)oldBoard.Bits ^ (ulong)boardResult.Bits );
              System.Diagnostics.Debug.Assert( new EmptyBitBoard( (BoardSquare)( (ulong)oldBoard.Bits ^ (ulong)boardResult.Bits ) ).Count == 2, "More than one piece moved" );

              result.Add( boardResult );
            }
          }
        }
      }
      else
      {
        sepPawnsInput = ColoredBitBoard.SplitBitBoard(inputChessBoard.BlackPawn).ToList();
        foreach (PawnBitBoard spb in sepPawnsInput)
        {
          legalPawnMoves.Add(new Tuple<PawnBitBoard, PawnBitBoard>(spb, ComputeBlackPawn(spb, inputChessBoard, blackPieces, whitePieces, allPieces)));
        }
        for (int i = 0; i < legalPawnMoves.Count; i++)
        {
          sepPawnsOutput = ColoredBitBoard.SplitBitBoard(legalPawnMoves[i].Item2).ToList();
          foreach (PawnBitBoard sepPBB in sepPawnsOutput)
          {
            if ( ( sepPBB.Bits & (BoardSquare)MoveGenUtils.RANK_1 ) != BoardSquare.Empty ) {
              PawnBitBoard boardResult_B = new PawnBitBoard( color );
              PawnBitBoard boardResult_R = new PawnBitBoard( color );
              PawnBitBoard boardResult_N = new PawnBitBoard( color );
              PawnBitBoard boardResult_Q = new PawnBitBoard( color );
              boardResult_B.Bits = sepPBB.Bits;
              boardResult_R.Bits = sepPBB.Bits;
              boardResult_N.Bits = sepPBB.Bits;
              boardResult_Q.Bits = sepPBB.Bits;
              boardResult_B.Promote( PawnBitBoard.PromotionPiece.Bishop );
              boardResult_R.Promote( PawnBitBoard.PromotionPiece.Rook );
              boardResult_N.Promote( PawnBitBoard.PromotionPiece.Knight );
              boardResult_Q.Promote( PawnBitBoard.PromotionPiece.Queen );
              boardResult_B.Bits = ( inputChessBoard.BlackPawn.Bits ^ legalPawnMoves[i].Item1.Bits ) | sepPBB.Bits;
              boardResult_R.Bits = ( inputChessBoard.BlackPawn.Bits ^ legalPawnMoves[i].Item1.Bits ) | sepPBB.Bits;
              boardResult_N.Bits = ( inputChessBoard.BlackPawn.Bits ^ legalPawnMoves[i].Item1.Bits ) | sepPBB.Bits;
              boardResult_Q.Bits = ( inputChessBoard.BlackPawn.Bits ^ legalPawnMoves[i].Item1.Bits ) | sepPBB.Bits;
              result.Add( boardResult_B );
              result.Add( boardResult_R );
              result.Add( boardResult_N );
              result.Add( boardResult_Q );
            } else {
              PawnBitBoard boardResult = new PawnBitBoard( color );
              boardResult.Bits = ( inputChessBoard.BlackPawn.Bits ^ legalPawnMoves[i].Item1.Bits ) | sepPBB.Bits;
              result.Add( boardResult );
            }
          }
        }
      }     
      return result;
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  public class StringBitboardConverter {
    private ChessBoard _chessBoard;
    private ChessPieceColors _engineColor;

    public StringBitboardConverter( ChessBoard chessBoard, ChessPieceColors engineColor ) {
      _chessBoard = chessBoard;
      _engineColor = engineColor;
    }

    public string ConvertBitBoardMoveToString( ColoredBitBoard bitBoard ) {
      ColoredBitBoard toBitBoard = bitBoard.DeepCopy();
      ColoredBitBoard fromBitBoard = _chessBoard.GetOldBitBoardFromBitBoard( toBitBoard );
      System.Diagnostics.Debug.Assert( bitBoard.Bits != fromBitBoard.Bits );
      
      EmptyBitBoard containerBitBoard = new EmptyBitBoard();
      containerBitBoard.Bits = fromBitBoard.Bits;
      fromBitBoard.Bits = ( fromBitBoard.Bits | toBitBoard.Bits ) ^ toBitBoard.Bits;
      toBitBoard.Bits = ( fromBitBoard.Bits | toBitBoard.Bits ) ^ containerBitBoard.Bits;

      string stringMove = fromBitBoard.Bits.ToString().ToLower() + toBitBoard.Bits.ToString().ToLower();

      if ( fromBitBoard is PawnBitBoard ) {
        PawnBitBoard toPawnBitBoardPromotion = (PawnBitBoard)toBitBoard;
        if ( toPawnBitBoardPromotion.IsPromoted ) {
          switch ( toPawnBitBoardPromotion.Promotion ) {

            case PawnBitBoard.PromotionPiece.Queen:
              return stringMove + "q";
            case PawnBitBoard.PromotionPiece.Rook:
              return stringMove + "r";
            case PawnBitBoard.PromotionPiece.Bishop:
              return stringMove + "b";
            case PawnBitBoard.PromotionPiece.Knight:
              return stringMove + "n";
            default:
              throw new ArgumentOutOfRangeException( "A promoted pawn bit board must contain a promotion type." );
          }
        }
      }
      return stringMove;
    }

    public ColoredBitBoard ConvertStringMoveToBitBoard( string stringMove ) {
      string stringFromCoordinate = string.Format( "{0}{1}", stringMove[0], stringMove[1] );
      string stringToCoordinate = string.Format( "{0}{1}", stringMove[2], stringMove[3] );
      string stringPromotion = "None";

      BoardSquare fromCoordinate = (BoardSquare)Enum.Parse( typeof( BoardSquare ), stringFromCoordinate, true );
      BoardSquare toCoordinate = (BoardSquare)Enum.Parse( typeof( BoardSquare ), stringToCoordinate, true );

      if ( _chessBoard.GetBitBoardFromSquare( fromCoordinate, _engineColor ) == null ) {
        if ( _engineColor == ChessPieceColors.White ) {
          _engineColor = ChessPieceColors.Black;
        } else {
          _engineColor = ChessPieceColors.White;
        }
      }

      ColoredBitBoard moveBitBoard = _chessBoard.GetBitBoardFromSquare( fromCoordinate, _engineColor );
      if ( moveBitBoard != null ) {
        moveBitBoard.Bits ^= fromCoordinate;
        moveBitBoard.Bits |= toCoordinate;
      }

      if ( stringMove.Length == 5 ) {
        stringPromotion = stringMove[4].ToString().ToLower();
        PawnBitBoard movePromotionPawnBitBoard = (PawnBitBoard)moveBitBoard;

        switch ( stringPromotion ) {
          case "q":
            movePromotionPawnBitBoard.Promote( PawnBitBoard.PromotionPiece.Queen );
            break;
          case "r":
            movePromotionPawnBitBoard.Promote( PawnBitBoard.PromotionPiece.Rook );
            break;
          case "b":
            movePromotionPawnBitBoard.Promote( PawnBitBoard.PromotionPiece.Bishop );
            break;
          case "n":
            movePromotionPawnBitBoard.Promote( PawnBitBoard.PromotionPiece.Knight );
            break;
          default:
            throw new ArgumentOutOfRangeException( "Move string 'promotion char' can only be q, r, b or n" );
        }

        return movePromotionPawnBitBoard;
      } else if ( stringMove.Length > 5 ) {
        throw new ArgumentOutOfRangeException( "Move sting cannot be over five chars. It should be in the form 'e7e5' or 'e7e8q'." );
      }
      return moveBitBoard;
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  public class UT_CECP {
    [Fact]
    public void ConvertBitBoardMoveToString_IsCorrectStringReturned_Equals() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.WhiteKnight.Bits = BoardSquare.B8 | BoardSquare.G8;
      ColoredBitBoard toCoordinate = new KnightBitBoard( ChessPieceColors.White );
      toCoordinate.Bits = BoardSquare.C6 | BoardSquare.G8;

      string correctMoveString = "b8c6";

      StringBitboardConverter converter = new StringBitboardConverter( chessBoard, ChessPieceColors.White );

      Assert.Equal( correctMoveString, converter.ConvertBitBoardMoveToString( toCoordinate ) );
    }

    [Fact]
    public void ConvertBitBoardMoveToString_IsCorrectPromotionQueenStringReturned_Equals() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.WhitePawn.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E7 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      PawnBitBoard toCoordinate = new PawnBitBoard( ChessPieceColors.White );
      toCoordinate.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E8 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      toCoordinate.Promote( PawnBitBoard.PromotionPiece.Queen );

      string correctMoveString = "e7e8q";

      StringBitboardConverter converter = new StringBitboardConverter( chessBoard, ChessPieceColors.White );

      Assert.Equal( correctMoveString, converter.ConvertBitBoardMoveToString( toCoordinate ) );
    }

    [Fact]
    public void ConvertBitBoardMoveToString_IsCorrectPromotionRookStringReturned_Equals() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.WhitePawn.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E7 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      PawnBitBoard toCoordinate = new PawnBitBoard( ChessPieceColors.White );
      toCoordinate.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E8 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      toCoordinate.Promote( PawnBitBoard.PromotionPiece.Rook );

      string correctMoveString = "e7e8r";

      StringBitboardConverter converter = new StringBitboardConverter( chessBoard, ChessPieceColors.White );

      Assert.Equal( correctMoveString, converter.ConvertBitBoardMoveToString( toCoordinate ) );
    }

    [Fact]
    public void ConvertBitBoardMoveToString_IsCorrectPromotionBishopStringReturned_Equals() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.WhitePawn.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E7 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      PawnBitBoard toCoordinate = new PawnBitBoard( ChessPieceColors.White );
      toCoordinate.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E8 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      toCoordinate.Promote( PawnBitBoard.PromotionPiece.Bishop );

      string correctMoveString = "e7e8b";

      StringBitboardConverter converter = new StringBitboardConverter( chessBoard, ChessPieceColors.White );

      Assert.Equal( correctMoveString, converter.ConvertBitBoardMoveToString( toCoordinate ) );
    }

    [Fact]
    public void ConvertBitBoardMoveToString_IsCorrectPromotionKnightStringReturned_Equals() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.WhitePawn.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E7 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      PawnBitBoard toCoordinate = new PawnBitBoard( ChessPieceColors.White );
      toCoordinate.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E8 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      toCoordinate.Promote( PawnBitBoard.PromotionPiece.Knight );

      string correctMoveString = "e7e8n";

      StringBitboardConverter converter = new StringBitboardConverter( chessBoard, ChessPieceColors.White );

      Assert.Equal( correctMoveString, converter.ConvertBitBoardMoveToString( toCoordinate ) );
    }

    [Fact]
    public void ConvertStringMoveToBitBoard_IsCorrectBitBoardReturned_Equals() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.BlackKnight.Bits = BoardSquare.B8 | BoardSquare.G8;
      string stringMove = "b8c6";

      StringBitboardConverter converter = new StringBitboardConverter( chessBoard, ChessPieceColors.Black );

      ColoredBitBoard currentMoveBitBoard = converter.ConvertStringMoveToBitBoard( stringMove );

      ColoredBitBoard correctMoveBitBoard = new KnightBitBoard( ChessPieceColors.Black );
      correctMoveBitBoard.Bits = BoardSquare.C6 | BoardSquare.G8;

      Assert.Equal( currentMoveBitBoard.GetType(), correctMoveBitBoard.GetType() );
      Assert.Equal( currentMoveBitBoard.Bits, correctMoveBitBoard.Bits );
    }

    [Fact]
    public void ConvertStringMoveToBitBoard_IsNullReturned_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      StringBitboardConverter converter = new StringBitboardConverter( chessBoard, ChessPieceColors.White );
      string stringMove = "e4e5";
      ColoredBitBoard correctMoveBitBoard = converter.ConvertStringMoveToBitBoard( stringMove );

      Assert.Equal( null,  correctMoveBitBoard);
    }

    [Fact]
    public void ConvertStringMoveToBitBoard_IsCorrectPromotionQueenBitBoardReturned_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.WhitePawn.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E7 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      string stringMove = "e7e8q";

      StringBitboardConverter converter = new StringBitboardConverter( chessBoard, ChessPieceColors.White );

      PawnBitBoard currentMoveBitBoard = (PawnBitBoard)converter.ConvertStringMoveToBitBoard( stringMove );

      PawnBitBoard correctMoveBitBoard = new PawnBitBoard( ChessPieceColors.Black );
      correctMoveBitBoard.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E8 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      correctMoveBitBoard.Promote( PawnBitBoard.PromotionPiece.Queen );

      Assert.Equal( correctMoveBitBoard.GetType(), currentMoveBitBoard.GetType() );
      Assert.Equal( correctMoveBitBoard.Bits, currentMoveBitBoard.Bits );
      Assert.Equal( correctMoveBitBoard.Promotion, currentMoveBitBoard.Promotion );
    }

    [Fact]
    public void ConvertStringMoveToBitBoard_IsCorrectPromotionRookBitBoardReturned_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.WhitePawn.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E7 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      string stringMove = "e7e8r";

      StringBitboardConverter converter = new StringBitboardConverter( chessBoard, ChessPieceColors.White );

      PawnBitBoard currentMoveBitBoard = (PawnBitBoard)converter.ConvertStringMoveToBitBoard( stringMove );

      PawnBitBoard correctMoveBitBoard = new PawnBitBoard( ChessPieceColors.White );
      correctMoveBitBoard.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E8 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      correctMoveBitBoard.Promote( PawnBitBoard.PromotionPiece.Rook );

      Assert.Equal( correctMoveBitBoard.GetType(), currentMoveBitBoard.GetType() );
      Assert.Equal( correctMoveBitBoard.Bits, currentMoveBitBoard.Bits );
      Assert.Equal( correctMoveBitBoard.Promotion, currentMoveBitBoard.Promotion );
    }

    [Fact]
    public void ConvertStringMoveToBitBoard_IsCorrectPromotionBishopBitBoardReturned_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.WhitePawn.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E7 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      string stringMove = "e7e8b";

      StringBitboardConverter converter = new StringBitboardConverter( chessBoard, ChessPieceColors.White );

      PawnBitBoard currentMoveBitBoard = (PawnBitBoard)converter.ConvertStringMoveToBitBoard( stringMove );

      PawnBitBoard correctMoveBitBoard = new PawnBitBoard( ChessPieceColors.White );
      correctMoveBitBoard.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E8 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      correctMoveBitBoard.Promote( PawnBitBoard.PromotionPiece.Bishop );

      Assert.Equal( correctMoveBitBoard.GetType(), currentMoveBitBoard.GetType() );
      Assert.Equal( correctMoveBitBoard.Bits, currentMoveBitBoard.Bits );
      Assert.Equal( correctMoveBitBoard.Promotion, currentMoveBitBoard.Promotion );
    }

    [Fact]
    public void ConvertStringMoveToBitBoard_IsCorrectPromotionKnigtBitBoardReturned_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.WhitePawn.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E7 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      string stringMove = "e7e8n";

      StringBitboardConverter converter = new StringBitboardConverter( chessBoard, ChessPieceColors.White );

      PawnBitBoard currentMoveBitBoard = (PawnBitBoard)converter.ConvertStringMoveToBitBoard( stringMove );

      PawnBitBoard correctMoveBitBoard = new PawnBitBoard( ChessPieceColors.White );
      correctMoveBitBoard.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E8 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      correctMoveBitBoard.Promote( PawnBitBoard.PromotionPiece.Knight );

      Assert.Equal( correctMoveBitBoard.GetType(), currentMoveBitBoard.GetType() );
      Assert.Equal( correctMoveBitBoard.Bits, currentMoveBitBoard.Bits );
      Assert.Equal( correctMoveBitBoard.Promotion, currentMoveBitBoard.Promotion );
    }
  }
}

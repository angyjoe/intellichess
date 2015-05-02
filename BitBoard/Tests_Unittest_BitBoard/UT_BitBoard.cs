using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using P5;
/*
 * Author: Sari Haj Hussein
 */
namespace P5_Tests {
  public class UT_BitBoard {
    [Fact]
    public void PositionIndexFromBoardSquare_Test() {
      List<BoardSquare> testList = new List<BoardSquare>
        {
             BoardSquare.C1, BoardSquare.B1, BoardSquare.A1, BoardSquare.B4, BoardSquare.B8, BoardSquare.A8
        };
      List<int> testValuesList = new List<int>
        {
            //-1 fordi vi vores array har et index som er zero based
            6, 7, 8, 31, 63, 64 
        };
      Assert.Equal( testValuesList, BitBoard.PositionIndexFromBoardSquare( testList ) );
    }
    [Fact]
    public void Equals_BitBoardNotEqual_True() {
      KingBitBoard king = new KingBitBoard( ChessPieceColors.White );
      king.Bits = BoardSquare.E5;
      PawnBitBoard pawn = new PawnBitBoard( ChessPieceColors.White );
      pawn.Bits = BoardSquare.E5;

      Assert.False( king.Equals( pawn ) );
    }

    [Fact]
    public void Equals_BitBoardEqual_True() {
      PawnBitBoard pawn1 = new PawnBitBoard( ChessPieceColors.White );
      pawn1.Bits = BoardSquare.E5;
      PawnBitBoard pawn = new PawnBitBoard( ChessPieceColors.White );
      pawn.Bits = BoardSquare.E5;

      Assert.True( pawn1.Equals( pawn ) );
    }

    [Fact]
    public void Equals_ColoredBitBoardEqual_True() {
      PawnBitBoard pawn1 = new PawnBitBoard( ChessPieceColors.White );
      pawn1.Bits = BoardSquare.E5;
      PawnBitBoard pawn = new PawnBitBoard( ChessPieceColors.White );
      pawn.Bits = BoardSquare.E5;

      Assert.True( pawn1.Equals( pawn ) );
    }

    [Fact]
    public void Equals_ColoredBitBoardNotEqual_True() {
      PawnBitBoard pawn1 = new PawnBitBoard( ChessPieceColors.White );
      pawn1.Bits = BoardSquare.E5;
      PawnBitBoard pawn = new PawnBitBoard( ChessPieceColors.Black );
      pawn.Bits = BoardSquare.E5;

      Assert.False( pawn1.Equals( pawn ) );
    }

    [Fact]
    public void Count_BoundaryMin_Equal() {
      EmptyBitBoard bb = new EmptyBitBoard();
      bb.Bits = 0x0; //Empty bitboard
      Assert.Equal( 0, bb.Count ); //Boundary min case
    }

    [Fact]
    public void Count_BoundaryMax_Equal() {
      EmptyBitBoard bb = new EmptyBitBoard();
      bb.Bits = BoardSquare.A8;
      Assert.Equal( 1, bb.Count ); //Boundary max case
    }

    [Fact]
    public void Count_UseCase_Equal() {
      EmptyBitBoard bb = new EmptyBitBoard();
      bb.Bits = BoardSquare.H1 | BoardSquare.A8;
      Assert.Equal( 2, bb.Count ); //General case
    }
       
    [Fact]
    public void Clear_IsBitBoardCleared_Equal() {
      EmptyBitBoard board = new EmptyBitBoard();
      board.Bits = BoardSquare.Full;
      board.Clear();
      BoardSquare correctBitBoard = BoardSquare.Empty;

      Assert.Equal( correctBitBoard, board.Bits );
    }

    [Fact]
    public void MinimumRaisedBit_IsCorrectBitBoardReturned_Equal() {
      EmptyBitBoard board = new EmptyBitBoard();
      board.Bits = BoardSquare.A8 | BoardSquare.B7 | BoardSquare.C6 | BoardSquare.F3;
      BoardSquare correctBitBoard = BoardSquare.F3;

      Assert.Equal( correctBitBoard, board.MinimumRaisedBit );
    }

    [Fact]
    public void MaximumRaisedBit_IsCorrectBitBoardReturned_Equal() {
      EmptyBitBoard board = new EmptyBitBoard();
      board.Bits = BoardSquare.C2 | BoardSquare.C1;
      BoardSquare correctBitBoard = BoardSquare.C2;

      Assert.Equal( correctBitBoard, board.MaximumRaisedBit );
    }

    [Fact]
    public void MinimumRaisedBit_IsMaximumBitBoardReturned_Equal() {
      EmptyBitBoard board = new EmptyBitBoard();
      board.Bits = BoardSquare.A8;
      BoardSquare correctBitBoard = BoardSquare.A8;

      Assert.Equal( correctBitBoard, board.MinimumRaisedBit );
    }

    [Fact]
    public void MinimumRaisedBit_IsMinimumBitBoardReturned_Equal() {
      EmptyBitBoard board = new EmptyBitBoard();
      board.Bits = BoardSquare.H1;
      BoardSquare correctBitBoard = BoardSquare.H1;

      Assert.Equal( correctBitBoard, board.MinimumRaisedBit );
    }

    [Fact]
    public void MaximumRaisedBit_IsMaximumBitBoardReturned_Equal() {
      EmptyBitBoard board = new EmptyBitBoard();
      board.Bits = BoardSquare.A8;
      BoardSquare correctBitBoard = BoardSquare.A8;

      Assert.Equal( correctBitBoard, board.MaximumRaisedBit );
    }

    [Fact]
    public void MaximumRaisedBit_IsMinimumBitBoardReturned_Equal() {
      EmptyBitBoard board = new EmptyBitBoard();
      board.Bits = BoardSquare.H1;
      BoardSquare correctBitBoard = BoardSquare.H1;

      Assert.Equal( correctBitBoard, board.MaximumRaisedBit );
    }

    [Fact]
    public void MinimumRaisedBit_IsEmptyBitBoardReturned_Equal() {
      EmptyBitBoard board = new EmptyBitBoard();
      board.Bits = BoardSquare.Empty;
      BoardSquare correctBitBoard = BoardSquare.Empty;

      Assert.Equal( correctBitBoard, board.MinimumRaisedBit );
    }

    [Fact]
    public void MaximumRaisedBit_IsEmptyBitBoardReturned_Equal() {
      EmptyBitBoard board = new EmptyBitBoard();
      board.Bits = BoardSquare.Empty;
      BoardSquare correctBitBoard = BoardSquare.Empty;

      Assert.Equal( correctBitBoard, board.MaximumRaisedBit );
    }
  }
}

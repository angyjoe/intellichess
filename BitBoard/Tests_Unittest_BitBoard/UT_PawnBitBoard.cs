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
  public class UT_PawnBitBoard {
    [Fact]
    public void Initialize_IsWhiteCorrectPlaced_Equal() {
      ulong correctPlacement = 0x000000000000FF00;
      PawnBitBoard board = new PawnBitBoard( ChessPieceColors.White );
      board.Initialize( null );

      Assert.Equal( correctPlacement, (ulong)board.Bits );
    }

    [Fact]
    public void Initialize_IsBlackCorrectPlaced_Equal() {
      ulong correctPlacement = 0x00FF000000000000;
      PawnBitBoard board = new PawnBitBoard( ChessPieceColors.Black );
      board.Initialize( null );

      Assert.Equal( correctPlacement, (ulong)board.Bits );
    }

    [Fact]
    public void Bits_BlackMovedTwoSquaresTwoOnSameFile_Equal() {
      BoardSquare correct = BoardSquare.H5;
      PawnBitBoard board = new PawnBitBoard( ChessPieceColors.Black );
      board.Bits = BoardSquare.A7 | BoardSquare.B7 | BoardSquare.B5 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

      /* "Perform" move, triggers the Bits property on pawnbitboard that sets the MovedTwoSquares */
      board.Bits = BoardSquare.A7 | BoardSquare.B7 | BoardSquare.B5 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H5;

      Assert.Equal( correct, board.MovedTwoSquares );
    }
   
    [Fact]
    public void Bits_WhiteMovedTwoSquaresTwoOnSameFile_Equal() {
      BoardSquare correct = BoardSquare.H4;
      PawnBitBoard board = new PawnBitBoard( ChessPieceColors.White );
      board.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.B4 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;

      /* "Perform" move, triggers the Bits property on pawnbitboard that sets the MovedTwoSquares */
      board.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.B4 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H4;

      Assert.Equal( correct, board.MovedTwoSquares );
    }

    [Fact]
    public void Bits_WhiteMovedTwoSquaresOneMove_Equal() {
      BoardSquare correct = BoardSquare.H4;
      PawnBitBoard board = new PawnBitBoard( ChessPieceColors.White );
      board.Initialize( null );

      /* "Perform" move, triggers the Bits property on pawnbitboard that sets the MovedTwoSquares */
      board.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H4;

      Assert.Equal( correct, board.MovedTwoSquares );
    }

    [Fact]
    public void Bits_BlackMovedTwoSquaresOneMove_Equal() {
      BoardSquare correct = BoardSquare.H5;
      PawnBitBoard board = new PawnBitBoard( ChessPieceColors.Black );
      board.Initialize( null );

      /* "Perform" move, triggers the Bits property on pawnbitboard that sets the MovedTwoSquares */
      board.Bits = BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H5;

      Assert.Equal( correct, board.MovedTwoSquares );
    }
  }
}

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
  public class UT_RookBitBoard {
    [Fact]
    public void Initialize_IsWhiteCorrectPlaced_Equal() {
      BoardSquare correctPlacement = BoardSquare.A1 | BoardSquare.H1;
      RookBitBoard board = new RookBitBoard( ChessPieceColors.White );
      board.Initialize( null );

      Assert.Equal( correctPlacement, board.Bits );
    }

    [Fact]
    public void Initialize_IsBlackCorrectPlaced_Equal() {
      BoardSquare correctPlacement = BoardSquare.A8 |BoardSquare.H8;
      RookBitBoard board = new RookBitBoard( ChessPieceColors.Black );
      board.Initialize( null );

      Assert.Equal( correctPlacement, board.Bits );
    }

    [Fact]
    public void HasMoved_IsWhiteRookQueenSideMoved_False() {
      RookBitBoard board = new RookBitBoard( ChessPieceColors.White );
      board.Initialize( null );

      Assert.False( board.HasMovedQueenSide );
    }

    [Fact]
    public void HasMoved_IsWhiteRookKingSideMoved_False() {
      RookBitBoard board = new RookBitBoard( ChessPieceColors.White );
      board.Initialize( null );

      Assert.False( board.HasMovedKingSide );
    }

    [Fact]
    public void HasMoved_IsWhiteRookQueenSideMoved_True() {
      RookBitBoard board = new RookBitBoard( ChessPieceColors.White );
      board.Initialize( null );
      board.Bits = BoardSquare.A2 | BoardSquare.H2;

      Assert.True( board.HasMovedQueenSide );
    }

    [Fact]
    public void HasMoved_IsWhiteRookKingSideMoved_True() {
      RookBitBoard board = new RookBitBoard( ChessPieceColors.White );
      board.Initialize( null );
      board.Bits = BoardSquare.A2 | BoardSquare.H2;

      Assert.True( board.HasMovedKingSide );
    }

    [Fact]
    public void HasMoved_IsBlackRookQueenSideMoved_False() {
      RookBitBoard board = new RookBitBoard( ChessPieceColors.Black );
      board.Initialize( null );

      Assert.False( board.HasMovedQueenSide );
    }

    [Fact]
    public void HasMoved_IsBlackRookKingSideMoved_False() {
      RookBitBoard board = new RookBitBoard( ChessPieceColors.Black );
      board.Initialize( null );

      Assert.False( board.HasMovedKingSide );
    }

    [Fact]
    public void HasMoved_IsBlackRookQueenSideMoved_True() {
      RookBitBoard board = new RookBitBoard( ChessPieceColors.Black );
      board.Initialize( null );
      board.Bits = BoardSquare.A7 | BoardSquare.H7;

      Assert.True( board.HasMovedQueenSide );
    }

    [Fact]
    public void HasMoved_IsBlackRookKingSideMoved_True() {
      RookBitBoard board = new RookBitBoard( ChessPieceColors.Black );
      board.Initialize( null );
      board.Bits = BoardSquare.A7 | BoardSquare.H7;

      Assert.True( board.HasMovedKingSide );
    }
  }
}

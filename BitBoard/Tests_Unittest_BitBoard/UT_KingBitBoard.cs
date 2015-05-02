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
  public class UT_KingBitBoard {
    [Fact]
    public void HasMoved_IsWhiteKingMoved_True() {
      KingBitBoard Board = new KingBitBoard( ChessPieceColors.White );
      Board.Initialize( null);
      Board.Bits = BoardSquare.F1;

      Assert.True( Board.HasMoved );
    }

    [Fact]
    public void HasMoved_IsBlackKingMoved_True() {
      KingBitBoard Board = new KingBitBoard( ChessPieceColors.Black );
      Board.Initialize( null );
      Board.Bits = BoardSquare.F8;

      Assert.True( Board.HasMoved );
    }

    [Fact]
    public void HasMoved_IsWhiteKingMoved_False() {
      KingBitBoard Board = new KingBitBoard( ChessPieceColors.White );
      Board.Initialize( null );

      Assert.False( Board.HasMoved );   
    }

    [Fact]
    public void HasMoved_IsBlackKingMoved_False() {
      KingBitBoard Board = new KingBitBoard( ChessPieceColors.Black );
      Board.Initialize( null );

      Assert.False( Board.HasMoved );
    }

    [Fact]
    public void Initialize_IsWhiteCorrectPlaced_Equal() {
      ulong correctPlacement = 0x0000000000000008;
      KingBitBoard board = new KingBitBoard( ChessPieceColors.White );
      board.Initialize( null );

      Assert.Equal( correctPlacement, (ulong)board.Bits );
    }

    [Fact]
    public void Initialize_IsBlackCorrectPlaced_Equal() {
      ulong correctPlacement = 0x0800000000000000;
      KingBitBoard board = new KingBitBoard( ChessPieceColors.Black );
      board.Initialize( null );

      Assert.Equal( correctPlacement, (ulong)board.Bits );
    }
  }
}

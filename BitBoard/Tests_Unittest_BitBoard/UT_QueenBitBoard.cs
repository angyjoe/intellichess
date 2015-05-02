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
  public class UT_QueenBitBoard {
    [Fact]
    public void Initialize_IsWhiteCorrectPlaced_Equal() {
      BoardSquare correctPlacement = BoardSquare.D1;
      QueenBitBoard board = new QueenBitBoard( ChessPieceColors.White );
      board.Initialize( null );

      Assert.Equal( correctPlacement, board.Bits );
    }

    [Fact]
    public void Initialize_IsBlackCorrectPlaced_Equal() {
      BoardSquare correctPlacement = BoardSquare.D8;
      QueenBitBoard board = new QueenBitBoard( ChessPieceColors.Black );
      board.Initialize( null );

      Assert.Equal( correctPlacement, board.Bits );
    }
  }
}

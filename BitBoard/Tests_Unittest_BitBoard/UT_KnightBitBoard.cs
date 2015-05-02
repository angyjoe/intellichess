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
  public class UT_KnightBitBoard {
    [Fact]
    public void Initialize_IsWhiteCorrectPlaced_Equal() {
      ulong correctPlacement = 0x0000000000000042;
      KnightBitBoard board = new KnightBitBoard( ChessPieceColors.White );
      board.Initialize( null );

      Assert.Equal( correctPlacement, (ulong)board.Bits );
    }

    [Fact]
    public void Initialize_IsBlackCorrectPlaced_Equal() {
      ulong correctPlacement = 0x4200000000000000;
      KnightBitBoard board = new KnightBitBoard( ChessPieceColors.Black );
      board.Initialize( null );

      Assert.Equal( correctPlacement, (ulong)board.Bits );
    }
  }
}

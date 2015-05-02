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
  public class UT_BishopBitBoard {
    [Fact]
    public void Initialize_IsWhiteCorrectPlaced_Equal() {
      ulong correctPlacement = 0x0000000000000024;
      BishopBitBoard board = new BishopBitBoard( ChessPieceColors.White );
      board.Initialize( null );

      Assert.Equal( correctPlacement, (ulong)board.Bits );
    }

    [Fact]
    public void Initialize_IsBlackCorrectPlaced_Equal() {
      ulong correctPlacement = 0x2400000000000000;
      BishopBitBoard board = new BishopBitBoard( ChessPieceColors.Black );
      board.Initialize( null );
      
      Assert.Equal( correctPlacement, (ulong)board.Bits );
    }
  }
}

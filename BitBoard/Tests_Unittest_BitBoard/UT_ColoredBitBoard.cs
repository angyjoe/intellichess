using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using P5;
using Xunit;
/*
 * Author: Sari Haj Hussein
 */
namespace P5_Tests {
  public class UT_ColoredBitBoard {
    [Fact]
    public void SplitBitboard_IfInputNull_Exception() {
      PawnBitBoard bb = null;
      Assert.Throws<BitBoard.BitboardException>( (Assert.ThrowsDelegate)( () => ColoredBitBoard.SplitBitBoard( bb ) ) );
    }

    [Fact]
    public void SplitBitboard_IfCountZero_Equal() {
      PawnBitBoard bb = new PawnBitBoard( ChessPieceColors.Black);
      bb.Bits = 0x0;

      PawnBitBoard[] bbArr = new PawnBitBoard[0];
      Assert.Equal( bbArr, ColoredBitBoard.SplitBitBoard( bb ) );
    }

    [Fact]
    public void SplitBitboard_UseCase_Equal() {
      PawnBitBoard bb = new PawnBitBoard( ChessPieceColors.Black );
      bb.Bits = BoardSquare.H1 | BoardSquare.G1 | BoardSquare.E1;
      PawnBitBoard bb_1 = new PawnBitBoard( ChessPieceColors.Black );
      bb_1.Bits = BoardSquare.H1;
      PawnBitBoard bb_2 = new PawnBitBoard( ChessPieceColors.Black );
      bb_2.Bits = BoardSquare.G1;
      PawnBitBoard bb_4 = new PawnBitBoard( ChessPieceColors.Black );
      bb_4.Bits = BoardSquare.E1;

      IEnumerable<BitBoard> splits = ColoredBitBoard.SplitBitBoard( bb );

      Assert.Equal( 3, splits.Count() );
      Assert.Contains( bb_1, splits, new BitBoardEqualityComparer() );
      Assert.Contains( bb_2, splits, new BitBoardEqualityComparer() );
      Assert.Contains( bb_4, splits, new BitBoardEqualityComparer() );
    }


    public class BitBoardEqualityComparer : IEqualityComparer<BitBoard> {
      public bool Equals( BitBoard x, BitBoard y ) {
        return x.Bits == y.Bits;
      }

      public int GetHashCode( BitBoard obj ) {
        throw new NotImplementedException();
      }
    }
  }
}

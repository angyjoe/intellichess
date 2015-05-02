using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Author: Sari Haj Hussein
 */
namespace BitBoard {
  public abstract class BitBoard {
    public UInt64 Bits { set; get; }
    public int Count { get { throw new NotImplementedException(); } }

    public virtual void SetBit( int bitNo ) { }

   
  }

  public class EmptyBitBoard : BitBoard { 
  
  }

  public class RookBitBoard : BitBoard {

  }

  public class BishopBitBoard : BitBoard {
    public UInt64 Bits_90 { set; get; }
    public UInt64 Bits_180 { set; get; }
    public UInt64 Bits_270 { set; get; }

    public override void SetBit( int bitNo ) {
      /* Set BIts */
      /* Set bits90 */

    }
  }

  public class KnightBitBoard : BitBoard {

  }

  public class KingBitBoard : BitBoard {

  }

  public class QueenBitBoard : BitBoard {

  }

  public class PawnBitBoard : BitBoard {

  }
}

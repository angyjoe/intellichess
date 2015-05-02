using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Author: Sari Haj Hussein
 */
namespace BitboardClass
{
  public class Bitboard
  {
    List<Bitboard> lolboards { get; set; }
    public Bitboard(){

    }

    public Boolean IsLegalMove(Bitboard endboard)
    {
      foreach (Bitboard b in this.lolboards)
      {
        if (this == b)
        {
          return true;
        }
      }
      return false;
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  public enum TranspositionEntryType {
    Exact,
    Alpha,
    Beta
  }

  [Serializable]
  public class TranspositionEntry {
    public ulong Hash { set; get; }
    public int Depth { set; get; }
    public double Score { set; get; }
    public int Ancient { set; get; }
    public TranspositionEntryType NodeType { set; get; }
    public ColoredBitBoard Move { set; get; }

    public TranspositionEntry(
        ulong hash,
        int depth,
        double score,
        int ancient,
        TranspositionEntryType nodeType,
        ColoredBitBoard move ) {
      Hash = hash;
      Depth = depth;
      Score = score;
      Ancient = ancient;
      NodeType = nodeType;
      Move = move;
    }

    


  }
}

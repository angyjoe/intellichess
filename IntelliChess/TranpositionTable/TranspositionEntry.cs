using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  public enum EntryType {
    Exact,
    Alpha,
    Beta
  }

  [Serializable]
  public class TranspositionEntry {
    public ulong Hash { set; get; }
    public int Depth { set; get; } 
    public double Score { set; get; }
    public bool Ancient { set; get; }
    public EntryType NodeType { set; get; }
    public ColoredBitBoard BestMove { set; get; }

    public TranspositionEntry(
        ulong hash,
        int depth,
        double score,
        bool ancient,
        EntryType nodeType  ) {
      Hash = hash;
      Depth = depth;
      Score = score;
      Ancient = ancient;
      NodeType = nodeType;
      BestMove = null; 
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  [Serializable]
  public class TranspositionTable {
    const ulong TABLE_SIZE_MB = 254;
    const ulong TABLE_SIZE_LENGTH = ( TABLE_SIZE_MB * (ulong)1000000 ) / 42UL;

    public static TranspositionTable TranspositionCache { set; get; }
    private TranspositionEntry[] _items = new TranspositionEntry[TABLE_SIZE_LENGTH];
    public int Count { private set; get; }

    public Stack<ColoredBitBoard> bestBranch { set; get; }
    public static Stack<ColoredBitBoard> currentBranch { get; set; }

    public TranspositionTable() {

    }

    public void Add( TranspositionEntry entry ) { 
      ulong index = ItemHashFunction( entry.Hash );
      TranspositionEntry tpEntry = _items[index]; 
      if ( tpEntry == null ) {
        Count++;
        _items[index] = entry; 
      } else { //Collision
        string tracemsg = "";
        string tracemsdgtype = "";
        if ( entry != null && entry.BestMove != null )
          tracemsg = entry.BestMove.Bits.ToString();
        if ( entry != null && entry.BestMove != null )
          tracemsdgtype = entry.BestMove.GetType().ToString();

        TranspositionTableStatistics.Collisions++; 
        _items[index] = ResolveCollision( tpEntry, entry );
      } 
    }

    public TranspositionEntry this[ulong hash] {
      get {
        ulong index = ItemHashFunction( hash );
        TranspositionEntry entry = _items[index];
        if ( entry == null ) {
          TranspositionTableStatistics.Misses++;
        } else {
          TranspositionTableStatistics.Hits++;
        }        
        return entry;
      }
    }

    private ulong ItemHashFunction( ulong zobristHash ) {
      return zobristHash % TABLE_SIZE_LENGTH;
    }

    private TranspositionEntry ResolveCollision( TranspositionEntry oldEntry, TranspositionEntry newEntry ) { 
      return newEntry;
    }
  }
  public static class TranspositionTableStatistics {
    public static int Hits { get; set; }
    public static int Misses { get; set; }
    public static int Collisions { set; get; }
  }
}

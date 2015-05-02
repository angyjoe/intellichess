using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace P5 {
  [Serializable]
  public class TranspositionTable {
    const ulong TABLE_SIZE_MB = 254;
    const ulong TABLE_SIZE_LENGTH = ( TABLE_SIZE_MB * (ulong)1000000 ) / 42UL;
    
    private TranspositionEntry[] _items = new TranspositionEntry[TABLE_SIZE_LENGTH];
    public int Count { private set; get; }

    public TranspositionTable() {  
    }

    public void Add( TranspositionEntry entry ) {
      if ( System.IO.File.Exists( "stop.now" ) ) {
        BinaryFormatter bf = new BinaryFormatter();
        var stream = File.Create( "TranspositionTable_serialize.obj" );
        bf.Serialize( stream, this );
        Environment.Exit( 0 );
      }

      ulong index = ItemHashFunction( entry.Hash );
      TranspositionEntry tpEntry = _items[index];
      //Trace.WriteLine( string.Format( "{0} : {1}", index, tpEntry.Hash ) );
      if ( tpEntry == null ) {
        Count++;
        _items[index] = entry;
        //Trace.WriteLine( string.Format( "[TranspositionTable]: Adding {0} : {1}", entry.Move.GetType().ToString(), entry.Move.Bits.ToString() ) );
      } else { //Collision
        Trace.WriteLine( string.Format( "[TranspositionTable]: Collision {0}: {1}", entry.Move.GetType().ToString(), entry.Move.Bits.ToString() ) );
        _items[index] = ResolveCollision( tpEntry, entry );        
      }  
    }

    public TranspositionEntry this[ulong hash] {
      get {
        ulong index = ItemHashFunction( hash );
        return _items[index];
      }
    }

    private ulong ItemHashFunction( ulong zobristHash ) {
      return zobristHash % TABLE_SIZE_LENGTH;
    }

    private TranspositionEntry ResolveCollision( TranspositionEntry oldEntry, TranspositionEntry newEntry ) {
      return newEntry;
    }
  }
}

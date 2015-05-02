using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  [Flags]
  public enum BitBoardFile {
    File0_A = 1 << 0,
    File1_B = 1 << 1,
    File2_C = 1 << 2,
    File3_D = 1 << 3,
    File4_E = 1 << 4,
    File5_F = 1 << 5,
    File6_G = 1 << 6,
    File7_H = 1 << 7
  }

  [Serializable]
  public abstract class BitBoard {
    public virtual BoardSquare Bits { set; get; }
    public int Count {
      get {
        int count = 0;
        for ( int i = 0; i < 64; i++ ) {
          ulong uShift = (ulong)0x1 << i;
          if ( ( (ulong)Bits & uShift ) != 0 ) {
            count++;
          }
        }
        return count;
      }
    }
    public BoardSquare MinimumRaisedBit {
      get {
        if ( Bits == BoardSquare.Empty ) return BoardSquare.Empty;
        return (BoardSquare)( (ulong)0x1 << MinRec( (ulong)Bits, 32, 0 ) );
      }
    }
    public BoardSquare MaximumRaisedBit {
      get {
        if ( Bits == BoardSquare.Empty ) return BoardSquare.Empty;
        return (BoardSquare)( (ulong)0x1 << MaxRec( (ulong)Bits, 32, 0 ) );
      }
    }

    protected BitBoard() {
      Bits = 0;
    }

    public virtual void Clear() {
      this.Bits = 0;
    }

    private int MinRec( ulong b, int splitSize, int place ) {
      var lowerHalf = ( b << splitSize );

      if ( lowerHalf == 0 ) {
        if ( splitSize == 1 ) return place + splitSize;
        else return MinRec( b, splitSize / 2, place + splitSize );
      } else {
        if ( splitSize == 1 ) return place;
        else return MinRec( lowerHalf, splitSize / 2, place );
      }
    }

    private int MaxRec( ulong b, int splitSize, int place ) {
      var upperHalf = b >> splitSize;

      if ( upperHalf != 0 ) {
        if ( splitSize == 1 ) return place + splitSize;
        else return MaxRec( upperHalf, splitSize / 2, place + splitSize );
      } else {
        if ( splitSize == 1 ) return place;
        else return MaxRec( b, splitSize / 2, place );
      }
    }

    public static List<int> PositionIndexFromBoardSquare( List<BoardSquare> positions ) {
      int fileOffset = 0;
      int rankFactor = 8;
      List<int> indexes = new List<int>();
      char[] boardsquares = new char[2];
      foreach ( BoardSquare i in positions ) {
        boardsquares = ( i.ToString().ToCharArray() );
        switch ( boardsquares[0] ) {
          case 'A':
            fileOffset = 0;
            break;
          case 'B':
            fileOffset = 1;
            break;
          case 'C':
            fileOffset = 2;
            break;
          case 'D':
            fileOffset = 3;
            break;
          case 'E':
            fileOffset = 4;
            break;
          case 'F':
            fileOffset = 5;
            break;
          case 'G':
            fileOffset = 6;
            break;
          case 'H':
            fileOffset = 7;
            break;
        }

        indexes.Add( (int)Char.GetNumericValue( boardsquares[1] ) * rankFactor - fileOffset );
      }
      return indexes;
    }

    static int[] index64 = { //Table used for DeBruijn multiplication in PositionIndexFromBoardSquare
       0,  1, 48,  2, 57, 49, 28,  3,
      61, 58, 50, 42, 38, 29, 17,  4,
      62, 55, 59, 36, 53, 51, 43, 22,
      45, 39, 33, 30, 24, 18, 12,  5,
      63, 47, 56, 27, 60, 41, 37, 16,
      54, 35, 52, 21, 44, 32, 23, 11,
      46, 26, 40, 15, 34, 20, 31, 10,
      25, 14, 19,  9, 13,  8,  7,  6
    };

    const ulong debruijn64 = (0x03f79d71b4cb0a89);

    public static int PositionIndexFromBoardSquare(BoardSquare position)
    {
      return index64[((ulong)position * debruijn64) >> 58];
    }

    public override bool Equals( object obj ) {
      if ( obj == null ) return false;

      BitBoard c = obj as BitBoard;
      if ( c == null ) return false;//Not of type BitBoard
      var thisType = this.GetType();
      var objType = obj.GetType();
      if ( this.GetType() != obj.GetType() ) return false;

      return c.Bits == Bits;
    }

    public class BitboardException : Exception {
      public BitboardException( string s )
        : base( s ) { }
    }
  }

  [Serializable]
  public class EmptyBitBoard : BitBoard {
    public EmptyBitBoard() : base() { }

    public EmptyBitBoard( BoardSquare bits ) : base() { Bits = bits; }
  }

  [Serializable]
  public abstract class ColoredBitBoard : BitBoard {
    public ChessPieceColors Color { private set; get; }
    public ChessPieceType? JustCaptured { set; get; }
    /// <summary>
    /// Set by MoveGen
    /// </summary>
    public bool DoesCheck { set; get; }
    /// <summary>
    /// Set by MoveGen
    /// </summary>
    public bool IsCapturing { set; get; }

    private double[] _positionValues = new double[64];
    public double[] PositionValues { get { return _positionValues; } private set { } }
    protected ColoredBitBoard( ChessPieceColors color )
      : base() {
      Color = color;
      IsCapturing = false;
            
      Debug.Assert( Color != 0 );
    }

    public abstract void Initialize( Zobrist hash );

    /// <summary>
    /// Creates a new ColoredBitBoard object with the same color as this current BitBoard
    /// </summary>
    abstract protected internal ColoredBitBoard NewEqualColoredInstance();

    /// <summary>
    /// Splits the bitboard of this instance into new bitboards with only one bit raise in each
    /// </summary>
    /// <typeparam name="T">Type of bitboard</typeparam>
    /// <returns>An array of Bitboards</returns>
    public static IEnumerable<T> SplitBitBoard<T>( T bitboardToSplit ) where T : ColoredBitBoard {
      if ( bitboardToSplit == null ) throw new BitboardException( "BitBoard to split is null" );

      List<T> splits = new List<T>();

      for ( int i = 0; i < 64; i++ ) {
        ulong uShift = (ulong)0x1 << i;

        if ( ( (ulong)bitboardToSplit.Bits & uShift ) != 0 ) {
          var bb = bitboardToSplit.NewEqualColoredInstance();
          bb.Bits = (BoardSquare)uShift;

          splits.Add( (T)bb );
        }
      }
      return splits;
    }
    /// <summary>
    /// Splits the bitboard of this instance into list of BoardSquares
    /// </summary>
    /// <typeparam name="T">Type of bitboard</typeparam>
    /// <returns>An array of Bitboards</returns>
    public static IEnumerable<BoardSquare> SplitBitBoardToBoardSquares<T>( T bitboardToSplit ) where T : ColoredBitBoard {
      if ( bitboardToSplit == null ) throw new BitboardException( "BitBoard to split is null" );

      List<BoardSquare> splits = new List<BoardSquare>();

      for ( int i = 0; i < 64; i++ ) {
        ulong uShift = (ulong)0x1 << i;

        if ( ( (ulong)bitboardToSplit.Bits & uShift ) != 0 ) {
          splits.Add( (BoardSquare)uShift );
        }
      }
      return splits;
    }

    public ColoredBitBoard DeepCopy() {
      Dictionary<Type, Func<ColoredBitBoard>> @switch = new Dictionary<Type, Func<ColoredBitBoard>>() {
        { typeof(KingBitBoard), () => {
          KingBitBoard resultBoard;
          if ( this.Color == ChessPieceColors.White ) {
            resultBoard = new KingBitBoard( ChessPieceColors.White );
          } else {
            resultBoard = new KingBitBoard( ChessPieceColors.Black );
          }
          resultBoard.Bits = this.Bits;
          resultBoard.DoesCheck = this.DoesCheck;
          resultBoard.HasMoved = ( (KingBitBoard)this ).HasMoved;
          resultBoard.IsCapturing = ( (KingBitBoard)this ).IsCapturing;
          resultBoard.JustCaptured = ( (KingBitBoard)this ).JustCaptured;
          return resultBoard; 

        } }, 
        { typeof(QueenBitBoard), () => {
          QueenBitBoard resultBoard;
          if ( this.Color == ChessPieceColors.White ) {
            resultBoard = new QueenBitBoard( ChessPieceColors.White );
          } else {
            resultBoard = new QueenBitBoard( ChessPieceColors.Black );
          }
          resultBoard.Bits = this.Bits;
          resultBoard.DoesCheck = this.DoesCheck;
          resultBoard.IsCapturing = ( (QueenBitBoard)this ).IsCapturing;
          resultBoard.JustCaptured = ( (QueenBitBoard)this ).JustCaptured;
          return resultBoard; 
        } },   
        { typeof(RookBitBoard), () => {
          RookBitBoard resultBoard;
          if ( this.Color == ChessPieceColors.White ) {
            resultBoard = new RookBitBoard( ChessPieceColors.White );
          } else {
            resultBoard = new RookBitBoard( ChessPieceColors.Black );
          }
          resultBoard.Bits = this.Bits;
          resultBoard.DoesCheck = this.DoesCheck;
          resultBoard.IsCapturing = ( (RookBitBoard)this ).IsCapturing;
          resultBoard.JustCaptured = ( (RookBitBoard)this ).JustCaptured;
          resultBoard.HasMovedKingSide = ( (RookBitBoard)this ).HasMovedKingSide;
          resultBoard.HasMovedQueenSide = ( (RookBitBoard)this ).HasMovedQueenSide;
          return resultBoard;
        } },   
        { typeof(BishopBitBoard), () => {
          BishopBitBoard resultBoard;
          if ( this.Color == ChessPieceColors.White ) {
            resultBoard = new BishopBitBoard( ChessPieceColors.White );
          } else {
            resultBoard = new BishopBitBoard( ChessPieceColors.Black );
          }
          resultBoard.Bits = this.Bits;
          resultBoard.DoesCheck = this.DoesCheck;
          resultBoard.IsCapturing = ( (BishopBitBoard)this ).IsCapturing;
          resultBoard.JustCaptured = ( (BishopBitBoard)this ).JustCaptured;
          return resultBoard;
        } },   
        { typeof(KnightBitBoard), () => {
          KnightBitBoard resultBoard;
          if ( this.Color == ChessPieceColors.White ) {
            resultBoard = new KnightBitBoard( ChessPieceColors.White );
          } else {
            resultBoard = new KnightBitBoard( ChessPieceColors.Black );
          }
          resultBoard.Bits = this.Bits;
          resultBoard.DoesCheck = this.DoesCheck;
          resultBoard.IsCapturing = ( (KnightBitBoard)this ).IsCapturing;
          resultBoard.JustCaptured = ( (KnightBitBoard)this ).JustCaptured;
          return resultBoard;
        } },  
        { typeof(PawnBitBoard), () => {
          PawnBitBoard resultBoard;
          if ( this.Color == ChessPieceColors.White ) {
            resultBoard = new PawnBitBoard( ChessPieceColors.White );
          } else {
            resultBoard = new PawnBitBoard( ChessPieceColors.Black );
          }
          resultBoard.Bits = this.Bits;
          resultBoard.DoesCheck = this.DoesCheck;
          resultBoard.IsCapturing = ( (PawnBitBoard)this ).IsCapturing;
          resultBoard.JustCaptured = ( (PawnBitBoard)this ).JustCaptured;
          resultBoard.MovedTwoSquares = ( (PawnBitBoard)this ).MovedTwoSquares;
          if (((PawnBitBoard)this).Promotion !=  null)
            resultBoard.Promote(((PawnBitBoard.PromotionPiece)((PawnBitBoard)this ).Promotion));
          
          return resultBoard;            
        } }       
      };

      return @switch[this.GetType()]();
    }


    public override bool Equals( object obj ) {
      if ( obj == null ) return false;

      ColoredBitBoard c = obj as ColoredBitBoard;
      if ( c == null ) return false;//Not of type BitBoard
      if ( this.Color != c.Color ) return false;

      return base.Equals( obj );
    }

  }

  [Serializable]
  public class RookBitBoard : ColoredBitBoard {
    public override BoardSquare Bits {
      get {
        return base.Bits;
      }
      set {
        base.Bits = value;

        if ( Color == ChessPieceColors.White ) {
          if ( !HasMovedQueenSide )
            HasMovedQueenSide = ( BoardSquare.A1 & Bits ) == 0;
          if ( !HasMovedKingSide )
            HasMovedKingSide = ( BoardSquare.H1 & Bits ) == 0;
        } else {
          if ( !HasMovedQueenSide )
            HasMovedQueenSide = ( BoardSquare.A8 & Bits ) == 0;
          if ( !HasMovedKingSide )
            HasMovedKingSide = ( BoardSquare.H8 & Bits ) == 0;
        }
      }
    }
    public bool HasMovedQueenSide { set; get; }
    public bool HasMovedKingSide { set; get; }

    public RookBitBoard( ChessPieceColors color )
      : base( color ) {
      if ( color == ChessPieceColors.White) {
        #region WhiteRook
        //Rank 1 starting with H
        PositionValues[0] = 0;
        PositionValues[1] = 0;
        PositionValues[2] = 0;
        PositionValues[3] = 5;
        PositionValues[4] = 5;
        PositionValues[5] = 0;
        PositionValues[6] = 0;
        PositionValues[7] = 0;
        //Rank 2
        PositionValues[8] = -5;
        PositionValues[9] = 0;
        PositionValues[10] = 0;
        PositionValues[11] = 0;
        PositionValues[12] = 0;
        PositionValues[13] = 0;
        PositionValues[14] = 0;
        PositionValues[15] = -5;
        //Rank 3
        PositionValues[16] = -5;
        PositionValues[17] = 0;
        PositionValues[18] = 0;
        PositionValues[19] = 0;
        PositionValues[20] = 0;
        PositionValues[21] = 0;
        PositionValues[22] = 0;
        PositionValues[23] = -5;
        //Rank 4
        PositionValues[24] = -5;
        PositionValues[25] = 0;
        PositionValues[26] = 0;
        PositionValues[27] = 0;
        PositionValues[28] = 0;
        PositionValues[29] = 0;
        PositionValues[30] = 0;
        PositionValues[31] = -5;
        //Rank 5
        PositionValues[32] = -5;
        PositionValues[33] = 0;
        PositionValues[34] = 0;
        PositionValues[35] = 0;
        PositionValues[36] = 0;
        PositionValues[37] = 0;
        PositionValues[38] = 0;
        PositionValues[39] = -5;
        //Rank 6
        PositionValues[40] = -5;
        PositionValues[41] = 0;
        PositionValues[42] = 0;
        PositionValues[43] = 0;
        PositionValues[44] = 0;
        PositionValues[45] = 0;
        PositionValues[46] = 0;
        PositionValues[47] = -5;
        //Rank 7
        PositionValues[48] = 5;
        PositionValues[49] = 10;
        PositionValues[50] = 10;
        PositionValues[51] = 10;
        PositionValues[52] = 10;
        PositionValues[53] = 10;
        PositionValues[54] = 10;
        PositionValues[55] = 5;
        //Rank 8
        PositionValues[56] = 0;
        PositionValues[57] = 0;
        PositionValues[58] = 0;
        PositionValues[59] = 0;
        PositionValues[60] = 0;
        PositionValues[61] = 0;
        PositionValues[62] = 0;
        PositionValues[63] = 0;
        #endregion
      } else {
        #region WhiteRook
        //Rank 1 starting with A
        PositionValues[63] = 0;
        PositionValues[62] = 0;
        PositionValues[61] = 0;
        PositionValues[60] = 5;
        PositionValues[59] = 5;
        PositionValues[58] = 0;
        PositionValues[57] = 0;
        PositionValues[56] = 0;
        //Rank 2
        PositionValues[55] = -5;
        PositionValues[54] = 0;
        PositionValues[53] = 0;
        PositionValues[52] = 0;
        PositionValues[51] = 0;
        PositionValues[50] = 0;
        PositionValues[49] = 0;
        PositionValues[48] = -5;
        //Rank 3
        PositionValues[47] = -5;
        PositionValues[46] = 0;
        PositionValues[45] = 0;
        PositionValues[44] = 0;
        PositionValues[43] = 0;
        PositionValues[42] = 0;
        PositionValues[41] = 0;
        PositionValues[40] = -5;
        //Rank 4
        PositionValues[39] = -5;
        PositionValues[38] = 0;
        PositionValues[37] = 0;
        PositionValues[36] = 0;
        PositionValues[35] = 0;
        PositionValues[34] = 0;
        PositionValues[33] = 0;
        PositionValues[32] = -5;
        //Rank 5
        PositionValues[31] = -5;
        PositionValues[30] = 0;
        PositionValues[29] = 0;
        PositionValues[28] = 0;
        PositionValues[27] = 0;
        PositionValues[26] = 0;
        PositionValues[25] = 0;
        PositionValues[24] = -5;
        //Rank 6
        PositionValues[23] = -5;
        PositionValues[22] = 0;
        PositionValues[21] = 0;
        PositionValues[20] = 0;
        PositionValues[19] = 0;
        PositionValues[18] = 0;
        PositionValues[17] = 0;
        PositionValues[16] = -5;
        //Rank 7
        PositionValues[15] = 5;
        PositionValues[14] = 10;
        PositionValues[13] = 10;
        PositionValues[12] = 10;
        PositionValues[11] = 10;
        PositionValues[10] = 10;
        PositionValues[9] = 10;
        PositionValues[8] = 5;
        //Rank 8
        PositionValues[7] = 0;
        PositionValues[6] = 0;
        PositionValues[5] = 0;
        PositionValues[4] = 0;
        PositionValues[3] = 0;
        PositionValues[2] = 0;
        PositionValues[1] = 0;
        PositionValues[0] = 0;
        #endregion
      }
      HasMovedQueenSide = false;
      HasMovedKingSide = false;
    }


    public override void Initialize( Zobrist hash ) {
      if ( Color == ChessPieceColors.White ) {
        Bits = ( BoardSquare.A1 | BoardSquare.H1 );
        if ( hash != null ) {
          hash.Update( ChessPieceType.Rook, ChessPieceColors.White, BoardSquare.A1, availableCastles: ZobristCastling.QueenSideRookMoved );
          hash.Update( ChessPieceType.Rook, ChessPieceColors.White, BoardSquare.H1, availableCastles: ZobristCastling.KingSideRookMoved );
        }
      } else {
        Bits = ( BoardSquare.A8 | BoardSquare.H8 );
        if ( hash != null ) {
          hash.Update( ChessPieceType.Rook, ChessPieceColors.Black, BoardSquare.A8, availableCastles: ZobristCastling.QueenSideRookMoved );
          hash.Update( ChessPieceType.Rook, ChessPieceColors.Black, BoardSquare.H8, availableCastles: ZobristCastling.KingSideRookMoved );
        }
      }
      HasMovedQueenSide = false;
      HasMovedKingSide = false;
    }

    protected internal override ColoredBitBoard NewEqualColoredInstance() {
      return new RookBitBoard( this.Color );
    }
  }

  [Serializable]
  public class BishopBitBoard : ColoredBitBoard {
    public BishopBitBoard( ChessPieceColors color )
      : base( color ) {
          if (color == ChessPieceColors.White)
          {
              #region WhiteBishop
              //Rank 1 starting with H
              PositionValues[0] = -20;
              PositionValues[1] = -10;
              PositionValues[2] = -10;
              PositionValues[3] = -10;
              PositionValues[4] = -10;
              PositionValues[5] = -10;
              PositionValues[6] = -10;
              PositionValues[7] = -20;
              //Rank 2
              PositionValues[8] = -10;
              PositionValues[9] = 5;
              PositionValues[10] = 0;
              PositionValues[11] = 0;
              PositionValues[12] = 0;
              PositionValues[13] = 0;
              PositionValues[14] = 5;
              PositionValues[15] = -10;
              //Rank 3
              PositionValues[16] = -10;
              PositionValues[17] = 10;
              PositionValues[18] = 10;
              PositionValues[19] = 10;
              PositionValues[20] = 10;
              PositionValues[21] = 10;
              PositionValues[22] = 10;
              PositionValues[23] = -10;
              //Rank 4
              PositionValues[24] = -10;
              PositionValues[25] = 0;
              PositionValues[26] = 10;
              PositionValues[27] = 10;
              PositionValues[28] = 10;
              PositionValues[29] = 10;
              PositionValues[30] = 0;
              PositionValues[31] = -10;
              //Rank 5
              PositionValues[32] = -10;
              PositionValues[33] = 5;
              PositionValues[34] = 5;
              PositionValues[35] = 10;
              PositionValues[36] = 10;
              PositionValues[37] = 5;
              PositionValues[38] = 5;
              PositionValues[39] = -10;
              //Rank 6
              PositionValues[40] = -10;
              PositionValues[41] = 0;
              PositionValues[42] = 5;
              PositionValues[43] = 10;
              PositionValues[44] = 10;
              PositionValues[45] = 5;
              PositionValues[46] = 0;
              PositionValues[47] = -10;
              //Rank 7
              PositionValues[48] = -10;
              PositionValues[49] = 0;
              PositionValues[50] = 0;
              PositionValues[51] = 0;
              PositionValues[52] = 0;
              PositionValues[53] = 0;
              PositionValues[54] = 0;
              PositionValues[55] = -10;
              //Rank 8
              PositionValues[56] = -20;
              PositionValues[57] = -10;
              PositionValues[58] = -10;
              PositionValues[59] = -10;
              PositionValues[60] = -10;
              PositionValues[61] = -10;
              PositionValues[62] = -10;
              PositionValues[63] = -20;
              #endregion
          }
          else
          {
              #region Blackbishop
              //Rank 1 starting with H
              PositionValues[63] = -20;
              PositionValues[62] = -10;
              PositionValues[61] = -10;
              PositionValues[60] = -10;
              PositionValues[59] = -10;
              PositionValues[58] = -10;
              PositionValues[57] = -10;
              PositionValues[56] = -20;
              //Rank 2
              PositionValues[55] = -10;
              PositionValues[54] = 5;
              PositionValues[53] = 0;
              PositionValues[52] = 0;
              PositionValues[51] = 0;
              PositionValues[50] = 0;
              PositionValues[49] = 5;
              PositionValues[48] = -10;
              //Rank 3
              PositionValues[47] = -10;
              PositionValues[46] = 10;
              PositionValues[45] = 10;
              PositionValues[44] = 10;
              PositionValues[43] = 10;
              PositionValues[42] = 10;
              PositionValues[41] = 10;
              PositionValues[40] = -100;
              //Rank 4
              PositionValues[39] = -10;
              PositionValues[38] = 0;
              PositionValues[37] = 10;
              PositionValues[36] = 10;
              PositionValues[35] = 10;
              PositionValues[34] = 10;
              PositionValues[33] = 0;
              PositionValues[32] = -10;
              //Rank 5
              PositionValues[31] = -10;
              PositionValues[30] = 5;
              PositionValues[29] = 5;
              PositionValues[28] = 10;
              PositionValues[27] = 10;
              PositionValues[26] = 5;
              PositionValues[25] = 5;
              PositionValues[24] = -10;
              //Rank 6
              PositionValues[23] = -10;
              PositionValues[22] = 0;
              PositionValues[21] = 5;
              PositionValues[20] = 10;
              PositionValues[19] = 10;
              PositionValues[18] = 5;
              PositionValues[17] = 0;
              PositionValues[16] = -10;
              //Rank 7
              PositionValues[15] = -10;
              PositionValues[14] = 0;
              PositionValues[13] = 0;
              PositionValues[12] = 0;
              PositionValues[11] = 0;
              PositionValues[10] = 0;
              PositionValues[9] = 0;
              PositionValues[8] = -10;
              //Rank 8
              PositionValues[7] = -20;
              PositionValues[6] = -10;
              PositionValues[5] = -10;
              PositionValues[4] = -10;
              PositionValues[3] = -10;
              PositionValues[2] = -10;
              PositionValues[1] = -10;
              PositionValues[0] = -20;
              #endregion
          }
    }

    public override void Initialize( Zobrist hash ) {
      if ( Color == ChessPieceColors.White ) {
        Bits = BoardSquare.C1 | BoardSquare.F1;
        if ( hash != null ) {
          hash.Update( ChessPieceType.Bishop, ChessPieceColors.White, BoardSquare.C1 );
          hash.Update( ChessPieceType.Bishop, ChessPieceColors.White, BoardSquare.F1 );
        }
      } else {
        Bits = BoardSquare.C8 | BoardSquare.F8;
        if ( hash != null ) {
          hash.Update( ChessPieceType.Bishop, ChessPieceColors.Black, BoardSquare.C8 );
          hash.Update( ChessPieceType.Bishop, ChessPieceColors.Black, BoardSquare.F8 );
        }
      }
    }

    protected internal override ColoredBitBoard NewEqualColoredInstance() {
      return new BishopBitBoard( this.Color );
    }
  }

  [Serializable]
  public class KnightBitBoard : ColoredBitBoard {
    public KnightBitBoard( ChessPieceColors color )
      : base( color ) {
      if ( color == ChessPieceColors.White ) {
        #region WhiteKnight
        //Rank 1 starting with H
        PositionValues[0] = -50;
        PositionValues[1] = -40;
        PositionValues[2] = -30;
        PositionValues[3] = -30;
        PositionValues[4] = -30;
        PositionValues[5] = -30;
        PositionValues[6] = -40;
        PositionValues[7] = -50;
        //Rank 2
        PositionValues[8] = -40;
        PositionValues[9] = -20;
        PositionValues[10] = 0;
        PositionValues[11] = 5;
        PositionValues[12] = 5;
        PositionValues[13] = 0;
        PositionValues[14] = -20;
        PositionValues[15] = -40;
        //Rank 3
        PositionValues[16] = -30;
        PositionValues[17] = 5;
        PositionValues[18] = 10;
        PositionValues[19] = 15;
        PositionValues[20] = 15;
        PositionValues[21] = 10;
        PositionValues[22] = 5;
        PositionValues[23] = -30;
        //Rank 4
        PositionValues[24] = -30;
        PositionValues[25] = 0;
        PositionValues[26] = 15;
        PositionValues[27] = 20;
        PositionValues[28] = 20;
        PositionValues[29] = 15;
        PositionValues[30] = 0;
        PositionValues[31] = -30;
        //Rank 5
        PositionValues[32] = -30;
        PositionValues[33] = 5;
        PositionValues[34] = 15;
        PositionValues[35] = 20;
        PositionValues[36] = 20;
        PositionValues[37] = 15;
        PositionValues[38] = 5;
        PositionValues[39] = -30;
        //Rank 6
        PositionValues[40] = -30;
        PositionValues[41] = 0;
        PositionValues[42] = 10;
        PositionValues[43] = 15;
        PositionValues[44] = 15;
        PositionValues[45] = 10;
        PositionValues[46] = 0;
        PositionValues[47] = -30;
        //Rank 7
        PositionValues[48] = -40;
        PositionValues[49] = -20;
        PositionValues[50] = 0;
        PositionValues[51] = 0;
        PositionValues[52] = 0;
        PositionValues[53] = 0;
        PositionValues[54] = -20;
        PositionValues[55] = -40;
        //Rank 8
        PositionValues[56] = -50;
        PositionValues[57] = -40;
        PositionValues[58] = -30;
        PositionValues[59] = -30;
        PositionValues[60] = -30;
        PositionValues[61] = -30;
        PositionValues[62] = -40;
        PositionValues[63] = -50;
        #endregion
      } else {
        #region BlackKnight
        //Rank 1 starting with H
        PositionValues[63] = -50;
        PositionValues[62] = -40;
        PositionValues[61] = -30;
        PositionValues[60] = -30;
        PositionValues[59] = -30;
        PositionValues[58] = -30;
        PositionValues[57] = -40;
        PositionValues[56] = -50;
        //Rank 2
        PositionValues[55] = -40;
        PositionValues[54] = -20;
        PositionValues[53] = 0;
        PositionValues[52] = 5;
        PositionValues[51] = 5;
        PositionValues[50] = 0;
        PositionValues[49] = -20;
        PositionValues[48] = -40;
        //Rank 3
        PositionValues[47] = -30;
        PositionValues[46] = 5;
        PositionValues[45] = 10;
        PositionValues[44] = 15;
        PositionValues[43] = 15;
        PositionValues[42] = 10;
        PositionValues[41] = 5;
        PositionValues[40] = -30;
        //Rank 4
        PositionValues[39] = -30;
        PositionValues[38] = 0;
        PositionValues[37] = 15;
        PositionValues[36] = 20;
        PositionValues[35] = 20;
        PositionValues[34] = 15;
        PositionValues[33] = 0;
        PositionValues[32] = -30;
        //Rank 5
        PositionValues[31] = -30;
        PositionValues[30] = 5;
        PositionValues[29] = 15;
        PositionValues[28] = 20;
        PositionValues[27] = 20;
        PositionValues[26] = 15;
        PositionValues[25] = 5;
        PositionValues[24] = -30;
        //Rank 6
        PositionValues[23] = -30;
        PositionValues[22] = 0;
        PositionValues[21] = 10;
        PositionValues[20] = 15;
        PositionValues[19] = 15;
        PositionValues[18] = 10;
        PositionValues[17] = 0;
        PositionValues[16] = -30;
        //Rank 7
        PositionValues[15] = -40;
        PositionValues[14] = -20;
        PositionValues[13] = 0;
        PositionValues[12] = 0;
        PositionValues[11] = 0;
        PositionValues[10] = 0;
        PositionValues[9] = -20;
        PositionValues[8] = -40;
        //Rank 8
        PositionValues[7] = -50;
        PositionValues[6] = -40;
        PositionValues[5] = -30;
        PositionValues[4] = -30;
        PositionValues[3] = -30;
        PositionValues[2] = -30;
        PositionValues[1] = -40;
        PositionValues[0] = -50;
        #endregion
      }
    }

    public override void Initialize( Zobrist hash ) {
      if ( Color == ChessPieceColors.White ) {
        Bits = BoardSquare.B1 | BoardSquare.G1;
        if ( hash != null ) {
          hash.Update( ChessPieceType.Knight, ChessPieceColors.White, BoardSquare.B1 );
          hash.Update( ChessPieceType.Knight, ChessPieceColors.White, BoardSquare.G1 );
        }
      } else {
        Bits = BoardSquare.B8 | BoardSquare.G8;
        if ( hash != null ) {
          hash.Update( ChessPieceType.Knight, ChessPieceColors.Black, BoardSquare.B8 );
          hash.Update( ChessPieceType.Knight, ChessPieceColors.Black, BoardSquare.G8 );
        }
      }
    }

    protected internal override ColoredBitBoard NewEqualColoredInstance() {
      return new KnightBitBoard( this.Color );
    }
  }

  [Serializable]
  public class KingBitBoard : ColoredBitBoard {
    public override BoardSquare Bits {
      get {
        return base.Bits;
      }
      set {
        base.Bits = value;
        if ( !HasMoved ) {
          if ( ChessPieceColors.White == Color ) {
            HasMoved = ( BoardSquare.E1 & Bits ) == 0;//Is not located on initial location
          } else {
            HasMoved = ( BoardSquare.E8 & Bits ) == 0;
          }
        }
      }
    }
    public bool HasMoved { set; get; }
    public bool IsChecked { set; get; }

    public KingBitBoard( ChessPieceColors color )
      : base( color ) {
      if ( color == ChessPieceColors.White ) {
        #region WhiteKingMidGame
        //Rank 1 starting with H
        PositionValues[0] = 20;
        PositionValues[1] = 30;
        PositionValues[2] = 10;
        PositionValues[3] = 0;
        PositionValues[4] = 0;
        PositionValues[5] = 10;
        PositionValues[6] = 30;
        PositionValues[7] = 20;
        //Rank 2
        PositionValues[8] = 20;
        PositionValues[9] = 20;
        PositionValues[10] = 0;
        PositionValues[11] = 0;
        PositionValues[12] = 0;
        PositionValues[13] = 0;
        PositionValues[14] = 20;
        PositionValues[15] = 20;
        //Rank 3
        PositionValues[16] = -10;
        PositionValues[17] = -20;
        PositionValues[18] = -20;
        PositionValues[19] = -20;
        PositionValues[20] = -20;
        PositionValues[21] = -20;
        PositionValues[22] = -20;
        PositionValues[23] = -10;
        //Rank 4
        PositionValues[24] = -20;
        PositionValues[25] = -30;
        PositionValues[26] = -30;
        PositionValues[27] = -40;
        PositionValues[28] = -40;
        PositionValues[29] = -30;
        PositionValues[30] = -30;
        PositionValues[31] = -20;
        //Rank 5
        PositionValues[32] = -30;
        PositionValues[33] = -40;
        PositionValues[34] = -40;
        PositionValues[35] = -50;
        PositionValues[36] = -50;
        PositionValues[37] = -40;
        PositionValues[38] = -40;
        PositionValues[39] = -30;
        //Rank 6
        PositionValues[40] = -30;
        PositionValues[41] = -40;
        PositionValues[42] = -40;
        PositionValues[43] = -50;
        PositionValues[44] = -50;
        PositionValues[45] = -40;
        PositionValues[46] = -40;
        PositionValues[47] = -30;
        //Rank 7
        PositionValues[48] = -30;
        PositionValues[49] = -40;
        PositionValues[50] = -40;
        PositionValues[51] = -50;
        PositionValues[52] = -50;
        PositionValues[53] = -40;
        PositionValues[54] = -40;
        PositionValues[55] = -30;
        //Rank 8
        PositionValues[56] = -30;
        PositionValues[57] = -40;
        PositionValues[58] = -40;
        PositionValues[59] = -50;
        PositionValues[60] = -50;
        PositionValues[61] = -40;
        PositionValues[62] = -40;
        PositionValues[63] = -30;
        #endregion
      } else {
        #region BlackKingMidGame
        //Rank 1 starting with H
        PositionValues[63] = 20;
        PositionValues[62] = 30;
        PositionValues[61] = 10;
        PositionValues[60] = 0;
        PositionValues[59] = 0;
        PositionValues[58] = 10;
        PositionValues[57] = 30;
        PositionValues[56] = 20;
        //Rank 2
        PositionValues[55] = 20;
        PositionValues[54] = 20;
        PositionValues[53] = 0;
        PositionValues[52] = 0;
        PositionValues[51] = 0;
        PositionValues[50] = 0;
        PositionValues[49] = 20;
        PositionValues[48] = 20;
        //Rank 3
        PositionValues[47] = -10;
        PositionValues[46] = -20;
        PositionValues[45] = -20;
        PositionValues[44] = -20;
        PositionValues[43] = -20;
        PositionValues[42] = -20;
        PositionValues[41] = -20;
        PositionValues[40] = -10;
        //Rank 4
        PositionValues[39] = -20;
        PositionValues[38] = -30;
        PositionValues[37] = -30;
        PositionValues[36] = -40;
        PositionValues[35] = -40;
        PositionValues[34] = -30;
        PositionValues[33] = -30;
        PositionValues[32] = -20;
        //Rank 5
        PositionValues[31] = -30;
        PositionValues[30] = -40;
        PositionValues[29] = -40;
        PositionValues[28] = -50;
        PositionValues[27] = -50;
        PositionValues[26] = -40;
        PositionValues[25] = -40;
        PositionValues[24] = -30;
        //Rank 6
        PositionValues[23] = -30;
        PositionValues[22] = -40;
        PositionValues[21] = -40;
        PositionValues[20] = -50;
        PositionValues[19] = -50;
        PositionValues[18] = -40;
        PositionValues[17] = -40;
        PositionValues[16] = -30;
        //Rank 7
        PositionValues[15] = -30;
        PositionValues[14] = -40;
        PositionValues[13] = -40;
        PositionValues[12] = -50;
        PositionValues[11] = -50;
        PositionValues[10] = -40;
        PositionValues[9] = -40;
        PositionValues[8] = -30;
        //Rank 8
        PositionValues[7] = -30;
        PositionValues[6] = -40;
        PositionValues[5] = -40;
        PositionValues[4] = -50;
        PositionValues[3] = -50;
        PositionValues[2] = -40;
        PositionValues[1] = -40;
        PositionValues[0] = -30;
        #endregion
      }

      HasMoved = false;
    }

    /// <summary>
    /// Assign to standard squares
    /// </summary>
    public override void Initialize( Zobrist hash ) {
      if ( Color == ChessPieceColors.White ) {
        Bits = BoardSquare.E1;
        if ( hash != null ) {
          hash.Update( ChessPieceType.King, ChessPieceColors.White, BoardSquare.E1 );
        }
      } else {
        Bits = BoardSquare.E8;
        if ( hash != null ) {
          hash.Update( ChessPieceType.King, ChessPieceColors.Black, BoardSquare.E8 );
        }
      }
      HasMoved = false;
    }

    protected internal override ColoredBitBoard NewEqualColoredInstance() {
      return new KingBitBoard( this.Color );
    }
  }

  [Serializable]
  public class QueenBitBoard : ColoredBitBoard {
    public QueenBitBoard( ChessPieceColors color )
      : base( color ) {
      if ( color == ChessPieceColors.White ) {
        #region WhiteQueen
        //Rank 1 starting with H
        PositionValues[0] = -20;
        PositionValues[1] = -10;
        PositionValues[2] = -10;
        PositionValues[3] = -5;
        PositionValues[4] = -5;
        PositionValues[5] = -10;
        PositionValues[6] = -10;
        PositionValues[7] = -20;
        //Rank 2
        PositionValues[8] = -10;
        PositionValues[9] = 0;
        PositionValues[10] = 0;
        PositionValues[11] = 0;
        PositionValues[12] = 0;
        PositionValues[13] = 5;
        PositionValues[14] = 0;
        PositionValues[15] = -10;
        //Rank 3
        PositionValues[16] = -10;
        PositionValues[17] = 0;
        PositionValues[18] = 5;
        PositionValues[19] = 5;
        PositionValues[20] = 5;
        PositionValues[21] = 5;
        PositionValues[22] = 5;
        PositionValues[23] = -10;
        //Rank 4
        PositionValues[24] = -5;
        PositionValues[25] = -0;
        PositionValues[26] = 5;
        PositionValues[27] = 5;
        PositionValues[28] = 5;
        PositionValues[29] = 5;
        PositionValues[30] = 0;
        PositionValues[31] = 0;
        //Rank 5
        PositionValues[32] = -5;
        PositionValues[33] = 0;
        PositionValues[34] = 5;
        PositionValues[35] = 5;
        PositionValues[36] = 5;
        PositionValues[37] = 5;
        PositionValues[38] = 0;
        PositionValues[39] = -5;
        //Rank 6
        PositionValues[40] = -10;
        PositionValues[41] = 0;
        PositionValues[42] = 5;
        PositionValues[43] = 5;
        PositionValues[44] = 5;
        PositionValues[45] = 5;
        PositionValues[46] = 0;
        PositionValues[47] = -10;
        //Rank 7
        PositionValues[48] = -10;
        PositionValues[49] = 0;
        PositionValues[50] = 0;
        PositionValues[51] = 0;
        PositionValues[52] = 0;
        PositionValues[53] = 0;
        PositionValues[54] = 0;
        PositionValues[55] = -10;
        //Rank 8
        PositionValues[56] = -20;
        PositionValues[57] = -10;
        PositionValues[58] = -10;
        PositionValues[59] = -5;
        PositionValues[60] = -5;
        PositionValues[61] = -10;
        PositionValues[62] = -10;
        PositionValues[63] = -20;
        #endregion
      } else {
        #region BlackQueen
        //Rank 1 starting with H
        PositionValues[0] = -20;
        PositionValues[1] = -10;
        PositionValues[2] = -10;
        PositionValues[3] = -5;
        PositionValues[4] = -5;
        PositionValues[5] = -10;
        PositionValues[6] = -10;
        PositionValues[7] = -20;
        //Rank 2
        PositionValues[8] = -10;
        PositionValues[9] = 0;
        PositionValues[10] = 0;
        PositionValues[11] = 0;
        PositionValues[12] = 0;
        PositionValues[13] = 0;
        PositionValues[14] = 0;
        PositionValues[15] = -10;
        //Rank 3
        PositionValues[16] = -10;
        PositionValues[17] = 0;
        PositionValues[18] = 5;
        PositionValues[19] = 5;
        PositionValues[20] = 5;
        PositionValues[21] = 5;
        PositionValues[22] = 0;
        PositionValues[23] = -10;
        //Rank 4
        PositionValues[24] = -5;
        PositionValues[25] = 0;
        PositionValues[26] = 5;
        PositionValues[27] = 5;
        PositionValues[28] = 5;
        PositionValues[29] = 5;
        PositionValues[30] = 0;
        PositionValues[31] = -5;
        //Rank 5
        PositionValues[32] = 0;
        PositionValues[33] = 0;
        PositionValues[34] = 5;
        PositionValues[35] = 5;
        PositionValues[36] = 5;
        PositionValues[37] = 5;
        PositionValues[38] = 0;
        PositionValues[39] = -5;
        //Rank 6
        PositionValues[40] = -10;
        PositionValues[41] = 5;
        PositionValues[42] = 5;
        PositionValues[43] = 5;
        PositionValues[44] = 5;
        PositionValues[45] = 5;
        PositionValues[46] = 0;
        PositionValues[47] = -10;
        //Rank 7
        PositionValues[48] = -10;
        PositionValues[49] = 0;
        PositionValues[50] = 5;
        PositionValues[51] = 0;
        PositionValues[52] = 0;
        PositionValues[53] = 0;
        PositionValues[54] = 0;
        PositionValues[55] = -10;
        //Rank 8
        PositionValues[56] = -20;
        PositionValues[57] = -10;
        PositionValues[58] = -10;
        PositionValues[59] = -5;
        PositionValues[60] = -5;
        PositionValues[61] = -10;
        PositionValues[62] = -10;
        PositionValues[63] = -20;
        #endregion

      }
    }

    public override void Initialize( Zobrist hash ) {
      if ( Color == ChessPieceColors.White ) {
        Bits = BoardSquare.D1;
        if ( hash != null ) {
          hash.Update( ChessPieceType.Queen, ChessPieceColors.White, BoardSquare.D1 );
        }
      } else {
        Bits = BoardSquare.D8;
        if ( hash != null ) {
          hash.Update( ChessPieceType.Queen, ChessPieceColors.Black, BoardSquare.D8 );
        }
      }
    }

    protected internal override ColoredBitBoard NewEqualColoredInstance() {
      return new QueenBitBoard( this.Color );
    }
  }

  [Serializable]
    public class PawnBitBoard : ColoredBitBoard {
    public BoardSquare MovedTwoSquares { set; get; }
    public PromotionPiece? Promotion { private set; get; }
    public bool IsPromoted {
      get { return Promotion.HasValue; }
    }
    public override BoardSquare Bits {
      get {
        return base.Bits;
      }
      set {
        if ( Color == ChessPieceColors.White ) {
          BoardSquare ActualMovedPiece = base.Bits ^ value;
          
          /* Find possible en passant moves */
          BoardSquare PawnStartPosition = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
          BoardSquare NonMovedPawns = base.Bits & PawnStartPosition;          
          BoardSquare PossibleTwoSquareMoves = (BoardSquare)( (ulong)NonMovedPawns << 16 );

          /* Verify that the moved piece is one of those at rank 2 */
          BoardSquare oldPos = ActualMovedPiece & base.Bits;

          if ( ( oldPos & PawnStartPosition ) != BoardSquare.Empty )
            MovedTwoSquares = ActualMovedPiece & PossibleTwoSquareMoves;
          else //Required, otherwise if MovedTwoSquares is already set, and the move is undone the property is still set. A2->A4, sets M2SQ = A4. Undo: A4->A2, M2SQ doesn't get set, so it's still A4. Error.
            MovedTwoSquares = BoardSquare.Empty;
        } else if ( Color == ChessPieceColors.Black ) {
          BoardSquare ActualMovedPiece = base.Bits ^ value;

          BoardSquare PawnStartPosition = BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
          BoardSquare NonMovedPawns = base.Bits & PawnStartPosition;
          BoardSquare PossibleTwoSquareMoves = (BoardSquare)( (ulong)NonMovedPawns >> 16 );

          BoardSquare oldPos = ActualMovedPiece & base.Bits;

          if ( ( oldPos & PawnStartPosition ) != BoardSquare.Empty )
            MovedTwoSquares = ActualMovedPiece & PossibleTwoSquareMoves;
          else
            MovedTwoSquares = BoardSquare.Empty;
        } 

        Debug.Assert( new EmptyBitBoard( MovedTwoSquares ).Count <= 1 );
        base.Bits = value;
      }
    }

    public PawnBitBoard( ChessPieceColors color )
      : base( color ) {
      if ( color == ChessPieceColors.White ) {  
        #region WhitePawn
        //Rank 1 starting with H
        PositionValues[0] = 0;
        PositionValues[1] = 0;
        PositionValues[2] = 0;
        PositionValues[3] = 0;
        PositionValues[4] = 0;
        PositionValues[5] = 0;
        PositionValues[6] = 0;
        PositionValues[7] = 0;
        //Rank 2
        PositionValues[8] = 5;
        PositionValues[9] = 10;
        PositionValues[10] = 10;
        PositionValues[11] = -20;
        PositionValues[12] = -20;
        PositionValues[13] = 10;
        PositionValues[14] = 10;
        PositionValues[15] = 5;
        //Rank 3
        PositionValues[16] = 5;
        PositionValues[17] = -5;
        PositionValues[18] = -10;
        PositionValues[19] = 0;
        PositionValues[20] = 0;
        PositionValues[21] = -10;
        PositionValues[22] = -5;
        PositionValues[23] = 5;
        //Rank 4
        PositionValues[24] = 0;
        PositionValues[25] = 0;
        PositionValues[26] = 0;
        PositionValues[27] = 20;
        PositionValues[28] = 20;
        PositionValues[29] = 0;
        PositionValues[30] = 0;
        PositionValues[31] = 0;
        //Rank 5
        PositionValues[32] = 5;
        PositionValues[33] = 5;
        PositionValues[34] = 10;
        PositionValues[35] = 25;
        PositionValues[36] = 25;
        PositionValues[37] = 10;
        PositionValues[38] = 5;
        PositionValues[39] = 5;
        //Rank 6
        PositionValues[40] = 10;
        PositionValues[41] = 10;
        PositionValues[42] = 20;
        PositionValues[43] = 30;
        PositionValues[44] = 30;
        PositionValues[45] = 20;
        PositionValues[46] = 10;
        PositionValues[47] = 10;
        //Rank 7
        PositionValues[48] = 50;
        PositionValues[49] = 50;
        PositionValues[50] = 50;
        PositionValues[51] = 50;
        PositionValues[52] = 50;
        PositionValues[53] = 50;
        PositionValues[54] = 50;
        PositionValues[55] = 50;
        //Rank 8
        PositionValues[56] = 0;
        PositionValues[57] = 0;
        PositionValues[58] = 0;
        PositionValues[59] = 0;
        PositionValues[60] = 0;
        PositionValues[61] = 0;
        PositionValues[62] = 0;
        PositionValues[63] = 0;
        #endregion
      } else {
        #region BlackPawn
        //Rank 1 starting with H
        PositionValues[63] = 0;
        PositionValues[62] = 0;
        PositionValues[61] = 0;
        PositionValues[60] = 0;
        PositionValues[59] = 0;
        PositionValues[58] = 0;
        PositionValues[57] = 0;
        PositionValues[56] = 0;
        //Rank 2
        PositionValues[55] = 5;
        PositionValues[54] = 10;
        PositionValues[53] = 10;
        PositionValues[52] = -20;
        PositionValues[51] = -20;
        PositionValues[50] = 10;
        PositionValues[49] = 10;
        PositionValues[48] = 5;
        //Rank 3
        PositionValues[47] = 5;
        PositionValues[46] = -5;
        PositionValues[45] = -10;
        PositionValues[44] = 0;
        PositionValues[43] = 0;
        PositionValues[42] = -10;
        PositionValues[41] = -5;
        PositionValues[40] = 5;
        //Rank 4
        PositionValues[39] = 0;
        PositionValues[38] = 0;
        PositionValues[37] = 0;
        PositionValues[36] = 20;
        PositionValues[35] = 20;
        PositionValues[34] = 0;
        PositionValues[33] = 0;
        PositionValues[32] = 0;
        //Rank 5
        PositionValues[31] = 5;
        PositionValues[30] = 5;
        PositionValues[29] = 10;
        PositionValues[28] = 25;
        PositionValues[27] = 25;
        PositionValues[26] = 10;
        PositionValues[25] = 5;
        PositionValues[24] = 5;
        //Rank 6
        PositionValues[23] = 10;
        PositionValues[22] = 10;
        PositionValues[21] = 20;
        PositionValues[20] = 30;
        PositionValues[19] = 30;
        PositionValues[18] = 20;
        PositionValues[17] = 10;
        PositionValues[16] = 10;
        //Rank 7
        PositionValues[15] = 50;
        PositionValues[14] = 50;
        PositionValues[13] = 50;
        PositionValues[12] = 50;
        PositionValues[11] = 50;
        PositionValues[10] = 50;
        PositionValues[9] = 50;
        PositionValues[8] = 50;
        //Rank 8
        PositionValues[7] = 0;
        PositionValues[6] = 0;
        PositionValues[5] = 0;
        PositionValues[4] = 0;
        PositionValues[3] = 0;
        PositionValues[2] = 0;
        PositionValues[1] = 0;
        PositionValues[0] = 0;
        #endregion
      }
      Promotion = null;
    }

    public override void Initialize( Zobrist hash ) {
      if ( Color == ChessPieceColors.White ) {
        Bits = ( BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2 );
        if ( hash != null ) {
          hash.Update( ChessPieceType.Pawn, ChessPieceColors.White, BoardSquare.A2 );
          hash.Update( ChessPieceType.Pawn, ChessPieceColors.White, BoardSquare.B2 );
          hash.Update( ChessPieceType.Pawn, ChessPieceColors.White, BoardSquare.C2 );
          hash.Update( ChessPieceType.Pawn, ChessPieceColors.White, BoardSquare.D2 );
          hash.Update( ChessPieceType.Pawn, ChessPieceColors.White, BoardSquare.E2 );
          hash.Update( ChessPieceType.Pawn, ChessPieceColors.White, BoardSquare.F2 );
          hash.Update( ChessPieceType.Pawn, ChessPieceColors.White, BoardSquare.G2 );
          hash.Update( ChessPieceType.Pawn, ChessPieceColors.White, BoardSquare.H2 );
        }
      } else {
        Bits = ( BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7 );
        if ( hash != null ) {
          hash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, BoardSquare.A2 );
          hash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, BoardSquare.B2 );
          hash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, BoardSquare.C2 );
          hash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, BoardSquare.D2 );
          hash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, BoardSquare.E2 );
          hash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, BoardSquare.F2 );
          hash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, BoardSquare.G2 );
          hash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, BoardSquare.H2 );
        }
      }
      MovedTwoSquares = BoardSquare.Empty;
    }

    public void ResetMovedTwoSquares( Zobrist hash ) {
      if ( hash != null && MovedTwoSquares != BoardSquare.Empty ) {
        hash.Update( ChessPieceType.Pawn, Color, enPassant: MovedTwoSquares );
      }
      /* Have to update hash before as the piece that has to be reset is in MovedTwoSquares */
      MovedTwoSquares = BoardSquare.Empty;
    }

    /// <summary>
    /// Promotes the rank 8 pawn in this bitboard to a specific piece
    /// </summary> 
    public void Promote( PromotionPiece pieceType ) {
      Promotion = pieceType;
    }

    protected internal override ColoredBitBoard NewEqualColoredInstance() {
      return new PawnBitBoard( this.Color );
    }

    public enum PromotionPiece {
      Queen,
      Knight,
      Rook,
      Bishop,
    }
  }

}

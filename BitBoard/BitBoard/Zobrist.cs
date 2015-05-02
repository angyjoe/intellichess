using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  public enum ZobristCastling { KingSideRookMoved = 0, QueenSideRookMoved, KingMoved, CastlingDone }

  [Serializable]
  public class Zobrist {
    private static Random rand = new Random( 1337 );

    private ulong[,,] _pieces = new ulong[6, 2, 64];
    private ulong[,] _castling = new ulong[2, 4]; //[farver][kingsideRookMoved, queensideRookMoved, KingMoved, castleMade]
    private ulong[,] _enPassant = new ulong[2, 64];
    private ulong _side;

#if DEBUG
    public bool[,,] _debugPieces = new bool[6, 2, 64];
    public bool[,] _debugCastling = new bool[2, 4];
    public bool[,] _debugEnPassant = new bool[2, 64];
    public bool _debugSide;

    public Zobrist Debug_Diff_COMP;


    public IEnumerable<Tuple<ChessPieceColors, ChessPieceType, BoardSquare, int>> Debug_Diff_Pieces {
      get {
        for ( int i = 0; i < _debugPieces.GetLength( 0 ); i++ )
        for ( int j = 0; j < _debugPieces.GetLength( 1 ); j++ )
        for ( int k = 0; k < _debugPieces.GetLength( 2 ); k++ ) {
          if ( _debugPieces[i, j, k] != Debug_Diff_COMP._debugPieces[i, j, k] )
            yield return new Tuple<ChessPieceColors, ChessPieceType, BoardSquare, int>(
              j == 0 ? ChessPieceColors.White : ChessPieceColors.Black,
              (ChessPieceType)i,
              (BoardSquare)( (ulong)1 << k ), 
              k );
        }
      }
    }
    
    public IEnumerable<Tuple<ChessPieceColors, ZobristCastling>> Debug_Diff_Castling {
      get {
        for ( int i = 0; i < _debugCastling.GetLength( 0 ); i++ )
        for ( int j = 0; j < _debugCastling.GetLength( 1 ); j++ )
          if ( _debugCastling[i, j] != Debug_Diff_COMP._debugCastling[i, j] )
            yield return new Tuple<ChessPieceColors, ZobristCastling>(
              i == 0 ? ChessPieceColors.White : ChessPieceColors.Black,
              (ZobristCastling)j );
      }
    }

    public IEnumerable<Tuple<ChessPieceColors, BoardSquare>> Debug_Diff_EnPassant {
      get {
        for ( int i = 0; i < _debugEnPassant.GetLength( 0 ); i++ )
        for ( int j = 0; j < _debugEnPassant.GetLength( 1 ); j++ )
          if ( _debugEnPassant[i, j] != Debug_Diff_COMP._debugEnPassant[i, j] )
            yield return new Tuple<ChessPieceColors, BoardSquare>(
              i == 0 ? ChessPieceColors.White : ChessPieceColors.Black,
              (BoardSquare)( (ulong)1 << j ) );
      }
    }    
#endif

    public ulong Key { private set; get; }

    public Zobrist( bool init = true ) {
      if ( init )
        Initialize();
    }

    /// <summary>
    /// Initializes with pseudo random numbers (should be the same numbers everytime if the seed doesn't change)
    /// As if empty chessboard.
    /// Needs to be set for chessboard start state before usage.
    /// </summary>
    private void Initialize() {
      int dim0 = _pieces.GetLength( 0 );
      int dim1 = _pieces.GetLength( 1 );
      int dim2 = _pieces.GetLength( 2 );

      for ( int i = 0; i < dim0; i++ )
        for ( int j = 0; j < dim1; j++ )
          for ( int k = 0; k < dim2; k++ ) {
            _pieces[i, j, k] = RandomInt64();
          }
      for ( int i = 0; i < dim1; i++ )
        for ( int j = 0; j < _castling.GetLength( 1 ); j++ ) {
          _castling[i, j] = RandomInt64();
        }
      for ( int i = 0; i < dim1; i++ )
        for ( int j = 0; j < dim2; j++ ) {
          _enPassant[i, j] = RandomInt64();
        }
      _side = RandomInt64();


      /* Init Key */
      for ( int i = 0; i < dim0; i++ )
        for ( int j = 0; j < dim1; j++ )
          for ( int k = 0; k < dim2; k++ ) {
            Key ^= _pieces[i, j, k];
#if DEBUG
            _debugPieces[i, j, k] = false;
#endif
          }
      for ( int i = 0; i < dim1; i++ )
        for ( int j = 0; j < _castling.GetLength( 1 ); j++ ) {
          Key ^= _castling[i, j];
#if DEBUG
          _debugCastling[i, j] = false;
#endif
        }
      for ( int i = 0; i < dim1; i++ )
        for ( int j = 0; j < dim2; j++ ) {
          Key ^= _enPassant[i, j];
#if DEBUG
          _debugEnPassant[i, j] = false;
#endif
        }

      Key ^= _side;
#if DEBUG
      _debugSide = false;
#endif
    }

    /// <summary>
    /// 
    /// </summary> 
    /// <param name="availableCastles">At init : both. queen side rook moves: KingSide or king side rook moves: QueenSide. If both rooks moved or king moved: none.
    /// The value passed here is the CHANGE, if one rook is moved, that is passed, if king is moved none is passed, if neither both is passed.</param>
    /// <param name="squareFrom">null if no from move</param>
    public void Update(
      ChessPieceType pieceType,
      ChessPieceColors color,
      BoardSquare? squareTo = null,
      BoardSquare? squareFrom = null,
      ZobristCastling? availableCastles = null,
      BoardSquare? enPassant = null
      ) {
      int colorMoving = color == ChessPieceColors.White ? 0 : 1;
      if ( squareFrom.HasValue ) {
        /* Remove bit from previous position */
        int squareIndexFrom = GetIndexFromBoardSquare( squareFrom.Value );
        ulong keyd = _pieces[(int)pieceType, colorMoving, squareIndexFrom];
        Key ^= _pieces[(int)pieceType, colorMoving, squareIndexFrom];
#if DEBUG
        _debugPieces[(int)pieceType, colorMoving, squareIndexFrom] = !_debugPieces[(int)pieceType, colorMoving, squareIndexFrom];
#endif
      }
      if ( squareTo.HasValue ) {
        /* Add bit from new position */
        int squareIndexTo = GetIndexFromBoardSquare( squareTo.Value );
        ulong keyd = _pieces[(int)pieceType, colorMoving, squareIndexTo];
        Key ^= _pieces[(int)pieceType, colorMoving, squareIndexTo];
#if DEBUG
        _debugPieces[(int)pieceType, colorMoving, squareIndexTo] = !_debugPieces[(int)pieceType, colorMoving, squareIndexTo];
#endif
      }

      if ( enPassant.HasValue ) {
        int squareIndexEnPassant = GetIndexFromBoardSquare( enPassant.Value );
        ulong keyd = _enPassant[colorMoving, squareIndexEnPassant];
        Key ^= _enPassant[colorMoving, squareIndexEnPassant];
#if DEBUG
        _debugEnPassant[colorMoving, squareIndexEnPassant] = !_debugEnPassant[colorMoving, squareIndexEnPassant];
#endif
      }
      if ( availableCastles.HasValue ) {
        ulong keyd = _castling[colorMoving, (int)availableCastles.Value];
        Key ^= _castling[colorMoving, (int)availableCastles.Value];
#if DEBUG
        _debugCastling[colorMoving, (int)availableCastles.Value] = !_debugCastling[colorMoving, (int)availableCastles.Value];
#endif
      }
    }
    public void Update(
      ChessPieceType pieceType,
      ColoredBitBoard oldBoard,
      ColoredBitBoard newBoard ) {
      ulong difference = (ulong)oldBoard.Bits ^ (ulong)newBoard.Bits;
      ulong from = difference & (ulong)oldBoard.Bits;
      ulong to = difference & (ulong)newBoard.Bits;

      Update( pieceType, oldBoard.Color, (BoardSquare)to, (BoardSquare)from );
    }

    public static int GetIndexFromBoardSquare( BoardSquare square ) {
      var eb = new EmptyBitBoard();
      eb.Bits = square;
      System.Diagnostics.Debug.Assert( eb.Count == 1 );
      ulong nb = (ulong)square;
      return Convert.ToInt32( Math.Log( (double)square, 2 ) );
    }

    public ulong RandomInt64() {
      var buffer = new byte[sizeof( ulong )];
      rand.NextBytes( buffer );
      return BitConverter.ToUInt64( buffer, 0 );
    }

    public Zobrist Clone() {
      Zobrist c = new Zobrist( false );
#if DEBUG
      c._debugCastling = new bool[_debugCastling.GetLength( 0 ), _debugCastling.GetLength( 1 )];
      for ( int i = 0; i < _debugCastling.GetLength( 0 ); i++ )
        for ( int j = 0; j < _debugCastling.GetLength( 1 ); j++ )
          c._debugCastling[i, j] = _debugCastling[i, j];

      c._debugEnPassant = new bool[_debugEnPassant.GetLength( 0 ), _debugEnPassant.GetLength( 1 )];
      for ( int i = 0; i < _debugEnPassant.GetLength( 0 ); i++ )
        for ( int j = 0; j < _debugEnPassant.GetLength( 1 ); j++ )
          c._debugEnPassant[i, j] = _debugEnPassant[i, j];

      c._debugPieces = new bool[_debugPieces.GetLength( 0 ), _debugPieces.GetLength( 1 ), _debugPieces.GetLength( 2 )];
      for ( int i = 0; i < _debugPieces.GetLength( 0 ); i++ )
        for ( int j = 0; j < _debugPieces.GetLength( 1 ); j++ )
          for ( int k = 0; k < _debugPieces.GetLength( 2 ); k++ )
            c._debugPieces[i, j, k] = _debugPieces[i, j, k];

      c._debugSide = _debugSide;
#endif
      c._castling = new ulong[_castling.GetLength( 0 ), _castling.GetLength( 1 )];
      for ( int i = 0; i < _castling.GetLength( 0 ); i++ )
        for ( int j = 0; j < _castling.GetLength( 1 ); j++ )
          c._castling[i, j] = _castling[i, j];

      c._enPassant = new ulong[_enPassant.GetLength( 0 ), _enPassant.GetLength( 1 )];
      for ( int i = 0; i < _enPassant.GetLength( 0 ); i++ )
        for ( int j = 0; j < _enPassant.GetLength( 1 ); j++ )
          c._enPassant[i, j] = _enPassant[i, j];
      
      c._pieces = new ulong[_pieces.GetLength( 0 ), _pieces.GetLength( 1 ), _pieces.GetLength( 2 )];
      for ( int i = 0; i < _pieces.GetLength( 0 ); i++ )
        for ( int j = 0; j < _pieces.GetLength( 1 ); j++ )
          for ( int k = 0; k < _pieces.GetLength( 2 ); k++ )
            c._pieces[i, j, k] = _pieces[i, j, k];

      c.Key = Key;
      c._side = _side;      

      return c;
    }
  }
}

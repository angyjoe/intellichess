using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  public enum ChessPieceType { King = 0, Queen, Rook, Knight, Bishop, Pawn }
  public enum ChessPieceColors { Black = -1, White = 1 }


  [Flags]
  public enum BoardSquare : ulong {
    Empty = 0,
    Full = ulong.MaxValue,
    A8 = (ulong)1 << 63, B8 = (ulong)1 << 62, C8 = (ulong)1 << 61, D8 = (ulong)1 << 60, E8 = (ulong)1 << 59, F8 = (ulong)1 << 58, G8 = (ulong)1 << 57, H8 = (ulong)1 << 56,
    A7 = (ulong)1 << 55, B7 = (ulong)1 << 54, C7 = (ulong)1 << 53, D7 = (ulong)1 << 52, E7 = (ulong)1 << 51, F7 = (ulong)1 << 50, G7 = (ulong)1 << 49, H7 = (ulong)1 << 48,
    A6 = (ulong)1 << 47, B6 = (ulong)1 << 46, C6 = (ulong)1 << 45, D6 = (ulong)1 << 44, E6 = (ulong)1 << 43, F6 = (ulong)1 << 42, G6 = (ulong)1 << 41, H6 = (ulong)1 << 40,
    A5 = (ulong)1 << 39, B5 = (ulong)1 << 38, C5 = (ulong)1 << 37, D5 = (ulong)1 << 36, E5 = (ulong)1 << 35, F5 = (ulong)1 << 34, G5 = (ulong)1 << 33, H5 = (ulong)1 << 32,
    A4 = (ulong)1 << 31, B4 = (ulong)1 << 30, C4 = (ulong)1 << 29, D4 = (ulong)1 << 28, E4 = (ulong)1 << 27, F4 = (ulong)1 << 26, G4 = (ulong)1 << 25, H4 = (ulong)1 << 24,
    A3 = (ulong)1 << 23, B3 = (ulong)1 << 22, C3 = (ulong)1 << 21, D3 = (ulong)1 << 20, E3 = (ulong)1 << 19, F3 = (ulong)1 << 18, G3 = (ulong)1 << 17, H3 = (ulong)1 << 16,
    A2 = (ulong)1 << 15, B2 = (ulong)1 << 14, C2 = (ulong)1 << 13, D2 = (ulong)1 << 12, E2 = (ulong)1 << 11, F2 = (ulong)1 << 10, G2 = (ulong)1 << 09, H2 = (ulong)1 << 08,
    A1 = (ulong)1 << 07, B1 = (ulong)1 << 06, C1 = (ulong)1 << 05, D1 = (ulong)1 << 04, E1 = (ulong)1 << 03, F1 = (ulong)1 << 02, G1 = (ulong)1 << 01, H1 = (ulong)1 << 00,
  }

  public enum ChessBoardGameState {
    Draw,
    BlackMate,
    WhiteMate,
    Running,
  }

  public enum ChessBoardGameStage {
    /// <summary>
    /// Opening book running
    /// </summary>
    Early,
    /// <summary>
    /// Move generation
    /// </summary>
    Middle,
    /// <summary>
    /// Closing book running 
    /// </summary>
    Late,
  }

  public class ScenarioPlace {
    public BoardSquare Square { set; get; }
    public ChessPieceType Piece { set; get; }
    public ChessPieceColors Color { set; get; }

    public ScenarioPlace( BoardSquare square, ChessPieceType piece, ChessPieceColors color ) {
      Square = square;
      Piece = piece;
      Color = color;
    }
  }

  public class ScenarioList : List<ScenarioPlace> {
    public void Add( BoardSquare square, ChessPieceType piece, ChessPieceColors color ) {
      this.Add( new ScenarioPlace( square, piece, color ) );
    }
  }

  [Serializable]
  public class ChessBoard {
    private Stack<Tuple<ColoredBitBoard, ColoredBitBoard>> MoveHistory = new Stack<Tuple<ColoredBitBoard, ColoredBitBoard>>();

    private const BoardSquare WHITE_SQUARES = (
      BoardSquare.A2 | BoardSquare.A4 | BoardSquare.A6 | BoardSquare.A8 |
      BoardSquare.B1 | BoardSquare.B3 | BoardSquare.B5 | BoardSquare.B7 |
      BoardSquare.C2 | BoardSquare.C4 | BoardSquare.C6 | BoardSquare.C8 |
      BoardSquare.D1 | BoardSquare.D3 | BoardSquare.D5 | BoardSquare.D7 |
      BoardSquare.E2 | BoardSquare.E4 | BoardSquare.E6 | BoardSquare.E8 |
      BoardSquare.F1 | BoardSquare.F3 | BoardSquare.F5 | BoardSquare.F7 |
      BoardSquare.G2 | BoardSquare.G4 | BoardSquare.G6 | BoardSquare.G8 |
      BoardSquare.H1 | BoardSquare.H3 | BoardSquare.H5 | BoardSquare.H7 );
    private const BoardSquare BLACK_SQUARES = BoardSquare.Full ^ WHITE_SQUARES;


    private int _movecount;
    public int MoveCount {
      get { return _movecount; }
      set { _movecount = value; }
    }
    public KingBitBoard WhiteKing { private set; get; }
    public QueenBitBoard WhiteQueen { private set; get; }
    public RookBitBoard WhiteRook { private set; get; }
    public BishopBitBoard WhiteBishop { private set; get; }
    public KnightBitBoard WhiteKnight { private set; get; }
    public PawnBitBoard WhitePawn { private set; get; }

    public KingBitBoard BlackKing { private set; get; }
    public QueenBitBoard BlackQueen { private set; get; }
    public RookBitBoard BlackRook { private set; get; }
    public BishopBitBoard BlackBishop { private set; get; }
    public KnightBitBoard BlackKnight { private set; get; }
    public PawnBitBoard BlackPawn { private set; get; }
    private ChessBoardGameState? _state = null;
    public ChessBoardGameState State {
      get {
        if ( _state != null && _state.HasValue ) return _state.Value;

        #region Draw
        if ( MoveCount >= 50 * 2 ) return ChessBoardGameState.Draw; /* each side 50 moves */

        if ( WhitePawn.Bits == BoardSquare.Empty &&
             WhiteQueen.Bits == BoardSquare.Empty &&
             WhiteRook.Bits == BoardSquare.Empty &&
             BlackPawn.Bits == BoardSquare.Empty &&
             BlackQueen.Bits == BoardSquare.Empty &&
             BlackRook.Bits == BoardSquare.Empty ) {
          if ( WhiteBishop.Bits == BoardSquare.Empty &&
               WhiteKnight.Bits == BoardSquare.Empty &&
               BlackBishop.Bits == BoardSquare.Empty &&
               BlackKnight.Bits == BoardSquare.Empty ) {
            return ChessBoardGameState.Draw;
          }
          if ( WhiteKnight.Bits == BoardSquare.Empty &&
               BlackKnight.Bits == BoardSquare.Empty &&
               ( ( BlackBishop.Bits == BoardSquare.Empty && WhiteBishop.Count == 1 ) || ( WhiteBishop.Bits == BoardSquare.Empty && BlackBishop.Count == 1 ) ) ) {
            return ChessBoardGameState.Draw;
          }
          if ( WhiteBishop.Bits == BoardSquare.Empty &&
               BlackBishop.Bits == BoardSquare.Empty &&
               ( ( BlackKnight.Bits == BoardSquare.Empty && WhiteKnight.Count == 1 ) || ( WhiteKnight.Bits == BoardSquare.Empty && BlackKnight.Count == 1 ) ) ) {
            return ChessBoardGameState.Draw;
          }
          if ( WhiteKnight.Bits == BoardSquare.Empty &&
               BlackKnight.Bits == BoardSquare.Empty &&
                ( /* Both bishops on black squares */
                  ( ( ( WhiteBishop.Bits & WHITE_SQUARES ) == BoardSquare.Empty && ( WhiteBishop.Bits & BLACK_SQUARES ) != BoardSquare.Empty ) &&  //White bishops on black squares
                  ( ( BlackBishop.Bits & WHITE_SQUARES ) == BoardSquare.Empty && ( BlackBishop.Bits & BLACK_SQUARES ) != BoardSquare.Empty ) )     //Black bishops on black squares
                  ||
            /* Both bishops on white squares */
                  ( ( ( WhiteBishop.Bits & BLACK_SQUARES ) == BoardSquare.Empty && ( WhiteBishop.Bits & WHITE_SQUARES ) != BoardSquare.Empty ) &&  //White bishops on white squares
                  ( ( BlackBishop.Bits & BLACK_SQUARES ) == BoardSquare.Empty && ( WhiteBishop.Bits & WHITE_SQUARES ) != BoardSquare.Empty ) )     //Black bishops on white squares
                ) ) {
            return ChessBoardGameState.Draw;
          }
        }
        #endregion

        /* IF not draw or checkmate, then the game is running as usual */
        return ChessBoardGameState.Running;
      }
      set {
        _state = value;
      }
    }
    private ChessBoardGameStage _stage;
    public ChessBoardGameStage Stage {
      get { return _stage; }
    }
    public Zobrist BoardHash { set; get; }

    private int _pieceCount;

    public ChessBoard() {
      WhiteKing = new KingBitBoard( ChessPieceColors.White );
      WhiteQueen = new QueenBitBoard( ChessPieceColors.White );
      WhiteRook = new RookBitBoard( ChessPieceColors.White );
      WhiteBishop = new BishopBitBoard( ChessPieceColors.White );
      WhiteKnight = new KnightBitBoard( ChessPieceColors.White );
      WhitePawn = new PawnBitBoard( ChessPieceColors.White );

      BlackKing = new KingBitBoard( ChessPieceColors.Black );
      BlackQueen = new QueenBitBoard( ChessPieceColors.Black );
      BlackRook = new RookBitBoard( ChessPieceColors.Black );
      BlackBishop = new BishopBitBoard( ChessPieceColors.Black );
      BlackKnight = new KnightBitBoard( ChessPieceColors.Black );
      BlackPawn = new PawnBitBoard( ChessPieceColors.Black );
      BoardHash = new Zobrist();
    }

    public void InitializeGame() {
      WhiteKing.Initialize( BoardHash );
      WhiteQueen.Initialize( BoardHash );
      WhiteRook.Initialize( BoardHash );
      WhiteBishop.Initialize( BoardHash );
      WhiteKnight.Initialize( BoardHash );
      WhitePawn.Initialize( BoardHash );

      BlackKing.Initialize( BoardHash );
      BlackQueen.Initialize( BoardHash );
      BlackRook.Initialize( BoardHash );
      BlackBishop.Initialize( BoardHash );
      BlackKnight.Initialize( BoardHash );
      BlackPawn.Initialize( BoardHash );

      _movecount = 0;
      _state = null;
      BoardHash = new Zobrist();
    }

    private void InitializeBoardHash() {

    }

    public void InitializeScenario( List<ScenarioPlace> Pieces ) {
      if ( Pieces == null || Pieces.Count == 0 ) {
        WhiteKing.Clear();
        WhiteQueen.Clear();
        WhiteRook.Clear();
        WhiteBishop.Clear();
        WhiteKnight.Clear();
        WhitePawn.Clear();

        BlackKing.Clear();
        BlackQueen.Clear();
        BlackRook.Clear();
        BlackBishop.Clear();
        BlackKnight.Clear();
        BlackPawn.Clear();
      } else {
        var val1 = Pieces.Distinct( new ChessPieceSquareEqualityComparer() ).Count();
        var val2 = Pieces.Count();
        if ( val1 < val2 ) {
          throw new IllegalPiecePlacementException( "Multiple pieces on same location" );
        }

        foreach ( ScenarioPlace currPiece in Pieces ) {
          switch ( currPiece.Piece ) {
            case ChessPieceType.King:
              if ( currPiece.Color == ChessPieceColors.White )
                WhiteKing.Bits |= currPiece.Square;
              else
                BlackKing.Bits |= currPiece.Square;
              break;
            case ChessPieceType.Queen:
              if ( currPiece.Color == ChessPieceColors.White )
                WhiteQueen.Bits |= currPiece.Square;
              else
                BlackQueen.Bits |= currPiece.Square;
              break;
            case ChessPieceType.Rook:
              if ( currPiece.Color == ChessPieceColors.White )
                WhiteRook.Bits |= currPiece.Square;
              else
                BlackRook.Bits |= currPiece.Square;
              break;
            case ChessPieceType.Knight:
              if ( currPiece.Color == ChessPieceColors.White )
                WhiteKnight.Bits |= currPiece.Square;
              else
                BlackKnight.Bits |= currPiece.Square;
              break;
            case ChessPieceType.Bishop:
              if ( currPiece.Color == ChessPieceColors.White )
                WhiteBishop.Bits |= currPiece.Square;
              else
                BlackBishop.Bits |= currPiece.Square;
              break;
            case ChessPieceType.Pawn:
              if ( currPiece.Color == ChessPieceColors.White )
                WhitePawn.Bits |= currPiece.Square;
              else
                BlackPawn.Bits |= currPiece.Square;
              break;
          }
        }
      }
    }

    public ColoredBitBoard GetBitBoardFromSquare( BoardSquare boardSquare, ChessPieceColors engineColor ) {
      if ( engineColor == ChessPieceColors.White ) {
        if ( ( WhiteKing.Bits & boardSquare ) != BoardSquare.Empty ) {
          return WhiteKing.DeepCopy();
        } else if ( ( WhiteQueen.Bits & boardSquare ) != BoardSquare.Empty ) {
          return WhiteQueen.DeepCopy();
        } else if ( ( WhiteRook.Bits & boardSquare ) != BoardSquare.Empty ) {
          return WhiteRook.DeepCopy();
        } else if ( ( WhiteBishop.Bits & boardSquare ) != BoardSquare.Empty ) {
          return WhiteBishop.DeepCopy();
        } else if ( ( WhiteKnight.Bits & boardSquare ) != BoardSquare.Empty ) {
          return WhiteKnight.DeepCopy();
        } else if ( ( WhitePawn.Bits & boardSquare ) != BoardSquare.Empty ) {
          return WhitePawn.DeepCopy();
        }
      } else {
        if ( ( BlackKing.Bits & boardSquare ) != BoardSquare.Empty ) {
          return BlackKing.DeepCopy();
        } else if ( ( BlackQueen.Bits & boardSquare ) != BoardSquare.Empty ) {
          return BlackQueen.DeepCopy();
        } else if ( ( BlackRook.Bits & boardSquare ) != BoardSquare.Empty ) {
          return BlackRook.DeepCopy();
        } else if ( ( BlackBishop.Bits & boardSquare ) != BoardSquare.Empty ) {
          return BlackBishop.DeepCopy();
        } else if ( ( BlackKnight.Bits & boardSquare ) != BoardSquare.Empty ) {
          return BlackKnight.DeepCopy();
        } else if ( ( BlackPawn.Bits & boardSquare ) != BoardSquare.Empty ) {
          return BlackPawn.DeepCopy();
        }
      }
      return null;
    }

    /// <summary>
    /// @ ConvertBitBoardMoveToString
    /// Gets the current BitBoard representing the given bitboard.
    /// Used to on a new bitboard before calling Update to get the bitboard representing
    /// the state of the given bitboard before a move, while the input board represents
    /// the bitboard after the move.
    /// </summary>
    public ColoredBitBoard GetOldBitBoardFromBitBoard( ColoredBitBoard board ) {
      Dictionary<Type, Func<ColoredBitBoard>> @switch = new Dictionary<Type, Func<ColoredBitBoard>>() {
        { typeof(KingBitBoard), () => { 
            if ( board.Color == ChessPieceColors.White ) {
              return WhiteKing;
            } else {
              return BlackKing;
            }  
        } }, 
        { typeof(QueenBitBoard), () => {
          if ( board.Color == ChessPieceColors.White ) {
            return WhiteQueen;
          } else {
            return BlackQueen;
          } 
        } },   
        { typeof(RookBitBoard), () => {
          if ( board.Color == ChessPieceColors.White ) {
            return WhiteRook;
          } else {
            return BlackRook;
          } 
        } },   
        { typeof(BishopBitBoard), () => {
          if ( board.Color == ChessPieceColors.White ) {
            return WhiteBishop;
          } else {
            return BlackBishop;
          } 
        } },   
        { typeof(KnightBitBoard), () => {
          if ( board.Color == ChessPieceColors.White ) {
            return WhiteKnight;
          } else {
            return BlackKnight;
          } 
        } },  
        { typeof(PawnBitBoard), () => {
          if ( board.Color == ChessPieceColors.White ) {
            return WhitePawn;
          } else {
            return BlackPawn;
          }
            
        } }       
      };

      return @switch[board.GetType()]().DeepCopy();
    }

    private const BoardSquare WHITE_KING_START_POSITION = BoardSquare.E1;
    private const BoardSquare BLACK_KING_START_POSITION = BoardSquare.E8;
    private const BoardSquare WHITE_ROOK_KINGSIDE_START_POSITION = BoardSquare.H1;
    private const BoardSquare WHITE_ROOK_QUEENSIDE_START_POSITION = BoardSquare.A1;
    private const BoardSquare BLACK_ROOK_KINGSIDE_START_POSITION = BoardSquare.H8;
    private const BoardSquare BLACK_ROOK_QUEENSIDE_START_POSITION = BoardSquare.A8;
    private const BoardSquare WHITE_ROOK_SHORT_CASTLING = BoardSquare.F1;
    private const BoardSquare WHITE_ROOK_LONG_CASTLING = BoardSquare.D1;
    private const BoardSquare BLACK_ROOK_SHORT_CASTLING = BoardSquare.F8;
    private const BoardSquare BLACK_ROOK_LONG_CASTLING = BoardSquare.D8;

    public void Undo() {
      Zobrist oldhash = BoardHash.Clone();
      MoveCount--;
      if ( MoveHistory.Count == 0 ) {
        throw new InvalidOperationException( "No moves to undo" );
      }
      Tuple<ColoredBitBoard, ColoredBitBoard> currentAndPreviousPosition = MoveHistory.Pop();
      ColoredBitBoard moveCoordinate = FindMoveCoordinate( currentAndPreviousPosition );
      ColoredBitBoard currentPosition = currentAndPreviousPosition.Item1;
      ColoredBitBoard previousPosition = currentAndPreviousPosition.Item2;
      ColoredBitBoard peekMove;//Enemy last move

      if ( MoveHistory.Count() != 0 ) {
        peekMove = MoveHistory.Peek().Item1;
      } else {
        peekMove = null;
      }


      //If previous move did capture a piece
      if ( currentPosition.JustCaptured != null ) {
        //Check if previous move did capture a piece through En Passant
        if ( ( currentPosition is PawnBitBoard ) && ( peekMove is PawnBitBoard ) && ( currentPosition.JustCaptured == ChessPieceType.Pawn ) ) {
          PawnBitBoard peekPawnMove = (PawnBitBoard)peekMove;

          Debug.Assert( new EmptyBitBoard( peekPawnMove.MovedTwoSquares ).Count <= 1 );
          if ( ( currentPosition.Color == ChessPieceColors.White ) && ( (BoardSquare)( (ulong)moveCoordinate.Bits >> 8 ) & peekPawnMove.MovedTwoSquares ) != BoardSquare.Empty ) {
            PawnBitBoard enPassantCaptureCoordinate = new PawnBitBoard( ChessPieceColors.White );
            enPassantCaptureCoordinate.JustCaptured = ChessPieceType.Pawn;
            enPassantCaptureCoordinate.Bits = peekPawnMove.MovedTwoSquares;
            AddCapturedPiece( enPassantCaptureCoordinate );
            //BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, enPassant: enPassantCaptureCoordinate.Bits );
          } else if ( ( currentPosition.Color == ChessPieceColors.Black ) && ( (BoardSquare)( (ulong)moveCoordinate.Bits << 8 ) & peekPawnMove.MovedTwoSquares ) != BoardSquare.Empty ) {
            PawnBitBoard enPassantCaptureCoordinate = new PawnBitBoard( ChessPieceColors.Black );
            enPassantCaptureCoordinate.JustCaptured = ChessPieceType.Pawn;
            enPassantCaptureCoordinate.Bits = peekPawnMove.MovedTwoSquares;
            AddCapturedPiece( enPassantCaptureCoordinate );
            //BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.White, enPassant: enPassantCaptureCoordinate.Bits );
          } else {
            // If previous move was not an En Passant
            AddCapturedPiece( moveCoordinate );
          }
        } else {
          AddCapturedPiece( moveCoordinate );
        }
      }

      //Replace the ColoredBitBoard "current-engine" with "previousPosition"  - To Undo the last move 
      Dictionary<Type, Action> @switch = new Dictionary<Type, Action>() {
        { typeof(KingBitBoard), () => {
          if ( previousPosition.Color == ChessPieceColors.White ) {            
            if (previousPosition.Bits == WHITE_KING_START_POSITION){
              if ( (WhiteKing.Bits == WHITE_KING_CASTLING_SHORT) ){
                WhiteRook.Bits ^= WHITE_ROOK_SHORT_CASTLING;
                WhiteRook.Bits |= WHITE_ROOK_KINGSIDE_START_POSITION;
                WhiteRook.HasMovedKingSide = false;

                BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.White, WHITE_ROOK_KINGSIDE_START_POSITION, WHITE_ROOK_SHORT_CASTLING, ZobristCastling.CastlingDone );
                Debug.Assert( previousPosition.Bits == BoardSquare.E1 );
                Debug.Assert( currentPosition.Bits == BoardSquare.G1 );
              } else if ((WhiteKing.Bits == WHITE_KING_CASTLING_LONG)) {
                WhiteRook.Bits ^= WHITE_ROOK_LONG_CASTLING;
                WhiteRook.Bits |= WHITE_ROOK_QUEENSIDE_START_POSITION;
                WhiteRook.HasMovedQueenSide = false;

                BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.White, WHITE_ROOK_QUEENSIDE_START_POSITION, WHITE_ROOK_LONG_CASTLING, ZobristCastling.CastlingDone );
                Debug.Assert( previousPosition.Bits == BoardSquare.E1 );
                Debug.Assert( currentPosition.Bits == BoardSquare.C1 );
              } else {
                if ( WhiteKing.HasMoved && !( (KingBitBoard)previousPosition ).HasMoved )
                  BoardHash.Update( ChessPieceType.King, ChessPieceColors.White, availableCastles: ZobristCastling.KingMoved );

              }
            } else {
              if ( WhiteKing.HasMoved && !( (KingBitBoard)previousPosition ).HasMoved )
                BoardHash.Update( ChessPieceType.King, ChessPieceColors.White, availableCastles: ZobristCastling.KingMoved );

            }            
            WhiteKing.Bits = previousPosition.Bits;            
            WhiteKing.HasMoved = ( (KingBitBoard)previousPosition ).HasMoved;
            
            BoardHash.Update( ChessPieceType.King, ChessPieceColors.White, previousPosition.Bits, currentPosition.Bits );
          } else if ( previousPosition.Color == ChessPieceColors.Black ) {
            if (previousPosition.Bits == BLACK_KING_START_POSITION){
              if ( (BlackKing.Bits == BLACK_KING_CASTLING_SHORT) ){
                BlackRook.Bits ^= BLACK_ROOK_SHORT_CASTLING;
                BlackRook.Bits |= BLACK_ROOK_KINGSIDE_START_POSITION;
                BlackRook.HasMovedKingSide = false;
                
                BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.Black, BLACK_ROOK_KINGSIDE_START_POSITION, BLACK_ROOK_SHORT_CASTLING, ZobristCastling.CastlingDone );
                Debug.Assert( previousPosition.Bits == BoardSquare.E8 );
                Debug.Assert( currentPosition.Bits == BoardSquare.G8 );
              } else if ((BlackKing.Bits == BLACK_KING_CASTLING_LONG)){
                BlackRook.Bits ^= BLACK_ROOK_LONG_CASTLING;
                BlackRook.Bits |= BLACK_ROOK_QUEENSIDE_START_POSITION;
                BlackRook.HasMovedQueenSide = false;
                
                BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.Black, BLACK_ROOK_QUEENSIDE_START_POSITION, BLACK_ROOK_LONG_CASTLING, ZobristCastling.CastlingDone );
                Debug.Assert( previousPosition.Bits == BoardSquare.E8 );
                Debug.Assert( currentPosition.Bits == BoardSquare.C8 );
              } else {
                if ( BlackKing.HasMoved && !( (KingBitBoard)previousPosition ).HasMoved )
                  BoardHash.Update( ChessPieceType.King, ChessPieceColors.Black, availableCastles: ZobristCastling.KingMoved );
              }
            } else {
              if ( BlackKing.HasMoved && !( (KingBitBoard)previousPosition ).HasMoved )
                BoardHash.Update( ChessPieceType.King, ChessPieceColors.Black, availableCastles: ZobristCastling.KingMoved );
            }            
            BlackKing.Bits = previousPosition.Bits;
            BlackKing.HasMoved = ( (KingBitBoard)previousPosition ).HasMoved;
            
            BoardHash.Update( ChessPieceType.King, ChessPieceColors.Black, previousPosition.Bits, currentPosition.Bits );
            }
        } }, 
        { typeof(QueenBitBoard), () => {
          if ( previousPosition.Color == ChessPieceColors.White ) {
            WhiteQueen.Bits = previousPosition.Bits;
            Debug.Assert( currentPosition.Color == ChessPieceColors.White );
            BoardHash.Update( ChessPieceType.Queen, currentPosition, previousPosition );
          } else if ( previousPosition.Color == ChessPieceColors.Black ) {
            BlackQueen.Bits = previousPosition.Bits;
            Debug.Assert( currentPosition.Color == ChessPieceColors.Black );
            BoardHash.Update( ChessPieceType.Queen, currentPosition, previousPosition );
          } 
        } },   
        { typeof(RookBitBoard), () => {
          if ( previousPosition.Color == ChessPieceColors.White ) {
            WhiteRook.Bits = previousPosition.Bits;
            Debug.Assert( currentPosition.Color == ChessPieceColors.White );
            BoardHash.Update( ChessPieceType.Rook, currentPosition, previousPosition );
            var pBB = previousPosition as RookBitBoard;
            var cBB = currentPosition as RookBitBoard;
            if ( !pBB.HasMovedKingSide && cBB.HasMovedKingSide )
              BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.White, availableCastles: ZobristCastling.KingSideRookMoved );
            if ( !pBB.HasMovedQueenSide && cBB.HasMovedQueenSide )
              BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.White, availableCastles: ZobristCastling.QueenSideRookMoved );
          } else if ( previousPosition.Color == ChessPieceColors.Black ) {
            BlackRook.Bits = previousPosition.Bits;
            Debug.Assert( currentPosition.Color == ChessPieceColors.Black );
            BoardHash.Update( ChessPieceType.Rook, currentPosition, previousPosition );
            var pBB = previousPosition as RookBitBoard;
            var cBB = currentPosition as RookBitBoard;
            if ( !pBB.HasMovedKingSide && cBB.HasMovedKingSide )
              BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.Black, availableCastles: ZobristCastling.KingSideRookMoved );
            if ( !pBB.HasMovedQueenSide && cBB.HasMovedQueenSide )
              BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.Black, availableCastles: ZobristCastling.QueenSideRookMoved );
          } 
        } },   
        { typeof(BishopBitBoard), () => {
          if ( previousPosition.Color == ChessPieceColors.White ) {
            WhiteBishop.Bits = previousPosition.Bits;
            Debug.Assert( currentPosition.Color == ChessPieceColors.White );
            BoardHash.Update( ChessPieceType.Bishop, currentPosition, previousPosition );
          } else if ( previousPosition.Color == ChessPieceColors.Black ) {
            BlackBishop.Bits = previousPosition.Bits;
            Debug.Assert( currentPosition.Color == ChessPieceColors.Black );
            BoardHash.Update( ChessPieceType.Bishop, currentPosition, previousPosition );
          } 
        } },   
        { typeof(KnightBitBoard), () => {
          if ( previousPosition.Color == ChessPieceColors.White ) {
            WhiteKnight.Bits = previousPosition.Bits;
            Debug.Assert( currentPosition.Color == ChessPieceColors.White );
            BoardHash.Update( ChessPieceType.Knight, currentPosition, previousPosition );
          } else if ( previousPosition.Color == ChessPieceColors.Black ) {
            BlackKnight.Bits = previousPosition.Bits;
            Debug.Assert( currentPosition.Color == ChessPieceColors.Black );
            BoardHash.Update( ChessPieceType.Knight, currentPosition, previousPosition );
          }
        } },  
        { typeof(PawnBitBoard), () => {
          if (((PawnBitBoard)currentPosition).IsPromoted == true) {
            PromotionUndoHandler( moveCoordinate,  (BoardSquare)(( (ulong)currentPosition.Bits ^ (ulong)previousPosition.Bits ) & (ulong)previousPosition.Bits ) );
          } else {
            if ( previousPosition.Color == ChessPieceColors.White ) {
              WhitePawn.Bits = previousPosition.Bits;
              Debug.Assert( currentPosition.Color == ChessPieceColors.White );
              if ( ( (PawnBitBoard)currentPosition ).MovedTwoSquares != BoardSquare.Empty )
                BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.White, enPassant: ( (PawnBitBoard)currentPosition ).MovedTwoSquares ); 
              BoardHash.Update( ChessPieceType.Pawn, currentPosition, previousPosition );
            } else if ( previousPosition.Color == ChessPieceColors.Black ) {
              BlackPawn.Bits = previousPosition.Bits;
              Debug.Assert( currentPosition.Color == ChessPieceColors.Black );
              if ( ( (PawnBitBoard)currentPosition ).MovedTwoSquares != BoardSquare.Empty )
                BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, enPassant: ( (PawnBitBoard)currentPosition ).MovedTwoSquares ); 
              BoardHash.Update( ChessPieceType.Pawn, currentPosition, previousPosition );
            }
          }
        } }        
      };
      @switch[previousPosition.GetType()]();

      if ( peekMove is PawnBitBoard && ( (PawnBitBoard)peekMove ).MovedTwoSquares != BoardSquare.Empty ) {
        if ( peekMove.Color == ChessPieceColors.Black ) {
          BlackPawn.MovedTwoSquares = ( (PawnBitBoard)peekMove ).MovedTwoSquares;
          BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, enPassant: BlackPawn.MovedTwoSquares );
        } else {
          WhitePawn.MovedTwoSquares = ( (PawnBitBoard)peekMove ).MovedTwoSquares;
          BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.White, enPassant: WhitePawn.MovedTwoSquares );
        }
      }
#if DEBUG
      BoardHash.Debug_Diff_COMP = oldhash;
      var hashpop = HashStack.Pop();
      Debug.Assert(hashpop == BoardHash.Key );
#endif
    }

    private void PromotionUndoHandler( ColoredBitBoard currentCoordinate, BoardSquare beforePromotionCoordinate ) {
      PawnBitBoard currentPawnCoordinate = (PawnBitBoard)currentCoordinate;
      if ( currentCoordinate.Color == ChessPieceColors.White ) {
        WhitePawn.Bits |= beforePromotionCoordinate;
        BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.White, squareTo: beforePromotionCoordinate );
        if ( currentPawnCoordinate.Promotion == PawnBitBoard.PromotionPiece.Queen ) {
          Debug.Assert( ( WhiteQueen.Bits & currentCoordinate.Bits ) != BoardSquare.Empty );
          WhiteQueen.Bits ^= currentCoordinate.Bits;
          BoardHash.Update( ChessPieceType.Queen, ChessPieceColors.White, squareFrom: currentCoordinate.Bits );
        } else if ( currentPawnCoordinate.Promotion == PawnBitBoard.PromotionPiece.Bishop ) {
          Debug.Assert( ( WhiteBishop.Bits & currentCoordinate.Bits ) != BoardSquare.Empty );
          WhiteBishop.Bits ^= currentCoordinate.Bits;
          BoardHash.Update( ChessPieceType.Bishop, ChessPieceColors.White, squareFrom: currentCoordinate.Bits );
        } else if ( currentPawnCoordinate.Promotion == PawnBitBoard.PromotionPiece.Rook ) {
          Debug.Assert( ( WhiteRook.Bits & currentCoordinate.Bits ) != BoardSquare.Empty );
          WhiteRook.Bits ^= currentCoordinate.Bits;
          BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.White, squareFrom: currentCoordinate.Bits );
        } else if ( currentPawnCoordinate.Promotion == PawnBitBoard.PromotionPiece.Knight ) {
          Debug.Assert( ( WhiteKnight.Bits & currentCoordinate.Bits ) != BoardSquare.Empty );
          WhiteKnight.Bits ^= currentCoordinate.Bits;
          BoardHash.Update( ChessPieceType.Knight, ChessPieceColors.White, squareFrom: currentCoordinate.Bits );
        }
      } else if ( currentCoordinate.Color == ChessPieceColors.Black ) {
        BlackPawn.Bits |= beforePromotionCoordinate;
        BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, squareTo: beforePromotionCoordinate );
        if ( currentPawnCoordinate.Promotion == PawnBitBoard.PromotionPiece.Queen ) {
          Debug.Assert( ( BlackQueen.Bits & currentCoordinate.Bits ) != BoardSquare.Empty );
          BlackQueen.Bits ^= currentCoordinate.Bits;
          BoardHash.Update( ChessPieceType.Queen, ChessPieceColors.Black, squareFrom: currentCoordinate.Bits );
        } else if ( currentPawnCoordinate.Promotion == PawnBitBoard.PromotionPiece.Bishop ) {
          Debug.Assert( ( BlackBishop.Bits & currentCoordinate.Bits ) != BoardSquare.Empty );
          BlackBishop.Bits ^= currentCoordinate.Bits;
          BoardHash.Update( ChessPieceType.Bishop, ChessPieceColors.Black, squareFrom: currentCoordinate.Bits );
        } else if ( currentPawnCoordinate.Promotion == PawnBitBoard.PromotionPiece.Rook ) {
          Debug.Assert( ( BlackRook.Bits & currentCoordinate.Bits ) != BoardSquare.Empty );
          BlackRook.Bits ^= currentCoordinate.Bits;
          BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.Black, squareFrom: currentCoordinate.Bits );
        } else if ( currentPawnCoordinate.Promotion == PawnBitBoard.PromotionPiece.Knight ) {
          Debug.Assert( ( BlackKnight.Bits & currentCoordinate.Bits ) != BoardSquare.Empty );
          BlackKnight.Bits ^= currentCoordinate.Bits;
          BoardHash.Update( ChessPieceType.Knight, ChessPieceColors.Black, squareFrom: currentCoordinate.Bits );
        }
      }
    }

    private ColoredBitBoard FindMoveCoordinate( Tuple<ColoredBitBoard, ColoredBitBoard> currentAndPreviousPosition ) {
      ColoredBitBoard result = currentAndPreviousPosition.Item1.DeepCopy();
      result.Bits = ( currentAndPreviousPosition.Item1.Bits & currentAndPreviousPosition.Item2.Bits ) ^ currentAndPreviousPosition.Item1.Bits;
      return result;
    }

    private void AddCapturedPiece( ColoredBitBoard captureCoordinate ) {

      switch ( captureCoordinate.JustCaptured ) {
        case ChessPieceType.King:
          throw new Exception( "The king has been captured" );
        case ChessPieceType.Queen:
          if ( captureCoordinate.Color == ChessPieceColors.Black ) {
            WhiteQueen.Bits |= captureCoordinate.Bits;
            Debug.Assert( captureCoordinate.Count == 1 );
            BoardHash.Update( ChessPieceType.Queen, ChessPieceColors.White, captureCoordinate.Bits );
          } else {
            BlackQueen.Bits |= captureCoordinate.Bits;
            Debug.Assert( captureCoordinate.Count == 1 );
            BoardHash.Update( ChessPieceType.Queen, ChessPieceColors.Black, captureCoordinate.Bits );
          }
          break;
        case ChessPieceType.Rook:
          if ( captureCoordinate.Color == ChessPieceColors.Black ) {
            WhiteRook.Bits |= captureCoordinate.Bits;
            Debug.Assert( captureCoordinate.Count == 1 );
            BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.White, captureCoordinate.Bits );
          } else {
            BlackRook.Bits |= captureCoordinate.Bits;
            Debug.Assert( captureCoordinate.Count == 1 );
            BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.Black, captureCoordinate.Bits );
          }
          break;
        case ChessPieceType.Bishop:
          if ( captureCoordinate.Color == ChessPieceColors.Black ) {
            WhiteBishop.Bits |= captureCoordinate.Bits;
            Debug.Assert( captureCoordinate.Count == 1 );
            BoardHash.Update( ChessPieceType.Bishop, ChessPieceColors.White, captureCoordinate.Bits );
          } else {
            BlackBishop.Bits |= captureCoordinate.Bits;
            Debug.Assert( captureCoordinate.Count == 1 );
            BoardHash.Update( ChessPieceType.Bishop, ChessPieceColors.Black, captureCoordinate.Bits );
          }
          break;
        case ChessPieceType.Knight:
          if ( captureCoordinate.Color == ChessPieceColors.Black ) {
            WhiteKnight.Bits |= captureCoordinate.Bits;
            Debug.Assert( captureCoordinate.Count == 1 );
            BoardHash.Update( ChessPieceType.Knight, ChessPieceColors.White, captureCoordinate.Bits );
          } else {
            BlackKnight.Bits |= captureCoordinate.Bits;
            Debug.Assert( captureCoordinate.Count == 1 );
            BoardHash.Update( ChessPieceType.Knight, ChessPieceColors.Black, captureCoordinate.Bits );
          }
          break;
        case ChessPieceType.Pawn:
          if ( captureCoordinate.Color == ChessPieceColors.Black ) {
            WhitePawn.Bits |= captureCoordinate.Bits;
            Debug.Assert( captureCoordinate.Count == 1 );
            BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.White, captureCoordinate.Bits );
          } else {
            BlackPawn.Bits |= captureCoordinate.Bits;
            Debug.Assert( captureCoordinate.Count == 1 );
            BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, captureCoordinate.Bits );
          }
          break;
      }
    }
#if DEBUG
    public Stack<ulong> HashStack = new Stack<ulong>();
#endif

    public void Update( ColoredBitBoard board ) {
#if DEBUG
      HashStack.Push( BoardHash.Key );
#endif
      //currentMove and previousMove are the ColoeredBitBoards which are put on the Undo stack

      _pieceCount = this.BlackBishop.Count + this.BlackKing.Count + this.BlackKnight.Count + this.BlackPawn.Count + this.BlackQueen.Count + this.BlackRook.Count
                    + this.WhiteBishop.Count + this.WhiteKing.Count + this.WhiteKnight.Count + this.WhitePawn.Count + this.WhiteQueen.Count + this.WhiteRook.Count;

      ColoredBitBoard currentMove = board.DeepCopy();
      ColoredBitBoard previousMove = GetOldBitBoardFromBitBoard( board ).DeepCopy();

      Dictionary<Type, Action> @switch = new Dictionary<Type, Action>() {
        { typeof(KingBitBoard), () => {
          if ( Castling( board ) == false ){            
            if ( board.Color == ChessPieceColors.White ) {
              BoardHash.Update( ChessPieceType.King, WhiteKing, board );
              if ( !WhiteKing.HasMoved )
                BoardHash.Update( ChessPieceType.King, ChessPieceColors.White, availableCastles: ZobristCastling.KingMoved );
              WhiteKing.Bits = board.Bits;              
            } else if ( board.Color == ChessPieceColors.Black ) {
              BoardHash.Update( ChessPieceType.King, BlackKing, board );
              if ( !BlackKing.HasMoved )
                BoardHash.Update( ChessPieceType.King, ChessPieceColors.Black, availableCastles: ZobristCastling.KingMoved );
              BlackKing.Bits = board.Bits;              
            } 
          }
        } }, 
        { typeof(QueenBitBoard), () => {
          if ( board.Color == ChessPieceColors.White ) {
            BoardHash.Update( ChessPieceType.Queen, WhiteQueen, board );
            WhiteQueen.Bits = board.Bits;            
          } else if ( board.Color == ChessPieceColors.Black ) {
            BoardHash.Update( ChessPieceType.Queen, BlackQueen, board );
            BlackQueen.Bits = board.Bits;            
          } 
        } },   
        { typeof(RookBitBoard), () => {
          if ( board.Color == ChessPieceColors.White ) {
            if ( !WhiteRook.HasMovedKingSide && ( board.Bits & BoardSquare.H1 ) == BoardSquare.Empty )
              BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.White, availableCastles: ZobristCastling.KingSideRookMoved );
            else if ( !WhiteRook.HasMovedQueenSide && ( board.Bits & BoardSquare.A1 ) == BoardSquare.Empty )
              BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.White, availableCastles: ZobristCastling.QueenSideRookMoved );

            BoardHash.Update( ChessPieceType.Rook, WhiteRook, board );
            WhiteRook.Bits = board.Bits;
          } else if ( board.Color == ChessPieceColors.Black ) {
            if ( !BlackRook.HasMovedKingSide && ( board.Bits & BoardSquare.H8 ) == BoardSquare.Empty )
              BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.Black, availableCastles: ZobristCastling.KingSideRookMoved );
            else if ( !BlackRook.HasMovedQueenSide && ( board.Bits & BoardSquare.A8 ) == BoardSquare.Empty )
              BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.Black, availableCastles: ZobristCastling.QueenSideRookMoved );

            BoardHash.Update( ChessPieceType.Rook, BlackRook, board );
            BlackRook.Bits = board.Bits;            
          } 
        } },   
        { typeof(BishopBitBoard), () => {
          if ( board.Color == ChessPieceColors.White ) {
            BoardHash.Update( ChessPieceType.Bishop, WhiteBishop, board );
            WhiteBishop.Bits = board.Bits;            
          } else if ( board.Color == ChessPieceColors.Black ) {            
            BoardHash.Update( ChessPieceType.Bishop, BlackBishop, board );
            BlackBishop.Bits = board.Bits;
          } 
        } },   
        { typeof(KnightBitBoard), () => {
          if ( board.Color == ChessPieceColors.White ) {
            BoardHash.Update( ChessPieceType.Knight, WhiteKnight, board );
            WhiteKnight.Bits = board.Bits;
          } else if ( board.Color == ChessPieceColors.Black ) {
            BoardHash.Update( ChessPieceType.Knight, BlackKnight, board );
            BlackKnight.Bits = board.Bits;            
          } 
        } },  
        { typeof(PawnBitBoard), () => {
          MoveCount = 0;
          if ( !EnPassant( board ) ) {                        
            if ( board.Color == ChessPieceColors.White ) {
              WhitePawn.Bits = board.Bits;
              if ( WhitePawn.MovedTwoSquares != BoardSquare.Empty )
                BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.White, enPassant: WhitePawn.MovedTwoSquares );
              BoardHash.Update( ChessPieceType.Pawn, previousMove, board );                       
              ( (PawnBitBoard)currentMove ).MovedTwoSquares = WhitePawn.MovedTwoSquares;
            } else if ( board.Color == ChessPieceColors.Black ) {
              BlackPawn.Bits = board.Bits;
              if ( BlackPawn.MovedTwoSquares != BoardSquare.Empty )
                BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, enPassant: BlackPawn.MovedTwoSquares );
              BoardHash.Update( ChessPieceType.Pawn, previousMove, board );             
              ( (PawnBitBoard)currentMove ).MovedTwoSquares = BlackPawn.MovedTwoSquares;
            }

            Debug.Assert(
              ( ( ( BoardSquare.A1 | BoardSquare.B1 | BoardSquare.C1 | BoardSquare.D1 | BoardSquare.E1 | BoardSquare.F1 | BoardSquare.G1 | BoardSquare.H1 |
                    BoardSquare.A8 | BoardSquare.B8 | BoardSquare.C8 | BoardSquare.D8 | BoardSquare.E8 | BoardSquare.F8 | BoardSquare.G8 | BoardSquare.H8 ) 
                    & board.Bits ) == 0 && !( (PawnBitBoard)board ).IsPromoted ) 
              || 
              ( ( ( BoardSquare.A1 | BoardSquare.B1 | BoardSquare.C1 | BoardSquare.D1 | BoardSquare.E1 | BoardSquare.F1 | BoardSquare.G1 | BoardSquare.H1 |
                    BoardSquare.A8 | BoardSquare.B8 | BoardSquare.C8 | BoardSquare.D8 | BoardSquare.E8 | BoardSquare.F8 | BoardSquare.G8 | BoardSquare.H8 ) 
                    & board.Bits ) != 0 && ( (PawnBitBoard)board ).IsPromoted ) 
            );

            

            PawnBitBoard pawnBoard = board as PawnBitBoard;
            if ( pawnBoard.IsPromoted ) {
              Promotion( pawnBoard );
            }
          } else {
            currentMove.JustCaptured = ChessPieceType.Pawn;
          }
        } }        
      };

      MoveCount++;

      @switch[board.GetType()]();

      ChessPieceType? pieceType = RemoveCapturedPiece( board );
      if ( pieceType != null ) {
        MoveCount = 0;
        currentMove.JustCaptured = pieceType;
      }

      ResetPawnMovedTwoSquares( board );
      DoesMoveCheck( board );

      Tuple<ColoredBitBoard, ColoredBitBoard> currentAndPreviousMove = new Tuple<ColoredBitBoard, ColoredBitBoard>( currentMove, previousMove );
      MoveHistory.Push( currentAndPreviousMove );
    }

    private void DoesMoveCheck( ColoredBitBoard board ) {
      if ( board.DoesCheck ) {
        if ( board.Color == ChessPieceColors.White ) {
          BlackKing.IsChecked = true;
        } else {
          WhiteKing.IsChecked = true;
        }
      }
    }

    private const BoardSquare WHITE_PAWN_PROMOTION_LINE = BoardSquare.A8 | BoardSquare.B8 | BoardSquare.C8 | BoardSquare.D8 | BoardSquare.E8 | BoardSquare.F8 | BoardSquare.G8 | BoardSquare.H8;
    private const BoardSquare BLACK_PAWN_PROMOTION_LINE = BoardSquare.A1 | BoardSquare.B1 | BoardSquare.C1 | BoardSquare.D1 | BoardSquare.E1 | BoardSquare.F1 | BoardSquare.G1 | BoardSquare.H1;

    private void Promotion( PawnBitBoard pawnBoard ) {
      switch ( pawnBoard.Promotion.Value ) {
        case PawnBitBoard.PromotionPiece.Queen:
          if ( pawnBoard.Color == ChessPieceColors.White ) {
            BoardSquare PromotionSquare = pawnBoard.Bits & WHITE_PAWN_PROMOTION_LINE;
            WhitePawn.Bits ^= PromotionSquare;
            WhiteQueen.Bits |= PromotionSquare;

            var eb = new EmptyBitBoard();
            eb.Bits = eb.Bits = PromotionSquare;
            Debug.Assert( eb.Count <= 1 );
            /* Update hash key */
            BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.White, squareFrom: PromotionSquare );
            BoardHash.Update( ChessPieceType.Queen, ChessPieceColors.White, squareTo: PromotionSquare );
          } else {
            BoardSquare PromotionSquare = pawnBoard.Bits & BLACK_PAWN_PROMOTION_LINE;
            BlackPawn.Bits ^= PromotionSquare;
            BlackQueen.Bits |= PromotionSquare;

            var eb = new EmptyBitBoard();
            eb.Bits = eb.Bits = PromotionSquare;
            Debug.Assert( eb.Count <= 1 );
            /* Update hash key */
            BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, squareFrom: PromotionSquare );
            BoardHash.Update( ChessPieceType.Queen, ChessPieceColors.Black, squareTo: PromotionSquare );
          }
          break;
        case PawnBitBoard.PromotionPiece.Rook:
          if ( pawnBoard.Color == ChessPieceColors.White ) {
            BoardSquare PromotionSquare = pawnBoard.Bits & WHITE_PAWN_PROMOTION_LINE;
            WhitePawn.Bits ^= PromotionSquare;
            WhiteRook.Bits |= PromotionSquare;

            /* Update hash key */
            BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.White, squareFrom: PromotionSquare );
            BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.White, squareTo: PromotionSquare );
          } else {
            BoardSquare PromotionSquare = pawnBoard.Bits & BLACK_PAWN_PROMOTION_LINE;
            BlackPawn.Bits ^= PromotionSquare;
            BlackRook.Bits |= PromotionSquare;

            /* Update hash key */
            BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, squareFrom: PromotionSquare );
            BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.Black, squareTo: PromotionSquare );
          }
          break;
        case PawnBitBoard.PromotionPiece.Bishop:
          if ( pawnBoard.Color == ChessPieceColors.White ) {
            BoardSquare PromotionSquare = pawnBoard.Bits & WHITE_PAWN_PROMOTION_LINE;
            WhitePawn.Bits ^= PromotionSquare;
            WhiteBishop.Bits |= PromotionSquare;

            /* Update hash key */
            BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.White, squareFrom: PromotionSquare );
            BoardHash.Update( ChessPieceType.Bishop, ChessPieceColors.White, squareTo: PromotionSquare );
          } else {
            BoardSquare PromotionSquare = pawnBoard.Bits & BLACK_PAWN_PROMOTION_LINE;
            BlackPawn.Bits ^= PromotionSquare;
            BlackBishop.Bits |= PromotionSquare;

            /* Update hash key */
            BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, squareFrom: PromotionSquare );
            BoardHash.Update( ChessPieceType.Bishop, ChessPieceColors.Black, squareTo: PromotionSquare );
          }
          break;
        case PawnBitBoard.PromotionPiece.Knight:
          if ( pawnBoard.Color == ChessPieceColors.White ) {
            BoardSquare PromotionSquare = pawnBoard.Bits & WHITE_PAWN_PROMOTION_LINE;
            WhitePawn.Bits ^= PromotionSquare;
            WhiteKnight.Bits |= PromotionSquare;

            /* Update hash key */
            BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.White, squareFrom: PromotionSquare );
            BoardHash.Update( ChessPieceType.Knight, ChessPieceColors.White, squareTo: PromotionSquare );
          } else {
            BoardSquare PromotionSquare = pawnBoard.Bits & BLACK_PAWN_PROMOTION_LINE;
            BlackPawn.Bits ^= PromotionSquare;
            BlackKnight.Bits |= PromotionSquare;

            /* Update hash key */
            BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, squareFrom: PromotionSquare );
            BoardHash.Update( ChessPieceType.Knight, ChessPieceColors.Black, squareTo: PromotionSquare );
          }
          break;
      }
    }

    private const BoardSquare WHITE_KING_CASTLING_SHORT = BoardSquare.G1;
    private const BoardSquare WHITE_KING_CASTLING_LONG = BoardSquare.C1;
    private const BoardSquare BLACK_KING_CASTLING_SHORT = BoardSquare.G8;
    private const BoardSquare BLACK_KING_CASTLING_LONG = BoardSquare.C8;
    private bool Castling( ColoredBitBoard board ) {
      if ( board.Color == ChessPieceColors.White ) {
        if ( WhiteKing.HasMoved == false && WHITE_KING_CASTLING_SHORT == ( board.Bits & WHITE_KING_CASTLING_SHORT ) ) {
          BoardHash.Update( ChessPieceType.King, ChessPieceColors.White, WHITE_KING_CASTLING_SHORT, WhiteKing.Bits, ZobristCastling.CastlingDone );
          BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.White, BoardSquare.F1, BoardSquare.H1 );

          WhiteKing.Bits = WHITE_KING_CASTLING_SHORT;
          WhiteRook.Bits ^= BoardSquare.H1;
          WhiteRook.Bits |= BoardSquare.F1;

          Debug.Assert( WhiteKing.Count == 1 );
          return true;
        } else if ( WhiteKing.HasMoved == false && WHITE_KING_CASTLING_LONG == ( board.Bits & WHITE_KING_CASTLING_LONG ) ) {
          BoardHash.Update( ChessPieceType.King, ChessPieceColors.White, WHITE_KING_CASTLING_LONG, WhiteKing.Bits, ZobristCastling.CastlingDone );
          BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.White, BoardSquare.D1, BoardSquare.A1 );

          WhiteKing.Bits = WHITE_KING_CASTLING_LONG;
          WhiteRook.Bits ^= BoardSquare.A1;
          WhiteRook.Bits |= BoardSquare.D1;

          Debug.Assert( WhiteKing.Count == 1 );
          return true;
        }
      } else {
        if ( BlackKing.HasMoved == false && BLACK_KING_CASTLING_SHORT == ( board.Bits & BLACK_KING_CASTLING_SHORT ) ) {
          BoardHash.Update( ChessPieceType.King, ChessPieceColors.Black, BLACK_KING_CASTLING_SHORT, BlackKing.Bits, ZobristCastling.CastlingDone );
          BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.Black, BoardSquare.F8, BoardSquare.H8 );

          BlackKing.Bits = BLACK_KING_CASTLING_SHORT;
          BlackRook.Bits ^= BoardSquare.H8;
          BlackRook.Bits |= BoardSquare.F8;

          Debug.Assert( BlackKing.Count == 1 );
          return true;
        } else if ( BlackKing.HasMoved == false && BLACK_KING_CASTLING_LONG == ( board.Bits & BLACK_KING_CASTLING_LONG ) ) {
          BoardHash.Update( ChessPieceType.King, ChessPieceColors.Black, BLACK_KING_CASTLING_LONG, BlackKing.Bits, ZobristCastling.CastlingDone );
          BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.Black, BoardSquare.D8, BoardSquare.A8 );

          BlackKing.Bits = BLACK_KING_CASTLING_LONG;
          BlackRook.Bits ^= BoardSquare.A8;
          BlackRook.Bits |= BoardSquare.D8;

          Debug.Assert( BlackKing.Count == 1 );
          return true;
        }
      }

      return false;
    }

    private bool EnPassant( ColoredBitBoard board ) {
      if ( board.Color == ChessPieceColors.White &&
           board.Bits == (BoardSquare)( (ulong)BlackPawn.MovedTwoSquares << 8 ) ) {
        BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, squareFrom: BlackPawn.MovedTwoSquares, enPassant: BlackPawn.MovedTwoSquares );
        BlackPawn.Bits ^= BlackPawn.MovedTwoSquares;
        BoardHash.Update( ChessPieceType.Pawn, WhitePawn, board );
        WhitePawn.Bits = board.Bits;

        return true;
      } else if ( board.Bits == (BoardSquare)( (ulong)WhitePawn.MovedTwoSquares >> 8 ) ) {
        BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.White, squareFrom: WhitePawn.MovedTwoSquares, enPassant: WhitePawn.MovedTwoSquares );
        WhitePawn.Bits ^= WhitePawn.MovedTwoSquares;
        BoardHash.Update( ChessPieceType.Pawn, BlackPawn, board );
        BlackPawn.Bits = board.Bits;
        return true;
      }

      return false;
    }

    private void ResetPawnMovedTwoSquares( ColoredBitBoard board ) {
      if ( board.Color == ChessPieceColors.White ) {
        BlackPawn.ResetMovedTwoSquares( BoardHash );
      } else {
        WhitePawn.ResetMovedTwoSquares( BoardHash );
      }
    }

    private ChessPieceType? RemoveCapturedPiece( ColoredBitBoard newBoard ) {
      if ( newBoard.Color == ChessPieceColors.Black ) {
        if ( ( WhiteKing.Bits & newBoard.Bits ) != BoardSquare.Empty ) {
          var CaptureCoordinate = WhiteKing.Bits & newBoard.Bits;
          WhiteKing.Bits ^= CaptureCoordinate;
          BoardHash.Update( ChessPieceType.King, ChessPieceColors.White, squareFrom: CaptureCoordinate );
          return ChessPieceType.King;
        } else if ( ( WhiteQueen.Bits & newBoard.Bits ) != BoardSquare.Empty ) {
          var CaptureCoordinate = WhiteQueen.Bits & newBoard.Bits;
          WhiteQueen.Bits ^= CaptureCoordinate;
          BoardHash.Update( ChessPieceType.Queen, ChessPieceColors.White, squareFrom: CaptureCoordinate );
          return ChessPieceType.Queen;
        } else if ( ( WhiteRook.Bits & newBoard.Bits ) != BoardSquare.Empty ) {
          var CaptureCoordinate = WhiteRook.Bits & newBoard.Bits;
          WhiteRook.Bits ^= CaptureCoordinate;
          BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.White, squareFrom: CaptureCoordinate );
          return ChessPieceType.Rook;
        } else if ( ( WhiteBishop.Bits & newBoard.Bits ) != BoardSquare.Empty ) {
          var CaptureCoordinate = WhiteBishop.Bits & newBoard.Bits;
          WhiteBishop.Bits ^= CaptureCoordinate;
          BoardHash.Update( ChessPieceType.Bishop, ChessPieceColors.White, squareFrom: CaptureCoordinate );
          return ChessPieceType.Bishop;
        } else if ( ( WhiteKnight.Bits & newBoard.Bits ) != BoardSquare.Empty ) {
          var CaptureCoordinate = WhiteKnight.Bits & newBoard.Bits;
          WhiteKnight.Bits ^= CaptureCoordinate;
          BoardHash.Update( ChessPieceType.Knight, ChessPieceColors.White, squareFrom: CaptureCoordinate );
          return ChessPieceType.Knight;
        } else if ( ( WhitePawn.Bits & newBoard.Bits ) != BoardSquare.Empty ) {
          var CaptureCoordinate = WhitePawn.Bits & newBoard.Bits;
          if ( WhitePawn.MovedTwoSquares != BoardSquare.Empty ) {
            BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.White, enPassant: WhitePawn.MovedTwoSquares );
          }

          WhitePawn.Bits ^= CaptureCoordinate;
          BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.White, squareFrom: CaptureCoordinate );
          return ChessPieceType.Pawn;
        }
      } else if ( newBoard.Color == ChessPieceColors.White ) {
        if ( ( BlackKing.Bits & newBoard.Bits ) != BoardSquare.Empty ) {
          var CaptureCoordinate = BlackKing.Bits & newBoard.Bits;
          BlackKing.Bits ^= CaptureCoordinate;
          BoardHash.Update( ChessPieceType.King, ChessPieceColors.Black, squareFrom: CaptureCoordinate );
          return ChessPieceType.King;
        } else if ( ( BlackQueen.Bits & newBoard.Bits ) != BoardSquare.Empty ) {
          var CaptureCoordinate = BlackQueen.Bits & newBoard.Bits;
          BlackQueen.Bits ^= CaptureCoordinate;
          BoardHash.Update( ChessPieceType.Queen, ChessPieceColors.Black, squareFrom: CaptureCoordinate );
          return ChessPieceType.Queen;
        } else if ( ( BlackRook.Bits & newBoard.Bits ) != BoardSquare.Empty ) {
          var CaptureCoordinate = BlackRook.Bits & newBoard.Bits;
          BlackRook.Bits ^= CaptureCoordinate;
          BoardHash.Update( ChessPieceType.Rook, ChessPieceColors.Black, squareFrom: CaptureCoordinate );
          return ChessPieceType.Rook;
        } else if ( ( BlackBishop.Bits & newBoard.Bits ) != BoardSquare.Empty ) {
          var CaptureCoordinate = BlackBishop.Bits & newBoard.Bits;
          BlackBishop.Bits ^= CaptureCoordinate;
          BoardHash.Update( ChessPieceType.Bishop, ChessPieceColors.Black, squareFrom: CaptureCoordinate );
          return ChessPieceType.Bishop;
        } else if ( ( BlackKnight.Bits & newBoard.Bits ) != BoardSquare.Empty ) {
          var CaptureCoordinate = BlackKnight.Bits & newBoard.Bits;
          BlackKnight.Bits ^= CaptureCoordinate;
          BoardHash.Update( ChessPieceType.Knight, ChessPieceColors.Black, squareFrom: CaptureCoordinate );
          return ChessPieceType.Knight;
        } else if ( ( BlackPawn.Bits & newBoard.Bits ) != BoardSquare.Empty ) {
          var CaptureCoordinate = BlackPawn.Bits & newBoard.Bits;
          if ( BlackPawn.MovedTwoSquares != BoardSquare.Empty ) {
            BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, enPassant: BlackPawn.MovedTwoSquares );
          }

          BlackPawn.Bits ^= CaptureCoordinate;
          BoardHash.Update( ChessPieceType.Pawn, ChessPieceColors.Black, squareFrom: CaptureCoordinate );
          return ChessPieceType.Pawn;
        }
      }

      return null;
    }

    public void SetGameStage( ChessBoardGameStage stage ) {
      _stage = stage;
    }

    public ChessBoard DeepCopy() {
      using ( var ms = new System.IO.MemoryStream() ) {
        var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        formatter.Serialize( ms, this );
        ms.Position = 0;

        return (ChessBoard)formatter.Deserialize( ms );
      }
    }

    public class ChessPieceSquareEqualityComparer : IEqualityComparer<ScenarioPlace> {
      public bool Equals( ScenarioPlace x, ScenarioPlace y ) {
        if ( x.Square == y.Square ) return true;
        else return false;
      }

      public int GetHashCode( ScenarioPlace obj ) {
        return (int)obj.Square;
      }
    }

    public class IllegalPiecePlacementException : Exception {
      public IllegalPiecePlacementException( string s ) : base( s ) { }
    }

    public class IllegalMoveException : Exception {
      public IllegalMoveException( string s ) : base( s ) { }
    }


    public override bool Equals( object obj ) {
      if ( obj == null ) return false;

      ChessBoard c = obj as ChessBoard;
      if ( c == null ) return false;//Not of type BitBoard


      return
        this.WhiteBishop.Equals( c.WhiteBishop ) &&
        this.WhiteKing.Equals( c.WhiteKing ) &&
        this.WhiteKnight.Equals( c.WhiteKnight ) &&
        this.WhitePawn.Equals( c.WhitePawn ) &&
        this.WhiteQueen.Equals( c.WhiteQueen ) &&
        this.WhiteRook.Equals( c.WhiteRook ) &&
        this.BlackBishop.Equals( c.BlackBishop ) &&
        this.BlackKing.Equals( c.BlackKing ) &&
        this.BlackKnight.Equals( c.BlackKnight ) &&
        this.BlackPawn.Equals( c.BlackPawn ) &&
        this.BlackQueen.Equals( c.BlackQueen ) &&
        this.BlackRook.Equals( c.BlackRook );
    }
  }
}

using Xunit;
using P5;
/* Source Scenario Result */
/*
 * Author: Sari Haj Hussein
 */
namespace P5_Tests {
  public class UT_ChessBoard {
    [Fact]
    public void BoardSquare_GetBoardSquareIndexFrom_Equal() {
      for ( int i = 0; i < 64; i++ ) {
        BoardSquare bs = (BoardSquare)( (ulong)1 << i );
        Assert.Equal( i, Zobrist.GetIndexFromBoardSquare( bs ) );
      }
    }

    [Fact]
    public void InitializeGame_IsWhiteKingCorrectlyPlaced_Equal() {
      ChessBoard board = new ChessBoard();
      board.InitializeGame();
      BoardSquare correctPlacement = BoardSquare.E1;

      Assert.Equal( correctPlacement, board.WhiteKing.Bits );
    }

    [Fact]
    public void InitializeGame_IsBlackKingCorrectlyPlaced_Equal() {
      ChessBoard board = new ChessBoard();
      board.InitializeGame();
      BoardSquare correctPlacement = BoardSquare.E8;


      Assert.Equal( correctPlacement, board.BlackKing.Bits );
    }

    [Fact]
    public void InitializeGame_IsWhiteQueenCorrectlyPlaced_Equal() {
      ChessBoard board = new ChessBoard();
      board.InitializeGame();
      BoardSquare correctPlacement = BoardSquare.D1;

      Assert.Equal( correctPlacement, board.WhiteQueen.Bits );
    }

    [Fact]
    public void InitializeGame_IsBlackQueenCorrectlyPlaced_Equal() {
      ChessBoard board = new ChessBoard();
      board.InitializeGame();
      BoardSquare correctPlacement = BoardSquare.D8;

      Assert.Equal( correctPlacement, board.BlackQueen.Bits );
    }

    [Fact]
    public void InitializeGame_IsWhiteRooksCorrectlyPlaced_Equal() {
      ChessBoard board = new ChessBoard();
      board.InitializeGame();
      BoardSquare correctPlacement = BoardSquare.A1 | BoardSquare.H1;

      Assert.Equal( correctPlacement, board.WhiteRook.Bits );
    }

    [Fact]
    public void InitializeGame_IsBlackRooksCorrectlyPlaced_Equal() {
      ChessBoard board = new ChessBoard();
      board.InitializeGame();
      BoardSquare correctPlacement = BoardSquare.A8 | BoardSquare.H8;

      Assert.Equal( correctPlacement, board.BlackRook.Bits );
    }

    [Fact]
    public void InitializeGame_IsWhiteBishopsCorrectlyPlaced_Equal() {
      ChessBoard board = new ChessBoard();
      board.InitializeGame();
      BoardSquare correctPlacement = BoardSquare.C1 | BoardSquare.F1;

      Assert.Equal( correctPlacement, board.WhiteBishop.Bits );
    }

    [Fact]
    public void InitializeGame_IsBlackBishopsCorrectlyPlaced_Equal() {
      ChessBoard board = new ChessBoard();
      board.InitializeGame();
      BoardSquare correctPlacement = BoardSquare.C8 | BoardSquare.F8;

      Assert.Equal( correctPlacement, board.BlackBishop.Bits );
    }

    [Fact]
    public void InitializeGame_IsWhiteKnightsCorrectlyPlaced_Equal() {
      ChessBoard board = new ChessBoard();
      board.InitializeGame();
      BoardSquare correctPlacement = BoardSquare.B1 | BoardSquare.G1;

      Assert.Equal( correctPlacement, board.WhiteKnight.Bits );
    }

    [Fact]
    public void InitializeGame_IsBlackKnightsCorrectlyPlaced_Equal() {
      ChessBoard board = new ChessBoard();
      board.InitializeGame();
      BoardSquare correctPlacement = BoardSquare.B8 | BoardSquare.G8;

      Assert.Equal( correctPlacement, board.BlackKnight.Bits );
    }

    [Fact]
    public void InitializeGame_IsWhitePawnsCorrectlyPlaced_Equal() {
      ChessBoard board = new ChessBoard();
      board.InitializeGame();
      BoardSquare correctPlacement = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;

      Assert.Equal( correctPlacement, board.WhitePawn.Bits );
    }

    [Fact]
    public void InitializeGame_IsBlackPawnsCorrectlyPlaced_Equal() {
      ChessBoard board = new ChessBoard();
      board.InitializeGame();
      BoardSquare correctPlacement = BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

      Assert.Equal( correctPlacement, board.BlackPawn.Bits );
    }

    [Fact]
    public void InitializeGame_IsPiecesNotPlacedInMid_Equal() {
      ChessBoard board = new ChessBoard();
      board.InitializeGame();
      BoardSquare correctPlacement = 
        BoardSquare.A8 | BoardSquare.B8 | BoardSquare.C8 | BoardSquare.D8 | BoardSquare.E8 | BoardSquare.F8 | BoardSquare.G8 | BoardSquare.H8 |
        BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7 |
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2 |
        BoardSquare.A1 | BoardSquare.B1 | BoardSquare.C1 | BoardSquare.D1 | BoardSquare.E1 | BoardSquare.F1 | BoardSquare.G1 | BoardSquare.H1;
      BoardSquare currentPlacement = BoardSquare.Empty;

      currentPlacement |= board.WhiteKing.Bits;
      currentPlacement |= board.WhiteQueen.Bits;
      currentPlacement |= board.WhiteRook.Bits;
      currentPlacement |= board.WhiteBishop.Bits;
      currentPlacement |= board.WhiteKnight.Bits;
      currentPlacement |= board.WhitePawn.Bits;

      currentPlacement |= board.BlackKing.Bits;
      currentPlacement |= board.BlackQueen.Bits;
      currentPlacement |= board.BlackRook.Bits;
      currentPlacement |= board.BlackBishop.Bits;
      currentPlacement |= board.BlackKnight.Bits;
      currentPlacement |= board.BlackPawn.Bits;


      Assert.Equal( correctPlacement, currentPlacement );
    }

    [Fact]
    public void InitializeScenario_IsPiecesNull_Equal() {
      ChessBoard board = new ChessBoard();
      board.InitializeScenario( null );

      BoardSquare correctPlacement = BoardSquare.Empty;
      BoardSquare currentPlacement = BoardSquare.Empty;

      currentPlacement |= board.WhiteKing.Bits;
      currentPlacement |= board.WhiteQueen.Bits;
      currentPlacement |= board.WhiteRook.Bits;
      currentPlacement |= board.WhiteBishop.Bits;
      currentPlacement |= board.WhiteKnight.Bits;
      currentPlacement |= board.WhitePawn.Bits;

      currentPlacement |= board.BlackKing.Bits;
      currentPlacement |= board.BlackQueen.Bits;
      currentPlacement |= board.BlackRook.Bits;
      currentPlacement |= board.BlackBishop.Bits;
      currentPlacement |= board.BlackKnight.Bits;
      currentPlacement |= board.BlackPawn.Bits;

      Assert.Equal( correctPlacement, currentPlacement );
    }

    [Fact]
    public void InitializeScenario_IsListEmpty_Equal() {
      ChessBoard board = new ChessBoard();

      board.InitializeScenario( new ScenarioList { } );

      BoardSquare correctPlacement = BoardSquare.Empty;
      BoardSquare currentPlacement = BoardSquare.Empty;

      currentPlacement |= board.WhiteKing.Bits;
      currentPlacement |= board.WhiteQueen.Bits;
      currentPlacement |= board.WhiteRook.Bits;
      currentPlacement |= board.WhiteBishop.Bits;
      currentPlacement |= board.WhiteKnight.Bits;
      currentPlacement |= board.WhitePawn.Bits;

      currentPlacement |= board.BlackKing.Bits;
      currentPlacement |= board.BlackQueen.Bits;
      currentPlacement |= board.BlackRook.Bits;
      currentPlacement |= board.BlackBishop.Bits;
      currentPlacement |= board.BlackKnight.Bits;
      currentPlacement |= board.BlackPawn.Bits;

      Assert.Equal( correctPlacement, currentPlacement );
    }

    [Fact]
    public void InitializeScenario_MultiplePiecesOnSquare_Exception() {
      ChessBoard board = new ChessBoard();

      Assert.Throws<P5.ChessBoard.IllegalPiecePlacementException>( (Assert.ThrowsDelegate)(
        () => {
          board.InitializeScenario( new ScenarioList {
            { BoardSquare.A8, ChessPieceType.King,   ChessPieceColors.Black },
            { BoardSquare.A8, ChessPieceType.King, ChessPieceColors.Black }
          } );
        }
      ) );
    }

    [Fact]
    public void InitializeScenario_UseCaseScenario_Equal() {
      BoardSquare correctPlacement = ( BoardSquare.A8 | BoardSquare.H1 );
      ChessBoard board = new ChessBoard();
      board.InitializeScenario( new ScenarioList {
        { BoardSquare.A8, ChessPieceType.King,   ChessPieceColors.Black },
        { BoardSquare.H1, ChessPieceType.King, ChessPieceColors.Black }
      } );

      Assert.Equal( correctPlacement, (BoardSquare)board.BlackKing.Bits );
    }

    [Fact]
    public void InitializeScenario_IsEnumBoardSquareCorrect_Equal() {

      BoardSquare correctA1Placement = (BoardSquare)0x0000000000000080u;

      Assert.Equal( (BoardSquare)correctA1Placement, BoardSquare.A1 );

      BoardSquare correctA2Placement = (BoardSquare)0x0000000000008000u;

      Assert.Equal( (BoardSquare)correctA2Placement, BoardSquare.A2 );

      BoardSquare correctA3Placement = (BoardSquare)0x0000000000800000u;

      Assert.Equal( (BoardSquare)correctA3Placement, BoardSquare.A3 );

      BoardSquare correctA4Placement = (BoardSquare)0x0000000080000000u;

      Assert.Equal( (BoardSquare)correctA4Placement, BoardSquare.A4 );

      BoardSquare correctA5Placement = (BoardSquare)0x0000008000000000u;

      Assert.Equal( (BoardSquare)correctA5Placement, BoardSquare.A5 );

      BoardSquare correctA6Placement = (BoardSquare)0x0000800000000000u;

      Assert.Equal( (BoardSquare)correctA6Placement, BoardSquare.A6 );

      BoardSquare correctA7Placement = (BoardSquare)0x0080000000000000u;

      Assert.Equal( (BoardSquare)correctA7Placement, BoardSquare.A7 );

      BoardSquare correctA8Placement = (BoardSquare)0x8000000000000000u;

      Assert.Equal( (ulong)correctA8Placement, (ulong)BoardSquare.A8 );


    }

    [Fact]
    public void Update_IsWhiteKingCorrectlyMoved_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      KingBitBoard move = new KingBitBoard( ChessPieceColors.White );
      chessBoard.WhiteKing.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H3;

      chessBoard.Update( move );

      Assert.Equal( move.Bits, chessBoard.WhiteKing.Bits );
    }

    [Fact]
    public void Update_IsBlackKingCorrectlyMoved_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      KingBitBoard move = new KingBitBoard( ChessPieceColors.Black );
      chessBoard.BlackKing.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H3;

      chessBoard.Update( move );

      Assert.Equal( move.Bits, chessBoard.BlackKing.Bits );
    }

    [Fact]
    public void Update_IsWhiteQueenCorrectlyMoved_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      QueenBitBoard move = new QueenBitBoard( ChessPieceColors.White );
      chessBoard.WhiteQueen.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H3;

      chessBoard.Update( move );

      Assert.Equal( move.Bits, chessBoard.WhiteQueen.Bits );
    }

    [Fact]
    public void Update_IsBlackQueenCorrectlyMoved_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      QueenBitBoard move = new QueenBitBoard( ChessPieceColors.Black );
      chessBoard.BlackQueen.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H3;

      chessBoard.Update( move );

      Assert.Equal( move.Bits, chessBoard.BlackQueen.Bits );
    }

    [Fact]
    public void Update_IsWhiteRookCorrectlyMoved_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      RookBitBoard move = new RookBitBoard( ChessPieceColors.White );
      chessBoard.WhiteRook.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H3;

      chessBoard.Update( move );

      Assert.Equal( move.Bits, chessBoard.WhiteRook.Bits );
    }

    [Fact]
    public void Update_IsBlackRookCorrectlyMoved_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      RookBitBoard move = new RookBitBoard( ChessPieceColors.Black );
      chessBoard.BlackRook.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H3;

      chessBoard.Update( move );

      Assert.Equal( move.Bits, chessBoard.BlackRook.Bits );
    }

    [Fact]
    public void Update_IsWhiteBishopCorrectlyMoved_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      BishopBitBoard move = new BishopBitBoard( ChessPieceColors.White );
      chessBoard.WhiteBishop.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H3;

      chessBoard.Update( move );

      Assert.Equal( move.Bits, chessBoard.WhiteBishop.Bits );
    }

    [Fact]
    public void Update_IsBlackBishopCorrectlyMoved_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      BishopBitBoard move = new BishopBitBoard( ChessPieceColors.Black );
      chessBoard.BlackBishop.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H3;

      chessBoard.Update( move );

      Assert.Equal( move.Bits, chessBoard.BlackBishop.Bits );
    }

    [Fact]
    public void Update_IsWhiteKnightCorrectlyMoved_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      KnightBitBoard move = new KnightBitBoard( ChessPieceColors.White );
      chessBoard.WhiteKnight.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H3;

      chessBoard.Update( move );

      Assert.Equal( move.Bits, chessBoard.WhiteKnight.Bits );
    }

    [Fact]
    public void Update_IsBlackKnightCorrectlyMoved_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      KnightBitBoard move = new KnightBitBoard( ChessPieceColors.Black );
      chessBoard.BlackKnight.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H3;

      chessBoard.Update( move );

      Assert.Equal( move.Bits, chessBoard.BlackKnight.Bits );
    }

    [Fact]
    public void Update_IsWhitePawnCorrectlyMoved_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard move = new PawnBitBoard( ChessPieceColors.White );
      chessBoard.WhitePawn.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H3;

      chessBoard.Update( move );

      Assert.Equal( move.Bits, chessBoard.WhitePawn.Bits );
    }

    [Fact]
    public void Update_IsBlackPawnCorrectlyMoved_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard move = new PawnBitBoard( ChessPieceColors.Black );
      chessBoard.BlackPawn.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H3;

      chessBoard.Update( move );

      Assert.Equal( move.Bits, chessBoard.BlackPawn.Bits );
    }

    [Fact]
    public void Update_IsBlackKingCorrectlyCaptured_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.WhiteKing.Bits = BoardSquare.Full ^ BoardSquare.D3;
      chessBoard.BlackKing.Bits = BoardSquare.D3;

      KingBitBoard move = new KingBitBoard( ChessPieceColors.Black );
      move.Bits = BoardSquare.C2;

      chessBoard.Update( move );

      KingBitBoard correctPlacement = new KingBitBoard( ChessPieceColors.White );
      correctPlacement.Bits = BoardSquare.Full ^ ( BoardSquare.C2 | BoardSquare.D3 );

      Assert.Equal( correctPlacement.Bits, chessBoard.WhiteKing.Bits );
    }

    [Fact]
    public void Update_IsWhiteKingCorrectlyCaptured_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.BlackKing.Bits = BoardSquare.Full ^ BoardSquare.D3;
      chessBoard.WhiteKing.Bits = BoardSquare.D3;

      KingBitBoard move = new KingBitBoard( ChessPieceColors.White );
      move.Bits = BoardSquare.C2;

      chessBoard.Update( move );

      KingBitBoard correctPlacement = new KingBitBoard( ChessPieceColors.Black );
      correctPlacement.Bits = BoardSquare.Full ^ ( BoardSquare.C2 | BoardSquare.D3 );

      Assert.Equal( correctPlacement.Bits, chessBoard.BlackKing.Bits );
    }

    [Fact]
    public void Update_IsBlackQueenCorrectlyCaptured_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.WhiteQueen.Bits = BoardSquare.Full ^ BoardSquare.D3;
      chessBoard.BlackQueen.Bits = BoardSquare.D3;

      QueenBitBoard move = new QueenBitBoard( ChessPieceColors.Black );
      move.Bits = BoardSquare.C2;

      chessBoard.Update( move );

      QueenBitBoard correctPlacement = new QueenBitBoard( ChessPieceColors.White );
      correctPlacement.Bits = BoardSquare.Full ^ ( BoardSquare.C2 | BoardSquare.D3 );

      Assert.Equal( correctPlacement.Bits, chessBoard.WhiteQueen.Bits );
    }

    [Fact]
    public void Update_IsWhiteQueenCorrectlyCaptured_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.BlackQueen.Bits = BoardSquare.Full ^ BoardSquare.D3;
      chessBoard.WhiteQueen.Bits = BoardSquare.D3;
      chessBoard.WhiteBishop.Bits = BoardSquare.B1;

      BishopBitBoard move = new BishopBitBoard( ChessPieceColors.White );
      move.Bits = BoardSquare.C2;

      chessBoard.Update( move );

      QueenBitBoard correctPlacement = new QueenBitBoard( ChessPieceColors.Black );
      correctPlacement.Bits = BoardSquare.Full ^ ( BoardSquare.C2 | BoardSquare.D3 );

      Assert.Equal( correctPlacement.Bits, chessBoard.BlackQueen.Bits );
    }

    [Fact]
    public void Update_IsBlackBishopCorrectlyCaptured_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.WhiteBishop.Bits = BoardSquare.Full ^ BoardSquare.D3;
      chessBoard.BlackBishop.Bits = BoardSquare.D3;

      BishopBitBoard move = new BishopBitBoard( ChessPieceColors.Black );
      move.Bits = BoardSquare.C2;

      chessBoard.Update( move );

      BishopBitBoard correctPlacement = new BishopBitBoard( ChessPieceColors.White );
      correctPlacement.Bits = BoardSquare.Full ^ ( BoardSquare.C2 | BoardSquare.D3 );

      Assert.Equal( correctPlacement.Bits, chessBoard.WhiteBishop.Bits );
    }

    [Fact]
    public void Update_IsWhiteBishopCorrectlyCaptured_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.BlackBishop.Bits = BoardSquare.Full ^ BoardSquare.D3;
      chessBoard.WhiteBishop.Bits = BoardSquare.D3;

      BishopBitBoard move = new BishopBitBoard( ChessPieceColors.White );
      move.Bits = BoardSquare.C2;

      chessBoard.Update( move );

      BishopBitBoard correctPlacement = new BishopBitBoard( ChessPieceColors.Black );
      correctPlacement.Bits = BoardSquare.Full ^ ( BoardSquare.C2 | BoardSquare.D3 );

      Assert.Equal( correctPlacement.Bits, chessBoard.BlackBishop.Bits );
    }

    [Fact]
    public void Update_IsBlackKnightCorrectlyCaptured_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.WhiteKnight.Bits = BoardSquare.Full ^ BoardSquare.D3;
      chessBoard.BlackKnight.Bits = BoardSquare.D3;

      KnightBitBoard move = new KnightBitBoard( ChessPieceColors.Black );
      move.Bits = BoardSquare.C2;

      chessBoard.Update( move );

      KnightBitBoard correctPlacement = new KnightBitBoard( ChessPieceColors.White );
      correctPlacement.Bits = BoardSquare.Full ^ ( BoardSquare.C2 | BoardSquare.D3 );

      Assert.Equal( correctPlacement.Bits, chessBoard.WhiteKnight.Bits );
    }

    [Fact]
    public void Update_IsWhiteKnightCorrectlyCaptured_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.BlackKnight.Bits = BoardSquare.Full ^ BoardSquare.D3;
      chessBoard.WhiteKnight.Bits = BoardSquare.D3;

      KnightBitBoard move = new KnightBitBoard( ChessPieceColors.White );
      move.Bits = BoardSquare.C2;

      chessBoard.Update( move );

      KnightBitBoard correctPlacement = new KnightBitBoard( ChessPieceColors.Black );
      correctPlacement.Bits = BoardSquare.Full ^ ( BoardSquare.C2 | BoardSquare.D3 );

      Assert.Equal( correctPlacement.Bits, chessBoard.BlackKnight.Bits );
    }

    [Fact]
    public void Update_IsBlackPawnCorrectlyCaptured_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.WhitePawn.Bits = BoardSquare.C2;
      chessBoard.BlackPawn.Bits = BoardSquare.D3;

      PawnBitBoard move = new PawnBitBoard( ChessPieceColors.Black );
      move.Bits = BoardSquare.C2;

      chessBoard.Update( move );

      PawnBitBoard correctPlacement = new PawnBitBoard( ChessPieceColors.White );
      correctPlacement.Bits = BoardSquare.Empty;

      Assert.Equal( correctPlacement.Bits, chessBoard.WhitePawn.Bits );
    }

    [Fact]
    public void Update_IsWhitePawnCorrectlyCaptured_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.BlackPawn.Bits = BoardSquare.C2;
      chessBoard.WhitePawn.Bits = BoardSquare.D3;

      PawnBitBoard move = new PawnBitBoard( ChessPieceColors.White );
      move.Bits = BoardSquare.C2;

      chessBoard.Update( move );

      PawnBitBoard correctPlacement = new PawnBitBoard( ChessPieceColors.Black );
      correctPlacement.Bits = BoardSquare.Empty;

      Assert.Equal( correctPlacement.Bits, chessBoard.BlackPawn.Bits );
    }

    [Fact]
    public void Update_IsWhitePawnMovingTwoSquaresReturnsCorrectlyBoardSquare_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard pawnMovingTwoSquares = new PawnBitBoard( ChessPieceColors.White );
      chessBoard.WhitePawn.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      pawnMovingTwoSquares.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E4 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;

      chessBoard.Update( pawnMovingTwoSquares );

      BoardSquare correctBoardSquare = BoardSquare.E4;

      Assert.Equal( correctBoardSquare, chessBoard.WhitePawn.MovedTwoSquares );
    }

    [Fact]
    public void Update_IsBlackPawnMovingTwoSquaresReturnsCorrectlyBoardSquare_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard pawnMovingTwoSquares = new PawnBitBoard( ChessPieceColors.Black );
      chessBoard.BlackPawn.Bits =
        BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      pawnMovingTwoSquares.Bits =
        BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E5 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

      chessBoard.Update( pawnMovingTwoSquares );

      BoardSquare correctBoardSquare = BoardSquare.E5;

      Assert.Equal( correctBoardSquare, chessBoard.BlackPawn.MovedTwoSquares );
    }

    [Fact]
    public void Update_IsWhitePawnMovingTwoSquaresRepeatedReturnsCorrectlyBoardSquare_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard whitePawnMovingTwoSquares = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard blackPawnMove = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard whitePawnMovingTwoSquaresAgain = new PawnBitBoard( ChessPieceColors.White );
      chessBoard.WhitePawn.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      chessBoard.BlackPawn.Bits =
        BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      whitePawnMovingTwoSquares.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E4 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      blackPawnMove.Bits =
         BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C6 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      whitePawnMovingTwoSquaresAgain.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D4 | BoardSquare.E4 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;

      chessBoard.Update( whitePawnMovingTwoSquares );
      chessBoard.Update( blackPawnMove );
      chessBoard.Update( whitePawnMovingTwoSquaresAgain );

      BoardSquare correctBoardSquare = BoardSquare.D4;

      Assert.Equal( correctBoardSquare, chessBoard.WhitePawn.MovedTwoSquares );
    }

    [Fact]
    public void Update_IsBlackPawnMovingTwoSquaresRepeatedReturnsCorrectlyBoardSquare_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard blackPawnMovingTwoSquares = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard whitePawnMove = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard blackPawnMove = new PawnBitBoard( ChessPieceColors.Black );
      chessBoard.WhitePawn.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      chessBoard.BlackPawn.Bits =
        BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      blackPawnMovingTwoSquares.Bits =
        BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E5 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      whitePawnMove.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E3 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      blackPawnMove.Bits =
        BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D5 | BoardSquare.E5 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

      chessBoard.Update( blackPawnMovingTwoSquares );
      chessBoard.Update( whitePawnMove );
      chessBoard.Update( blackPawnMove );

      BoardSquare correctBoardSquare = BoardSquare.D5;

      Assert.Equal( correctBoardSquare, chessBoard.BlackPawn.MovedTwoSquares );
    }

    [Fact]
    public void Update_IsWhitePawnMovedTwoSquaresReturnsEmptyBoardSquare_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard pawnMovingOneSquare = new PawnBitBoard( ChessPieceColors.White );
      chessBoard.WhitePawn.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      pawnMovingOneSquare.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E3 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;

      chessBoard.Update( pawnMovingOneSquare );

      BoardSquare correctBoardSquare = BoardSquare.Empty;

      Assert.Equal( correctBoardSquare, chessBoard.WhitePawn.MovedTwoSquares );
    }

    [Fact]
    public void Update_IsBlackPawnMovingTwoSquaresReturnsEmptyBoardSquare_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard pawnMovingOneSquare = new PawnBitBoard( ChessPieceColors.Black );
      chessBoard.BlackPawn.Bits =
        BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      pawnMovingOneSquare.Bits =
        BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E6 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

      chessBoard.Update( pawnMovingOneSquare );

      BoardSquare correctBoardSquare = BoardSquare.Empty;

      Assert.Equal( correctBoardSquare, chessBoard.BlackPawn.MovedTwoSquares );
    }

    [Fact]
    public void Update_IsWhitePawnMovingTwoSquaresSetToEmptyAfterTurn_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard whitePawnMovingTwoSquares = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard blackPawnMove = new PawnBitBoard( ChessPieceColors.Black );
      KnightBitBoard whiteKnightMove = new KnightBitBoard( ChessPieceColors.White );
      chessBoard.WhitePawn.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      chessBoard.BlackPawn.Bits =
        BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      whitePawnMovingTwoSquares.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E4 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      blackPawnMove.Bits =
        BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C6 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      chessBoard.WhiteKnight.Bits = BoardSquare.C4;
      whiteKnightMove.Bits =
        BoardSquare.A2;

      chessBoard.Update( whitePawnMovingTwoSquares );
      chessBoard.Update( blackPawnMove );
      chessBoard.Update( whiteKnightMove );

      BoardSquare correctBoardSquare = BoardSquare.Empty;

      Assert.Equal( correctBoardSquare, chessBoard.WhitePawn.MovedTwoSquares );
    }

    [Fact]
    public void Update_IsBlackPawnMovingTwoSquaresSetToEmptyAfterTurn_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard blackPawnMovingTwoSquares = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard whitePawnMove = new PawnBitBoard( ChessPieceColors.White );
      KnightBitBoard blackKnightMove = new KnightBitBoard( ChessPieceColors.Black );
      chessBoard.WhitePawn.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      chessBoard.BlackPawn.Bits =
        BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      blackPawnMovingTwoSquares.Bits =
        BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E5 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      whitePawnMove.Bits =
        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E3 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      chessBoard.BlackKnight.Bits = BoardSquare.B5;
      blackKnightMove.Bits =
        BoardSquare.A7;

      chessBoard.Update( blackPawnMovingTwoSquares );
      chessBoard.Update( whitePawnMove );
      chessBoard.Update( blackKnightMove );

      BoardSquare correctBoardSquare = BoardSquare.Empty;

      Assert.Equal( correctBoardSquare, chessBoard.BlackPawn.MovedTwoSquares );
    }

    [Fact]
    public void Update_IsWhiteKingCastlingShortCorrect_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      KingBitBoard shortCastling = new KingBitBoard( ChessPieceColors.White );
      chessBoard.WhiteKing.Initialize( null );
      chessBoard.WhiteRook.Initialize( null );
      shortCastling.Bits = BoardSquare.G1;

      chessBoard.Update( shortCastling );

      BoardSquare correctKingPlacement = BoardSquare.G1;
      BoardSquare correctRooksPlacement = BoardSquare.A1 | BoardSquare.F1;

      Assert.Equal( correctKingPlacement, chessBoard.WhiteKing.Bits );
      Assert.Equal( correctRooksPlacement, chessBoard.WhiteRook.Bits );
    }

    [Fact]
    public void Update_IsWhiteKingCastlingLongCorrect_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      KingBitBoard longCastling = new KingBitBoard( ChessPieceColors.White );
      chessBoard.WhiteKing.Initialize( null );
      chessBoard.WhiteRook.Initialize( null );
      longCastling.Bits = BoardSquare.C1;

      chessBoard.Update( longCastling );

      BoardSquare correctKingPlacement = BoardSquare.C1;
      BoardSquare correctRooksPlacement = BoardSquare.D1 | BoardSquare.H1;

      Assert.Equal( correctKingPlacement, chessBoard.WhiteKing.Bits );
      Assert.Equal( correctRooksPlacement, chessBoard.WhiteRook.Bits );
    }

    [Fact]
    public void Update_IsBlackKingCastlingShortCorrect_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      KingBitBoard shortCastling = new KingBitBoard( ChessPieceColors.Black );
      chessBoard.BlackKing.Initialize( null );
      chessBoard.BlackRook.Initialize( null );
      shortCastling.Bits = BoardSquare.G8;

      chessBoard.Update( shortCastling );

      BoardSquare correctKingPlacement = BoardSquare.G8;
      BoardSquare correctRooksPlacement = BoardSquare.A8 | BoardSquare.F8;

      Assert.Equal( correctKingPlacement, chessBoard.BlackKing.Bits );
      Assert.Equal( correctRooksPlacement, chessBoard.BlackRook.Bits );
    }

    [Fact]
    public void Update_IsBlackKingCastlingLongCorrect_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      KingBitBoard longCastling = new KingBitBoard( ChessPieceColors.Black );
      chessBoard.BlackKing.Initialize( null );
      chessBoard.BlackRook.Initialize( null );
      longCastling.Bits = BoardSquare.C8;

      chessBoard.Update( longCastling );

      BoardSquare correctKingPlacement = BoardSquare.C8;
      BoardSquare correctRooksPlacement = BoardSquare.D8 | BoardSquare.H8;

      Assert.Equal( correctKingPlacement, chessBoard.BlackKing.Bits );
      Assert.Equal( correctRooksPlacement, chessBoard.BlackRook.Bits );
    }

    [Fact]
    public void Update_whiteKingMoveShouldNotActiveCastlingShort_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      KingBitBoard whiteKingMove = new KingBitBoard( ChessPieceColors.White );
      chessBoard.WhiteKing.Bits = BoardSquare.H1;
      whiteKingMove.Bits = BoardSquare.G1;

      chessBoard.Update( whiteKingMove );

      BoardSquare correctRooksPlacement = BoardSquare.Empty;
      BoardSquare correctKingPlacement = BoardSquare.G1;

      Assert.Equal( correctRooksPlacement, chessBoard.WhiteRook.Bits );
      Assert.Equal( correctKingPlacement, chessBoard.WhiteKing.Bits );
    }

    [Fact]
    public void Update_whiteKingMoveShouldNotActiveCastlingLong_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      KingBitBoard whiteKingMove = new KingBitBoard( ChessPieceColors.White );
      chessBoard.WhiteKing.Bits = BoardSquare.B1;
      whiteKingMove.Bits = BoardSquare.C1;

      chessBoard.Update( whiteKingMove );

      BoardSquare correctRooksPlacement = BoardSquare.Empty;
      BoardSquare correctKingPlacement = BoardSquare.C1;

      Assert.Equal( correctRooksPlacement, chessBoard.WhiteRook.Bits );
      Assert.Equal( correctKingPlacement, chessBoard.WhiteKing.Bits );
    }

    [Fact]
    public void Update_blackKingMoveShouldNotActiveCastlingShort_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      KingBitBoard blackKingMove = new KingBitBoard( ChessPieceColors.Black );
      chessBoard.BlackKing.Bits = BoardSquare.H8;
      blackKingMove.Bits = BoardSquare.G8;

      chessBoard.Update( blackKingMove );

      BoardSquare correctRooksPlacement = BoardSquare.Empty;
      BoardSquare correctKingPlacement = BoardSquare.G8;

      Assert.Equal( correctRooksPlacement, chessBoard.BlackRook.Bits );
      Assert.Equal( correctKingPlacement, chessBoard.BlackKing.Bits );
    }

    [Fact]
    public void Update_blackKingMoveShouldNotActiveCastlingLong_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      KingBitBoard blackKingMove = new KingBitBoard( ChessPieceColors.Black );
      chessBoard.BlackKing.Bits = BoardSquare.B8;
      blackKingMove.Bits = BoardSquare.C8;

      chessBoard.Update( blackKingMove );

      BoardSquare correctRooksPlacement = BoardSquare.Empty;
      BoardSquare correctKingPlacement = BoardSquare.C8;

      Assert.Equal( correctRooksPlacement, chessBoard.BlackRook.Bits );
      Assert.Equal( correctKingPlacement, chessBoard.BlackKing.Bits );
    }

    [Fact]
    public void Update_IsWhiteEnPassantCapture_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.BlackPawn.Bits = BoardSquare.E7;
      chessBoard.WhitePawn.Bits = BoardSquare.D5;

      PawnBitBoard blackMove = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard enPassant = new PawnBitBoard( ChessPieceColors.White );

      blackMove.Bits = BoardSquare.E5;
      enPassant.Bits = BoardSquare.E6;

      chessBoard.Update( blackMove );
      chessBoard.Update( enPassant );

      BoardSquare correctBlackPawnPlacement = BoardSquare.Empty;

      Assert.Equal( correctBlackPawnPlacement, chessBoard.BlackPawn.Bits );
    }

    [Fact]
    public void Update_IsBlackEnPassantCapture_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.BlackPawn.Bits = BoardSquare.D4;
      chessBoard.WhitePawn.Bits = BoardSquare.E2;

      PawnBitBoard whiteMove = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard enPassant = new PawnBitBoard( ChessPieceColors.Black );

      whiteMove.Bits = BoardSquare.E4;
      enPassant.Bits = BoardSquare.E3;

      chessBoard.Update( whiteMove );
      chessBoard.Update( enPassant );

      BoardSquare correctWhitePawnPlacement = BoardSquare.Empty;

      Assert.Equal( correctWhitePawnPlacement, chessBoard.WhitePawn.Bits );
    }

    [Fact]
    public void Update_DoWhitePawnPromotionSpawnQueenCorrectly_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard pawnMove = new PawnBitBoard( ChessPieceColors.White );
      chessBoard.BlackQueen.Bits = BoardSquare.D8;
      chessBoard.WhiteQueen.Bits = BoardSquare.A8;
      chessBoard.WhitePawn.Bits = BoardSquare.E7 | BoardSquare.A2 | BoardSquare.B2 | BoardSquare.E6 | BoardSquare.H7;
      pawnMove.Bits = BoardSquare.D8 | BoardSquare.A2 | BoardSquare.B2 | BoardSquare.E6 | BoardSquare.H7; ;
      pawnMove.Promote( PawnBitBoard.PromotionPiece.Queen );

      chessBoard.Update( pawnMove );

      BoardSquare correctPawnPlacement = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.E6 | BoardSquare.H7;
      BoardSquare correctWhiteQueenPlacement = BoardSquare.A8 | BoardSquare.D8;
      BoardSquare correctBlackQueenPlacement = BoardSquare.Empty;

      Assert.Equal( correctPawnPlacement, chessBoard.WhitePawn.Bits );
      Assert.Equal( correctWhiteQueenPlacement, chessBoard.WhiteQueen.Bits );
      Assert.Equal( correctBlackQueenPlacement, chessBoard.BlackQueen.Bits );
    }

    [Fact]
    public void Update_DoBlackPawnPromotionSpawnQueenCorrectly_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard pawnMove = new PawnBitBoard( ChessPieceColors.Black );
      chessBoard.WhiteQueen.Bits = BoardSquare.D1;
      chessBoard.BlackQueen.Bits = BoardSquare.A1;
      chessBoard.BlackPawn.Bits = BoardSquare.E2 | BoardSquare.A7 | BoardSquare.B7 | BoardSquare.E3 | BoardSquare.H2;
      pawnMove.Bits = BoardSquare.D1 | BoardSquare.A7 | BoardSquare.B7 | BoardSquare.E3 | BoardSquare.H2;
      pawnMove.Promote( PawnBitBoard.PromotionPiece.Queen );

      chessBoard.Update( pawnMove );

      BoardSquare correctPawnPlacement = BoardSquare.A7 | BoardSquare.B7 | BoardSquare.E3 | BoardSquare.H2;
      BoardSquare correctBlackQueenPlacement = BoardSquare.A1 | BoardSquare.D1;
      BoardSquare correctWhiteQueenPlacement = BoardSquare.Empty;

      Assert.Equal( correctPawnPlacement, chessBoard.BlackPawn.Bits );
      Assert.Equal( correctBlackQueenPlacement, chessBoard.BlackQueen.Bits );
      Assert.Equal( correctWhiteQueenPlacement, chessBoard.WhiteQueen.Bits );
    }
    [Fact]
    public void Update_DoWhitePawnPromotionSpawnRookCorrectly_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard pawnMove = new PawnBitBoard( ChessPieceColors.White );
      chessBoard.BlackQueen.Bits = BoardSquare.D8;
      chessBoard.WhiteRook.Bits = BoardSquare.A8;
      chessBoard.WhitePawn.Bits = BoardSquare.E7 | BoardSquare.A2 | BoardSquare.B2 | BoardSquare.E6 | BoardSquare.H7;
      pawnMove.Bits = BoardSquare.D8 | BoardSquare.A2 | BoardSquare.B2 | BoardSquare.E6 | BoardSquare.H7; ;
      pawnMove.Promote( PawnBitBoard.PromotionPiece.Rook );

      chessBoard.Update( pawnMove );

      BoardSquare correctPawnPlacement = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.E6 | BoardSquare.H7;
      BoardSquare correctWhiteQueenPlacement = BoardSquare.A8 | BoardSquare.D8;
      BoardSquare correctBlackQueenPlacement = BoardSquare.Empty;

      Assert.Equal( correctPawnPlacement, chessBoard.WhitePawn.Bits );
      Assert.Equal( correctWhiteQueenPlacement, chessBoard.WhiteRook.Bits );
      Assert.Equal( correctBlackQueenPlacement, chessBoard.BlackQueen.Bits );
    }

    [Fact]
    public void Update_DoBlackPawnPromotionSpawnRookCorrectly_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard pawnMove = new PawnBitBoard( ChessPieceColors.Black );
      chessBoard.WhiteQueen.Bits = BoardSquare.D1;
      chessBoard.BlackRook.Bits = BoardSquare.A1;
      chessBoard.BlackPawn.Bits = BoardSquare.E2 | BoardSquare.A7 | BoardSquare.B7 | BoardSquare.E3 | BoardSquare.H2;
      pawnMove.Bits = BoardSquare.D1 | BoardSquare.A7 | BoardSquare.B7 | BoardSquare.E3 | BoardSquare.H2;
      pawnMove.Promote( PawnBitBoard.PromotionPiece.Rook );

      chessBoard.Update( pawnMove );

      BoardSquare correctPawnPlacement = BoardSquare.A7 | BoardSquare.B7 | BoardSquare.E3 | BoardSquare.H2;
      BoardSquare correctBlackQueenPlacement = BoardSquare.A1 | BoardSquare.D1;
      BoardSquare correctWhiteQueenPlacement = BoardSquare.Empty;

      Assert.Equal( correctPawnPlacement, chessBoard.BlackPawn.Bits );
      Assert.Equal( correctBlackQueenPlacement, chessBoard.BlackRook.Bits );
      Assert.Equal( correctWhiteQueenPlacement, chessBoard.WhiteQueen.Bits );
    }

    [Fact]
    public void Update_DoWhitePawnPromotionSpawnBishopCorrectly_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard pawnMove = new PawnBitBoard( ChessPieceColors.White );
      chessBoard.BlackQueen.Bits = BoardSquare.D8;
      chessBoard.WhiteBishop.Bits = BoardSquare.A8;
      chessBoard.WhitePawn.Bits = BoardSquare.E7 | BoardSquare.A2 | BoardSquare.B2 | BoardSquare.E6 | BoardSquare.H7;
      pawnMove.Bits = BoardSquare.D8 | BoardSquare.A2 | BoardSquare.B2 | BoardSquare.E6 | BoardSquare.H7; ;
      pawnMove.Promote( PawnBitBoard.PromotionPiece.Bishop );

      chessBoard.Update( pawnMove );

      BoardSquare correctPawnPlacement = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.E6 | BoardSquare.H7;
      BoardSquare correctWhiteQueenPlacement = BoardSquare.A8 | BoardSquare.D8;
      BoardSquare correctBlackQueenPlacement = BoardSquare.Empty;

      Assert.Equal( correctPawnPlacement, chessBoard.WhitePawn.Bits );
      Assert.Equal( correctWhiteQueenPlacement, chessBoard.WhiteBishop.Bits );
      Assert.Equal( correctBlackQueenPlacement, chessBoard.BlackQueen.Bits );
    }

    [Fact]
    public void Update_DoBlackPawnPromotionSpawnBishopCorrectly_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard pawnMove = new PawnBitBoard( ChessPieceColors.Black );
      chessBoard.WhiteQueen.Bits = BoardSquare.D1;
      chessBoard.BlackBishop.Bits = BoardSquare.A1;
      chessBoard.BlackPawn.Bits = BoardSquare.E2 | BoardSquare.A7 | BoardSquare.B7 | BoardSquare.E3 | BoardSquare.H2;
      pawnMove.Bits = BoardSquare.D1 | BoardSquare.A7 | BoardSquare.B7 | BoardSquare.E3 | BoardSquare.H2;
      pawnMove.Promote( PawnBitBoard.PromotionPiece.Bishop );

      chessBoard.Update( pawnMove );

      BoardSquare correctPawnPlacement = BoardSquare.A7 | BoardSquare.B7 | BoardSquare.E3 | BoardSquare.H2;
      BoardSquare correctBlackQueenPlacement = BoardSquare.A1 | BoardSquare.D1;
      BoardSquare correctWhiteQueenPlacement = BoardSquare.Empty;

      Assert.Equal( correctPawnPlacement, chessBoard.BlackPawn.Bits );
      Assert.Equal( correctBlackQueenPlacement, chessBoard.BlackBishop.Bits );
      Assert.Equal( correctWhiteQueenPlacement, chessBoard.WhiteQueen.Bits );
    }

    [Fact]
    public void Update_DoWhitePawnPromotionSpawnKnightCorrectly_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard pawnMove = new PawnBitBoard( ChessPieceColors.White );
      chessBoard.BlackQueen.Bits = BoardSquare.D8;
      chessBoard.WhiteKnight.Bits = BoardSquare.A8;
      chessBoard.WhitePawn.Bits = BoardSquare.E7 | BoardSquare.A2 | BoardSquare.B2 | BoardSquare.E6 | BoardSquare.H7;
      pawnMove.Bits = BoardSquare.D8 | BoardSquare.A2 | BoardSquare.B2 | BoardSquare.E6 | BoardSquare.H7; ;
      pawnMove.Promote( PawnBitBoard.PromotionPiece.Knight );

      chessBoard.Update( pawnMove );

      BoardSquare correctPawnPlacement = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.E6 | BoardSquare.H7;
      BoardSquare correctWhiteQueenPlacement = BoardSquare.A8 | BoardSquare.D8;
      BoardSquare correctBlackQueenPlacement = BoardSquare.Empty;

      Assert.Equal( correctPawnPlacement, chessBoard.WhitePawn.Bits );
      Assert.Equal( correctWhiteQueenPlacement, chessBoard.WhiteKnight.Bits );
      Assert.Equal( correctBlackQueenPlacement, chessBoard.BlackQueen.Bits );
    }

    [Fact]
    public void Update_DoBlackPawnPromotionSpawnKnightCorrectly_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard pawnMove = new PawnBitBoard( ChessPieceColors.Black );
      chessBoard.WhiteQueen.Bits = BoardSquare.D1;
      chessBoard.BlackKnight.Bits = BoardSquare.A1;
      chessBoard.BlackPawn.Bits = BoardSquare.E2 | BoardSquare.A7 | BoardSquare.B7 | BoardSquare.E3 | BoardSquare.H2;
      pawnMove.Bits = BoardSquare.D1 | BoardSquare.A7 | BoardSquare.B7 | BoardSquare.E3 | BoardSquare.H2;
      pawnMove.Promote( PawnBitBoard.PromotionPiece.Knight );

      chessBoard.Update( pawnMove );

      BoardSquare correctPawnPlacement = BoardSquare.A7 | BoardSquare.B7 | BoardSquare.E3 | BoardSquare.H2;
      BoardSquare correctBlackQueenPlacement = BoardSquare.A1 | BoardSquare.D1;
      BoardSquare correctWhiteQueenPlacement = BoardSquare.Empty;

      Assert.Equal( correctPawnPlacement, chessBoard.BlackPawn.Bits );
      Assert.Equal( correctBlackQueenPlacement, chessBoard.BlackKnight.Bits );
      Assert.Equal( correctWhiteQueenPlacement, chessBoard.WhiteQueen.Bits );
    }

    //[Fact]
    //public void Undo_WhiteKingMove_Equal() {
    //    ChessBoard chessBoard = new ChessBoard();
    //    KingBitBoard move = new KingBitBoard(ChessPieceColors.White);

    //    BoardSquare placement =
    //        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
    //    move.Bits =
    //        BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E3 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;

    //    chessBoard.WhiteKing.Bits = placement;
    //    chessBoard.Update(move);
    //    chessBoard.Undo();

    //    Assert.Equal(placement, chessBoard.WhiteKing.Bits);
    //}
    [Fact]
    public void Undo_WhiteQueenMove_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      QueenBitBoard move = new QueenBitBoard( ChessPieceColors.White );

      BoardSquare placement =
            BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E6 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;

      chessBoard.WhiteQueen.Bits = placement;
      chessBoard.Update( move );
      chessBoard.Undo();

      Assert.Equal( placement, chessBoard.WhiteQueen.Bits );
    }
    [Fact]
    public void Undo_WhiteRookMove_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      RookBitBoard move = new RookBitBoard( ChessPieceColors.White );

      BoardSquare placement =
            BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E6 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;

      chessBoard.WhiteRook.Bits = placement;
      chessBoard.Update( move );
      chessBoard.Undo();

      Assert.Equal( placement, chessBoard.WhiteRook.Bits );
    }
    [Fact]
    public void Undo_WhiteBishopMove_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      BishopBitBoard move = new BishopBitBoard( ChessPieceColors.White );

      BoardSquare placement =
            BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.H5 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;

      chessBoard.WhiteBishop.Bits = placement;
      chessBoard.Update( move );
      chessBoard.Undo();

      Assert.Equal( placement, chessBoard.WhiteBishop.Bits );
    }
    [Fact]
    public void Undo_WhiteKnightMove_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      KnightBitBoard move = new KnightBitBoard( ChessPieceColors.White );

      BoardSquare placement =
            BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.D4 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;

      chessBoard.WhiteKnight.Bits = placement;
      chessBoard.Update( move );
      chessBoard.Undo();

      Assert.Equal( placement, chessBoard.WhiteKnight.Bits );
    }
    [Fact]
    public void Undo_WhitePawnMove_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard move = new PawnBitBoard( ChessPieceColors.White );

      BoardSquare placement =
            BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E3 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;

      chessBoard.WhitePawn.Bits = placement;
      chessBoard.Update( move );
      chessBoard.Undo();

      Assert.Equal( placement, chessBoard.WhitePawn.Bits );
    }
    [Fact]
    public void Undo_WhitePawnMoveTwoSquares_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard move = new PawnBitBoard( ChessPieceColors.White );

      BoardSquare placement =
            BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E4 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;

      chessBoard.WhitePawn.Bits = placement;
      chessBoard.Update( move );
      chessBoard.Undo();

      Assert.Equal( placement, chessBoard.WhitePawn.Bits );
    }

    [Fact]
    public void Undo_WhitePawnMoveTwoSquaresTwoTimes_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard move = new PawnBitBoard( ChessPieceColors.White );

      BoardSquare placement =
            BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      move.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E4 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;

      chessBoard.WhitePawn.Bits = placement;
      chessBoard.Update( move );
      chessBoard.Undo();
      chessBoard.Update( move );
      chessBoard.Undo();

      Assert.Equal( placement, chessBoard.WhitePawn.Bits );
    }

    //[Fact]
    //public void Undo_BlackKingMove_Equal() {
    //    ChessBoard chessBoard = new ChessBoard();
    //    KingBitBoard move = new KingBitBoard(ChessPieceColors.Black);

    //    BoardSquare placement =
    //        BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
    //    move.Bits =
    //        BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E6 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

    //    chessBoard.BlackKing.Bits = placement;
    //    chessBoard.Update(move);
    //    chessBoard.Undo();

    //    Assert.Equal(placement, chessBoard.BlackKing.Bits);
    //}
    [Fact]
    public void Undo_BlackQueenMove_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      QueenBitBoard move = new QueenBitBoard( ChessPieceColors.Black );

      BoardSquare placement =
            BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      move.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E4 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

      chessBoard.BlackQueen.Bits = placement;
      chessBoard.Update( move );
      chessBoard.Undo();

      Assert.Equal( placement, chessBoard.BlackQueen.Bits );
    }
    [Fact]
    public void Undo_BlackRookMove_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      RookBitBoard move = new RookBitBoard( ChessPieceColors.Black );

      BoardSquare placement =
            BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      move.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E4 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

      chessBoard.BlackRook.Bits = placement;
      chessBoard.Update( move );
      chessBoard.Undo();

      Assert.Equal( placement, chessBoard.BlackRook.Bits );
    }
    [Fact]
    public void Undo_BlackBishopMove_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      BishopBitBoard move = new BishopBitBoard( ChessPieceColors.Black );

      BoardSquare placement =
            BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      move.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.C3 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

      chessBoard.BlackBishop.Bits = placement;
      chessBoard.Update( move );
      chessBoard.Undo();

      Assert.Equal( placement, chessBoard.BlackBishop.Bits );
    }
    [Fact]
    public void Undo_BlackKnightMove_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      KnightBitBoard move = new KnightBitBoard( ChessPieceColors.Black );

      BoardSquare placement =
            BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      move.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.D5 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

      chessBoard.BlackKnight.Bits = placement;
      chessBoard.Update( move );
      chessBoard.Undo();

      Assert.Equal( placement, chessBoard.BlackKnight.Bits );
    }

    [Fact]
    public void Undo_BlackPawnMove_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard move = new PawnBitBoard( ChessPieceColors.Black );

      BoardSquare placement =
            BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      move.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E6 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

      chessBoard.BlackPawn.Bits = placement;
      chessBoard.Update( move );
      chessBoard.Undo();

      Assert.Equal( placement, chessBoard.BlackPawn.Bits );
    }

    [Fact]
    public void Undo_BlackPawnMoveTwoSquares_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard move = new PawnBitBoard( ChessPieceColors.Black );

      BoardSquare placement =
            BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      move.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E5 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

      chessBoard.BlackPawn.Bits = placement;
      chessBoard.Update( move );
      chessBoard.Undo();

      Assert.Equal( placement, chessBoard.BlackPawn.Bits );
    }

    [Fact]
    public void Undo_BlackPawnMoveTwoSquaresTwoTimes_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard move = new PawnBitBoard( ChessPieceColors.Black );

      BoardSquare placement =
            BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      move.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E5 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

      chessBoard.BlackPawn.Bits = placement;
      chessBoard.Update( move );
      chessBoard.Undo();
      chessBoard.Update( move );
      chessBoard.Undo();

      Assert.Equal( placement, chessBoard.BlackPawn.Bits );
    }

    //[Fact]
    //public void Undo_WhiteKingCapture_Equal() {
    //  ChessBoard chessBoard = new ChessBoard();

    //  KingBitBoard kingCaptureMove = new KingBitBoard(ChessPieceColors.White);
    //  chessBoard.WhiteKing.Bits =
    //      BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
    //  chessBoard.BlackKing.Bits =
    //      BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E3 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
    //  kingCaptureMove.Bits =
    //      BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.E3 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
    //  BoardSquare correctWhiteKingPlacement = chessBoard.WhiteKing.Bits;
    //  BoardSquare correctBlackKingPlacement = chessBoard.BlackKing.Bits;

    //  chessBoard.Update(kingCaptureMove);
    //  chessBoard.Undo();

    //  Assert.Equal(correctWhiteKingPlacement, chessBoard.WhiteKing.Bits);
    //  Assert.Equal(correctBlackKingPlacement, chessBoard.BlackKing.Bits);
    //}
    [Fact]
    public void Undo_WhiteQueenCapture_Equal() {
      ChessBoard chessBoard = new ChessBoard();

      QueenBitBoard queenCaptureMove = new QueenBitBoard( ChessPieceColors.White );
      chessBoard.WhiteQueen.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      chessBoard.BlackQueen.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E3 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      queenCaptureMove.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.E3 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      BoardSquare correctWhiteQueenPlacement = chessBoard.WhiteQueen.Bits;
      BoardSquare correctBlackQueenPlacement = chessBoard.BlackQueen.Bits;

      chessBoard.Update( queenCaptureMove );
      chessBoard.Undo();

      Assert.Equal( correctWhiteQueenPlacement, chessBoard.WhiteQueen.Bits );
      Assert.Equal( correctBlackQueenPlacement, chessBoard.BlackQueen.Bits );
    }
    [Fact]
    public void Undo_WhiteRookCapture_Equal() {
      ChessBoard chessBoard = new ChessBoard();

      RookBitBoard rookCaptureMove = new RookBitBoard( ChessPieceColors.White );
      chessBoard.WhiteRook.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      chessBoard.BlackRook.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E3 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      rookCaptureMove.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E3 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      BoardSquare correctWhiteRookPlacement = chessBoard.WhiteRook.Bits;
      BoardSquare correctBlackRookPlacement = chessBoard.BlackRook.Bits;

      chessBoard.Update( rookCaptureMove );
      chessBoard.Undo();

      Assert.Equal( correctWhiteRookPlacement, chessBoard.WhiteRook.Bits );
      Assert.Equal( correctBlackRookPlacement, chessBoard.BlackRook.Bits );
    }
    [Fact]
    public void Undo_WhiteBishopCapture_Equal() {
      ChessBoard chessBoard = new ChessBoard();

      BishopBitBoard bishopCaptureMove = new BishopBitBoard( ChessPieceColors.White );
      chessBoard.WhiteBishop.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      chessBoard.BlackBishop.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E3 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      bishopCaptureMove.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.E3 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      BoardSquare correctWhiteBishopPlacement = chessBoard.WhiteBishop.Bits;
      BoardSquare correctBlackBishopPlacement = chessBoard.BlackBishop.Bits;

      chessBoard.Update( bishopCaptureMove );
      chessBoard.Undo();

      Assert.Equal( correctWhiteBishopPlacement, chessBoard.WhiteBishop.Bits );
      Assert.Equal( correctBlackBishopPlacement, chessBoard.BlackBishop.Bits );
    }
    [Fact]
    public void Undo_WhiteKnightCapture_Equal() {
      ChessBoard chessBoard = new ChessBoard();

      KnightBitBoard knightCaptureMove = new KnightBitBoard( ChessPieceColors.White );
      chessBoard.WhiteKnight.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      chessBoard.BlackKnight.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E4 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      knightCaptureMove.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.E4 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      BoardSquare correctWhiteKnightPlacement = chessBoard.WhiteKnight.Bits;
      BoardSquare correctBlackKnightPlacement = chessBoard.BlackKnight.Bits;

      chessBoard.Update( knightCaptureMove );
      chessBoard.Undo();

      Assert.Equal( correctWhiteKnightPlacement, chessBoard.WhiteKnight.Bits );
      Assert.Equal( correctBlackKnightPlacement, chessBoard.BlackKnight.Bits );
    }
    [Fact]
    public void Undo_WhitePawnCapture_Equal() {
      ChessBoard chessBoard = new ChessBoard();

      PawnBitBoard pawnCaptureMove = new PawnBitBoard( ChessPieceColors.White );
      chessBoard.WhitePawn.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      chessBoard.BlackPawn.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E3 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      pawnCaptureMove.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.E3 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      BoardSquare correctWhitePawnPlacement = chessBoard.WhitePawn.Bits;
      BoardSquare correctBlackPawnPlacement = chessBoard.BlackPawn.Bits;

      chessBoard.Update( pawnCaptureMove );
      chessBoard.Undo();

      Assert.Equal( correctWhitePawnPlacement, chessBoard.WhitePawn.Bits );
      Assert.Equal( correctBlackPawnPlacement, chessBoard.BlackPawn.Bits );
    }

    //[Fact]
    //public void Undo_BlackKingCapture_Equal() {
    //  ChessBoard chessBoard = new ChessBoard();

    //  KingBitBoard kingCaptureMove = new KingBitBoard(ChessPieceColors.Black);
    //  chessBoard.BlackKing.Bits =
    //      BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
    //  chessBoard.WhiteKing.Bits =
    //      BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E6 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
    //  kingCaptureMove.Bits =
    //      BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.E6 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
    //  BoardSquare correctBlackKingPlacement = chessBoard.BlackKing.Bits;
    //  BoardSquare correctWhiteKingPlacement = chessBoard.WhiteKing.Bits;

    //  chessBoard.Update(kingCaptureMove);
    //  chessBoard.Undo();

    //  Assert.Equal(correctBlackKingPlacement, chessBoard.BlackKing.Bits);
    //  Assert.Equal(correctWhiteKingPlacement, chessBoard.WhiteKing.Bits);
    //}
    [Fact]
    public void Undo_BlackQueenCapture_Equal() {
      ChessBoard chessBoard = new ChessBoard();

      QueenBitBoard queenCaptureMove = new QueenBitBoard( ChessPieceColors.Black );
      chessBoard.BlackQueen.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      chessBoard.WhiteQueen.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E6 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      queenCaptureMove.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.E6 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      BoardSquare correctBlackQueenPlacement = chessBoard.BlackQueen.Bits;
      BoardSquare correctWhiteQueenPlacement = chessBoard.WhiteQueen.Bits;

      chessBoard.Update( queenCaptureMove );
      chessBoard.Undo();

      Assert.Equal( correctBlackQueenPlacement, chessBoard.BlackQueen.Bits );
      Assert.Equal( correctWhiteQueenPlacement, chessBoard.WhiteQueen.Bits );
    }
    [Fact]
    public void Undo_BlackRookCapture_Equal() {
      ChessBoard chessBoard = new ChessBoard();

      RookBitBoard rookCaptureMove = new RookBitBoard( ChessPieceColors.Black );
      chessBoard.BlackRook.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      chessBoard.WhiteRook.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E6 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      rookCaptureMove.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E6 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      BoardSquare correctBlackRookPlacement = chessBoard.BlackRook.Bits;
      BoardSquare correctWhiteRookPlacement = chessBoard.WhiteRook.Bits;

      chessBoard.Update( rookCaptureMove );
      chessBoard.Undo();

      Assert.Equal( correctBlackRookPlacement, chessBoard.BlackRook.Bits );
      Assert.Equal( correctWhiteRookPlacement, chessBoard.WhiteRook.Bits );
    }
    [Fact]
    public void Undo_BlackBishopCapture_Equal() {
      ChessBoard chessBoard = new ChessBoard();

      BishopBitBoard bishopCaptureMove = new BishopBitBoard( ChessPieceColors.Black );
      chessBoard.BlackBishop.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      chessBoard.WhiteBishop.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E6 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      bishopCaptureMove.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.E6 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      BoardSquare correctBlackBishopPlacement = chessBoard.BlackBishop.Bits;
      BoardSquare correctWhiteBishopPlacement = chessBoard.WhiteBishop.Bits;

      chessBoard.Update( bishopCaptureMove );
      chessBoard.Undo();

      Assert.Equal( correctBlackBishopPlacement, chessBoard.BlackBishop.Bits );
      Assert.Equal( correctWhiteBishopPlacement, chessBoard.WhiteBishop.Bits );
    }
    [Fact]
    public void Undo_BlackKnightCapture_Equal() {
      ChessBoard chessBoard = new ChessBoard();

      KnightBitBoard knightCaptureMove = new KnightBitBoard( ChessPieceColors.Black );
      chessBoard.BlackKnight.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      chessBoard.WhiteKnight.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E5 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      knightCaptureMove.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.E5 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      BoardSquare correctBlackKnightPlacement = chessBoard.BlackKnight.Bits;
      BoardSquare correctWhiteKnightPlacement = chessBoard.WhiteKnight.Bits;

      chessBoard.Update( knightCaptureMove );
      chessBoard.Undo();

      Assert.Equal( correctBlackKnightPlacement, chessBoard.BlackKnight.Bits );
      Assert.Equal( correctWhiteKnightPlacement, chessBoard.WhiteKnight.Bits );
    }

    [Fact]
    public void Undo_BlackPawnCapture_Equal() {
      ChessBoard chessBoard = new ChessBoard();

      PawnBitBoard pawnCaptureMove = new PawnBitBoard( ChessPieceColors.Black );
      chessBoard.BlackPawn.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      chessBoard.WhitePawn.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E6 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      pawnCaptureMove.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.E6 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      BoardSquare correctBlackPawnPlacement = chessBoard.BlackPawn.Bits;
      BoardSquare correctWhitePawnPlacement = chessBoard.WhitePawn.Bits;

      chessBoard.Update( pawnCaptureMove );
      chessBoard.Undo();

      Assert.Equal( correctBlackPawnPlacement, chessBoard.BlackPawn.Bits );
      Assert.Equal( correctWhitePawnPlacement, chessBoard.WhitePawn.Bits );
    }

    [Fact]
    public void Undo_WhiteEnPassant_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard blackPawnTwoSquareMove = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard whitePawnEnPassantMove = new PawnBitBoard( ChessPieceColors.White );
      chessBoard.BlackPawn.Initialize( null );

      BoardSquare whitePawnPlacement =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E5 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      chessBoard.WhitePawn.Bits = whitePawnPlacement;
      blackPawnTwoSquareMove.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D5 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      whitePawnEnPassantMove.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D6 | BoardSquare.D2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;

      chessBoard.Update( blackPawnTwoSquareMove );
      chessBoard.Update( whitePawnEnPassantMove );
      chessBoard.Undo();

      Assert.Equal( whitePawnPlacement, chessBoard.WhitePawn.Bits );
      Assert.Equal( blackPawnTwoSquareMove.Bits, chessBoard.BlackPawn.Bits );
    }

    [Fact]
    public void Undo_WhiteEnPassantTwoTimes_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard blackPawnTwoSquareMove = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard whitePawnEnPassantMove = new PawnBitBoard( ChessPieceColors.White );
      chessBoard.BlackPawn.Initialize( null );

      BoardSquare whitePawnPlacement =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E5 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      chessBoard.WhitePawn.Bits = whitePawnPlacement;
      blackPawnTwoSquareMove.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D5 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      whitePawnEnPassantMove.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D6 | BoardSquare.D2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;

      chessBoard.Update( blackPawnTwoSquareMove );
      chessBoard.Update( whitePawnEnPassantMove );
      chessBoard.Undo();
      chessBoard.Update( whitePawnEnPassantMove );
      chessBoard.Undo();

      Assert.Equal( whitePawnPlacement, chessBoard.WhitePawn.Bits );
      Assert.Equal( blackPawnTwoSquareMove.Bits, chessBoard.BlackPawn.Bits );
    }

    [Fact]
    public void Undo_BlackEnPassant_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard whitePawnTwoSquareMove = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard blackPawnEnPassantMove = new PawnBitBoard( ChessPieceColors.Black );
      chessBoard.WhitePawn.Initialize( null );

      BoardSquare blackPawnPlacement =
            BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E4 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      chessBoard.BlackPawn.Bits = blackPawnPlacement;
      whitePawnTwoSquareMove.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D4 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      blackPawnEnPassantMove.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.D3 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

      chessBoard.Update( whitePawnTwoSquareMove );
      chessBoard.Update( blackPawnEnPassantMove );
      chessBoard.Undo();

      Assert.Equal( blackPawnPlacement, chessBoard.BlackPawn.Bits );
      Assert.Equal( whitePawnTwoSquareMove.Bits, chessBoard.WhitePawn.Bits );
    }

    [Fact]
    public void Undo_BlackEnPassantTwoTimes_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard whitePawnTwoSquareMove = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard blackPawnEnPassantMove = new PawnBitBoard( ChessPieceColors.Black );
      chessBoard.WhitePawn.Initialize( null );

      BoardSquare blackPawnPlacement =
            BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E4 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      chessBoard.BlackPawn.Bits = blackPawnPlacement;
      whitePawnTwoSquareMove.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D4 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      blackPawnEnPassantMove.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.D3 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

      chessBoard.Update( whitePawnTwoSquareMove );
      chessBoard.Update( blackPawnEnPassantMove );
      chessBoard.Undo();
      chessBoard.Update( blackPawnEnPassantMove );
      chessBoard.Undo();

      Assert.Equal( blackPawnPlacement, chessBoard.BlackPawn.Bits );
      Assert.Equal( whitePawnTwoSquareMove.Bits, chessBoard.WhitePawn.Bits );
    }

    [Fact]
    public void Undo_WhiteCastlingKingside_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.WhiteKing.Initialize( null );
      chessBoard.WhiteRook.Initialize( null );
      KingBitBoard whiteKingCastling = new KingBitBoard( ChessPieceColors.White );
      whiteKingCastling.Bits = BoardSquare.G1;

      chessBoard.Update( whiteKingCastling );
      chessBoard.Undo();

      BoardSquare correctWhiteKingPlacement = BoardSquare.E1;
      BoardSquare correctWhiteRookPlacement = BoardSquare.A1 | BoardSquare.H1;

      Assert.Equal( correctWhiteKingPlacement, chessBoard.WhiteKing.Bits );
      Assert.Equal( correctWhiteRookPlacement, chessBoard.WhiteRook.Bits );
    }

    [Fact]
    public void Undo_WhiteCastlingQueenside_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.WhiteKing.Initialize( null );
      chessBoard.WhiteRook.Initialize( null );
      KingBitBoard whiteKingCastling = new KingBitBoard( ChessPieceColors.White );
      whiteKingCastling.Bits = BoardSquare.C1;

      chessBoard.Update( whiteKingCastling );
      chessBoard.Undo();

      BoardSquare correctWhiteKingPlacement = BoardSquare.E1;
      BoardSquare correctWhiteRookPlacement = BoardSquare.A1 | BoardSquare.H1;

      Assert.Equal( correctWhiteKingPlacement, chessBoard.WhiteKing.Bits );
      Assert.Equal( correctWhiteRookPlacement, chessBoard.WhiteRook.Bits );
    }
    [Fact]
    public void Undo_BlackCastlingKingside_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.BlackKing.Initialize( null );
      chessBoard.BlackRook.Initialize( null );
      KingBitBoard whiteKingCastling = new KingBitBoard( ChessPieceColors.Black );
      whiteKingCastling.Bits = BoardSquare.G8;

      chessBoard.Update( whiteKingCastling );
      chessBoard.Undo();

      BoardSquare correctBlackKingPlacement = BoardSquare.E8;
      BoardSquare correctBlackRookPlacement = BoardSquare.A8 | BoardSquare.H8;

      Assert.Equal( correctBlackKingPlacement, chessBoard.BlackKing.Bits );
      Assert.Equal( correctBlackRookPlacement, chessBoard.BlackRook.Bits );
    }
    [Fact]
    public void Undo_BlackCastlingQueenside_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.BlackKing.Initialize( null );
      chessBoard.BlackRook.Initialize( null );
      KingBitBoard whiteKingCastling = new KingBitBoard( ChessPieceColors.Black );
      whiteKingCastling.Bits = BoardSquare.C8;

      chessBoard.Update( whiteKingCastling );
      chessBoard.Undo();

      BoardSquare correctBlackKingPlacement = BoardSquare.E8;
      BoardSquare correctBlackRookPlacement = BoardSquare.A8 | BoardSquare.H8;

      Assert.Equal( correctBlackKingPlacement, chessBoard.BlackKing.Bits );
      Assert.Equal( correctBlackRookPlacement, chessBoard.BlackRook.Bits );
    }

    [Fact]
    public void Undo_BlackCastlingQueensideTwoTimes_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.BlackKing.Initialize( null );
      chessBoard.BlackRook.Initialize( null );
      KingBitBoard whiteKingCastling = new KingBitBoard( ChessPieceColors.Black );
      whiteKingCastling.Bits = BoardSquare.C8;

      chessBoard.Update( whiteKingCastling );
      chessBoard.Undo();
      chessBoard.Update( whiteKingCastling );
      chessBoard.Undo();

      BoardSquare correctBlackKingPlacement = BoardSquare.E8;
      BoardSquare correctBlackRookPlacement = BoardSquare.A8 | BoardSquare.H8;

      Assert.Equal( correctBlackKingPlacement, chessBoard.BlackKing.Bits );
      Assert.Equal( correctBlackRookPlacement, chessBoard.BlackRook.Bits );
    }

    [Fact]
    public void Undo_WhitePromotionQueen_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard whitePromotionQueenMove = new PawnBitBoard( ChessPieceColors.White );
      whitePromotionQueenMove.Promote( PawnBitBoard.PromotionPiece.Queen );
      BoardSquare whitePawnPlacement =
            BoardSquare.A7 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      whitePromotionQueenMove.Bits =
          BoardSquare.A8 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      chessBoard.WhitePawn.Bits = whitePawnPlacement;

      chessBoard.Update( whitePromotionQueenMove );
      chessBoard.Undo();

      Assert.Equal( whitePawnPlacement, chessBoard.WhitePawn.Bits );
    }
    [Fact]
    public void Undo_WhitePromotionRook_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard whitePromotionRookMove = new PawnBitBoard( ChessPieceColors.White );
      whitePromotionRookMove.Promote( PawnBitBoard.PromotionPiece.Rook );
      BoardSquare whitePawnPlacement =
          BoardSquare.A7 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      whitePromotionRookMove.Bits =
          BoardSquare.A8 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      chessBoard.WhitePawn.Bits = whitePawnPlacement;

      chessBoard.Update( whitePromotionRookMove );
      chessBoard.Undo();

      Assert.Equal( whitePawnPlacement, chessBoard.WhitePawn.Bits );
    }
    [Fact]
    public void Undo_WhitePromotionBishop_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard whitePromotionBishopMove = new PawnBitBoard( ChessPieceColors.White );
      whitePromotionBishopMove.Promote( PawnBitBoard.PromotionPiece.Bishop );
      BoardSquare whitePawnPlacement =
          BoardSquare.A7 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      whitePromotionBishopMove.Bits =
          BoardSquare.A8 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      chessBoard.WhitePawn.Bits = whitePawnPlacement;

      chessBoard.Update( whitePromotionBishopMove );
      chessBoard.Undo();

      Assert.Equal( whitePawnPlacement, chessBoard.WhitePawn.Bits );
    }
    [Fact]
    public void Undo_WhitePromotionKnight_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard whitePromotionKnightMove = new PawnBitBoard( ChessPieceColors.White );
      whitePromotionKnightMove.Promote( PawnBitBoard.PromotionPiece.Knight );
      BoardSquare whitePawnPlacement =
          BoardSquare.A7 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      whitePromotionKnightMove.Bits =
          BoardSquare.A8 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      chessBoard.WhitePawn.Bits = whitePawnPlacement;

      chessBoard.Update( whitePromotionKnightMove );
      chessBoard.Undo();

      Assert.Equal( whitePawnPlacement, chessBoard.WhitePawn.Bits );
    }
    [Fact]
    public void Undo_BlackPromotionQueen_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard blackPromotionQueenMove = new PawnBitBoard( ChessPieceColors.Black );
      blackPromotionQueenMove.Promote( PawnBitBoard.PromotionPiece.Queen );
      BoardSquare blackPawnPlacement =
          BoardSquare.A2 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      blackPromotionQueenMove.Bits =
          BoardSquare.A1 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      chessBoard.BlackPawn.Bits = blackPawnPlacement;

      chessBoard.Update( blackPromotionQueenMove );
      chessBoard.Undo();

      Assert.Equal( blackPawnPlacement, chessBoard.BlackPawn.Bits );
    }
    [Fact]
    public void Undo_BlackPromotionRook_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard blackPromotionRookMove = new PawnBitBoard( ChessPieceColors.Black );
      blackPromotionRookMove.Promote( PawnBitBoard.PromotionPiece.Rook );
      BoardSquare blackPawnPlacement =
          BoardSquare.A2 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      blackPromotionRookMove.Bits =
          BoardSquare.A1 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      chessBoard.BlackPawn.Bits = blackPawnPlacement;

      chessBoard.Update( blackPromotionRookMove );
      chessBoard.Undo();

      Assert.Equal( blackPawnPlacement, chessBoard.BlackPawn.Bits );
    }
    [Fact]
    public void Undo_BlackPromotionBishop_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard blackPromotionBishopMove = new PawnBitBoard( ChessPieceColors.Black );
      blackPromotionBishopMove.Promote( PawnBitBoard.PromotionPiece.Bishop );
      BoardSquare blackPawnPlacement =
          BoardSquare.A2 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      blackPromotionBishopMove.Bits =
          BoardSquare.A1 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      chessBoard.BlackPawn.Bits = blackPawnPlacement;

      chessBoard.Update( blackPromotionBishopMove );
      chessBoard.Undo();

      Assert.Equal( blackPawnPlacement, chessBoard.BlackPawn.Bits );
    }
    [Fact]
    public void Undo_BlackPromotionKnight_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      PawnBitBoard blackPromotionKnightMove = new PawnBitBoard( ChessPieceColors.Black );
      blackPromotionKnightMove.Promote( PawnBitBoard.PromotionPiece.Knight );
      BoardSquare blackPawnPlacement =
          BoardSquare.A2 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      blackPromotionKnightMove.Bits =
          BoardSquare.A1 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
      chessBoard.BlackPawn.Bits = blackPawnPlacement;

      chessBoard.Update( blackPromotionKnightMove );
      chessBoard.Undo();

      Assert.Equal( blackPawnPlacement, chessBoard.BlackPawn.Bits );
    }
    [Fact]
    public void Undo_BlackPawnCaptureAheadOfEnPassant_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      chessBoard.InitializeGame();
      ulong hashBefore;
      ulong hashAfterUndo;
      
      ColoredBitBoard[] moves = new ColoredBitBoard[] {
        new PawnBitBoard( ChessPieceColors.White ),
        new PawnBitBoard( ChessPieceColors.Black ),
        new KnightBitBoard( ChessPieceColors.White ),
        new KnightBitBoard( ChessPieceColors.Black ),
        new KnightBitBoard( ChessPieceColors.White ),
        new PawnBitBoard( ChessPieceColors.Black ),
        new PawnBitBoard( ChessPieceColors.White ),
        new KnightBitBoard( ChessPieceColors.Black ),
        new KnightBitBoard( ChessPieceColors.White ),
        new KnightBitBoard( ChessPieceColors.Black ),
        new BishopBitBoard( ChessPieceColors.White ),
        new PawnBitBoard( ChessPieceColors.Black ),
        new PawnBitBoard( ChessPieceColors.White ),
        new PawnBitBoard( ChessPieceColors.Black ),
      };

      int moveNo = 0;
      moves[moveNo++].Bits = chessBoard.WhitePawn.Bits ^ BoardSquare.E2 ^ BoardSquare.E4;
      moves[moveNo++].Bits = chessBoard.BlackPawn.Bits ^ BoardSquare.E7 ^ BoardSquare.E5;
      moves[moveNo++].Bits = chessBoard.WhiteKnight.Bits ^ BoardSquare.B1 ^ BoardSquare.C3;
      moves[moveNo++].Bits = chessBoard.BlackKnight.Bits ^ BoardSquare.B8 ^ BoardSquare.C6;
      moves[moveNo++].Bits = moves[2].Bits ^ BoardSquare.G1 ^ BoardSquare.F3;
      moves[moveNo++].Bits = moves[1].Bits ^ BoardSquare.D7 ^ BoardSquare.D5;
      moves[moveNo++].Bits = moves[0].Bits ^ BoardSquare.E4 ^ BoardSquare.D5;
      moves[moveNo++].Bits = moves[3].Bits ^ BoardSquare.C6 ^ BoardSquare.B4;
      moves[moveNo++].Bits = moves[4].Bits ^ BoardSquare.F3 ^ BoardSquare.E5;
      moves[moveNo++].Bits = moves[7].Bits ^ BoardSquare.G8 ^ BoardSquare.F6;
      moves[moveNo++].Bits = chessBoard.WhiteBishop.Bits ^ BoardSquare.F1 ^ BoardSquare.B5;
      moves[moveNo++].Bits = moves[5].Bits ^ BoardSquare.C7 ^ BoardSquare.C6 ^ BoardSquare.D5 ^ BoardSquare.E5;
      moves[moveNo++].Bits = moves[6].Bits ^ BoardSquare.D2 ^ BoardSquare.D4;
      moves[moveNo++].Bits = moves[11].Bits ^ BoardSquare.C6 ^ BoardSquare.D5;

      for ( moveNo = 0; moveNo < moves.Length; moveNo++ ) {
        hashBefore = chessBoard.BoardHash.Key;
        chessBoard.Update( moves[moveNo] );
        chessBoard.Undo();
        hashAfterUndo = chessBoard.BoardHash.Key;
        Assert.Equal( hashBefore, hashAfterUndo );

        chessBoard.Update( moves[moveNo] );
      }

      //hashBefore = chessBoard.BoardHash.Key;
    }
    [Fact]
    public void Undo_TwoMoves_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      ChessBoard correctBoard = new ChessBoard();
      PawnBitBoard whitePawnMove = new PawnBitBoard( ChessPieceColors.White );
      KnightBitBoard blackKnightMove = new KnightBitBoard( ChessPieceColors.Black );
      chessBoard.InitializeGame();
      correctBoard.InitializeGame();

      whitePawnMove.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E4 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2;
      blackKnightMove.Bits =
          BoardSquare.B8 | BoardSquare.H6;


      chessBoard.Update( whitePawnMove );
      chessBoard.Update( blackKnightMove );
      chessBoard.Undo();
      chessBoard.Undo();

      Assert.Equal( correctBoard.WhiteKing.Bits, chessBoard.WhiteKing.Bits );
      Assert.Equal( correctBoard.WhiteQueen.Bits, chessBoard.WhiteQueen.Bits );
      Assert.Equal( correctBoard.WhiteRook.Bits, chessBoard.WhiteRook.Bits );
      Assert.Equal( correctBoard.WhiteBishop.Bits, chessBoard.WhiteBishop.Bits );
      Assert.Equal( correctBoard.WhiteKnight.Bits, chessBoard.WhiteKnight.Bits );
      Assert.Equal( correctBoard.WhitePawn.Bits, chessBoard.WhitePawn.Bits );

      Assert.Equal( correctBoard.BlackKing.Bits, chessBoard.BlackKing.Bits );
      Assert.Equal( correctBoard.BlackQueen.Bits, chessBoard.BlackQueen.Bits );
      Assert.Equal( correctBoard.BlackRook.Bits, chessBoard.BlackRook.Bits );
      Assert.Equal( correctBoard.BlackBishop.Bits, chessBoard.BlackBishop.Bits );
      Assert.Equal( correctBoard.BlackKnight.Bits, chessBoard.BlackKnight.Bits );
      Assert.Equal( correctBoard.BlackPawn.Bits, chessBoard.BlackPawn.Bits );
    }

    [Fact]
    public void Undo_13MovesQueenPromotionAtG8_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      ChessBoard correctBoard = new ChessBoard();
      PawnBitBoard whitePawnMove1 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard blackPawnMove1 = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard whitePawnMove2 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard blackPawnCapturingMove = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard whitePawnMove3 = new PawnBitBoard( ChessPieceColors.White );
      KnightBitBoard blackKnightMove = new KnightBitBoard( ChessPieceColors.Black );
      PawnBitBoard whitePawnMove4 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard blackPawnMove2 = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard whitePawnMove5 = new PawnBitBoard( ChessPieceColors.White );
      QueenBitBoard blackQueenMove1 = new QueenBitBoard( ChessPieceColors.Black );
      PawnBitBoard whitePawnMove6 = new PawnBitBoard( ChessPieceColors.White );
      whitePawnMove6.Promote( PawnBitBoard.PromotionPiece.Queen );
      QueenBitBoard blackQueenMove2 = new QueenBitBoard( ChessPieceColors.Black );
      QueenBitBoard whiteQueenMove = new QueenBitBoard( ChessPieceColors.White );

      chessBoard.InitializeGame();
      correctBoard.InitializeGame();

      whitePawnMove1.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G4 | BoardSquare.H2;
      blackPawnMove1.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G5 | BoardSquare.H7;
      whitePawnMove2.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G4 | BoardSquare.H4;
      blackPawnCapturingMove.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.H4 | BoardSquare.H7;
      whitePawnMove3.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G5;
      blackKnightMove.Bits =
          BoardSquare.B8 | BoardSquare.F6;
      whitePawnMove4.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G6;
      blackPawnMove2.Bits =
          BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C6 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.H4 | BoardSquare.H7;
      whitePawnMove5.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G7;
      blackQueenMove1.Bits =
          BoardSquare.A5;
      whitePawnMove6.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G8;
      blackQueenMove2.Bits =
          BoardSquare.B4;
      whiteQueenMove.Bits =
          BoardSquare.D1 | BoardSquare.G4;

      chessBoard.Update( whitePawnMove1 );
      chessBoard.Update( blackPawnMove1 );
      chessBoard.Update( whitePawnMove2 );
      chessBoard.Update( blackPawnCapturingMove );
      chessBoard.Update( whitePawnMove3 );
      chessBoard.Update( blackKnightMove );
      chessBoard.Update( whitePawnMove4 );
      chessBoard.Update( blackPawnMove2 );
      chessBoard.Update( whitePawnMove5 );
      chessBoard.Update( blackQueenMove1 );
      chessBoard.Update( whitePawnMove6 );
      chessBoard.Update( blackQueenMove2 );
      chessBoard.Update( whiteQueenMove );
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();

      Assert.Equal( correctBoard.WhiteKing.Bits, chessBoard.WhiteKing.Bits );
      Assert.Equal( correctBoard.WhiteQueen.Bits, chessBoard.WhiteQueen.Bits );
      Assert.Equal( correctBoard.WhiteRook.Bits, chessBoard.WhiteRook.Bits );
      Assert.Equal( correctBoard.WhiteBishop.Bits, chessBoard.WhiteBishop.Bits );
      Assert.Equal( correctBoard.WhiteKnight.Bits, chessBoard.WhiteKnight.Bits );
      Assert.Equal( correctBoard.WhitePawn.Bits, chessBoard.WhitePawn.Bits );

      Assert.Equal( correctBoard.BlackKing.Bits, chessBoard.BlackKing.Bits );
      Assert.Equal( correctBoard.BlackQueen.Bits, chessBoard.BlackQueen.Bits );
      Assert.Equal( correctBoard.BlackRook.Bits, chessBoard.BlackRook.Bits );
      Assert.Equal( correctBoard.BlackBishop.Bits, chessBoard.BlackBishop.Bits );
      Assert.Equal( correctBoard.BlackKnight.Bits, chessBoard.BlackKnight.Bits );
      Assert.Equal( correctBoard.BlackPawn.Bits, chessBoard.BlackPawn.Bits );
    }

    [Fact]
    public void Undo_CastlingInGameSetup_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      ChessBoard correctBoard = new ChessBoard();
      PawnBitBoard whitePawnMove = new PawnBitBoard( ChessPieceColors.White );
      KnightBitBoard blackKnightMove = new KnightBitBoard( ChessPieceColors.Black );
      BishopBitBoard whiteBishopMove = new BishopBitBoard( ChessPieceColors.White );
      RookBitBoard blackRookMove1 = new RookBitBoard( ChessPieceColors.Black );
      KnightBitBoard whiteKnightMove = new KnightBitBoard( ChessPieceColors.White );
      RookBitBoard blackRookMove2 = new RookBitBoard( ChessPieceColors.Black );
      KingBitBoard whiteKingCastlingMove = new KingBitBoard( ChessPieceColors.White );

      chessBoard.InitializeGame();
      correctBoard.InitializeGame();

      whitePawnMove.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H3;
      blackKnightMove.Bits =
          BoardSquare.A6 | BoardSquare.G8;
      whiteBishopMove.Bits =
          BoardSquare.C1 | BoardSquare.H2;
      blackRookMove1.Bits =
          BoardSquare.H8 | BoardSquare.B8;
      whiteKnightMove.Bits =
          BoardSquare.B1 | BoardSquare.H3;
      blackRookMove2.Bits =
          BoardSquare.H8 | BoardSquare.A8;
      whiteKingCastlingMove.Bits =
          BoardSquare.G1;
      chessBoard.Update( whitePawnMove );
      chessBoard.Update( blackKnightMove );
      chessBoard.Update( whiteBishopMove );
      chessBoard.Update( blackRookMove1 );
      chessBoard.Update( whiteKnightMove );
      chessBoard.Update( blackRookMove2 );
      chessBoard.Update( whiteKingCastlingMove );
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();


      Assert.Equal( correctBoard.WhiteKing.Bits, chessBoard.WhiteKing.Bits );
      Assert.Equal( correctBoard.WhiteQueen.Bits, chessBoard.WhiteQueen.Bits );
      Assert.Equal( correctBoard.WhiteRook.Bits, chessBoard.WhiteRook.Bits );
      Assert.Equal( correctBoard.WhiteBishop.Bits, chessBoard.WhiteBishop.Bits );
      Assert.Equal( correctBoard.WhiteKnight.Bits, chessBoard.WhiteKnight.Bits );
      Assert.Equal( correctBoard.WhitePawn.Bits, chessBoard.WhitePawn.Bits );

      Assert.Equal( correctBoard.BlackKing.Bits, chessBoard.BlackKing.Bits );
      Assert.Equal( correctBoard.BlackQueen.Bits, chessBoard.BlackQueen.Bits );
      Assert.Equal( correctBoard.BlackRook.Bits, chessBoard.BlackRook.Bits );
      Assert.Equal( correctBoard.BlackBishop.Bits, chessBoard.BlackBishop.Bits );
      Assert.Equal( correctBoard.BlackKnight.Bits, chessBoard.BlackKnight.Bits );
      Assert.Equal( correctBoard.BlackPawn.Bits, chessBoard.BlackPawn.Bits );
    }

    [Fact]
    public void Undo_EnPassantInGameSetup_Equal() {
      ChessBoard chessBoard = new ChessBoard();
      ChessBoard correctBoard = new ChessBoard();
      PawnBitBoard whitePawnMove1 = new PawnBitBoard( ChessPieceColors.White );
      KnightBitBoard blackKnightMove1 = new KnightBitBoard( ChessPieceColors.Black );
      PawnBitBoard whitePawnMove2 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard blackPawnMove = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard whitePawnEnPassantMove = new PawnBitBoard( ChessPieceColors.White );
      KnightBitBoard blackKnightMove2 = new KnightBitBoard( ChessPieceColors.Black );
      PawnBitBoard whitePawnMove3 = new PawnBitBoard( ChessPieceColors.White );

      chessBoard.InitializeGame();
      correctBoard.InitializeGame();

      whitePawnMove1.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H4;
      blackKnightMove1.Bits =
          BoardSquare.A6 | BoardSquare.G8;
      whitePawnMove2.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H5;
      blackPawnMove.Bits =
           BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G5 | BoardSquare.H7;
      whitePawnEnPassantMove.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.G6;
      blackKnightMove2.Bits =
      BoardSquare.B8 | BoardSquare.G8;
      whitePawnMove3.Bits =
          BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.G7;

      chessBoard.Update( whitePawnMove1 );
      chessBoard.Update( blackKnightMove1 );
      chessBoard.Update( whitePawnMove2 );
      chessBoard.Update( blackPawnMove );
      chessBoard.Update( whitePawnEnPassantMove );
      chessBoard.Update( blackKnightMove2 );
      chessBoard.Update( whitePawnMove3 );
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();
      chessBoard.Undo();


      Assert.Equal( correctBoard.WhiteKing.Bits, chessBoard.WhiteKing.Bits );
      Assert.Equal( correctBoard.WhiteQueen.Bits, chessBoard.WhiteQueen.Bits );
      Assert.Equal( correctBoard.WhiteRook.Bits, chessBoard.WhiteRook.Bits );
      Assert.Equal( correctBoard.WhiteBishop.Bits, chessBoard.WhiteBishop.Bits );
      Assert.Equal( correctBoard.WhiteKnight.Bits, chessBoard.WhiteKnight.Bits );
      Assert.Equal( correctBoard.WhitePawn.Bits, chessBoard.WhitePawn.Bits );

      Assert.Equal( correctBoard.BlackKing.Bits, chessBoard.BlackKing.Bits );
      Assert.Equal( correctBoard.BlackQueen.Bits, chessBoard.BlackQueen.Bits );
      Assert.Equal( correctBoard.BlackRook.Bits, chessBoard.BlackRook.Bits );
      Assert.Equal( correctBoard.BlackBishop.Bits, chessBoard.BlackBishop.Bits );
      Assert.Equal( correctBoard.BlackKnight.Bits, chessBoard.BlackKnight.Bits );
      Assert.Equal( correctBoard.BlackPawn.Bits, chessBoard.BlackPawn.Bits );
    }
    [Fact]
    public void Undo_NotPossible_Equal() {
      ChessBoard chessBoard = new ChessBoard();

      Assert.Throws<System.InvalidOperationException>( (Assert.ThrowsDelegate)(
        () => { chessBoard.Undo(); }
      ) );
    }
    [Fact]
    public void LarsTest_Equal() {
      Winboard wb = new Winboard();
      wb.Handler( "new" );
      wb.Handler( "force" );
      ulong hashBefore = 0;

      PawnBitBoard w_1 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard b_1 = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard w_2 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard b_2 = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard w_3 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard b_3 = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard w_4 = new PawnBitBoard( ChessPieceColors.White );

      w_1.Bits = wb.chessBoard.WhitePawn.Bits ^ BoardSquare.H2 ^ BoardSquare.H3;
      b_1.Bits = wb.chessBoard.BlackPawn.Bits ^ BoardSquare.G7 ^ BoardSquare.G5;
      w_2.Bits = w_1.Bits ^ BoardSquare.F2 ^ BoardSquare.F4;
      b_2.Bits = b_1.Bits ^ BoardSquare.G5 ^ BoardSquare.F4;
      w_3.Bits = w_2.Bits ^ BoardSquare.G2 ^ BoardSquare.G3 ^ BoardSquare.F4;
      b_3.Bits = b_2.Bits ^ BoardSquare.F7 ^ BoardSquare.F5;
      w_4.Bits = w_3.Bits ^ BoardSquare.G3 ^ BoardSquare.F4;

      hashBefore = wb.chessBoard.BoardHash.Key;
      wb.chessBoard.Update( w_1 );
      wb.chessBoard.Undo();
      Assert.Equal( hashBefore, wb.chessBoard.BoardHash.Key );
      wb.chessBoard.Update( w_1 );

      hashBefore = wb.chessBoard.BoardHash.Key;
      wb.chessBoard.Update( b_1 );
      wb.chessBoard.Undo();
      Assert.Equal( hashBefore, wb.chessBoard.BoardHash.Key );
      wb.chessBoard.Update( b_1 );

      hashBefore = wb.chessBoard.BoardHash.Key;
      wb.chessBoard.Update( w_2 );
      wb.chessBoard.Undo();
      Assert.Equal( hashBefore, wb.chessBoard.BoardHash.Key );
      wb.chessBoard.Update( w_2 );

      hashBefore = wb.chessBoard.BoardHash.Key;
      wb.chessBoard.Update( b_2 );
      wb.chessBoard.Undo();
      Assert.Equal( hashBefore, wb.chessBoard.BoardHash.Key );
      wb.chessBoard.Update( b_2 );

      hashBefore = wb.chessBoard.BoardHash.Key;
      wb.chessBoard.Update( w_3 );
      wb.chessBoard.Undo();
      Assert.Equal( hashBefore, wb.chessBoard.BoardHash.Key );
      wb.chessBoard.Update( w_3 );

      hashBefore = wb.chessBoard.BoardHash.Key;
      wb.chessBoard.Update( b_3 );
      wb.chessBoard.Undo();
      Assert.Equal( hashBefore, wb.chessBoard.BoardHash.Key );
      wb.chessBoard.Update( b_3 );

      hashBefore = wb.chessBoard.BoardHash.Key;
      wb.chessBoard.Update( w_4 );
      wb.chessBoard.Undo();
      Assert.Equal( hashBefore, wb.chessBoard.BoardHash.Key );
      wb.chessBoard.Update( w_4 );

      //ColoredBitBoard bitBoardMoveTaken = NegaMax.GetBestMove( wb.chessBoard, 3, ChessPieceColors.Black, MoveGen.GenerateLegalMoves, Eval.EvaluateState );
    
    }
    [Fact]
    public void PeterScenarioTestMandag()
    {
      ChessBoard cb = new ChessBoard();
      cb.InitializeGame();

      PawnBitBoard wpm = cb.WhitePawn.DeepCopy() as PawnBitBoard;
      PawnBitBoard bpm = cb.BlackPawn.DeepCopy() as PawnBitBoard;
      RookBitBoard wrm = cb.WhiteRook.DeepCopy() as RookBitBoard;
      RookBitBoard brm = cb.BlackRook.DeepCopy() as RookBitBoard;
      KnightBitBoard wnm = cb.WhiteKnight.DeepCopy() as KnightBitBoard;
      KnightBitBoard bnm = cb.BlackKnight.DeepCopy() as KnightBitBoard;
      BishopBitBoard wbm = cb.WhiteBishop.DeepCopy() as BishopBitBoard;
      BishopBitBoard bbm = cb.BlackBishop.DeepCopy() as BishopBitBoard;
      QueenBitBoard wqm = cb.WhiteQueen.DeepCopy() as QueenBitBoard;
      QueenBitBoard bqm = cb.BlackQueen.DeepCopy() as QueenBitBoard;
      KingBitBoard wkm = cb.WhiteKing.DeepCopy() as KingBitBoard;
      KingBitBoard bkm = cb.BlackKing.DeepCopy() as KingBitBoard;

      ulong hashisBeforeish;
      ulong hashisAfterish;

      wpm.Bits ^= BoardSquare.E2 ^ BoardSquare.E4;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wpm);

      bpm.Bits ^= BoardSquare.E7 ^ BoardSquare.E5;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bpm);

      wnm.Bits ^= BoardSquare.B1 ^ BoardSquare.C3;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wnm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wnm);

      bnm.Bits ^= BoardSquare.G8 ^ BoardSquare.F6;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bnm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bnm);


      wpm.Bits ^= BoardSquare.D2 ^ BoardSquare.D3;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wpm);

      bnm.Bits ^= BoardSquare.B8 ^ BoardSquare.C6;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bnm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bnm);

      wnm.Bits ^= BoardSquare.G1 ^ BoardSquare.F3;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wnm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wnm);

      bpm.Bits ^= BoardSquare.D7 ^ BoardSquare.D5;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bpm);

      wnm.Bits ^= BoardSquare.F3 ^ BoardSquare.G5;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wnm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wnm);

      bpm.Bits ^= BoardSquare.D5 ^ BoardSquare.E4; //capt.
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bpm);
      wpm.Bits ^= BoardSquare.E4;


      wpm.Bits ^= BoardSquare.D3 ^ BoardSquare.E4; //capt
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wpm);
      bpm.Bits ^= BoardSquare.E4;


      bqm.Bits ^= BoardSquare.D8 ^ BoardSquare.D1; //capt
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bqm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bqm);
      wqm.Bits ^= BoardSquare.D1;


      wnm.Bits ^= BoardSquare.C3 ^ BoardSquare.D1; //capt
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wnm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wnm);
      bqm.Bits ^= BoardSquare.D1;

      bbm.Bits ^= BoardSquare.F8 ^ BoardSquare.B4;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);


      wkm.Bits ^= BoardSquare.E1 ^ BoardSquare.E2;
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wkm);

      bbm.Bits ^= BoardSquare.C8 ^ BoardSquare.G4;
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);


      wpm.Bits ^= BoardSquare.F2 ^ BoardSquare.F3;
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wpm);

      bnm.Bits ^= BoardSquare.C6 ^ BoardSquare.D4;
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bnm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bnm);


      wkm.Bits ^= BoardSquare.E2 ^ BoardSquare.D3;
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wkm);

      bbm.Bits ^= BoardSquare.G4 ^ BoardSquare.D7;
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);


      wkm.Bits ^= BoardSquare.D3 ^ BoardSquare.C4;
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wkm);

      bnm.Bits ^= BoardSquare.D4 ^ BoardSquare.C2; //cap
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bnm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bnm);
      wpm.Bits ^= BoardSquare.C2;



      wrm.Bits ^= BoardSquare.A1 ^ BoardSquare.B1;
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wrm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wrm);

      bpm.Bits ^= BoardSquare.B7 ^ BoardSquare.B5;
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bpm);


      wkm.Bits ^= BoardSquare.C4 ^ BoardSquare.B3;
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wkm);

      bnm.Bits ^= BoardSquare.F6 ^ BoardSquare.E4;//cap
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bnm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bnm);
      wpm.Bits ^= BoardSquare.E4;


      wkm.Bits ^= BoardSquare.B3 ^ BoardSquare.C2; //cap  
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wkm);
      bnm.Bits ^= BoardSquare.C2;

      bnm.Bits ^= BoardSquare.E4 ^ BoardSquare.G5;//cap
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bnm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bnm);
      wnm.Bits ^= BoardSquare.G5;


      wkm.Bits ^= BoardSquare.C2 ^ BoardSquare.B3;
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wkm);

      bbm.Bits ^= BoardSquare.B4 ^ BoardSquare.C5;
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);


      wbm.Bits ^= BoardSquare.C1 ^ BoardSquare.G5; //cap
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wbm);
      bnm.Bits ^= BoardSquare.G5;

      bbm.Bits ^= BoardSquare.D7 ^ BoardSquare.E6;
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);


      wkm.Bits ^= BoardSquare.B3 ^ BoardSquare.C2;
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wkm);

      bbm.Bits ^= BoardSquare.E6 ^ BoardSquare.F5;
       hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);


      wkm.Bits ^= BoardSquare.C2 ^ BoardSquare.C3;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wkm);

      bbm.Bits ^= BoardSquare.F5 ^ BoardSquare.B1; //cap 
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);
      wrm.Bits ^= BoardSquare.B1;


      wbm.Bits ^= BoardSquare.F1 ^ BoardSquare.B5; //cap
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wbm);
      bpm.Bits ^= BoardSquare.B5;

      bkm.Bits ^= BoardSquare.E8 ^ BoardSquare.F8;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bkm);

      wpm.Bits ^= BoardSquare.G2 ^BoardSquare.G3;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wpm);

      bbm.Bits ^= BoardSquare.B1 ^ BoardSquare.A2;//cap
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);
      wpm.Bits ^= BoardSquare.A2;

      wpm.Bits ^= BoardSquare.F3 ^ BoardSquare.F4;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wpm);

      bbm.Bits ^= BoardSquare.C5 ^ BoardSquare.D4;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update( bbm );
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal( hashisBeforeish, hashisAfterish );
      cb.Update( bbm );

      wkm.Bits ^= BoardSquare.C3 ^ BoardSquare.D3;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wkm);

      bbm.Bits ^= BoardSquare.A2 ^ BoardSquare.B1;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);


      wkm.Bits ^= BoardSquare.D3 ^ BoardSquare.C4;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wkm);

      bbm.Bits ^= BoardSquare.B1 ^ BoardSquare.A2;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);


      wkm.Bits ^= BoardSquare.C4 ^ BoardSquare.B4;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wkm);

      bpm.Bits ^= BoardSquare.A7 ^ BoardSquare.A5;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bpm);

      wkm.Bits ^= BoardSquare.B4 ^ BoardSquare.A3;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wkm);

      bbm.Bits ^= BoardSquare.A2 ^ BoardSquare.E6;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);


      wrm.Bits ^= BoardSquare.H1 ^ BoardSquare.E1;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wrm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wrm);

      bbm.Bits ^= BoardSquare.D4 ^ BoardSquare.C5;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);


      wkm.Bits ^= BoardSquare.A3 ^ BoardSquare.A4;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wkm);

      bkm.Bits ^= BoardSquare.F8 ^ BoardSquare.G8;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bkm);



      wrm.Bits ^= BoardSquare.E1 ^ BoardSquare.E5;//cap
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wrm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wrm);

      bpm.Bits ^= BoardSquare.E5;
      bbm.Bits ^= BoardSquare.E6 ^ BoardSquare.D5;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);

      wrm.Bits ^= BoardSquare.E5 ^ BoardSquare.D5;//cap
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wrm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wrm);

      bbm.Bits ^= BoardSquare.D5;
      bbm.Bits ^= BoardSquare.C5 ^ BoardSquare.B6;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);

      wbm.Bits ^= BoardSquare.B5 ^ BoardSquare.C6;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wbm);

      brm.Bits ^= BoardSquare.A8 ^ BoardSquare.C8;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(brm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(brm);

      wkm.Bits ^= BoardSquare.A4 ^ BoardSquare.B5;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wkm);

      brm.Bits ^= BoardSquare.C8 ^ BoardSquare.B8;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(brm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(brm);


      wpm.Bits ^= BoardSquare.B2 ^ BoardSquare.B4;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wpm);

      bpm.Bits ^= BoardSquare.A5 ^ BoardSquare.B4;//cap 
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bpm);
      wpm.Bits ^= BoardSquare.B4;


      wkm.Bits ^= BoardSquare.B5 ^ BoardSquare.B4; // cap
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wkm);
      bpm.Bits ^= BoardSquare.B4;

      bbm.Bits ^= BoardSquare.B6 ^ BoardSquare.D4;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);


      wkm.Bits ^= BoardSquare.B4 ^ BoardSquare.C4;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wkm);

      bbm.Bits ^= BoardSquare.D4 ^ BoardSquare.B6;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);

      wpm.Bits ^= BoardSquare.F4 ^ BoardSquare.F5;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wpm);

      brm.Bits ^= BoardSquare.B8 ^ BoardSquare.C8;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(brm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(brm);


      wpm.Bits ^= BoardSquare.G3 ^ BoardSquare.G4;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wpm);

      brm.Bits ^= BoardSquare.C8 ^ BoardSquare.B8;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(brm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(brm);


      wpm.Bits ^= BoardSquare.H2 ^ BoardSquare.H4;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wpm);

      brm.Bits ^= BoardSquare.B8 ^ BoardSquare.C8;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(brm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(brm);


      wrm.Bits ^= BoardSquare.D5 ^ BoardSquare.B5;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wrm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wrm);

      bbm.Bits ^= BoardSquare.B6 ^ BoardSquare.G1;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);


      wnm.Bits ^= BoardSquare.D1 ^ BoardSquare.C3;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wnm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wnm);

      bbm.Bits ^= BoardSquare.G1 ^ BoardSquare.F2;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);


      wnm.Bits ^= BoardSquare.C3 ^ BoardSquare.E4;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wnm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wnm);

      bbm.Bits ^= BoardSquare.F2 ^ BoardSquare.E1;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bbm);


      wpm.Bits ^= BoardSquare.H4 ^ BoardSquare.H5;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wpm);

      bkm.Bits ^= BoardSquare.G8 ^ BoardSquare.F8;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bkm);


      wbm.Bits ^= BoardSquare.C6 ^ BoardSquare.B7;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wbm);

      brm.Bits ^= BoardSquare.C8 ^ BoardSquare.B8;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(brm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(brm);


      wbm.Bits ^= BoardSquare.B7 ^ BoardSquare.A6;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wbm);

      brm.Bits ^= BoardSquare.B8 ^ BoardSquare.A8;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(brm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(brm);


      wbm.Bits ^= BoardSquare.A6 ^ BoardSquare.B7;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wbm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wbm);

      brm.Bits ^= BoardSquare.A8 ^ BoardSquare.A4;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(brm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(brm);


      wkm.Bits ^= BoardSquare.C4 ^ BoardSquare.D5;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wkm);

      bkm.Bits ^= BoardSquare.F8 ^ BoardSquare.G8;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bkm);

      wnm.Bits ^= BoardSquare.E4 ^ BoardSquare.C5;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wnm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wnm);

      brm.Bits ^= BoardSquare.A4 ^ BoardSquare.G4;//cap
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(brm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(brm);
      wpm.Bits ^= BoardSquare.G4;


      wnm.Bits ^= BoardSquare.C5 ^ BoardSquare.D7;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wnm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wnm);

      brm.Bits ^= BoardSquare.G4 ^ BoardSquare.G5;//cap
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(brm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(brm);
      wbm.Bits ^= BoardSquare.G5;


      wnm.Bits ^= BoardSquare.D7 ^ BoardSquare.E5;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wnm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wnm);

      brm.Bits ^= BoardSquare.G5 ^ BoardSquare.F5;//cap
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(brm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(brm);
      wpm.Bits ^= BoardSquare.F5;


      wkm.Bits ^= BoardSquare.D5 ^ BoardSquare.E4;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wkm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wkm);


      bpm.Bits ^= BoardSquare.G7 ^BoardSquare.G5;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(bpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(bpm);


      wpm.Bits ^= BoardSquare.H5 ^BoardSquare.G6;
      hashisBeforeish = cb.BoardHash.Key;
      cb.Update(wpm);
      cb.Undo();
      hashisAfterish = cb.BoardHash.Key;
      Assert.Equal(hashisBeforeish, hashisAfterish);
      cb.Update(wpm);

    }
    [Fact]
    public void EloRating1()
    {
      ChessBoard cb = new ChessBoard();
      cb.InitializeScenario(new ScenarioList{
                              {BoardSquare.A2 | BoardSquare.B2 | BoardSquare.A6 | BoardSquare.D4 | BoardSquare.F2 | BoardSquare.G2| BoardSquare.H2, ChessPieceType.Pawn, ChessPieceColors.White},
                              {BoardSquare.C5 | BoardSquare.E6 | BoardSquare.G7 | BoardSquare.H6, ChessPieceType.Pawn, ChessPieceColors.Black},
                              {BoardSquare.B4 |BoardSquare.C8, ChessPieceType.Bishop, ChessPieceColors.Black},
                              {BoardSquare.E2, ChessPieceType.Bishop, ChessPieceColors.White},
                              {BoardSquare.A1 | BoardSquare.H1, ChessPieceType.Rook, ChessPieceColors.White},
                              {BoardSquare.A8 | BoardSquare.F6, ChessPieceType.Rook, ChessPieceColors.Black},
                              {BoardSquare.C6, ChessPieceType.Knight, ChessPieceColors.Black},
                              {BoardSquare.C3 | BoardSquare.F3, ChessPieceType.Knight, ChessPieceColors.White},
                              {BoardSquare.A5, ChessPieceType.Queen, ChessPieceColors.Black},
                              {BoardSquare.D2, ChessPieceType.Queen, ChessPieceColors.White},
                              {BoardSquare.E1, ChessPieceType.King, ChessPieceColors.White},
                              {BoardSquare.G8, ChessPieceType.King, ChessPieceColors.Black}});
      var foo = NegaMax.GetBestMove(cb, 4, ChessPieceColors.Black, MoveGen.GenerateLegalMoves, Eval.EvaluateState);
    }
    [Fact]
    public void EloRating2()
    {
      ChessBoard cb = new ChessBoard();
      cb.InitializeScenario(new ScenarioList{
                              {BoardSquare.A2 | BoardSquare.B3 | BoardSquare.E5 | BoardSquare.D4 | BoardSquare.F2 | BoardSquare.G4| BoardSquare.H5, ChessPieceType.Pawn, ChessPieceColors.White},
                              {BoardSquare.A5 | BoardSquare.B4 | BoardSquare.D5 | BoardSquare.E6 |BoardSquare.F7 | BoardSquare.G6 | BoardSquare.H7, ChessPieceType.Pawn, ChessPieceColors.Black},
                              {BoardSquare.B5, ChessPieceType.Bishop, ChessPieceColors.Black},
                              {BoardSquare.G2, ChessPieceType.Bishop, ChessPieceColors.White},
                              {BoardSquare.C8 | BoardSquare.F8, ChessPieceType.Knight, ChessPieceColors.Black},
                              {BoardSquare.G3 | BoardSquare.G5, ChessPieceType.Knight, ChessPieceColors.White},
                              {BoardSquare.D8, ChessPieceType.Queen, ChessPieceColors.Black},
                              {BoardSquare.H6, ChessPieceType.Queen, ChessPieceColors.White},
                              {BoardSquare.G1, ChessPieceType.King, ChessPieceColors.White},
                              {BoardSquare.G8, ChessPieceType.King, ChessPieceColors.Black}});
      var foo = NegaMax.GetBestMove(cb, 4, ChessPieceColors.White, MoveGen.GenerateLegalMoves, Eval.EvaluateState);
    }
    [Fact]
    public void EloRating3()
    {
      ChessBoard cb = new ChessBoard();
      cb.InitializeScenario(new ScenarioList{
                              {BoardSquare.B2 | BoardSquare.C5 |BoardSquare.F2, ChessPieceType.Pawn, ChessPieceColors.White},
                              {BoardSquare.A6 | BoardSquare.B6 | BoardSquare.E6 |BoardSquare.G7 | BoardSquare.G6, ChessPieceType.Pawn, ChessPieceColors.Black},
                              {BoardSquare.D6, ChessPieceType.Bishop, ChessPieceColors.White},
                              {BoardSquare.B4, ChessPieceType.Knight, ChessPieceColors.Black},
                              {BoardSquare.D7, ChessPieceType.Rook, ChessPieceColors.Black},
                              {BoardSquare.G3, ChessPieceType.Rook, ChessPieceColors.White},
                              {BoardSquare.E4, ChessPieceType.King, ChessPieceColors.White},
                              {BoardSquare.B5, ChessPieceType.King, ChessPieceColors.Black}});
      var foo = NegaMax.GetBestMove(cb, 4, ChessPieceColors.White, MoveGen.GenerateLegalMoves, Eval.EvaluateState);
    }
    [Fact]
    public void EloRating4()
    {
      ChessBoard cb = new ChessBoard();
      cb.InitializeScenario(new ScenarioList{
                              {BoardSquare.B4 | BoardSquare.C5 | BoardSquare.E5| BoardSquare.G5| BoardSquare.H6, ChessPieceType.Pawn, ChessPieceColors.White},
                              {BoardSquare.B5 | BoardSquare.C6 | BoardSquare.G6 | BoardSquare.H7, ChessPieceType.Pawn, ChessPieceColors.Black},
                              {BoardSquare.F7, ChessPieceType.Bishop, ChessPieceColors.Black},
                              {BoardSquare.B3, ChessPieceType.Bishop, ChessPieceColors.White},
                              {BoardSquare.F4, ChessPieceType.King, ChessPieceColors.White},
                              {BoardSquare.E7, ChessPieceType.King, ChessPieceColors.Black}});
      var foo = NegaMax.GetBestMove(cb, 4, ChessPieceColors.White, MoveGen.GenerateLegalMoves, Eval.EvaluateState);
    }
    [Fact]
    public void EloRating5()
    {
      ChessBoard cb = new ChessBoard();
      cb.InitializeScenario(new ScenarioList{
                              {BoardSquare.B3 | BoardSquare.F3 | BoardSquare.G2 | BoardSquare.H2, ChessPieceType.Pawn, ChessPieceColors.White},
                              {BoardSquare.B6 | BoardSquare.E4 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7, ChessPieceType.Pawn, ChessPieceColors.Black},
                              {BoardSquare.A8 |BoardSquare.B4, ChessPieceType.Bishop, ChessPieceColors.Black},
                              {BoardSquare.E2 |BoardSquare.E3, ChessPieceType.Bishop, ChessPieceColors.White},
                              {BoardSquare.C8, ChessPieceType.Rook, ChessPieceColors.White},
                              {BoardSquare.F6 | BoardSquare.F8, ChessPieceType.Knight, ChessPieceColors.Black},
                              {BoardSquare.F5, ChessPieceType.Knight, ChessPieceColors.White},
                              {BoardSquare.A2, ChessPieceType.Queen, ChessPieceColors.Black},
                              {BoardSquare.F2, ChessPieceType.Queen, ChessPieceColors.White},
                              {BoardSquare.G1, ChessPieceType.King, ChessPieceColors.White},
                              {BoardSquare.G8, ChessPieceType.King, ChessPieceColors.Black}});
      var foo = NegaMax.GetBestMove(cb, 4, ChessPieceColors.White, MoveGen.GenerateLegalMoves, Eval.EvaluateState);
    }

    [Fact]
    public void EloRating_6() {
      ChessBoard cb = new ChessBoard();
      cb.InitializeScenario( new ScenarioList {
        { BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.E4 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H3, ChessPieceType.Pawn, ChessPieceColors.White },
        { BoardSquare.A7 | BoardSquare.B7 | BoardSquare.D6 | BoardSquare.E5 | BoardSquare.F7 | BoardSquare.G6 | BoardSquare.H7, ChessPieceType.Pawn, ChessPieceColors.Black },
        { BoardSquare.C3, ChessPieceType.Knight, ChessPieceColors.White },
        { BoardSquare.F6, ChessPieceType.Knight, ChessPieceColors.Black },
        { BoardSquare.C4 | BoardSquare.G5, ChessPieceType.Bishop, ChessPieceColors.White },
        { BoardSquare.C6 | BoardSquare.G7, ChessPieceType.Bishop, ChessPieceColors.Black },
        { BoardSquare.D1 | BoardSquare.E1, ChessPieceType.Rook, ChessPieceColors.White },
        { BoardSquare.D8 | BoardSquare.E8, ChessPieceType.Rook, ChessPieceColors.Black },
        { BoardSquare.D2, ChessPieceType.Queen, ChessPieceColors.White },
        { BoardSquare.A5, ChessPieceType.Queen, ChessPieceColors.Black },
        { BoardSquare.G1, ChessPieceType.King, ChessPieceColors.White },
        { BoardSquare.G8, ChessPieceType.King, ChessPieceColors.Black },
      } );

      var foo = NegaMax.GetBestMove( cb, 4, ChessPieceColors.White, MoveGen.GenerateLegalMoves, Eval.EvaluateState );
    }

    [Fact]
    public void EloRating_7() {
      ChessBoard cb = new ChessBoard();
      cb.InitializeScenario( new ScenarioList {
        { BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C4 | BoardSquare.D5 | BoardSquare.E4 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H2, ChessPieceType.Pawn, ChessPieceColors.White },
        { BoardSquare.A6 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D6 | BoardSquare.E5 | BoardSquare.F7 | BoardSquare.G6 | BoardSquare.H6, ChessPieceType.Pawn, ChessPieceColors.Black },
        { BoardSquare.C3 | BoardSquare.D2, ChessPieceType.Knight, ChessPieceColors.White },
        { BoardSquare.D7 | BoardSquare.F6, ChessPieceType.Knight, ChessPieceColors.Black },
        { BoardSquare.E2 | BoardSquare.H4, ChessPieceType.Bishop, ChessPieceColors.White },
        { BoardSquare.C8 | BoardSquare.G7, ChessPieceType.Bishop, ChessPieceColors.Black },
        { BoardSquare.A1 | BoardSquare.F1, ChessPieceType.Rook, ChessPieceColors.White },
        { BoardSquare.A8 | BoardSquare.F8, ChessPieceType.Rook, ChessPieceColors.Black },
        { BoardSquare.D1, ChessPieceType.Queen, ChessPieceColors.White },
        { BoardSquare.E8, ChessPieceType.Queen, ChessPieceColors.Black },
        { BoardSquare.G1, ChessPieceType.King, ChessPieceColors.White },
        { BoardSquare.G8, ChessPieceType.King, ChessPieceColors.Black },
      } );
      var foo = NegaMax.GetBestMove( cb, 4, ChessPieceColors.Black, MoveGen.GenerateLegalMoves, Eval.EvaluateState );
    }

    [Fact]
    public void EloRating_8() {
      ChessBoard cb = new ChessBoard();
      cb.InitializeScenario( new ScenarioList {
        { BoardSquare.C2 | BoardSquare.C6 | BoardSquare.F3 | BoardSquare.G2 | BoardSquare.H3, ChessPieceType.Pawn, ChessPieceColors.White },
        { BoardSquare.A6 | BoardSquare.G5 | BoardSquare.H6, ChessPieceType.Pawn, ChessPieceColors.Black },
        { BoardSquare.E6, ChessPieceType.Knight, ChessPieceColors.Black },
        { BoardSquare.B6, ChessPieceType.Bishop, ChessPieceColors.White },
        { BoardSquare.C8, ChessPieceType.Rook, ChessPieceColors.White },
        { BoardSquare.E8, ChessPieceType.Rook, ChessPieceColors.Black },
        { BoardSquare.H1, ChessPieceType.King, ChessPieceColors.White },
        { BoardSquare.F7, ChessPieceType.King, ChessPieceColors.Black },
      } );
      var foo = NegaMax.GetBestMove( cb, 4, ChessPieceColors.Black, MoveGen.GenerateLegalMoves, Eval.EvaluateState );
    }

    [Fact]
    public void EloRating_9() {
      ChessBoard cb = new ChessBoard();
      cb.InitializeScenario( new ScenarioList {
        { BoardSquare.C2 | BoardSquare.G3 | BoardSquare.H2, ChessPieceType.Pawn, ChessPieceColors.White },
        { BoardSquare.A6 | BoardSquare.B7 | BoardSquare.E6 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H6, ChessPieceType.Pawn, ChessPieceColors.Black },
        { BoardSquare.E3 | BoardSquare.E4, ChessPieceType.Bishop, ChessPieceColors.White },
        { BoardSquare.D7 , ChessPieceType.Rook, ChessPieceColors.White },
        { BoardSquare.C8 | BoardSquare.F8, ChessPieceType.Rook, ChessPieceColors.Black },
        { BoardSquare.D3, ChessPieceType.Queen, ChessPieceColors.White },
        { BoardSquare.A2, ChessPieceType.Queen, ChessPieceColors.Black },
        { BoardSquare.G2, ChessPieceType.King, ChessPieceColors.White },
        { BoardSquare.G8, ChessPieceType.King, ChessPieceColors.Black },
      } );
      var foo = NegaMax.GetBestMove(cb, 4, ChessPieceColors.White, MoveGen.GenerateLegalMoves, Eval.EvaluateState);
    }

    [Fact]
    public void EloRating_10() {
      ChessBoard cb = new ChessBoard();
      cb.InitializeScenario( new ScenarioList {
        { BoardSquare.A2 | BoardSquare.C3 | BoardSquare.C4 | BoardSquare.D4 | BoardSquare.E3 | BoardSquare.F4 | BoardSquare.G2 | BoardSquare.H2, ChessPieceType.Pawn, ChessPieceColors.White },
        { BoardSquare.A7 | BoardSquare.B6 | BoardSquare.C5 | BoardSquare.D6 | BoardSquare.E4 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7, ChessPieceType.Pawn, ChessPieceColors.Black },
        { BoardSquare.B3, ChessPieceType.Knight, ChessPieceColors.White },
        { BoardSquare.C6 | BoardSquare.F6, ChessPieceType.Knight, ChessPieceColors.Black },
        { BoardSquare.C1 | BoardSquare.E2, ChessPieceType.Bishop, ChessPieceColors.White },
        { BoardSquare.C8, ChessPieceType.Bishop, ChessPieceColors.Black },
        { BoardSquare.A1 | BoardSquare.F1, ChessPieceType.Rook, ChessPieceColors.White },
        { BoardSquare.A8 | BoardSquare.F8, ChessPieceType.Rook, ChessPieceColors.Black },
        { BoardSquare.D1, ChessPieceType.Queen, ChessPieceColors.White },
        { BoardSquare.D8, ChessPieceType.Queen, ChessPieceColors.Black },
        { BoardSquare.G1, ChessPieceType.King, ChessPieceColors.White },
        { BoardSquare.G8, ChessPieceType.King, ChessPieceColors.Black },
      } );

      var foo = NegaMax.GetBestMove( cb, 4, ChessPieceColors.Black, MoveGen.GenerateLegalMoves, Eval.EvaluateState );
    }
  }
}

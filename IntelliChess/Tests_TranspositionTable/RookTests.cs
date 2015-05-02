using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  public partial class Tests_TranspositionTable {
    [Fact]
    public void Undo_WhiteRookRank2Forward_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      ColoredBitBoard[] InitMoves = new ColoredBitBoard[] {
new PawnBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black ),
new RookBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black ),
new RookBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black ),
new RookBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black )
};

      InitMoves[0].Bits = testBoard.WhitePawn.Bits ^ BoardSquare.H2 ^ BoardSquare.H4;
      InitMoves[1].Bits = testBoard.BlackKnight.Bits ^ BoardSquare.B8 ^ BoardSquare.A6;
      InitMoves[2].Bits = testBoard.WhiteRook.Bits ^ BoardSquare.H1 ^ BoardSquare.H3;
      InitMoves[3].Bits = InitMoves[1].Bits ^ BoardSquare.A6 ^ BoardSquare.B8;
      InitMoves[4].Bits = InitMoves[2].Bits ^ BoardSquare.H3 ^ BoardSquare.D3;
      InitMoves[5].Bits = InitMoves[3].Bits ^ BoardSquare.B8 ^ BoardSquare.A6;
      InitMoves[6].Bits = InitMoves[4].Bits ^ BoardSquare.D3 ^ BoardSquare.D4;
      InitMoves[7].Bits = InitMoves[5].Bits ^ BoardSquare.A6 ^ BoardSquare.B8;

      int MoveNo = 0;
      foreach ( ColoredBitBoard CBB_Mobil in InitMoves ) {        
        ulong expectedHashKey = testBoard.BoardHash.Key;
        testBoard.Update( CBB_Mobil );
        testBoard.Undo();
        Assert.Equal( expectedHashKey, testBoard.BoardHash.Key );
        testBoard.Update( CBB_Mobil );
        
        MoveNo++;
      }

      ulong expectedKey = testBoard.BoardHash.Key;
      RookBitBoard move = new RookBitBoard( ChessPieceColors.White );
      move.Bits = InitMoves[6].Bits ^ BoardSquare.D4 ^ BoardSquare.D6;

      testBoard.Update( move );
      testBoard.Undo();

      Assert.Equal( expectedKey, testBoard.BoardHash.Key );
    }

    [Fact]
    public void Undo_WhiteRookRank2Backward_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      ColoredBitBoard[] InitMoves = new ColoredBitBoard[] {
new PawnBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black ),
new RookBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black ),
new RookBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black ),
new RookBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black )
};

      InitMoves[0].Bits = testBoard.WhitePawn.Bits ^ BoardSquare.H2 ^ BoardSquare.H4;
      InitMoves[1].Bits = testBoard.BlackKnight.Bits ^ BoardSquare.B8 ^ BoardSquare.A6;
      InitMoves[2].Bits = testBoard.WhiteRook.Bits ^ BoardSquare.H1 ^ BoardSquare.H3;
      InitMoves[3].Bits = InitMoves[1].Bits ^ BoardSquare.A6 ^ BoardSquare.B8;
      InitMoves[4].Bits = InitMoves[2].Bits ^ BoardSquare.H3 ^ BoardSquare.D3;
      InitMoves[5].Bits = InitMoves[3].Bits ^ BoardSquare.B8 ^ BoardSquare.A6;
      InitMoves[6].Bits = InitMoves[4].Bits ^ BoardSquare.D3 ^ BoardSquare.D4;
      InitMoves[7].Bits = InitMoves[5].Bits ^ BoardSquare.A6 ^ BoardSquare.B8;

      foreach ( ColoredBitBoard CBB_Mobil in InitMoves ) {
        testBoard.Update( CBB_Mobil );
      }

      ulong expectedKey = testBoard.BoardHash.Key;
      RookBitBoard move = new RookBitBoard( ChessPieceColors.White );
      move.Bits = InitMoves[6].Bits ^ BoardSquare.D4 ^ BoardSquare.D2;

      testBoard.Update( move );
      testBoard.Undo();

      Assert.Equal( expectedKey, testBoard.BoardHash.Key );
    }

    [Fact]
    public void Undo_WhiteRookFile2Left_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      ColoredBitBoard[] InitMoves = new ColoredBitBoard[] {
new PawnBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black ),
new RookBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black ),
new RookBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black ),
new RookBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black )
};

      InitMoves[0].Bits = testBoard.WhitePawn.Bits ^ BoardSquare.H2 ^ BoardSquare.H4;
      InitMoves[1].Bits = testBoard.BlackKnight.Bits ^ BoardSquare.B8 ^ BoardSquare.A6;
      InitMoves[2].Bits = testBoard.WhiteRook.Bits ^ BoardSquare.H1 ^ BoardSquare.H3;
      InitMoves[3].Bits = InitMoves[1].Bits ^ BoardSquare.A6 ^ BoardSquare.B8;
      InitMoves[4].Bits = InitMoves[2].Bits ^ BoardSquare.H3 ^ BoardSquare.D3;
      InitMoves[5].Bits = InitMoves[3].Bits ^ BoardSquare.B8 ^ BoardSquare.A6;
      InitMoves[6].Bits = InitMoves[4].Bits ^ BoardSquare.D3 ^ BoardSquare.D4;
      InitMoves[7].Bits = InitMoves[5].Bits ^ BoardSquare.A6 ^ BoardSquare.B8;

      foreach ( ColoredBitBoard CBB_Mobil in InitMoves ) {
        testBoard.Update( CBB_Mobil );
      }

      ulong expectedKey = testBoard.BoardHash.Key;
      RookBitBoard move = new RookBitBoard( ChessPieceColors.White );
      move.Bits = InitMoves[6].Bits ^ BoardSquare.D4 ^ BoardSquare.B4;

      testBoard.Update( move );
      testBoard.Undo();

      Assert.Equal( expectedKey, testBoard.BoardHash.Key );
    }

    [Fact]
    public void Undo_WhiteRookFile2Right_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      ColoredBitBoard[] InitMoves = new ColoredBitBoard[] {
new PawnBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black ),
new RookBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black ),
new RookBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black ),
new RookBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black )
};

      InitMoves[0].Bits = testBoard.WhitePawn.Bits ^ BoardSquare.H2 ^ BoardSquare.H4;
      InitMoves[1].Bits = testBoard.BlackKnight.Bits ^ BoardSquare.B8 ^ BoardSquare.A6;
      InitMoves[2].Bits = testBoard.WhiteRook.Bits ^ BoardSquare.H1 ^ BoardSquare.H3;
      InitMoves[3].Bits = InitMoves[1].Bits ^ BoardSquare.A6 ^ BoardSquare.B8;
      InitMoves[4].Bits = InitMoves[2].Bits ^ BoardSquare.H3 ^ BoardSquare.D3;
      InitMoves[5].Bits = InitMoves[3].Bits ^ BoardSquare.B8 ^ BoardSquare.A6;
      InitMoves[6].Bits = InitMoves[4].Bits ^ BoardSquare.D3 ^ BoardSquare.D4;
      InitMoves[7].Bits = InitMoves[5].Bits ^ BoardSquare.A6 ^ BoardSquare.B8;

      foreach ( ColoredBitBoard CBB_Mobil in InitMoves ) {
        testBoard.Update( CBB_Mobil );
      }

      ulong expectedKey = testBoard.BoardHash.Key;
      RookBitBoard move = new RookBitBoard( ChessPieceColors.White );
      move.Bits = InitMoves[6].Bits ^ BoardSquare.D4 ^ BoardSquare.F4;

      testBoard.Update( move );
      testBoard.Undo();

      Assert.Equal( expectedKey, testBoard.BoardHash.Key );
    }

    [Fact]
    public void Undo_WhiteRookCapture_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      ColoredBitBoard[] InitMoves = new ColoredBitBoard[] {
new PawnBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black ),
new RookBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black ),
new RookBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black ),
new RookBitBoard( ChessPieceColors.White ),
new KnightBitBoard( ChessPieceColors.Black )
};

      InitMoves[0].Bits = testBoard.WhitePawn.Bits ^ BoardSquare.H2 ^ BoardSquare.H4;
      InitMoves[1].Bits = testBoard.BlackKnight.Bits ^ BoardSquare.B8 ^ BoardSquare.A6;
      InitMoves[2].Bits = testBoard.WhiteRook.Bits ^ BoardSquare.H1 ^ BoardSquare.H3;
      InitMoves[3].Bits = InitMoves[1].Bits ^ BoardSquare.A6 ^ BoardSquare.B8;
      InitMoves[4].Bits = InitMoves[2].Bits ^ BoardSquare.H3 ^ BoardSquare.D3;
      InitMoves[5].Bits = InitMoves[3].Bits ^ BoardSquare.B8 ^ BoardSquare.A6;
      InitMoves[6].Bits = InitMoves[4].Bits ^ BoardSquare.D3 ^ BoardSquare.D4;
      InitMoves[7].Bits = InitMoves[5].Bits ^ BoardSquare.A6 ^ BoardSquare.B8;

      foreach ( ColoredBitBoard CBB_Mobil in InitMoves ) {
        testBoard.Update( CBB_Mobil );
      }

      ulong expectedKey = testBoard.BoardHash.Key;
      RookBitBoard move = new RookBitBoard( ChessPieceColors.White );
      move.Bits = InitMoves[6].Bits ^ BoardSquare.D4 ^ BoardSquare.D7;

      testBoard.Update( move );
      testBoard.Undo();

      Assert.Equal( expectedKey, testBoard.BoardHash.Key );
    }

    [Fact]
    public void Undo_BlackRookRank2Forward_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      ColoredBitBoard[] InitMoves = new ColoredBitBoard[] {
new PawnBitBoard( ChessPieceColors.White ),
new PawnBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White ),
new RookBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White ),
new RookBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White ),
new RookBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White)
};

      InitMoves[0].Bits = testBoard.WhitePawn.Bits ^ BoardSquare.H2 ^ BoardSquare.H4;
      InitMoves[1].Bits = testBoard.BlackPawn.Bits ^ BoardSquare.A7 ^ BoardSquare.A5;
      InitMoves[2].Bits = InitMoves[0].Bits ^ BoardSquare.H4 ^ BoardSquare.H5;
      InitMoves[3].Bits = testBoard.BlackRook.Bits ^ BoardSquare.A8 ^ BoardSquare.A6;
      InitMoves[4].Bits = InitMoves[2].Bits ^ BoardSquare.H5 ^ BoardSquare.H6;
      InitMoves[5].Bits = InitMoves[3].Bits ^ BoardSquare.A6 ^ BoardSquare.D6;
      InitMoves[6].Bits = InitMoves[4].Bits ^ BoardSquare.G2 ^ BoardSquare.G4;
      InitMoves[7].Bits = InitMoves[5].Bits ^ BoardSquare.D6 ^ BoardSquare.D4;
      InitMoves[8].Bits = InitMoves[6].Bits ^ BoardSquare.G4 ^ BoardSquare.G5;
      int MoveNo = 0;
      foreach ( ColoredBitBoard CBB_Mobil in InitMoves ) {
        ulong expectedHashKey = testBoard.BoardHash.Key;
        testBoard.Update( CBB_Mobil );
        testBoard.Undo();
        Assert.Equal( expectedHashKey, testBoard.BoardHash.Key );
        testBoard.Update( CBB_Mobil );

        MoveNo++;
      }

      ulong expectedKey = testBoard.BoardHash.Key;
      RookBitBoard move = new RookBitBoard( ChessPieceColors.Black );
      move.Bits = InitMoves[7].Bits ^ BoardSquare.D4 ^ BoardSquare.D6;

      testBoard.Update( move );
      testBoard.Undo();

      Assert.Equal( expectedKey, testBoard.BoardHash.Key );
    }

    [Fact]
    public void Undo_BlackRookRank1Backward_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      ColoredBitBoard[] InitMoves = new ColoredBitBoard[] {
new PawnBitBoard( ChessPieceColors.White ),
new PawnBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White ),
new RookBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White ),
new RookBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White ),
new RookBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White )
};

      InitMoves[0].Bits = testBoard.WhitePawn.Bits ^ BoardSquare.H2 ^ BoardSquare.H4;
      InitMoves[1].Bits = testBoard.BlackPawn.Bits ^ BoardSquare.A7 ^ BoardSquare.A5;
      InitMoves[2].Bits = InitMoves[0].Bits ^ BoardSquare.H4 ^ BoardSquare.H5;
      InitMoves[3].Bits = testBoard.BlackRook.Bits ^ BoardSquare.A8 ^ BoardSquare.A6;
      InitMoves[4].Bits = InitMoves[2].Bits ^ BoardSquare.H5 ^ BoardSquare.H6;
      InitMoves[5].Bits = InitMoves[3].Bits ^ BoardSquare.A6 ^ BoardSquare.D6;
      InitMoves[6].Bits = InitMoves[4].Bits ^ BoardSquare.G2 ^ BoardSquare.G4;
      InitMoves[7].Bits = InitMoves[5].Bits ^ BoardSquare.D6 ^ BoardSquare.D4;
      InitMoves[8].Bits = InitMoves[6].Bits ^ BoardSquare.G4 ^ BoardSquare.G5;
      foreach ( ColoredBitBoard CBB_Mobil in InitMoves ) {
        testBoard.Update( CBB_Mobil );
      }

      ulong expectedKey = testBoard.BoardHash.Key;
      RookBitBoard move = new RookBitBoard( ChessPieceColors.Black );
      move.Bits = InitMoves[7].Bits ^ BoardSquare.D4 ^ BoardSquare.D3;

      testBoard.Update( move );
      testBoard.Undo();

      Assert.Equal( expectedKey, testBoard.BoardHash.Key );
    }

    [Fact]
    public void Undo_BlackRookFile2Left_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      ColoredBitBoard[] InitMoves = new ColoredBitBoard[] {
new PawnBitBoard( ChessPieceColors.White ),
new PawnBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White ),
new RookBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White ),
new RookBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White ),
new RookBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White )
};

      InitMoves[0].Bits = testBoard.WhitePawn.Bits ^ BoardSquare.H2 ^ BoardSquare.H4;
      InitMoves[1].Bits = testBoard.BlackPawn.Bits ^ BoardSquare.A7 ^ BoardSquare.A5;
      InitMoves[2].Bits = InitMoves[0].Bits ^ BoardSquare.H4 ^ BoardSquare.H5;
      InitMoves[3].Bits = testBoard.BlackRook.Bits ^ BoardSquare.A8 ^ BoardSquare.A6;
      InitMoves[4].Bits = InitMoves[2].Bits ^ BoardSquare.H5 ^ BoardSquare.H6;
      InitMoves[5].Bits = InitMoves[3].Bits ^ BoardSquare.A6 ^ BoardSquare.D6;
      InitMoves[6].Bits = InitMoves[4].Bits ^ BoardSquare.G2 ^ BoardSquare.G4;
      InitMoves[7].Bits = InitMoves[5].Bits ^ BoardSquare.D6 ^ BoardSquare.D4;
      InitMoves[8].Bits = InitMoves[6].Bits ^ BoardSquare.G4 ^ BoardSquare.G5;
      foreach ( ColoredBitBoard CBB_Mobil in InitMoves ) {
        testBoard.Update( CBB_Mobil );
      }

      ulong expectedKey = testBoard.BoardHash.Key;
      RookBitBoard move = new RookBitBoard( ChessPieceColors.Black );
      move.Bits = InitMoves[7].Bits ^ BoardSquare.D4 ^ BoardSquare.B4;

      testBoard.Update( move );
      testBoard.Undo();

      Assert.Equal( expectedKey, testBoard.BoardHash.Key );
    }

    [Fact]
    public void Undo_BlackRookFile2Right_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      ColoredBitBoard[] InitMoves = new ColoredBitBoard[] {
new PawnBitBoard( ChessPieceColors.White ),
new PawnBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White ),
new RookBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White ),
new RookBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White ),
new RookBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White )
};

      InitMoves[0].Bits = testBoard.WhitePawn.Bits ^ BoardSquare.H2 ^ BoardSquare.H4;
      InitMoves[1].Bits = testBoard.BlackPawn.Bits ^ BoardSquare.A7 ^ BoardSquare.A5;
      InitMoves[2].Bits = InitMoves[0].Bits ^ BoardSquare.H4 ^ BoardSquare.H5;
      InitMoves[3].Bits = testBoard.BlackRook.Bits ^ BoardSquare.A8 ^ BoardSquare.A6;
      InitMoves[4].Bits = InitMoves[2].Bits ^ BoardSquare.H5 ^ BoardSquare.H6;
      InitMoves[5].Bits = InitMoves[3].Bits ^ BoardSquare.A6 ^ BoardSquare.D6;
      InitMoves[6].Bits = InitMoves[4].Bits ^ BoardSquare.G2 ^ BoardSquare.G4;
      InitMoves[7].Bits = InitMoves[5].Bits ^ BoardSquare.D6 ^ BoardSquare.D4;
      InitMoves[8].Bits = InitMoves[6].Bits ^ BoardSquare.G4 ^ BoardSquare.G5;
      foreach ( ColoredBitBoard CBB_Mobil in InitMoves ) {
        testBoard.Update( CBB_Mobil );
      }

      ulong expectedKey = testBoard.BoardHash.Key;
      RookBitBoard move = new RookBitBoard( ChessPieceColors.Black );
      move.Bits = InitMoves[7].Bits ^ BoardSquare.D4 ^ BoardSquare.F4;

      testBoard.Update( move );
      testBoard.Undo();

      Assert.Equal( expectedKey, testBoard.BoardHash.Key );
    }

    [Fact]
    public void Undo_BlackRookCapture_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      ColoredBitBoard[] InitMoves = new ColoredBitBoard[] {
new PawnBitBoard( ChessPieceColors.White ),
new PawnBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White ),
new RookBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White ),
new RookBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White ),
new RookBitBoard( ChessPieceColors.Black ),
new PawnBitBoard( ChessPieceColors.White )
};

      InitMoves[0].Bits = testBoard.WhitePawn.Bits ^ BoardSquare.H2 ^ BoardSquare.H4;
      InitMoves[1].Bits = testBoard.BlackPawn.Bits ^ BoardSquare.A7 ^ BoardSquare.A5;
      InitMoves[2].Bits = InitMoves[0].Bits ^ BoardSquare.H4 ^ BoardSquare.H5;
      InitMoves[3].Bits = testBoard.BlackRook.Bits ^ BoardSquare.A8 ^ BoardSquare.A6;
      InitMoves[4].Bits = InitMoves[2].Bits ^ BoardSquare.H5 ^ BoardSquare.H6;
      InitMoves[5].Bits = InitMoves[3].Bits ^ BoardSquare.A6 ^ BoardSquare.D6;
      InitMoves[6].Bits = InitMoves[4].Bits ^ BoardSquare.G2 ^ BoardSquare.G4;
      InitMoves[7].Bits = InitMoves[5].Bits ^ BoardSquare.D6 ^ BoardSquare.D4;
      InitMoves[8].Bits = InitMoves[6].Bits ^ BoardSquare.G4 ^ BoardSquare.G5;
      foreach ( ColoredBitBoard CBB_Mobil in InitMoves ) {
        testBoard.Update( CBB_Mobil );
      }

      ulong expectedKey = testBoard.BoardHash.Key;
      RookBitBoard move = new RookBitBoard( ChessPieceColors.Black );
      move.Bits = InitMoves[7].Bits ^ BoardSquare.D4 ^ BoardSquare.D2;

      testBoard.Update( move );
      testBoard.Undo();

      Assert.Equal( expectedKey, testBoard.BoardHash.Key );
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  partial class Tests_TranspositionTable {
    [Fact]
    public void Undo_WhiteKnightLeft_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      KnightBitBoard move1 = new KnightBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhiteKnight.Bits ^ BoardSquare.B1 ) | BoardSquare.A3;

      ulong expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move1 );
      testBoard.Undo();
      ulong testHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, testHash );
    }
    [Fact]
    public void Undo_WhiteKnightRight_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      KnightBitBoard move1 = new KnightBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhiteKnight.Bits ^ BoardSquare.B1 ) | BoardSquare.C3;

      ulong expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move1 );
      testBoard.Undo();
      ulong testHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, testHash );
    }
    [Fact]
    public void Undo_WhiteKnightCapture_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      KnightBitBoard move1 = new KnightBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhiteKnight.Bits ^ BoardSquare.G1 ) | BoardSquare.F3;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.B7 ) | BoardSquare.B6;
      KnightBitBoard move3 = new KnightBitBoard( ChessPieceColors.White );
      move3.Bits = ( move1.Bits ^ BoardSquare.F3 ) | BoardSquare.G5;
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      move4.Bits = ( move2.Bits ^ BoardSquare.B6 ) | BoardSquare.B5;
      KnightBitBoard move5 = new KnightBitBoard( ChessPieceColors.White );
      move5.Bits = ( move3.Bits ^ BoardSquare.G5 ) | BoardSquare.F7;

      ulong expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move1 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move1 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move2 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move2 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move3 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move3 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move4 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move4 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move5 );
      testBoard.Undo();
      ulong testHash = testBoard.BoardHash.Key;
      Assert.Equal( expectedHash, testHash );
    }
    [Fact]
    public void Undo_BlackKnightLeft_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      KnightBitBoard move1 = new KnightBitBoard( ChessPieceColors.Black );
      move1.Bits = ( testBoard.BlackKnight.Bits ^ BoardSquare.B8 ) | BoardSquare.A6;

      ulong expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move1 );
      testBoard.Undo();
      ulong testHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, testHash );
    }
    [Fact]
    public void Undo_BlackKnightRight_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      KnightBitBoard move1 = new KnightBitBoard( ChessPieceColors.Black );
      move1.Bits = ( testBoard.BlackKnight.Bits ^ BoardSquare.B8 ) | BoardSquare.C6;

      ulong expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move1 );

      testBoard.Undo();
      ulong testHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, testHash );
    }
    [Fact]
    public void Undo_BlackKnightCapture_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.G2 ) | BoardSquare.G3;
      KnightBitBoard move2 = new KnightBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackKnight.Bits ^ BoardSquare.B8 ) | BoardSquare.A6;
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      move3.Bits = ( move1.Bits ^ BoardSquare.G3 ) | BoardSquare.G4;
      KnightBitBoard move4 = new KnightBitBoard( ChessPieceColors.Black );
      move4.Bits = ( move2.Bits ^ BoardSquare.A6 ) | BoardSquare.B4;
      PawnBitBoard move5 = new PawnBitBoard( ChessPieceColors.White );
      move5.Bits = ( move3.Bits ^ BoardSquare.G4 ) | BoardSquare.G5;
      KnightBitBoard move6 = new KnightBitBoard( ChessPieceColors.Black );
      move6.Bits = ( move4.Bits ^ BoardSquare.B4 ) | BoardSquare.A2;


      ulong expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move1 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move1 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move2 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move2 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move3 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move3 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move4 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move4 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move5 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move5 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move6 );
      testBoard.Undo();
      ulong testHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, testHash );
    }
  }
}

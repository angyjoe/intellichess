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
    public void Undo_WhiteKingLeft_Equal() {

      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.C2 ) | BoardSquare.C3;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.G7 ) | BoardSquare.G6;
      QueenBitBoard move3 = new QueenBitBoard( ChessPieceColors.White );
      move3.Bits = ( testBoard.WhiteQueen.Bits ^ BoardSquare.D1 ) | BoardSquare.C2;
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      move4.Bits = ( move2.Bits ^ BoardSquare.G6 ) | BoardSquare.G5;
      KingBitBoard move5 = new KingBitBoard( ChessPieceColors.White );
      move5.Bits = ( testBoard.WhiteKing.Bits ^ BoardSquare.E1 ) | BoardSquare.D1;
      PawnBitBoard move6 = new PawnBitBoard( ChessPieceColors.Black );
      move6.Bits = ( move4.Bits ^ BoardSquare.G5 ) | BoardSquare.G4;
      PawnBitBoard move7 = new PawnBitBoard( ChessPieceColors.White );
      move7.Bits = ( move1.Bits ^ BoardSquare.B2 ) | BoardSquare.B3;
      PawnBitBoard move8 = new PawnBitBoard( ChessPieceColors.Black );
      move8.Bits = ( move6.Bits ^ BoardSquare.G4 ) | BoardSquare.G3;
      BishopBitBoard move9 = new BishopBitBoard( ChessPieceColors.White );
      move9.Bits = ( testBoard.WhiteBishop.Bits ^ BoardSquare.C1 ) | BoardSquare.B2;
      PawnBitBoard move10 = new PawnBitBoard( ChessPieceColors.Black );
      move10.Bits = ( move8.Bits ^ BoardSquare.H7 ) | BoardSquare.H6;
      KingBitBoard move11 = new KingBitBoard( ChessPieceColors.White );
      move11.Bits = ( move5.Bits ^ BoardSquare.D1 ) | BoardSquare.C1;

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
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move6 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move7 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move7 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move8 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move8 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move9 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move9 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move10 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move10 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move11 );
      testBoard.Undo();

      ulong testHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, testHash );
    }
    [Fact]
    public void Undo_WhiteKingRight_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.G2 ) | BoardSquare.G3;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.G7 ) | BoardSquare.G6;
      BishopBitBoard move3 = new BishopBitBoard( ChessPieceColors.White );
      move3.Bits = ( testBoard.WhiteBishop.Bits ^ BoardSquare.F1 ) | BoardSquare.G2;
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      move4.Bits = ( move2.Bits ^ BoardSquare.G6 ) | BoardSquare.G5;
      KingBitBoard move5 = new KingBitBoard( ChessPieceColors.White );
      move5.Bits = ( testBoard.WhiteKing.Bits ^ BoardSquare.E1 ) | BoardSquare.F1;
      PawnBitBoard move6 = new PawnBitBoard( ChessPieceColors.Black );
      move6.Bits = ( move4.Bits ^ BoardSquare.G5 ) | BoardSquare.G4;
      KingBitBoard move7 = new KingBitBoard( ChessPieceColors.White );
      move7.Bits = ( move5.Bits ^ BoardSquare.F1 ) | BoardSquare.G1;

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
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move6 );

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move7 );
      testBoard.Undo();
      ulong testHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, testHash );
    }
    [Fact]
    public void Undo_WhiteKingRankForward_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.E2 ) | BoardSquare.E3;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.A7 ) | BoardSquare.A6;
      KingBitBoard move3 = new KingBitBoard( ChessPieceColors.White );
      move3.Bits = ( testBoard.WhiteKing.Bits ^ BoardSquare.E1 ) | BoardSquare.E2;

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
      ulong testHash = testBoard.BoardHash.Key;
      Assert.Equal( expectedHash, testHash );
    }
    [Fact]
    public void Undo_WhiteKingRankBackward_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.E2 ) | BoardSquare.E3;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.A7 ) | BoardSquare.A6;
      KingBitBoard move3 = new KingBitBoard( ChessPieceColors.White );
      move3.Bits = ( testBoard.WhiteKing.Bits ^ BoardSquare.E1 ) | BoardSquare.E2;
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      move4.Bits = ( move2.Bits ^ BoardSquare.A6 ) | BoardSquare.A5;
      KingBitBoard move5 = new KingBitBoard( ChessPieceColors.White );
      move5.Bits = ( move3.Bits ^ BoardSquare.E2 ) | BoardSquare.E1;

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
    public void Undo_WhiteKingCapture_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.E2 ) | BoardSquare.E3;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.F7 ) | BoardSquare.F5;
      KingBitBoard move3 = new KingBitBoard( ChessPieceColors.White );
      move3.Bits = ( testBoard.WhiteKing.Bits ^ BoardSquare.E1 ) | BoardSquare.E2;
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      move4.Bits = ( move2.Bits ^ BoardSquare.F5 ) | BoardSquare.F4;
      KingBitBoard move5 = new KingBitBoard( ChessPieceColors.White );
      move5.Bits = ( move3.Bits ^ BoardSquare.E2 ) | BoardSquare.F3;
      PawnBitBoard move6 = new PawnBitBoard( ChessPieceColors.Black );
      move6.Bits = ( move4.Bits ^ BoardSquare.A7 ) | BoardSquare.A6;
      KingBitBoard move7 = new KingBitBoard( ChessPieceColors.White );
      move7.Bits = ( move5.Bits ^ BoardSquare.F3 ) | BoardSquare.F4;

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
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move6 );
      expectedHash = testBoard.BoardHash.Key;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move7 );
      testBoard.Undo();
      ulong testHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, testHash );
    }
    [Fact]
    public void Undo_WhiteKingKingSideCastling_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      KnightBitBoard move1 = new KnightBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhiteKnight.Bits ^ BoardSquare.G1 ) | BoardSquare.F3;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.A7 ) | BoardSquare.A6;
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      move3.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.G2 ) | BoardSquare.G3;
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      move4.Bits = ( move2.Bits ^ BoardSquare.A6 ) | BoardSquare.A5;
      BishopBitBoard move5 = new BishopBitBoard( ChessPieceColors.White );
      move5.Bits = ( testBoard.WhiteBishop.Bits ^ BoardSquare.F1 ) | BoardSquare.G2;
      PawnBitBoard move6 = new PawnBitBoard( ChessPieceColors.Black );
      move6.Bits = ( move4.Bits ^ BoardSquare.A5 ) | BoardSquare.A4;
      KingBitBoard move7 = new KingBitBoard( ChessPieceColors.White );
      move7.Bits = ( testBoard.WhiteKing.Bits ^ BoardSquare.E1 ) | BoardSquare.G1;

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
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move6 );
      expectedHash = testBoard.BoardHash.Key;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move7 );
      testBoard.Undo();
      ulong testHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedHash, testHash );
    }
    [Fact]
    public void Undo_WhiteKingQueenSideCastling_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      KnightBitBoard move1 = new KnightBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhiteKnight.Bits ^ BoardSquare.B1 ) | BoardSquare.C3;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.H7 ) | BoardSquare.H6;
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      move3.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.B2 ) | BoardSquare.B3;
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      move4.Bits = ( move2.Bits ^ BoardSquare.H6 ) | BoardSquare.H5;
      BishopBitBoard move5 = new BishopBitBoard( ChessPieceColors.White );
      move5.Bits = ( testBoard.WhiteBishop.Bits ^ BoardSquare.C1 ) | BoardSquare.A3;
      PawnBitBoard move6 = new PawnBitBoard( ChessPieceColors.Black );
      move6.Bits = ( move4.Bits ^ BoardSquare.H5 ) | BoardSquare.H4;
      QueenBitBoard move7 = new QueenBitBoard( ChessPieceColors.White );
      move7.Bits = ( testBoard.WhiteQueen.Bits ^ BoardSquare.D1 ) | BoardSquare.B1;
      PawnBitBoard move8 = new PawnBitBoard( ChessPieceColors.Black );
      move8.Bits = ( move6.Bits ^ BoardSquare.H4 ) | BoardSquare.H3;
      QueenBitBoard move9 = new QueenBitBoard( ChessPieceColors.White );
      move9.Bits = ( move7.Bits ^ BoardSquare.B1 ) | BoardSquare.B2;
      PawnBitBoard move10 = new PawnBitBoard( ChessPieceColors.Black );
      move10.Bits = ( move8.Bits ^ BoardSquare.G7 ) | BoardSquare.G5;
      KingBitBoard move11 = new KingBitBoard( ChessPieceColors.White );
      move11.Bits = ( testBoard.WhiteKing.Bits ^ BoardSquare.E1 ) | BoardSquare.C1;

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
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move6 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move7 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move7 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move8 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move8 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move9 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move9 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move10 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move10 );
      expectedHash = testBoard.BoardHash.Key;

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update( move11 );
      testBoard.Undo();
      ulong testHash = testBoard.BoardHash.Key;
      Assert.Equal( expectedHash, testHash );
    }
    [Fact]
    public void Undo_BlackKingLeft_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.H2 ) | BoardSquare.H3;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.C7 ) | BoardSquare.C6;
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      move3.Bits = ( move1.Bits ^ BoardSquare.H3 ) | BoardSquare.H4;
      QueenBitBoard move4 = new QueenBitBoard( ChessPieceColors.Black );
      move4.Bits = ( testBoard.BlackQueen.Bits ^ BoardSquare.D8 ) | BoardSquare.C7;
      PawnBitBoard move5 = new PawnBitBoard( ChessPieceColors.White );
      move5.Bits = ( move3.Bits ^ BoardSquare.H4 ) | BoardSquare.H5;
      KingBitBoard move6 = new KingBitBoard( ChessPieceColors.Black );
      move6.Bits = ( testBoard.BlackKing.Bits ^ BoardSquare.E8 ) | BoardSquare.D8;
      PawnBitBoard move7 = new PawnBitBoard( ChessPieceColors.White );
      move7.Bits = ( move5.Bits ^ BoardSquare.H5 ) | BoardSquare.H6;
      PawnBitBoard move8 = new PawnBitBoard( ChessPieceColors.Black );
      move8.Bits = ( move2.Bits ^ BoardSquare.B7 ) | BoardSquare.B6;
      PawnBitBoard move9 = new PawnBitBoard( ChessPieceColors.White );
      move9.Bits = ( move7.Bits ^ BoardSquare.G2 ) | BoardSquare.G3;
      BishopBitBoard move10 = new BishopBitBoard( ChessPieceColors.Black );
      move10.Bits = ( testBoard.BlackBishop.Bits ^ BoardSquare.C8 ) | BoardSquare.B7;
      PawnBitBoard move11 = new PawnBitBoard( ChessPieceColors.White );
      move11.Bits = ( move9.Bits ^ BoardSquare.G3 ) | BoardSquare.G4;
      KingBitBoard move12 = new KingBitBoard( ChessPieceColors.Black );
      move12.Bits = ( move6.Bits ^ BoardSquare.D8 ) | BoardSquare.C8;

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
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move6 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move7 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move7 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move8 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move8 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move9 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move9 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move10 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move10 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move11 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move11 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move12 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
    }
    [Fact]
    public void Undo_BlackKingRight_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.A2 ) | BoardSquare.A3;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.G7 ) | BoardSquare.G6;
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      move3.Bits = ( move1.Bits ^ BoardSquare.A3 ) | BoardSquare.A4;
      BishopBitBoard move4 = new BishopBitBoard( ChessPieceColors.Black );
      move4.Bits = ( testBoard.BlackBishop.Bits ^ BoardSquare.F8 ) | BoardSquare.G7;
      PawnBitBoard move5 = new PawnBitBoard( ChessPieceColors.White );
      move5.Bits = ( move3.Bits ^ BoardSquare.A4 ) | BoardSquare.A5;
      KingBitBoard move6 = new KingBitBoard( ChessPieceColors.Black );
      move6.Bits = ( testBoard.BlackKing.Bits ^ BoardSquare.E8 ) | BoardSquare.F8;
      PawnBitBoard move7 = new PawnBitBoard( ChessPieceColors.White );
      move7.Bits = ( move5.Bits ^ BoardSquare.A5 ) | BoardSquare.A6;
      KnightBitBoard move8 = new KnightBitBoard( ChessPieceColors.Black );
      move8.Bits = ( testBoard.BlackKnight.Bits ^ BoardSquare.G8 ) | BoardSquare.H6;
      PawnBitBoard move9 = new PawnBitBoard( ChessPieceColors.White );
      move9.Bits = ( move7.Bits ^ BoardSquare.B2 ) | BoardSquare.B4;
      KingBitBoard move10 = new KingBitBoard( ChessPieceColors.Black );
      move10.Bits = ( move6.Bits ^ BoardSquare.F8 ) | BoardSquare.G8;

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
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move6 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move7 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move7 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move8 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move8 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move9 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move9 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move10 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
    }
    [Fact]
    public void Undo_BlackKingRankBackward_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.H2 ) | BoardSquare.H3;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.E7 ) | BoardSquare.E6;
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      move3.Bits = ( move1.Bits ^ BoardSquare.H3 ) | BoardSquare.H4;
      KingBitBoard move4 = new KingBitBoard( ChessPieceColors.Black );
      move4.Bits = ( testBoard.BlackKing.Bits ^ BoardSquare.E8 ) | BoardSquare.E7;

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
    }
    [Fact]
    public void Undo_BlackKingRankForward_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.H2 ) | BoardSquare.H3;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.E7 ) | BoardSquare.E6;
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      move3.Bits = ( move1.Bits ^ BoardSquare.H3 ) | BoardSquare.H4;
      KingBitBoard move4 = new KingBitBoard( ChessPieceColors.Black );
      move4.Bits = ( testBoard.BlackKing.Bits ^ BoardSquare.E8 ) | BoardSquare.E7;
      PawnBitBoard move5 = new PawnBitBoard( ChessPieceColors.White );
      move5.Bits = ( move3.Bits ^ BoardSquare.H4 ) | BoardSquare.H5;
      KingBitBoard move6 = new KingBitBoard( ChessPieceColors.Black );
      move6.Bits = ( move4.Bits ^ BoardSquare.E7 ) | BoardSquare.E8;

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
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
    }
    [Fact]
    public void Undo_BlackKingCapture_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.E2 ) | BoardSquare.E4;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.F7 ) | BoardSquare.F6;
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      move3.Bits = ( move1.Bits ^ BoardSquare.E4 ) | BoardSquare.E5;
      KingBitBoard move4 = new KingBitBoard( ChessPieceColors.Black );
      move4.Bits = ( testBoard.BlackKing.Bits ^ BoardSquare.E8 ) | BoardSquare.F7;
      PawnBitBoard move5 = new PawnBitBoard( ChessPieceColors.White );
      move5.Bits = ( move3.Bits ^ BoardSquare.E5 ) | BoardSquare.E6;
      KingBitBoard move6 = new KingBitBoard( ChessPieceColors.Black );
      move6.Bits = ( move4.Bits ^ BoardSquare.F7 ) | BoardSquare.E6;

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
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
    }
    [Fact]
    public void Undo_BlackKingKingSideCastling_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.A2 ) | BoardSquare.A3;
      KnightBitBoard move2 = new KnightBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackKnight.Bits ^ BoardSquare.G8 ) | BoardSquare.F6;
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      move3.Bits = ( move1.Bits ^ BoardSquare.A3 ) | BoardSquare.A4;
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      move4.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.G7 ) | BoardSquare.G6;
      PawnBitBoard move5 = new PawnBitBoard( ChessPieceColors.White );
      move5.Bits = ( move3.Bits ^ BoardSquare.A4 ) | BoardSquare.A5;
      BishopBitBoard move6 = new BishopBitBoard( ChessPieceColors.Black );
      move6.Bits = ( testBoard.BlackBishop.Bits ^ BoardSquare.F8 ) | BoardSquare.G7;
      PawnBitBoard move7 = new PawnBitBoard( ChessPieceColors.White );
      move7.Bits = ( move5.Bits ^ BoardSquare.A5 ) | BoardSquare.A6;
      KingBitBoard move8 = new KingBitBoard( ChessPieceColors.Black );
      move8.Bits = ( testBoard.BlackKing.Bits ^ BoardSquare.E8 ) | BoardSquare.G8;

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
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move6 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move7 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move7 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move8 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
    }
    [Fact]
    public void Undo_BlackKingQueenSideCastling_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.H2 ) | BoardSquare.H3;
      KnightBitBoard move2 = new KnightBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackKnight.Bits ^ BoardSquare.B8 ) | BoardSquare.C6;
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      move3.Bits = ( move1.Bits ^ BoardSquare.H3 ) | BoardSquare.H4;
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      move4.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.B7 ) | BoardSquare.B6;
      PawnBitBoard move5 = new PawnBitBoard( ChessPieceColors.White );
      move5.Bits = ( move3.Bits ^ BoardSquare.H4 ) | BoardSquare.H5;
      BishopBitBoard move6 = new BishopBitBoard( ChessPieceColors.Black );
      move6.Bits = ( testBoard.BlackBishop.Bits ^ BoardSquare.C8 ) | BoardSquare.A6;
      PawnBitBoard move7 = new PawnBitBoard( ChessPieceColors.White );
      move7.Bits = ( move5.Bits ^ BoardSquare.H5 ) | BoardSquare.H6;
      QueenBitBoard move8 = new QueenBitBoard( ChessPieceColors.Black );
      move8.Bits = ( testBoard.BlackQueen.Bits ^ BoardSquare.D8 ) | BoardSquare.B8;
      PawnBitBoard move9 = new PawnBitBoard( ChessPieceColors.White );
      move9.Bits = ( move7.Bits ^ BoardSquare.G2 ) | BoardSquare.G3;
      QueenBitBoard move10 = new QueenBitBoard( ChessPieceColors.Black );
      move10.Bits = ( move8.Bits ^ BoardSquare.B8 ) | BoardSquare.B7;
      PawnBitBoard move11 = new PawnBitBoard( ChessPieceColors.White );
      move11.Bits = ( move9.Bits ^ BoardSquare.G3 ) | BoardSquare.G4;
      KingBitBoard move12 = new KingBitBoard( ChessPieceColors.Black );
      move12.Bits = ( testBoard.BlackKing.Bits ^ BoardSquare.E8 ) | BoardSquare.C8;

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
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move6 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move7 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move7 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move8 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move8 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move9 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move9 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move10 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move10 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move11 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
      testBoard.Update( move11 );
      expectedHash = testBoard.BoardHash.Key;

      testBoard.Update( move12 );
      testBoard.Undo();
      Assert.Equal( expectedHash, testBoard.BoardHash.Key );
    }
  }
}

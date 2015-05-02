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
#region Pawns
    [Fact]
    public void Undo_WhitePawnCapture_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.H2 ) | BoardSquare.H4;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.G7 ) | BoardSquare.G5;
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      move3.Bits = ( move1.Bits ^ BoardSquare.H4 ) | BoardSquare.G5;

      testBoard.Update(move1);
      testBoard.Update(move2);
      ulong expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(move3);
      testBoard.Undo();
      ulong testHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, testHash);    
    }

    [Fact]
    public void Undo_BlackPawnCapture_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.H2 ) | BoardSquare.H4;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.G7 ) | BoardSquare.G5;
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      move3.Bits = ( move1.Bits ^ BoardSquare.G2 ) | BoardSquare.G4;
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      move4.Bits = ( move2.Bits ^ BoardSquare.G5 ) | BoardSquare.H4;

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
    public void Undo_WhitePawnRank1Forward_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      ulong expectedKey = testBoard.BoardHash.Key;

      PawnBitBoard move = new PawnBitBoard( ChessPieceColors.White );
      move.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.A2 ) | BoardSquare.A3;

      testBoard.Update( move );
      testBoard.Undo();

      ulong actualKey = testBoard.BoardHash.Key;
      Assert.Equal( expectedKey, actualKey );
    }

    [Fact]
    public void Undo_WhitePawnRank2Forward_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      ulong expectedKey = testBoard.BoardHash.Key;

      PawnBitBoard move = new PawnBitBoard( ChessPieceColors.White );
      move.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.A2 ) | BoardSquare.A4;

      testBoard.Update( move );
      testBoard.Undo();

      ulong actualKey = testBoard.BoardHash.Key;
      Assert.Equal( expectedKey, actualKey );
    }

    [Fact]
    public void Undo_BlackPawnRank2Forward2_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move0 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.Black );
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.White );
      PawnBitBoard move5 = new PawnBitBoard( ChessPieceColors.Black );

      move0.Bits = testBoard.WhitePawn.Bits ^ BoardSquare.E2 ^ BoardSquare.E4;
      move1.Bits = testBoard.BlackPawn.Bits ^ BoardSquare.C7 ^ BoardSquare.C5;
      move2.Bits = move0.Bits ^ BoardSquare.F2 ^ BoardSquare.F3;
      move3.Bits = move1.Bits ^ BoardSquare.E7 ^ BoardSquare.E5;
      move4.Bits = move2.Bits ^ BoardSquare.A2 ^ BoardSquare.A4;
      move5.Bits = move3.Bits ^ BoardSquare.H7 ^ BoardSquare.H5;

      testBoard.Update( move0 );
      testBoard.Update( move1 );
      testBoard.Update( move2 );
      testBoard.Update( move3 );
      testBoard.Update( move4 );
      ulong hashbefore = testBoard.BoardHash.Key;
      testBoard.Update( move5 );
      testBoard.Undo();

      ulong actualKey = testBoard.BoardHash.Key;
      Assert.Equal( hashbefore, actualKey );
    }

    [Fact]
    public void Undo_BlackPawnRank1Forward_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      ulong expectedKey = testBoard.BoardHash.Key;

      PawnBitBoard move = new PawnBitBoard( ChessPieceColors.Black );
      move.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.A7 ) | BoardSquare.A6;

      testBoard.Update( move );
      testBoard.Undo();

      ulong actualKey = testBoard.BoardHash.Key;
      Assert.Equal( expectedKey, actualKey );
    }

    [Fact]
    public void Undo_BlackPawnRank2Forward_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      ulong expectedKey = testBoard.BoardHash.Key;

      PawnBitBoard move = new PawnBitBoard( ChessPieceColors.Black );
      move.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.A7 ) | BoardSquare.A5;

      testBoard.Update( move );
      testBoard.Undo();

      ulong actualKey = testBoard.BoardHash.Key;
      Assert.Equal( expectedKey, actualKey );
    }

    [Fact]
    public void Undo_WhitePawnEnPassantRight_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.A2 ) | BoardSquare.A4;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.H7 ) | BoardSquare.H6;
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      move3.Bits = ( move1.Bits ^ BoardSquare.A4 ) | BoardSquare.A5;
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      move4.Bits = ( move2.Bits ^ BoardSquare.B7 ) | BoardSquare.B5;
      PawnBitBoard move5 = new PawnBitBoard( ChessPieceColors.White );
      move5.Bits = ( move3.Bits ^ BoardSquare.A5 ) | BoardSquare.B6;


      testBoard.Update( move1 );
      testBoard.Update( move2 );
      testBoard.Update( move3 );
      testBoard.Update( move4 );

      ulong expectedKey = testBoard.BoardHash.Key;
      testBoard.Update( move5 );

      testBoard.Undo();

      ulong actualKey = testBoard.BoardHash.Key;
      Assert.Equal( expectedKey, actualKey );
    }
    
    [Fact]
    public void Undo_WhitePawnEnPassantLeft_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.C2 ) | BoardSquare.C4;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.H7 ) | BoardSquare.H6;
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      move3.Bits = ( move1.Bits ^ BoardSquare.C4 ) | BoardSquare.C5;
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      move4.Bits = ( move2.Bits ^ BoardSquare.B7 ) | BoardSquare.B5;
      PawnBitBoard move5 = new PawnBitBoard( ChessPieceColors.White );
      move5.Bits = ( move3.Bits ^ BoardSquare.C5 ) | BoardSquare.B6;


      testBoard.Update( move1 );
      testBoard.Update( move2 );
      testBoard.Update( move3 );
      testBoard.Update( move4 );

      ulong expectedKey = testBoard.BoardHash.Key;
      testBoard.Update( move5 );

      testBoard.Undo();

      ulong actualKey = testBoard.BoardHash.Key;
      Assert.Equal( expectedKey, actualKey );
    }

    [Fact]
    public void Undo_BlackPawnEnPassantRight_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.Black );
      move1.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.D7 ) | BoardSquare.D5;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.White );
      move2.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.H2 ) | BoardSquare.H3;
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.Black );
      move3.Bits = ( move1.Bits ^ BoardSquare.D5 ) | BoardSquare.D4;
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.White );
      move4.Bits = ( move2.Bits^ BoardSquare.C2 ) | BoardSquare.C4;
      PawnBitBoard move5 = new PawnBitBoard( ChessPieceColors.Black );
      move5.Bits = ( move3.Bits ^ BoardSquare.D4 ) | BoardSquare.C3;


      testBoard.Update( move1 );
      testBoard.Update( move2 );
      testBoard.Update( move3 );
      testBoard.Update( move4 );

      ulong expectedKey = testBoard.BoardHash.Key;
      testBoard.Update( move5 );

      testBoard.Undo();

      ulong actualKey = testBoard.BoardHash.Key;
      Assert.Equal( expectedKey, actualKey );
    }

    [Fact]
    public void Undo_BlackPawnEnPassantLeft_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();

      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.Black );
      move1.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.B7 ) | BoardSquare.B5;
      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.White );
      move2.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.H2 ) | BoardSquare.H3;
      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.Black );
      move3.Bits = ( move1.Bits ^ BoardSquare.B5 ) | BoardSquare.B4;
      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.White );
      move4.Bits = ( move2.Bits ^ BoardSquare.C2 ) | BoardSquare.C4;
      PawnBitBoard move5 = new PawnBitBoard( ChessPieceColors.Black );
      move5.Bits = ( move3.Bits ^ BoardSquare.B4 ) | BoardSquare.C3;


      testBoard.Update( move1 );
      testBoard.Update( move2 );
      testBoard.Update( move3 );
      testBoard.Update( move4 );

      ulong expectedKey = testBoard.BoardHash.Key;
      testBoard.Update( move5 );

      testBoard.Undo();

      ulong actualKey = testBoard.BoardHash.Key;
      Assert.Equal( expectedKey, actualKey );
    }

    [Fact]
    public void Undo_WhitePromotionQueen_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.G2 ) | BoardSquare.G4;

      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.G7 ) | BoardSquare.G5;

      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      move3.Bits = ( move1.Bits ^ BoardSquare.H2 ) | BoardSquare.H4;

      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      move4.Bits = ( move2.Bits ^ BoardSquare.G5 ) | BoardSquare.H4;

      PawnBitBoard move5 = new PawnBitBoard( ChessPieceColors.White );
      move5.Bits = ( move3.Bits ^ BoardSquare.G4 ^ BoardSquare.H4 ) | BoardSquare.G5;

      KnightBitBoard move6 = new KnightBitBoard( ChessPieceColors.Black );
      move6.Bits = ( testBoard.BlackKnight.Bits ^ BoardSquare.G8 ) | BoardSquare.F6;

      PawnBitBoard move7 = new PawnBitBoard( ChessPieceColors.White );
      move7.Bits = ( move5.Bits ^ BoardSquare.G5 ) | BoardSquare.G6;

      KnightBitBoard move8 = new KnightBitBoard( ChessPieceColors.Black );
      move8.Bits = ( move6.Bits ^ BoardSquare.F6 ) | BoardSquare.G8;

      PawnBitBoard move9 = new PawnBitBoard( ChessPieceColors.White );
      move9.Bits = ( move7.Bits ^ BoardSquare.G6 ) | BoardSquare.G7;

      KnightBitBoard move10 = new KnightBitBoard( ChessPieceColors.Black );
      move10.Bits = ( move8.Bits ^ BoardSquare.G8 ) | BoardSquare.F6;

      PawnBitBoard move11 = new PawnBitBoard( ChessPieceColors.White );
      move11.Bits = ( move9.Bits ^ BoardSquare.G7 ) | BoardSquare.G8;
      move11.Promote( PawnBitBoard.PromotionPiece.Queen );

      testBoard.Update( move1 );
      testBoard.Update( move2 );
      testBoard.Update( move3 );
      testBoard.Update( move4 );
      testBoard.Update( move5 );
      testBoard.Update( move6 );
      testBoard.Update( move7 );
      testBoard.Update( move8 );
      testBoard.Update( move9 );
      testBoard.Update( move10 );
      ulong expectedKey = testBoard.BoardHash.Key;
      testBoard.Update( move11 );
      testBoard.Undo();
      ulong testHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedKey, testHash );
    }

    [Fact]
    public void Undo_BlackPromotionQueen_Equal() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      PawnBitBoard move1 = new PawnBitBoard( ChessPieceColors.White );
      move1.Bits = ( testBoard.WhitePawn.Bits ^ BoardSquare.G2 ) | BoardSquare.G4;

      PawnBitBoard move2 = new PawnBitBoard( ChessPieceColors.Black );
      move2.Bits = ( testBoard.BlackPawn.Bits ^ BoardSquare.H7 ) | BoardSquare.H5;

      PawnBitBoard move3 = new PawnBitBoard( ChessPieceColors.White );
      move3.Bits = ( move1.Bits ^ BoardSquare.H2 ) | BoardSquare.H3;

      PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      move4.Bits = ( move2.Bits ^ BoardSquare.H5) | BoardSquare.G4;

      KnightBitBoard move5 = new KnightBitBoard( ChessPieceColors.White );
      move5.Bits = ( testBoard.WhiteKnight.Bits ^ BoardSquare.G1 ) | BoardSquare.H3;

      //PawnBitBoard move4 = new PawnBitBoard( ChessPieceColors.Black );
      //move4.Bits = ( move2.Bits ^ BoardSquare.G5 ) | BoardSquare.H4;

      
      PawnBitBoard move6 = new PawnBitBoard( ChessPieceColors.Black );
      move6.Bits = ( move4.Bits ^ BoardSquare.G4 ) | BoardSquare.G3;

      KnightBitBoard move7 = new KnightBitBoard( ChessPieceColors.White );
      move7.Bits = ( move5.Bits ^ BoardSquare.H3 ) | BoardSquare.G1;

      PawnBitBoard move8 = new PawnBitBoard( ChessPieceColors.Black );
      move8.Bits = ( move6.Bits ^ BoardSquare.G3 ) | BoardSquare.G2;

      KnightBitBoard move9 = new KnightBitBoard( ChessPieceColors.White );
      move9.Bits = ( move7.Bits ^ BoardSquare.G1 ) | BoardSquare.H3;

      PawnBitBoard move10 = new PawnBitBoard( ChessPieceColors.Black );
      move10.Bits = ( move8.Bits ^ BoardSquare.G2 ) | BoardSquare.G1;
      move10.Promote( PawnBitBoard.PromotionPiece.Queen );

      testBoard.Update( move1 );
      testBoard.Update( move2 );
      testBoard.Update( move3 );
      testBoard.Update( move4 );
      testBoard.Update( move5 );
      testBoard.Update( move6 );
      testBoard.Update( move7 );
      testBoard.Update( move8 );
      testBoard.Update( move9 );
      ulong expectedKey = testBoard.BoardHash.Key;
      testBoard.Update( move10 );
      testBoard.Undo();
      ulong testHash = testBoard.BoardHash.Key;

      Assert.Equal( expectedKey, testHash );
    }

#endregion

  }
}

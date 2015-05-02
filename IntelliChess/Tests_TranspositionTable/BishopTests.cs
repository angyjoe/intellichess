using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
/*
 * Author: Sari Haj Hussein
 */
namespace P5
{
  partial class Tests_TranspositionTable
  {
    [Fact]
    public void WhiteBishopMoved2NW_Undo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      PawnBitBoard movePawn = new PawnBitBoard(ChessPieceColors.White);
      movePawn.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.B2) | BoardSquare.B3;
      PawnBitBoard moveBlackPawn = new PawnBitBoard(ChessPieceColors.Black);
      moveBlackPawn.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.H7) | BoardSquare.H6;

      ulong expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(movePawn);
      testBoard.Undo();
      ulong actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(movePawn);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBlackPawn);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveBlackPawn);

      BishopBitBoard moveBishop = new BishopBitBoard(ChessPieceColors.White);
      moveBishop.Bits = (testBoard.WhiteBishop.Bits ^ BoardSquare.C1) | BoardSquare.A3;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBishop);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
    }
    [Fact]
    public void WhiteBishopMoved2NE_Undo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      PawnBitBoard movePawn = new PawnBitBoard(ChessPieceColors.White);
      movePawn.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.D2) | BoardSquare.D3;
      PawnBitBoard moveBlackPawn = new PawnBitBoard(ChessPieceColors.Black);
      moveBlackPawn.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.H7) | BoardSquare.H6;

      ulong expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(movePawn);
      testBoard.Undo();
      ulong actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(movePawn);

      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBlackPawn);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveBlackPawn);

      BishopBitBoard moveBishop = new BishopBitBoard(ChessPieceColors.White);
      moveBishop.Bits = (testBoard.WhiteBishop.Bits ^ BoardSquare.C1) | BoardSquare.E3;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBishop);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
    }
    [Fact]
    public void WhiteBishopMoved2SE_Undo_Test()
    {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      PawnBitBoard movePawn = new PawnBitBoard(ChessPieceColors.White);
      movePawn.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.B2) | BoardSquare.B3;
      PawnBitBoard moveBlackPawn1 = new PawnBitBoard(ChessPieceColors.Black);

      ulong expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(movePawn);
      testBoard.Undo();
      ulong actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(movePawn);

      moveBlackPawn1.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.H7) | BoardSquare.H6;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBlackPawn1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveBlackPawn1);

      BishopBitBoard moveBishop1 = new BishopBitBoard(ChessPieceColors.White);
      moveBishop1.Bits = (testBoard.WhiteBishop.Bits ^ BoardSquare.C1) | BoardSquare.A3;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBishop1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveBishop1);

      moveBlackPawn1.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.H6) | BoardSquare.H5;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBlackPawn1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveBlackPawn1);

      BishopBitBoard moveBishop2 = new BishopBitBoard(ChessPieceColors.White);
      moveBishop2.Bits = (moveBishop1.Bits ^ BoardSquare.A3) | BoardSquare.C1;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBishop2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);

    }
    [Fact]
    public void WhiteBishopMoved2SW_Undo_Test() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      PawnBitBoard movePawn = new PawnBitBoard(ChessPieceColors.White);
      movePawn.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.D2) | BoardSquare.D3;
      PawnBitBoard moveBlackPawn = new PawnBitBoard(ChessPieceColors.Black);

      ulong expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(movePawn);
      testBoard.Undo();
      ulong actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(movePawn);

      moveBlackPawn.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.H7) | BoardSquare.H6;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBlackPawn);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveBlackPawn);

      BishopBitBoard moveBishop1 = new BishopBitBoard(ChessPieceColors.White);
      moveBishop1.Bits = (testBoard.WhiteBishop.Bits ^ BoardSquare.C1) | BoardSquare.E3;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBishop1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveBishop1);

      moveBlackPawn.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.H6) | BoardSquare.H5;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBlackPawn);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveBlackPawn);

      BishopBitBoard moveBishop2 = new BishopBitBoard(ChessPieceColors.White);
      moveBishop2.Bits = (moveBishop1.Bits ^ BoardSquare.E3) | BoardSquare.C1;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBishop2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
    }
    [Fact]
    public void BlackBishopMoved2SW_Undo_Test() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      PawnBitBoard movePawn = new PawnBitBoard(ChessPieceColors.Black);
      PawnBitBoard moveWhitePawn = new PawnBitBoard(ChessPieceColors.White);

      movePawn.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.B7) | BoardSquare.B6;
      ulong expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(movePawn);
      testBoard.Undo();
      ulong actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(movePawn);

      moveWhitePawn.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.H2) | BoardSquare.H3;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveWhitePawn);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveWhitePawn);

      BishopBitBoard moveBishop = new BishopBitBoard(ChessPieceColors.Black);
      moveBishop.Bits = (testBoard.BlackBishop.Bits ^ BoardSquare.C8) | BoardSquare.A6;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBishop);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
    }
    [Fact]
    public void BlackBishopMoved2SE_Undo_Test() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      PawnBitBoard moveWhitePawn = new PawnBitBoard(ChessPieceColors.White);

      PawnBitBoard movePawn = new PawnBitBoard(ChessPieceColors.Black);
      movePawn.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.D7) | BoardSquare.D6;
      ulong expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(movePawn);
      testBoard.Undo();
      ulong actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(movePawn);

      moveWhitePawn.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.H2) | BoardSquare.H3;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveWhitePawn);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveWhitePawn);

      BishopBitBoard moveBishop = new BishopBitBoard(ChessPieceColors.Black);
      moveBishop.Bits = (testBoard.BlackBishop.Bits ^ BoardSquare.C8) | BoardSquare.E6;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBishop);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
    }
    [Fact]
    public void BlackBishopMoved2NE_Undo_Test() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      PawnBitBoard moveWhitePawn = new PawnBitBoard(ChessPieceColors.White);

      PawnBitBoard movePawn = new PawnBitBoard(ChessPieceColors.Black);
      movePawn.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.B7) | BoardSquare.B6;
      ulong expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(movePawn);
      testBoard.Undo();
      ulong actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(movePawn);

      moveWhitePawn.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.H2) | BoardSquare.H3;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveWhitePawn);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveWhitePawn);

      BishopBitBoard moveBishop1 = new BishopBitBoard(ChessPieceColors.Black);
      moveBishop1.Bits = (testBoard.BlackBishop.Bits ^ BoardSquare.C8) | BoardSquare.A6;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBishop1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveBishop1);

      moveWhitePawn.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.H3) | BoardSquare.H4;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveWhitePawn);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveWhitePawn);

      BishopBitBoard moveBishop2 = new BishopBitBoard(ChessPieceColors.Black);
      moveBishop2.Bits = (moveBishop1.Bits ^ BoardSquare.A6) | BoardSquare.C8;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBishop2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
    }
    [Fact]
    public void BlackBishopMoved2NW_Undo_Test() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      PawnBitBoard moveWhitePawn = new PawnBitBoard(ChessPieceColors.White);

      PawnBitBoard movePawn = new PawnBitBoard(ChessPieceColors.Black);
      movePawn.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.D7) | BoardSquare.D6;
      ulong expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(movePawn);
      testBoard.Undo();
      ulong actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(movePawn);

      moveWhitePawn.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.H2) | BoardSquare.H3;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveWhitePawn);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveWhitePawn);

      BishopBitBoard moveBishop1 = new BishopBitBoard(ChessPieceColors.Black);
      moveBishop1.Bits = (testBoard.BlackBishop.Bits ^ BoardSquare.C8) | BoardSquare.E6;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBishop1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveBishop1);

      moveWhitePawn.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.H3) | BoardSquare.H4;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveWhitePawn);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveWhitePawn);

      BishopBitBoard moveBishop2 = new BishopBitBoard(ChessPieceColors.Black);
      moveBishop2.Bits = (moveBishop1.Bits ^ BoardSquare.E6) | BoardSquare.C8;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBishop2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
    }
    [Fact]
    public void WhiteBishopCaptureUndo_Test() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      PawnBitBoard moveBlackPawn = new PawnBitBoard(ChessPieceColors.Black);

      PawnBitBoard movePawn = new PawnBitBoard(ChessPieceColors.White);
      movePawn.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.B2) | BoardSquare.B3;
      ulong expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(movePawn);
      testBoard.Undo();
      ulong actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(movePawn);

      moveBlackPawn.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.H7) | BoardSquare.H6;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBlackPawn);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveBlackPawn);

      BishopBitBoard moveBishop1 = new BishopBitBoard(ChessPieceColors.White);
      moveBishop1.Bits = (testBoard.WhiteBishop.Bits ^ BoardSquare.C1) | BoardSquare.A3;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBishop1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveBishop1);

      moveBlackPawn.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.H6) | BoardSquare.H5;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBlackPawn);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveBlackPawn);

      BishopBitBoard moveBishop2 = new BishopBitBoard(ChessPieceColors.White);
      moveBishop2.Bits = (moveBishop1.Bits ^ BoardSquare.A3) | BoardSquare.E7;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBishop2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
    }
    [Fact]
    public void BlackBishopCaptureUndo_Test() {
      ChessBoard testBoard = new ChessBoard();
      testBoard.InitializeGame();
      PawnBitBoard moveWhitePawn = new PawnBitBoard(ChessPieceColors.White);

      PawnBitBoard movePawn = new PawnBitBoard(ChessPieceColors.Black);
      movePawn.Bits = (testBoard.BlackPawn.Bits ^ BoardSquare.B7) | BoardSquare.B6;
      ulong expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(movePawn);
      testBoard.Undo();
      ulong actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(movePawn);

      moveWhitePawn.Bits = (testBoard.WhitePawn.Bits ^ BoardSquare.H2) | BoardSquare.H3;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveWhitePawn);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveWhitePawn);

      BishopBitBoard moveBishop1 = new BishopBitBoard(ChessPieceColors.Black);
      moveBishop1.Bits = (testBoard.BlackBishop.Bits ^ BoardSquare.C8) | BoardSquare.A6;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBishop1);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
      testBoard.Update(moveBishop1);

      BishopBitBoard moveBishop2 = new BishopBitBoard(ChessPieceColors.Black);
      moveBishop2.Bits = (moveBishop1.Bits ^ BoardSquare.A6) | BoardSquare.E2;
      expectedHash = testBoard.BoardHash.Key;
      testBoard.Update(moveBishop2);
      testBoard.Undo();
      actualHash = testBoard.BoardHash.Key;
      Assert.Equal(expectedHash, actualHash);
    }

  }
}

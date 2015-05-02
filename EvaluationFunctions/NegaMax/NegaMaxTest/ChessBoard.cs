using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Author: Sari Haj Hussein
 */
namespace BitBoard {
  public class ChessBoard {
    public KingBitBoard WhiteKing { set; get; }
    public QueenBitBoard WhiteQueen { set; get; }
    public RookBitBoard WhiteRook { set; get; }
    public BishopBitBoard WhiteBishop { set; get; }
    public KnightBitBoard WhiteKnight { set; get; }
    public PawnBitBoard WhitePawn { set; get; }
    public KingBitBoard BlackKing { set; get; }
    public QueenBitBoard BlackQueen { set; get; }
    public RookBitBoard BlackRook { set; get; }
    public BishopBitBoard BlackBishop { set; get; }
    public KnightBitBoard BlackKnight { set; get; }
    public PawnBitBoard BlackPawn { set; get; }


    public void InitializeGame() {
      InitializeScenario( new List<Tuple<BoardSquare, ChessPieceType, ChessPieceColors>> {
        new Tuple<BoardSquare,ChessPieceType,ChessPieceColors>(BoardSquare.A4, ChessPieceType.Knight, ChessPieceColors.Black ),
        new Tuple<BoardSquare,ChessPieceType,ChessPieceColors>(BoardSquare.A6, ChessPieceType.Knight, ChessPieceColors.Black ),
        new Tuple<BoardSquare,ChessPieceType,ChessPieceColors>(BoardSquare.B4, ChessPieceType.Bishop, ChessPieceColors.Black ),
      } );
    }
    
    public void InitializeScenario( List<Tuple<int, ChessPieceType, ChessPieceColors>> Pieces ) {
      throw new NotImplementedException();
    }

    public void InitializeScenario( List<Tuple<BoardSquare, ChessPieceType, ChessPieceColors>> Pieces ) {
      throw new NotImplementedException();
    }



    public class IllegalPiecePlacementException : Exception {
      public IllegalPiecePlacementException( string s ) : base( s ) { }
    }

    public enum ChessPieceType { King, Queen, Rook, Knight, Bishop, Pawn }

    public enum ChessPieceColors { Black, White }

    public enum BoardSquare {
      A1, B1, C1, D1, E1, F1, G1, H1,
      A2, B2, C2, D2, E2, F2, G2, H2,
      A3, B3, C3, D3, E3, F3, G3, H3,
      A4, B4, C4, D4, E4, F4, G4, H4,
      A5, B5, C5, D5, E5, F5, G5, H5,
      A6, B6, C6, D6, E6, F6, G6, H6,
      A7, B7, C7, D7, E7, F7, G7, H7,
      A8, B8, C8, D8, E8, F8, G8, H8, 
    }
  }
}

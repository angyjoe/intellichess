using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  class Program {
    private static StringBitboardConverter _winboardConverter;
    public ChessPieceColors winboardColor { get; private set; }

    static void Main( string[] args ) {
        FAILFAILtest();
       // List<BoardSquare> testList = new List<BoardSquare>
     //   {
      //       BoardSquare.C1, BoardSquare.B1, BoardSquare.A1, BoardSquare.B4, BoardSquare.B8, BoardSquare.A8
       // };
        //Eval.PositionIndexFromBoardSquare_debug(testList);
      //WinBoardCastlingFAIL( lolBoard, ChessPieceColors.Black );
      //Console.WriteLine( lolBoard.WhiteRook.positionValuesWhite[BoardSquare.A1] + lolBoard.WhiteRook.positionValuesWhite[BoardSquare.A4] );
      //EvalFail();
      //blackqueensidecastle();
      //sacrificeQueenDebugStuffSomethingTestMethod();
      //anotherIllegalMoveFromMovegen(); //virker åbenbart igen
      //f3d5failmove(); // virker åbenbart igen
      //KingSideCastlingFAIL();
      //anotherBlackQueenSideCastleFail();
    }

  /*  private static void WinBoardCastlingFAIL( ChessBoard chessBoard, ChessPieceColors winboardColor ) {
      _winboardConverter = new StringBitboardConverter( chessBoard, winboardColor );
      ColoredBitBoard bitBoardMoveRecived = _winboardConverter.ConvertStringMoveToBitBoard( "E8G8" );
      chessBoard.Update( bitBoardMoveRecived );
    }*/
    private static void FAILFAILtest()
    {
        ChessBoard lolboard = new ChessBoard();
        lolboard.InitializeGame();

        PawnBitBoard pawnWhite = new PawnBitBoard( ChessPieceColors.White );
        pawnWhite.Bits = BoardSquare.E4 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.H2 | BoardSquare.G2 | BoardSquare.F2 | BoardSquare.A2;
        lolboard.Update(pawnWhite);

        PawnBitBoard pawnBlack = new PawnBitBoard(ChessPieceColors.Black);
        pawnBlack.Bits = BoardSquare.A7 |BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E5 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
        lolboard.Update(pawnBlack);

        KnightBitBoard lilleKnejtWhite = new KnightBitBoard(ChessPieceColors.White);
        lilleKnejtWhite.Bits = BoardSquare.C3 | BoardSquare.G1;
        lolboard.Update(lilleKnejtWhite);

        KnightBitBoard lilleKnejtBlack = new KnightBitBoard(ChessPieceColors.Black);
        lilleKnejtBlack.Bits = BoardSquare.C6 | BoardSquare.G8;
        lolboard.Update(lilleKnejtBlack);

        KnightBitBoard lilleKnejtWhitenummer2 = new KnightBitBoard(ChessPieceColors.White);
        lilleKnejtWhitenummer2.Bits = BoardSquare.C3 | BoardSquare.F3;
        lolboard.Update(lilleKnejtWhitenummer2);

        PawnBitBoard pawnBlacknummerto = new PawnBitBoard(ChessPieceColors.Black);
        pawnBlacknummerto.Bits = BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D5 | BoardSquare.E5 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;
        lolboard.Update(pawnBlacknummerto);
        //INGEN FEJL!
        PawnBitBoard pawnWhitenummerto = new PawnBitBoard(ChessPieceColors.White);
        pawnWhitenummerto.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.D5 | BoardSquare.G2 | BoardSquare.F2 | BoardSquare.H2;
        lolboard.Update(pawnWhitenummerto);
        //Måske fejl?
        KnightBitBoard lilleKnejtBlacknummerto = new KnightBitBoard(ChessPieceColors.Black);
        lilleKnejtBlacknummerto.Bits = BoardSquare.B4 | BoardSquare.G8;
        lolboard.Update(lilleKnejtBlacknummerto);

        KnightBitBoard lilleKnejtWhitenummer3 = new KnightBitBoard(ChessPieceColors.White);
        lilleKnejtWhitenummer3.Bits = BoardSquare.C3 | BoardSquare.E5;
        lolboard.Update(lilleKnejtWhitenummer3);

        KnightBitBoard lilleKnejtBlacknummertre = new KnightBitBoard(ChessPieceColors.Black);
        lilleKnejtBlacknummertre.Bits = BoardSquare.B4 | BoardSquare.F6;
        lolboard.Update(lilleKnejtBlacknummertre);

        BishopBitBoard lillebishopruner = new BishopBitBoard(ChessPieceColors.White);
        lillebishopruner.Bits = BoardSquare.C1 | BoardSquare.B5;
        lolboard.Update(lillebishopruner);



        ColoredBitBoard bestMove = NegaMax.GetBestMove(lolboard, 2, ChessPieceColors.Black, MoveGen.GenerateLegalMoves, Eval.EvaluateState);

    }

    private static void KingSideCastlingFAIL() {
      ChessBoard lolBoard = new ChessBoard();
      lolBoard.InitializeGame();

      //White
      PawnBitBoard pawnWhite = new PawnBitBoard( ChessPieceColors.White );
      pawnWhite.Bits = BoardSquare.A4 | BoardSquare.B4 | BoardSquare.C2 | BoardSquare.F4 | BoardSquare.H5;

      BishopBitBoard bishopWhite = new BishopBitBoard( ChessPieceColors.White );
      bishopWhite.Bits = BoardSquare.E3;

      RookBitBoard rookWhite = new RookBitBoard( ChessPieceColors.White );
      rookWhite.Bits = BoardSquare.C1 | BoardSquare.H1;

      KnightBitBoard knightWhite = new KnightBitBoard( ChessPieceColors.White );
      knightWhite.Bits = BoardSquare.G1 ;

      QueenBitBoard queenWhite = new QueenBitBoard( ChessPieceColors.White );
      queenWhite.Bits = 0;

      KingBitBoard kingWhite = new KingBitBoard( ChessPieceColors.White );
      kingWhite.Bits = BoardSquare.F2;

      //black
      PawnBitBoard pawnBlack = new PawnBitBoard( ChessPieceColors.Black );
      pawnBlack.Bits = BoardSquare.A7 | BoardSquare.B7 | BoardSquare.E7 | BoardSquare.E4 | BoardSquare.G4 | BoardSquare.G5 | BoardSquare.H7;

      BishopBitBoard bishopBlack = new BishopBitBoard( ChessPieceColors.Black );
      bishopBlack.Bits = BoardSquare.C3 | BoardSquare.C4;

      RookBitBoard rookBlack = new RookBitBoard( ChessPieceColors.Black );
      rookBlack.Bits = BoardSquare.A8 | BoardSquare.H8;

      KnightBitBoard knightBlack = new KnightBitBoard( ChessPieceColors.Black );
      knightBlack.Bits = BoardSquare.D4;

      QueenBitBoard queenBlack = new QueenBitBoard( ChessPieceColors.Black );
      queenBlack.Bits = 0;

      KingBitBoard kingBlack = new KingBitBoard( ChessPieceColors.Black );
      kingBlack.Bits = BoardSquare.G8;

      //Update

      lolBoard.Update( pawnWhite );
      lolBoard.Update( knightWhite );
      lolBoard.Update( queenWhite );
      lolBoard.Update( rookWhite );
      lolBoard.Update( bishopWhite );
      lolBoard.Update( kingWhite );
      lolBoard.Update( pawnBlack );
      lolBoard.Update( bishopBlack );
      lolBoard.Update( queenBlack );
      lolBoard.Update( knightBlack );
      lolBoard.Update( kingBlack );

      List<ColoredBitBoard> legalMoves = new List<ColoredBitBoard>();
      legalMoves = MoveGen.GenerateLegalMoves( lolBoard, ChessPieceColors.White );
      ColoredBitBoard bestMove = NegaMax.GetBestMove( lolBoard, 3, ChessPieceColors.White, MoveGen.GenerateLegalMoves, Eval.EvaluateState );
    }

    private static void anotherBlackQueenSideCastleFail() {
      ChessBoard lolBoard = new ChessBoard();
      lolBoard.InitializeGame();

      //White
      PawnBitBoard pawnWhite = new PawnBitBoard( ChessPieceColors.White );
      pawnWhite.Bits = BoardSquare.A2 | BoardSquare.C3 | BoardSquare.E3 | BoardSquare.H3;

      BishopBitBoard bishopWhite = new BishopBitBoard( ChessPieceColors.White );
      bishopWhite.Bits = 0;

      RookBitBoard rookWhite = new RookBitBoard( ChessPieceColors.White );
      rookWhite.Bits = BoardSquare.A1;

      KnightBitBoard knightWhite = new KnightBitBoard( ChessPieceColors.White );
      knightWhite.Bits = BoardSquare.B1;

      QueenBitBoard queenWhite = new QueenBitBoard( ChessPieceColors.White );
      queenWhite.Bits = BoardSquare.E2;

      KingBitBoard kingWhite = new KingBitBoard( ChessPieceColors.White );
      kingWhite.Bits = BoardSquare.D2;

      //black
      PawnBitBoard pawnBlack = new PawnBitBoard( ChessPieceColors.Black );
      pawnBlack.Bits = BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C5 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H5 ;

      BishopBitBoard bishopBlack = new BishopBitBoard( ChessPieceColors.Black );
      bishopBlack.Bits = BoardSquare.E4 | BoardSquare.F8;

      RookBitBoard rookBlack = new RookBitBoard( ChessPieceColors.Black );
      rookBlack.Bits = BoardSquare.A8 | BoardSquare.H8;

      KnightBitBoard knightBlack = new KnightBitBoard( ChessPieceColors.Black );
      knightBlack.Bits = BoardSquare.F4;

      QueenBitBoard queenBlack = new QueenBitBoard( ChessPieceColors.Black );
      queenBlack.Bits = BoardSquare.H3;

      KingBitBoard kingBlack = new KingBitBoard( ChessPieceColors.Black );
      kingBlack.Bits = BoardSquare.C8;

      //Update

      lolBoard.Update( pawnWhite );
      lolBoard.Update( knightWhite );
      lolBoard.Update( queenWhite );
      lolBoard.Update( rookWhite );
      lolBoard.Update( bishopWhite );
      lolBoard.Update( kingWhite );
      lolBoard.Update( pawnBlack );
      lolBoard.Update( rookBlack );
      lolBoard.Update( bishopBlack );
      lolBoard.Update( queenBlack );
      lolBoard.Update( kingBlack );
      lolBoard.Update( knightBlack );
     
      ColoredBitBoard bestMove = NegaMax.GetBestMove( lolBoard, 3, ChessPieceColors.White, MoveGen.GenerateLegalMoves, Eval.EvaluateState );
    }

    private static void EvalFail() {
      ChessBoard lolBoard = new ChessBoard();
      lolBoard.InitializeGame();
      //White
      PawnBitBoard pawnWhite = new PawnBitBoard( ChessPieceColors.White );
      pawnWhite.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E2 | BoardSquare.F2 | BoardSquare.G2 | BoardSquare.H3;

      PawnBitBoard pawnBlack = new PawnBitBoard( ChessPieceColors.Black );
      pawnBlack.Bits = BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.D7 | BoardSquare.E7 | BoardSquare.F5 | BoardSquare.G7 | BoardSquare.H7;

      lolBoard.Update( pawnWhite );
      lolBoard.Update( pawnBlack );
      ColoredBitBoard bestMove = NegaMax.GetBestMove( lolBoard, 3, ChessPieceColors.White, MoveGen.GenerateLegalMoves, Eval.EvaluateState );
    }

    private static void f3d5failmove() {
      //Winboard winboard = new Winboard();
      ChessBoard lolBoard = new ChessBoard();
      lolBoard.InitializeGame();

      //White
      PawnBitBoard pawnWhite = new PawnBitBoard( ChessPieceColors.White );
      pawnWhite.Bits = BoardSquare.A3 | BoardSquare.B3 | BoardSquare.C3 | BoardSquare.D3 | BoardSquare.H4;

      BishopBitBoard bishopWhite = new BishopBitBoard( ChessPieceColors.White );
      bishopWhite.Bits = BoardSquare.C1 ;

      KingBitBoard kingWhite = new KingBitBoard( ChessPieceColors.White );
      kingWhite.Bits = BoardSquare.F2;

      RookBitBoard rookWhite = new RookBitBoard( ChessPieceColors.White );
      rookWhite.Bits = BoardSquare.A1 | BoardSquare.H1;

      KnightBitBoard knightWhite = new KnightBitBoard( ChessPieceColors.White );
      knightWhite.Bits = BoardSquare.B1 | BoardSquare.G1;

      QueenBitBoard queenWhite = new QueenBitBoard( ChessPieceColors.White );
      queenWhite.Bits = BoardSquare.F3;

      //Black
      PawnBitBoard pawnBlack = new PawnBitBoard( ChessPieceColors.Black );
      pawnBlack.Bits = BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C5 | BoardSquare.D7 | BoardSquare.G7 | BoardSquare.G4 | BoardSquare.H7;

      RookBitBoard rookBlack = new RookBitBoard( ChessPieceColors.Black );
      rookBlack.Bits = BoardSquare.A8 | BoardSquare.F8;

      BishopBitBoard bishopBlack = new BishopBitBoard( ChessPieceColors.Black );
      bishopBlack.Bits = BoardSquare.C8 | BoardSquare.D6;

      QueenBitBoard queenBlack = new QueenBitBoard( ChessPieceColors.Black );
      queenBlack.Bits = BoardSquare.G6;

      KingBitBoard kingBlack = new KingBitBoard( ChessPieceColors.Black );
      kingBlack.Bits = BoardSquare.G8;

      KnightBitBoard knightBlack = new KnightBitBoard( ChessPieceColors.Black );
      knightBlack.Bits = BoardSquare.D5;


      //RookBitBoard badMove = new RookBitBoard( ChessPieceColors.Black );
      //badMove.Bits = BoardSquare.B8;
      lolBoard.Update( pawnWhite );
      lolBoard.Update( knightWhite );
      lolBoard.Update( queenWhite );
      lolBoard.Update( rookWhite );
      lolBoard.Update( bishopWhite );
      lolBoard.Update( kingWhite );
      lolBoard.Update( pawnBlack );
      lolBoard.Update( rookBlack );
      lolBoard.Update( bishopBlack );
      lolBoard.Update( queenBlack );
      lolBoard.Update( kingBlack );
      lolBoard.Update( knightBlack );


      ColoredBitBoard bestMove = NegaMax.GetBestMove( lolBoard, 3, ChessPieceColors.White, MoveGen.GenerateLegalMoves, Eval.EvaluateState );
    }

    private static void anotherIllegalMoveFromMovegen() {
      //Winboard winboard = new Winboard();
      ChessBoard lolBoard = new ChessBoard();
      lolBoard.InitializeGame();

      //White
      PawnBitBoard pawnWhite = new PawnBitBoard( ChessPieceColors.White );
      pawnWhite.Bits = BoardSquare.A3 | BoardSquare.C3 | BoardSquare.C4 | BoardSquare.F4 | BoardSquare.G4 | BoardSquare.H3;

      BishopBitBoard bishopWhite = new BishopBitBoard( ChessPieceColors.White );
      bishopWhite.Bits = 0;

      KingBitBoard kingWhite = new KingBitBoard( ChessPieceColors.White );
      kingWhite.Bits = BoardSquare.C2;

      RookBitBoard rookWhite = new RookBitBoard( ChessPieceColors.White );
      rookWhite.Bits = BoardSquare.A1 | BoardSquare.E2;

      KnightBitBoard knightWhite = new KnightBitBoard( ChessPieceColors.White );
      knightWhite.Bits = BoardSquare.B1;

      QueenBitBoard queenWhite = new QueenBitBoard( ChessPieceColors.White );
      queenWhite.Bits = 0;

      //Black
      PawnBitBoard pawnBlack = new PawnBitBoard( ChessPieceColors.Black );
      pawnBlack.Bits = BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C6 | BoardSquare.E7 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

      RookBitBoard rookBlack = new RookBitBoard( ChessPieceColors.Black );
      rookBlack.Bits = BoardSquare.D8 | BoardSquare.H8;

      BishopBitBoard bishopBlack = new BishopBitBoard( ChessPieceColors.Black );
      bishopBlack.Bits = BoardSquare.F8;

      QueenBitBoard queenBlack = new QueenBitBoard( ChessPieceColors.Black );
      queenBlack.Bits = BoardSquare.D1;

      KingBitBoard kingBlack = new KingBitBoard( ChessPieceColors.Black );
      kingBlack.Bits = BoardSquare.C8;

      KnightBitBoard knightBlack = new KnightBitBoard( ChessPieceColors.Black );
      knightBlack.Bits = 0;


      //RookBitBoard badMove = new RookBitBoard( ChessPieceColors.Black );
      //badMove.Bits = BoardSquare.B8;
      lolBoard.Update( pawnWhite );
      lolBoard.Update( knightWhite );
      lolBoard.Update( queenWhite );
      lolBoard.Update( rookWhite );
      lolBoard.Update( bishopWhite );
      lolBoard.Update( kingWhite );
      lolBoard.Update( pawnBlack );
      lolBoard.Update( rookBlack );
      lolBoard.Update( bishopBlack );
      lolBoard.Update( queenBlack );
      lolBoard.Update( kingBlack );
      lolBoard.Update( knightBlack );


      ColoredBitBoard bestMove = NegaMax.GetBestMove( lolBoard, 3, ChessPieceColors.White, MoveGen.GenerateLegalMoves, Eval.EvaluateState );
    }

    private static void blackqueensidecastle() {
      //Winboard winboard = new Winboard();
      ChessBoard lolBoard = new ChessBoard();
      lolBoard.InitializeGame();

      //White
      PawnBitBoard pawnWhite = new PawnBitBoard( ChessPieceColors.White );
      pawnWhite.Bits = BoardSquare.A2 | BoardSquare.B2 | BoardSquare.C2 | BoardSquare.D2 | BoardSquare.E4 | BoardSquare.H3;

      BishopBitBoard bishopWhite = new BishopBitBoard( ChessPieceColors.White );
      bishopWhite.Bits = BoardSquare.C1;

      KingBitBoard kingWhite = new KingBitBoard( ChessPieceColors.White );
      kingWhite.Bits = BoardSquare.D3;

      RookBitBoard rookWhite = new RookBitBoard( ChessPieceColors.White );
      rookWhite.Bits = BoardSquare.A1 | BoardSquare.H1;

      KnightBitBoard knightWhite = new KnightBitBoard( ChessPieceColors.White );
      knightWhite.Bits = BoardSquare.D2;

      QueenBitBoard queenWhite = new QueenBitBoard( ChessPieceColors.White );
      queenWhite.Bits = BoardSquare.D1;

      //Black
      PawnBitBoard pawnBlack = new PawnBitBoard( ChessPieceColors.Black );
      pawnBlack.Bits = BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C7 | BoardSquare.C6 | BoardSquare.F7 | BoardSquare.G7 | BoardSquare.H7;

      RookBitBoard rookBlack = new RookBitBoard( ChessPieceColors.Black );
      rookBlack.Bits = BoardSquare.A8 | BoardSquare.H8;

      BishopBitBoard bishopBlack = new BishopBitBoard( ChessPieceColors.Black );
      bishopBlack.Bits = BoardSquare.F5 | BoardSquare.H4;

      QueenBitBoard queenBlack = new QueenBitBoard( ChessPieceColors.Black );
      queenBlack.Bits = BoardSquare.F2;

      KingBitBoard kingBlack = new KingBitBoard( ChessPieceColors.Black );
      kingBlack.Bits = BoardSquare.E8;

      KnightBitBoard knightBlack = new KnightBitBoard( ChessPieceColors.Black );
      knightBlack.Bits = BoardSquare.H6;


      //RookBitBoard badMove = new RookBitBoard( ChessPieceColors.Black );
      //badMove.Bits = BoardSquare.B8;
      lolBoard.Update( pawnWhite );
      lolBoard.Update( knightWhite );
      lolBoard.Update( queenWhite );
      lolBoard.Update( rookWhite );
      lolBoard.Update( bishopWhite );
      lolBoard.Update( kingWhite );
      lolBoard.Update( pawnBlack );
      lolBoard.Update( rookBlack );
      lolBoard.Update( bishopBlack );
      lolBoard.Update( queenBlack );
      lolBoard.Update( kingBlack );
      lolBoard.Update( knightBlack );
      KingBitBoard test = new KingBitBoard(ChessPieceColors.Black);
      test.Bits = BoardSquare.C8;
      lolBoard.Update( test );

      List<ColoredBitBoard> legalMoves = new List<ColoredBitBoard>();

      legalMoves = MoveGen.GenerateLegalMoves( lolBoard, ChessPieceColors.Black );
      ColoredBitBoard bestMove = NegaMax.GetBestMove( lolBoard, 3, ChessPieceColors.White, MoveGen.GenerateLegalMoves, Eval.EvaluateState );
      Console.Write( legalMoves.ToString() + bestMove.ToString() );

    }

    private static void sacrificeQueenDebugStuffSomethingTestMethod() {
      //Winboard winboard = new Winboard();
      ChessBoard lolBoard = new ChessBoard();
      lolBoard.InitializeGame();

      //White
      PawnBitBoard pawnWhite = new PawnBitBoard( ChessPieceColors.White );
      pawnWhite.Bits = BoardSquare.A4 | BoardSquare.C2 | BoardSquare.D3 | BoardSquare.E4 | BoardSquare.F2 | BoardSquare.G2;

      BishopBitBoard bishopWhite = new BishopBitBoard( ChessPieceColors.White );
      bishopWhite.Bits = BoardSquare.E2 | BoardSquare.H4;

      KingBitBoard kingWhite = new KingBitBoard( ChessPieceColors.White );
      kingWhite.Bits = BoardSquare.E1;

      RookBitBoard rookWhite = new RookBitBoard( ChessPieceColors.White );
      rookWhite.Bits = BoardSquare.B1 | BoardSquare.H1;

      KnightBitBoard knightWhite = new KnightBitBoard( ChessPieceColors.White );
      knightWhite.Bits = BoardSquare.D2;

      QueenBitBoard queenWhite = new QueenBitBoard( ChessPieceColors.White );
      queenWhite.Bits = BoardSquare.D1;

      //Black
      PawnBitBoard pawnBlack = new PawnBitBoard( ChessPieceColors.Black );
      pawnBlack.Bits = BoardSquare.A7 | BoardSquare.B7 | BoardSquare.C5 | BoardSquare.E5 | BoardSquare.H5;
      RookBitBoard rookBlack = new RookBitBoard( ChessPieceColors.Black );
      rookBlack.Bits = BoardSquare.A8 | BoardSquare.H7;

      BishopBitBoard bishopBlack = new BishopBitBoard( ChessPieceColors.Black );
      bishopBlack.Bits = BoardSquare.C8 | BoardSquare.F8;

      QueenBitBoard queenBlack = new QueenBitBoard( ChessPieceColors.Black );
      queenBlack.Bits = BoardSquare.B4;

      KingBitBoard kingBlack = new KingBitBoard( ChessPieceColors.Black );
      kingBlack.Bits = BoardSquare.F7;

      KnightBitBoard knightBlack = new KnightBitBoard( ChessPieceColors.Black );
      knightBlack.Bits = BoardSquare.B8;


      //RookBitBoard badMove = new RookBitBoard( ChessPieceColors.Black );
      //badMove.Bits = BoardSquare.B8;
      lolBoard.Update( pawnWhite );
      lolBoard.Update( knightWhite );
      lolBoard.Update( queenWhite );
      lolBoard.Update( rookWhite );
      lolBoard.Update( bishopWhite );
      lolBoard.Update( kingWhite );
      lolBoard.Update( pawnBlack );
      lolBoard.Update( rookBlack );
      lolBoard.Update( bishopBlack );
      lolBoard.Update( queenBlack );
      lolBoard.Update( kingBlack );
      lolBoard.Update( knightBlack );

      List<ColoredBitBoard> legalMoves = new List<ColoredBitBoard>();

      legalMoves = MoveGen.GenerateLegalMoves( lolBoard, ChessPieceColors.Black );
      ColoredBitBoard bestMove = NegaMax.GetBestMove( lolBoard, 3, ChessPieceColors.Black, MoveGen.GenerateLegalMoves, Eval.EvaluateState );
      Console.Write( legalMoves.ToString() + bestMove.ToString() );
    }
  }
}
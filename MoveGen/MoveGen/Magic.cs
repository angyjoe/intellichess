using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * Author: Sari Haj Hussein
 */
namespace P5 {
  public static class Magic {
    private static bool initialized = false;
    private static BoardSquare[] occupancyMaskRook = new BoardSquare[64];
    private static BoardSquare[] occupancyMaskBishop = new BoardSquare[64];
    private static BoardSquare[][] occupancyVariationsRook = new BoardSquare[64][];
    private static BoardSquare[][] occupancyVariationsBishop = new BoardSquare[64][];
    private static BoardSquare[][] magicMovesRook = new BoardSquare[64][];
    private static BoardSquare[][] magicMovesBishop = new BoardSquare[64][];

    #region pre-calculated values
    //64 minus number of bits in index
    private static int[] magicNumberShiftsRook = new int[] {
    52,53,53,53,53,53,53,52,53,54,54,54,54,54,54,53,
    53,54,54,54,54,54,54,53,53,54,54,54,54,54,54,53,
    53,54,54,54,54,54,54,53,53,54,54,54,54,54,54,53,
    53,54,54,54,54,54,54,53,52,53,53,53,53,53,53,52
    };

    private static int[] magicNumberShiftsBishop = new int[] {
    58,59,59,59,59,59,59,58,59,59,59,59,59,59,59,59,
    59,59,57,57,57,57,59,59,59,59,57,55,55,57,59,59,
    59,59,57,55,55,57,59,59,59,59,57,57,57,57,59,59,
    59,59,59,59,59,59,59,59,58,59,59,59,59,59,59,58
    };

    //Numbers borrowed from http://www.rivalchess.com/magic-bitboards/
    private static ulong[] magicNumberRook = {
        0xa180022080400230UL, 0x40100040022000UL,   0x80088020001002UL,   0x80080280841000UL,   0x4200042010460008UL, 0x4800a0003040080UL,  0x400110082041008UL,  0x8000a041000880UL,
        0x10138001a080c010UL, 0x804008200480UL,     0x10011012000c0UL,    0x22004128102200UL,   0x200081201200cUL,    0x202a001048460004UL, 0x81000100420004UL,   0x4000800380004500UL,
        0x208002904001UL,     0x90004040026008UL,   0x208808010002001UL,  0x2002020020704940UL, 0x8048010008110005UL, 0x6820808004002200UL, 0xa80040008023011UL,  0xb1460000811044UL,
        0x4204400080008ea0UL, 0xb002400180200184UL, 0x2020200080100380UL, 0x10080080100080UL,   0x2204080080800400UL, 0xa40080360080UL,     0x2040604002810b1UL,  0x8c218600004104UL,
        0x8180004000402000UL, 0x488c402000401001UL, 0x4018a00080801004UL, 0x1230002105001008UL, 0x8904800800800400UL, 0x42000c42003810UL,   0x8408110400b012UL,   0x18086182000401UL,
        0x2240088020c28000UL, 0x1001201040c004UL,   0xa02008010420020UL,  0x10003009010060UL,   0x4008008008014UL,    0x80020004008080UL,   0x282020001008080UL,  0x50000181204a0004UL,
        0x102042111804200UL,  0x40002010004001c0UL, 0x19220045508200UL,   0x20030010060a900UL,  0x8018028040080UL,    0x88240002008080UL,   0x10301802830400UL,   0x332a4081140200UL,
        0x8080010a601241UL,   0x1008010400021UL,    0x4082001007241UL,    0x211009001200509UL,  0x8015001002441801UL, 0x801000804000603UL,  0xc0900220024a401UL,  0x1000200608243UL
    };

    private static ulong[] magicNumberBishop = {
        0x2910054208004104UL, 0x2100630a7020180UL,  0x5822022042000000UL, 0x2ca804a100200020UL, 0x204042200000900UL,  0x2002121024000002UL, 0x80404104202000e8UL, 0x812a020205010840UL,
        0x8005181184080048UL, 0x1001c20208010101UL, 0x1001080204002100UL, 0x1810080489021800UL, 0x62040420010a00UL,   0x5028043004300020UL, 0xc0080a4402605002UL, 0x8a00a0104220200UL,
        0x940000410821212UL,  0x1808024a280210UL,   0x40c0422080a0598UL,  0x4228020082004050UL, 0x200800400e00100UL,  0x20b001230021040UL,  0x90a0201900c00UL,    0x4940120a0a0108UL,
        0x20208050a42180UL,   0x1004804b280200UL,   0x2048020024040010UL, 0x102c04004010200UL,  0x20408204c002010UL,  0x2411100020080c1UL,  0x102a008084042100UL, 0x941030000a09846UL,
        0x244100800400200UL,  0x4000901010080696UL, 0x280404180020UL,     0x800042008240100UL,  0x220008400088020UL,  0x4020182000904c9UL,  0x23010400020600UL,   0x41040020110302UL,
        0x412101004020818UL,  0x8022080a09404208UL, 0x1401210240484800UL, 0x22244208010080UL,   0x1105040104000210UL, 0x2040088800c40081UL, 0x8184810252000400UL, 0x4004610041002200UL,
        0x40201a444400810UL,  0x4611010802020008UL, 0x80000b0401040402UL, 0x20004821880a00UL,   0x8200002022440100UL, 0x9431801010068UL,    0x1040c20806108040UL, 0x804901403022a40UL,
        0x2400202602104000UL, 0x208520209440204UL,  0x40c000022013020UL,  0x2000104000420600UL, 0x400000260142410UL,  0x800633408100500UL,  0x2404080a1410UL,     0x138200122002900UL    
    };
    #endregion

    public static BoardSquare getMagicMoves(BoardSquare position, BoardSquare allPieces, bool isRook)
    {
      if (!initialized)
      {
        initialize();
        initialized = true;
      }
      int bitIndex = BitBoard.PositionIndexFromBoardSquare(position);
      BoardSquare mask, occupancy;
      int shifts, magicIndex;
      ulong magicNumber;

      if (isRook)
      {
        mask = occupancyMaskRook[bitIndex];
        magicNumber = magicNumberRook[bitIndex];
        shifts = magicNumberShiftsRook[bitIndex];
        occupancy = allPieces & mask;
        magicIndex = (int)(((ulong)occupancy * magicNumber) >> shifts);
        return magicMovesRook[bitIndex][magicIndex];
      }
      else
      {
        mask = occupancyMaskBishop[bitIndex];
        magicNumber = magicNumberBishop[bitIndex];
        shifts = magicNumberShiftsBishop[bitIndex];
        occupancy = allPieces & mask;
        magicIndex = (int)(((ulong)occupancy * magicNumber) >> shifts);
        return magicMovesBishop[bitIndex][magicIndex];
      }   
    }

    private static List<int> getSetBits(BoardSquare inputBoardSquare)
    {
      List<int> resultList = new List<int>();

      for (int i = 0; i < 64; i++)
      {
        ulong uShift = (ulong)0x1 << i;

        if (((ulong)inputBoardSquare & uShift) != 0)
        {
          resultList.Add(i);
        }
      }
      return resultList;
    }

    private static void initialize()
    {
      generateOccupancyMask();
      generateOccupancyVariations();
      generateMoveDatabase();
    }

    private static void generateOccupancyMask() {
      ulong mask;
      int i, bit;

      for ( bit = 0; bit < 64; bit++ ) {

        //Rooks
        mask = 0;
        for (i = bit + 8; i <= 55; i += 8)
        {
          mask |= (1UL << i);
        }
        for (i = bit - 8; i >= 8; i -= 8)
        {
          mask |= (1UL << i);
        }
        for (i = bit + 1; i % 8 != 7 && i % 8 != 0; i++)
        {
          mask |= (1UL << i);
        }
        for (i = bit - 1; i % 8 != 7 && i % 8 != 0 && i >= 0; i--)
        {
          mask |= (1UL << i);
        }
        occupancyMaskRook[bit] = (BoardSquare)mask;

        //Bishops
        mask = 0;
        for (i = bit + 9; i % 8 != 7 && i % 8 != 0 && i <= 55; i += 9)
        {
          mask |= (1UL << i);
        }
        for (i = bit - 9; i % 8 != 7 && i % 8 != 0 && i >= 8; i -= 9)
        {
          mask |= (1UL << i);
        }
        for (i = bit + 7; i % 8 != 7 && i % 8 != 0 && i <= 55; i += 7)
        {
          mask |= (1UL << i);
        }
        for (i = bit - 7; i % 8 != 7 && i % 8 != 0 && i >= 8; i -= 7)
        {
          mask |= (1UL << i);
        }
        occupancyMaskBishop[bit] = (BoardSquare)mask;
      }
    }

    private static void generateOccupancyVariations()
    {
      int i, j, bit, variationCount, maskBitCount, indexBitCount;
      List<int> setBitsInMask, setBitsInIndex;
      BoardSquare mask;

      for (bit = 0; bit < 64; bit++)
      {
        //Rooks
        mask = occupancyMaskRook[bit];
        setBitsInMask = getSetBits(mask);
        maskBitCount = setBitsInMask.Count();
        variationCount = (int)(1UL << maskBitCount);
        occupancyVariationsRook[bit] = new BoardSquare[variationCount];
        for (i = 0; i < variationCount; i++)
        {
          occupancyVariationsRook[bit][i] = 0;
          setBitsInIndex = getSetBits((BoardSquare)i);
          indexBitCount = setBitsInIndex.Count();
          for (j = 0; j < indexBitCount; j++)
          {
            occupancyVariationsRook[bit][i] |= (BoardSquare)(1UL << setBitsInMask[setBitsInIndex[j]]);
          }
        }

        //Bishops
        mask = occupancyMaskBishop[bit];
        setBitsInMask = getSetBits(mask);
        maskBitCount = setBitsInMask.Count();
        variationCount = (int)(1UL << maskBitCount);
        occupancyVariationsBishop[bit] = new BoardSquare[variationCount];
        for (i = 0; i < variationCount; i++)
        {
          occupancyVariationsBishop[bit][i] = 0;
          setBitsInIndex = getSetBits((BoardSquare)i);
          indexBitCount = setBitsInIndex.Count();
          for (j = 0; j < indexBitCount; j++)
          {
            occupancyVariationsBishop[bit][i] |= (BoardSquare)(1UL << setBitsInMask[setBitsInIndex[j]]);
          }
        }
      }
    }

    private static void generateMoveDatabase()
    {
      BoardSquare validMoves, mask;
      int i, j, bit, variationCount, maskBitCount, magicIndex;

      for (bit = 0; bit < 64; bit++)
      {
        //Rooks
        mask = occupancyMaskRook[bit];
        maskBitCount = getSetBits(mask).Count;
        variationCount = (int)(1UL << maskBitCount);
        magicMovesRook[bit] = new BoardSquare[variationCount];

        for (i = 0; i < variationCount; i++)
        {
          validMoves = 0;
          magicIndex = (int)(((ulong)occupancyVariationsRook[bit][i] * magicNumberRook[bit]) >> magicNumberShiftsRook[bit]);

          for (j = bit + 8; j <= 63; j += 8) { 
            validMoves |= (BoardSquare)(1UL << j); 
            if ((occupancyVariationsRook[bit][i] & (BoardSquare)(1UL << j)) != 0) 
              break; 
          }
          for (j = bit - 8; j >= 0; j -= 8) { 
            validMoves |= (BoardSquare)(1UL << j); 
            if ((occupancyVariationsRook[bit][i] & (BoardSquare)(1UL << j)) != 0) 
              break; 
          }
          for (j = bit + 1; j % 8 != 0; j++) { 
            validMoves |= (BoardSquare)(1UL << j); 
            if ((occupancyVariationsRook[bit][i] & (BoardSquare)(1UL << j)) != 0) 
              break; 
          }
          for (j = bit - 1; j % 8 != 7 && j >= 0; j--) { 
            validMoves |= (BoardSquare)(1UL << j); 
            if ((occupancyVariationsRook[bit][i] & (BoardSquare)(1UL << j)) != 0) 
              break; 
          }
          magicMovesRook[bit][magicIndex] = validMoves;
        }

        //Bishops
        mask = occupancyMaskBishop[bit];
        maskBitCount = getSetBits(mask).Count;
        variationCount = (int)(1UL << maskBitCount);
        magicMovesBishop[bit] = new BoardSquare[variationCount];

        for (i = 0; i < variationCount; i++)
        {
          validMoves = 0;
          magicIndex = (int)(((ulong)occupancyVariationsBishop[bit][i] * magicNumberBishop[bit]) >> magicNumberShiftsBishop[bit]);

          for (j = bit + 9; j % 8 != 0 && j <= 63; j += 9) { 
            validMoves |= (BoardSquare)(1UL << j); 
            if ((occupancyVariationsBishop[bit][i] & (BoardSquare)(1UL << j)) != 0) 
              break; 
          }
          for (j = bit - 9; j % 8 != 7 && j >= 0; j -= 9) { 
            validMoves |= (BoardSquare)(1UL << j); 
            if ((occupancyVariationsBishop[bit][i] & (BoardSquare)(1UL << j)) != 0) 
              break; 
          }
          for (j = bit + 7; j % 8 != 7 && j <= 63; j += 7) {
            validMoves |= (BoardSquare)(1UL << j);
            if ((occupancyVariationsBishop[bit][i] & (BoardSquare)(1UL << j)) != 0)
              break;
          }
          for (j = bit - 7; j % 8 != 0 && j >= 0; j -= 7) {
            validMoves |= (BoardSquare)(1UL << j);
            if ((occupancyVariationsBishop[bit][i] & (BoardSquare)(1UL << j)) != 0)
              break;
          }
          magicMovesBishop[bit][magicIndex] = validMoves;
        }
      }
    }
  }
}

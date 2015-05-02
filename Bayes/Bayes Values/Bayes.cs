using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Security.Cryptography;
/*
 * Author: Sari Haj Hussein
 */
namespace P5
{
    public class Bayes
    {
        //BayesConfig
        private static bool _tuneEvalValues = false;
        private static int _tuningBounderies = 0;
        private static int _seed = 0;
        private static int _repetetionPerEvalConfiguration = 0;

        public static bool TuneEvalValues { get { return _tuneEvalValues; } set { _tuneEvalValues = value; } }
        public static int TuningBounderies { get { return _tuningBounderies; } set { _tuningBounderies = value; } }
        public static int Seed { get { return _seed; } set { _seed = value; } }
        public static int RepetetionPerEvalConfiguration { get { return _repetetionPerEvalConfiguration; } set { _repetetionPerEvalConfiguration = value; } }

        public static string CreateOutputString(ChessBoard board)
        {

            #region ResultString
            string result;
            result = Eval.BasePawnValue.ToString() + " " + Eval.BaseBishopValue.ToString() + " " + Eval.BaseKnightValue.ToString() + " " + Eval.BaseRookValue + " " +
                     Eval.BaseQueenValue.ToString() + " " + Eval.BaseKingValue.ToString() + " " + Eval.CheckMateConstant.ToString() + " " + Eval.LateGameBonusRooks.ToString() + " " +
                     Eval.LateGameBonusKnights.ToString() + " " + Eval.LateGameBonusBishop.ToString() + " " + Eval.FactorRook.ToString() + " " + Eval.FactorBishop.ToString() + " " +
                     Eval.FactorKnight.ToString() + " ";


            foreach (int i in board.WhitePawn.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteKnight.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteBishop.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteRook.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteQueen.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.WhiteKing.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackPawn.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackKnight.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackBishop.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackRook.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackQueen.PositionValues)
            {
                result += i + " ";
            }

            foreach (int i in board.BlackKing.PositionValues)
            {
                result += i + " ";
            }
            #endregion
            return result;
        }

        public static void ModifyValues(string ToBeModified, ChessBoard board)
        {
            bool hasString = false;
            string stringToModified = ToBeModified;
            char[] delimiter = { ' ' };
            string[] resultOfSplit;
            if (stringToModified != null)
            {
                hasString = true;
            }
            if (hasString)
            {
                
                resultOfSplit = stringToModified.Split(delimiter);
                Eval.BasePawnValue = Convert.ToDouble(resultOfSplit[0]);
                Eval.BaseBishopValue = Convert.ToDouble(resultOfSplit[1]);
                Eval.BaseKnightValue = Convert.ToDouble(resultOfSplit[2]);
                Eval.BaseRookValue = Convert.ToDouble(resultOfSplit[3]);
                Eval.BaseQueenValue = Convert.ToDouble(resultOfSplit[4]);
                Eval.BaseKingValue = Convert.ToDouble(resultOfSplit[5]);
                Eval.CheckMateConstant = Convert.ToDouble(resultOfSplit[6]);
                Eval.LateGameBonusRooks = Convert.ToDouble(resultOfSplit[7]);
                Eval.LateGameBonusKnights = Convert.ToDouble(resultOfSplit[8]);
                Eval.LateGameBonusBishop = Convert.ToDouble(resultOfSplit[9]);
                Eval.FactorRook = Convert.ToDouble(resultOfSplit[10]);
                Eval.FactorBishop = Convert.ToDouble(resultOfSplit[11]);
                Eval.FactorKnight = Convert.ToDouble(resultOfSplit[12]);
                for (int i = 13; i < resultOfSplit.Length; i++)
                {
                    if (i <= 76)
                    {
                        board.WhitePawn.PositionValues[i - 13] = Convert.ToDouble(resultOfSplit[i]);
                    }
                    if (i >= 77 && i <= 140)
                    {
                        board.WhiteKnight.PositionValues[i - 77] = Convert.ToDouble( resultOfSplit[i] );
                    }
                    if (i >= 141 && i <= 204)
                    {
                        board.WhiteBishop.PositionValues[i - 141] = Convert.ToDouble( resultOfSplit[i] );
                    }
                    if (i >= 205 && i <= 268)
                    {
                        board.WhiteRook.PositionValues[i - 205] = Convert.ToDouble( resultOfSplit[i] );
                    }
                    if (i >= 269 && i <= 332)
                    {
                        board.WhiteQueen.PositionValues[i - 269] = Convert.ToDouble( resultOfSplit[i] );
                    }
                    if (i >= 333 && i <= 396)
                    {
                        board.WhiteKing.PositionValues[i - 333] = Convert.ToDouble( resultOfSplit[i] );
                    }
                    if (i >= 397 && i <= 460)
                    {
                        board.BlackPawn.PositionValues[i - 397] = Convert.ToDouble( resultOfSplit[i] );
                    }
                    if (i >= 461 && i <= 524)
                    {
                        board.BlackKnight.PositionValues[i - 461] = Convert.ToDouble( resultOfSplit[i] );
                    }
                    if (i >= 525 && i <= 588)
                    {
                        board.BlackBishop.PositionValues[i - 525] = Convert.ToDouble( resultOfSplit[i] );
                    }
                    if (i >= 589 && i <= 652)
                    {
                        board.BlackRook.PositionValues[i - 589] = Convert.ToDouble( resultOfSplit[i] );
                    }
                    if (i >= 653 && i <= 716)
                    {
                        board.BlackQueen.PositionValues[i - 653] = Convert.ToDouble( resultOfSplit[i] );
                    }
                    if (i >= 717 && i <= 780)
                    {
                        board.BlackKing.PositionValues[i - 717] = Convert.ToDouble( resultOfSplit[i] );
                    }
                }
            }
        }

        public static string[] ReadLinesFromTxtFile(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            string[] readText = File.ReadAllLines(path);
            return readText;
        }

        public static void WriteLineToTxtFile(string path, string textString)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            using (StreamWriter s = File.AppendText(path))
            {
                s.WriteLine(textString);
            }
        }

        public static string CreateEvalConfigurationStringLogFormat(bool won, string EvalConfiguration)
        {

            string result = CreateHashFromEvalConfigurationString(EvalConfiguration) + " " + (won).ToString() + " " + EvalConfiguration;

            return result;
        }

        public static void HandleEvalConfig(out int depth, string path)
        {
            string[] config = File.ReadAllLines(path);

            Bayes.TuneEvalValues = Convert.ToBoolean(config[0]);
            Bayes.TuningBounderies = Convert.ToInt32( config[1] );
            Bayes.Seed = Convert.ToInt32( config[2] );
            Bayes.RepetetionPerEvalConfiguration = Convert.ToInt32( config[3] );
            depth = Convert.ToInt32(config[4]);
        }

        public static void WriteToEvalConfig(string path, int depth)
        {
            string[] config = new string[5];

            config[0] = Bayes.TuneEvalValues.ToString();
            config[1] = Bayes.TuningBounderies.ToString();
            config[2] = Bayes.Seed.ToString();
            config[3] = Bayes.RepetetionPerEvalConfiguration.ToString();
            config[4] = depth.ToString();

            File.WriteAllLines(path, config);
        }

        public static string CreateHashFromEvalConfigurationString(string EvalConfiguration)
        {
            HashAlgorithm algorithm = SHA256.Create();
            byte[] resultHash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(EvalConfiguration));
            string resultHashString = BitConverter.ToString( resultHash ).Replace( "-", string.Empty );

            return resultHashString;
        }

        public static double RandomizeValue( int modifyProcentage, int seed)
        {
            Random modifier = new Random(seed);

            double result = ((double)(modifier.Next(-modifyProcentage, modifyProcentage)) / 100);

            return result;
        }

        public static string RandomizeEvalConfiguration( string configuration)
        {

          Random randomGenerator = new Random( Bayes.Seed );

          string[] ToBeModifiedConfigurationString = configuration.Split( ' ' );
          string modifiedString = null;

          foreach ( string valueString in ToBeModifiedConfigurationString ) {
            if ( valueString != string.Empty ) {
              double value = Convert.ToDouble( valueString );
              value = value * ( 1 - ( (double)( randomGenerator.Next( -Bayes.TuningBounderies, Bayes.TuningBounderies ) ) / 100 ) );
              modifiedString += value + " ";
            }
          }

          return modifiedString;
        }

        public static string FindBestEvalConfiguration(string path)
        {
            string[] evalStrings = File.ReadAllLines(path);
            char[] delimiter = { ' ' };
            List<Tuple<string, bool, string>> hashBoolConfigurations = new List<Tuple<string, bool, string>>();
            List<Tuple<string, bool>> hashBools = new List<Tuple<string, bool>>();
            List<string> hashes = new List<string>();
            double winCount = 0;
            double nonWinCount = 0;
            double pWin = 0;
            List<Tuple<string, double>> pConfigurationGivenWin = new List<Tuple<string, double>>();
            List<Tuple<string, double>> pConfiguration = new List<Tuple<string, double>>();
            List<Tuple<string, double>> pWinGivenConfiguration = new List<Tuple<string, double>>();
            List<Tuple<string, double>> pWinGivenConfigurationNormalized = new List<Tuple<string, double>>();
            double normalizationConstant = 0;

            foreach (string configuration in evalStrings)
            {
                string[] HashBoolConfigurationString = configuration.Split(delimiter, 3);
                hashBoolConfigurations.Add(new Tuple<string, bool, string>(HashBoolConfigurationString[0], Convert.ToBoolean(HashBoolConfigurationString[1]), HashBoolConfigurationString[2]));
                hashBools.Add(new Tuple<string, bool>(HashBoolConfigurationString[0], Convert.ToBoolean(HashBoolConfigurationString[1])));
                hashes.Add(HashBoolConfigurationString[0]);
            }

            List<string> uniqueHashes = hashes.Distinct().ToList();
            double configurationCountWin = 0;
            double configurationCountNonWin = 0;
            
            foreach (string hash in uniqueHashes)
            {
                List<Tuple<string, bool>> subHashBools = hashBools.FindAll(p => p.Item1 == hash).ToList();
                foreach (Tuple<string, bool> gameResult in subHashBools)
                {
                    if (gameResult.Item2)
                    {
                        winCount++;
                        configurationCountWin++;
                        
                    }
                    else
                    {
                        nonWinCount++;
                        configurationCountNonWin++;
                        
                    }

                }

                    pConfigurationGivenWin.Add(new Tuple<string, double>(hash, (configurationCountWin / (configurationCountWin + configurationCountNonWin))));
                    pConfiguration.Add(new Tuple<string, double>(hash, (configurationCountWin + configurationCountNonWin)));

              
                configurationCountNonWin = 0;
                configurationCountWin = 0;
            }

            pWin = winCount / ( winCount + nonWinCount);

            List<Tuple<string, double>> tmpList = new List<Tuple<string, double>>();
            foreach (Tuple<string, double> configurationEntry in pConfiguration)
            {
                tmpList.Add(new Tuple<string, double>(configurationEntry.Item1, configurationEntry.Item2 / (winCount + nonWinCount)));
            }
            pConfiguration = tmpList;

            foreach (Tuple<string, double> configuration in pConfigurationGivenWin)
            {
                List<Tuple<string, double>> oneListedConfiguration = pConfiguration.Where(p => p.Item1 == configuration.Item1).ToList();
                Debug.Assert(oneListedConfiguration.Count == 1);
                double probabilityConfigurationGivenWin = configuration.Item2;
                double probabilityConfiguration = oneListedConfiguration[0].Item2;

                double probabilityWinGivenConfiguration = (pWin * probabilityConfigurationGivenWin) / probabilityConfiguration;
                pWinGivenConfiguration.Add(new Tuple<string, double>(configuration.Item1, probabilityWinGivenConfiguration));

                normalizationConstant += probabilityWinGivenConfiguration;
            }
            
            normalizationConstant = 1 / normalizationConstant;

            foreach (Tuple<string, double> configuration in pWinGivenConfiguration)
            {
                pWinGivenConfigurationNormalized.Add(new Tuple<string, double>(configuration.Item1, configuration.Item2 * normalizationConstant));
            }
            
            Tuple<string, double> bestConfiguration = pWinGivenConfigurationNormalized.Find(p => p.Item2 == pWinGivenConfigurationNormalized.Max(a => a.Item2));


            return hashBoolConfigurations.Find(p => p.Item1 == bestConfiguration.Item1).Item3.ToString();
        }
    }
}

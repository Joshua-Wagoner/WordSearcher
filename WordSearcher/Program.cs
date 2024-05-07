namespace WordSearcher
{

    /** *
     * DLM: 05/06/2024 
     * Program Name: WordSearcher
     * Author: Joshua M. Wagoner
     * Copyright @ 2024 All Rights Reserved
     * 
     * Description:
     * This program will take a designated word search, a list of words,
     * and search the word search until all words are found.
     * 
     */

    /*
     * TODO:
     *  Finished: Yes
     *  Clean and Simplify
     *  
     *  Finsihed: Yes
     *  Simplfy the Directional Criterias
     *  
     *  Finished: Yes
     *  Finish writing the boundary method.
     *  
     *  Finished: Yes
     *  Finishing writing the Find Row method & Find Column method.
     *  Also finish all the helpers.
     * 
     *  Finished: Yes
     *  Write method checkers.
     *  
     *  Finished: Yes
     *  Finish implementing the new method checkers into the directional searches.
     *  
     *  Finished: Yes
     *  Finish implementing the final searching methods, and the public directional search
     *  that will use the new Protected Search Method.
     *  
     *  Finished: No
     *  Finish the algorithm.
     *  
     *  Finished: No
     *  Test the algorithm.
     *  
     *  Finished: No
     *  Finalize everything.
     */

    internal class Program
    {

        //Directions
        public enum Directions
        {
            NORTH = 1,
            NORTH_EAST = 2,
            EAST = 3,
            SOUTH_EAST = 4,
            SOUTH = 5,
            SOUTH_WEST = 6,
            WEST = 7,
            NORTH_WEST = 8,
            NONE = 9
        }

        //Constants

        //Debugging
        public readonly static string EOL_MESSAGE = "EOL";
        public readonly static string NEW_LINE = "\n";
        public readonly static string COLON = ":";

        public readonly static char ERROR = '!';
        public readonly static char NOT_EXIST = ' ';
        public readonly static char[] ERROR_CHAR_ARRAY = ['!'];

        public readonly static Directions[] DIRECTIONS =
        [
            Directions.NORTH,
            Directions.NORTH_EAST,
            Directions.EAST,
            Directions.SOUTH_EAST,
            Directions.SOUTH,
            Directions.SOUTH_WEST,
            Directions.WEST,
            Directions.NORTH_WEST
        ];

        public readonly static Directions[] CORNERS =
        [
            DIRECTIONS[7],
            DIRECTIONS[1],
            DIRECTIONS[5],
            DIRECTIONS[3]
        ];

        public readonly static Directions[] DIRECTIONALS =
        [
            DIRECTIONS[0],
            DIRECTIONS[2],
            DIRECTIONS[4],
            DIRECTIONS[6]
        ];

        public readonly static int NUM_DIRECTIONS = 8;
        public readonly static int ROWS = 20;
        public readonly static int COLUMNS = 22;
        public readonly static int WORD_CHARACTER_LENGTH = 12;
        public readonly static int WORD_CHARACTER_ROWS = 7;
        public readonly static int WORDS_A_ROW = 5;
        public readonly static int OVERALL_ROW_LENGTH = 
            WORDS_A_ROW * WORD_CHARACTER_LENGTH;

        public readonly static int ZERO = 0;
        public readonly static int ONE = 1;


        //Directional Criteria
        private static readonly int directionalRows = NUM_DIRECTIONS / 2;

        private static readonly int cornerCols = 3;

        public readonly static Directions[,] CORNERS_CRITERIA =
        { 
            //Northwest Criteria
            {Directions.EAST, Directions.SOUTH_EAST, Directions.SOUTH},

            //Northeast Criteria
            {Directions.SOUTH, Directions.SOUTH_WEST, Directions.WEST},

            //Southwest Criteria
            {Directions.NORTH, Directions.NORTH_EAST, Directions.EAST},

            //Southeast Criteria
            {Directions.NORTH, Directions.WEST, Directions.NORTH_WEST}
        };

        private static readonly int directionCols = 5;

        public readonly static Directions[,] DIRECTIONS_CRITERIA =
        {
            //North Criteria
            {Directions.EAST, Directions.SOUTH_EAST, Directions.SOUTH, 
                Directions.SOUTH_WEST, Directions.WEST},

            //East Criteria
            {Directions.NORTH, Directions.SOUTH, Directions.SOUTH_WEST, 
                Directions.WEST, Directions.NORTH_WEST},

            //South Criteria
            {Directions.NORTH, Directions.NORTH_EAST, Directions.EAST,
                Directions.WEST, Directions.NORTH_WEST},

            //West Criteria
            {Directions.NORTH, Directions.NORTH_EAST, Directions.EAST,
                Directions.SOUTH_EAST, Directions.SOUTH},
        };

        public readonly static double C_RATIO = 0.045454545454545;
        public readonly static double R_RATIO = 0.05;

        //Variables
        private readonly static string[] words =
            [
            "blizzard    ", "december    ", "february    ", "fireplace   ",
            "flannel     ", "flurries    ", "frigid      ", "frostbite   ",
            "frozen      ", "gloves      ", "hockey      ", "holidays    ",
            "hotchocolate", "icicle      ", "igloo       ", "jacket      ",
            "january     ", "longjohns   ", "mitts       ", "scarf       ",
            "shovel      ", "skating     ", "skiing      ", "sleigh      ",
            "slippery    ", "snowballs   ", "snowboarding", "snowflakes  ",
            "snowman     ", "showshoes   ", "solstice    ", "sweater     ",
            "toboggan    ", "whiteout    ", "wintertime  "
        ];
        private readonly static string wordSearch =
            "koaxmgupnqdshovelcwfit" +
            "zbniretaewsojsyadilohk" +
            "ajtqhvimogtlbrnpxugefc" +
            "ntfsolsticegeiahrbdlrx" +
            "sdezcubnhlypvmfrtaqwop" +
            "libukhfoauprtdilsnegzo" +
            "wgrdelcicimxaeptohtseb" +
            "hinfyokglovesumcrdipnl" +
            "srxilbwsphufntrkaebjqy" +
            "cfpadecemberozhbngtasn" +
            "ahtogrqhftliwvdaeksnpa" +
            "reklwhapunotsbgjcfouim" +
            "fcvbekjodxnqhglyswratw" +
            "masgunembkgzopwibqfrlo" +
            "ulokftnhswjbecsazpvygn" +
            "epdticgaeromsflbkzhcas" +
            "qejhbidulthnylgnitaksm" +
            "arwoepnbzfncsxjohmurvg" +
            "biclrmhgqpsekalfwonsdu" +
            "yfsnvtekcajhdqziplxmbr";

        private readonly static char[] chars = wordSearch.ToCharArray();
        private readonly static int characters = ROWS * COLUMNS;
        private readonly static int rows = ROWS;
        private readonly static int columns = COLUMNS;

        //Debugging Methods
        public static void Print(string s)
        => Console.Write(s);

        public static void PrintArray(char[] array)
        {
            foreach (int i in array)
                if (i != array[^ONE])
                    Print(i + COLON);
                else
                    Print(i + NEW_LINE);
        }

        public static void PrintArray(int[] array)
        {
            foreach (int i in array)
                if (i != array[^ONE])
                    Print(i + COLON);
                else
                    Print(i + NEW_LINE);
        }

        //Find Helpers

        /// <summary>
        /// This method is a helper method for the Finder methods
        /// It takes the index and multiplies it by the column ratio.
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>The Column Ratio</returns>
        private static double ColumnRatio(double index)
        => index * C_RATIO;

        /// <summary>
        /// Does the same thing as ColumnRatio, but used the Math Operation Floor.
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>Floored Column Ratio</returns>
        private static double FlooredColumnRatio(double index)
        => Math.Floor(ColumnRatio(index));

        /// <summary>
        /// This method is a helper method for the Finder methods.
        /// It takes the index and multiplies it by the column ratio.
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>The Row Ratio</returns>
        private static double RowRatio(double index)
        => index * R_RATIO;

        /// <summary>
        /// This method is a helper method for the Finder methods.
        /// It takes the index and divides it by the rows to get the row min.
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>The Row Min</returns>
        private static double RowMin(double index)
        => index / rows;

        private static double RowMax(double index)
        => RowMin(index) + ONE;
        
        /// <summary>
        /// This method is a helper method for the Finder methods.
        /// It takes the index and divides it by the columns to get the column min.
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>The Column Min</returns>
        private static double ColumnMin(double index)
        => index / columns;

        private static double ColumnMax(double index)
        => ColumnMin(index) + ONE;

        //Finder
        /// <summary>
        /// Finds the index of a character by using the row and column.
        /// </summary>
        /// <param name="row">The row of the character. (Integer)</param>
        /// <param name="column">The column of the character. (Integer)</param>
        /// <returns>Index</returns>
        private static int FindIndex(int row, int column)
        => (row * columns) - (columns - column);

        /// <summary>
        /// Finds the row just by using the index.
        /// It uses a complex assortments of operations. (Refer to Help Sheet)
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The row</returns>
        private static int FindRow(double index)
        => (int)Math.Round
            (
                (
                    (ColumnMax(index) / ONE) 
                    - ColumnRatio(index)
                ) 
                + FlooredColumnRatio(index)
            );

        /// <summary>
        /// Find the column just by using the index.
        /// It uses a complex assortments of operations. (Refer to Help Sheet)
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The column</returns>
        private static int FindColumn(double index)
        => (int)Math.Round
            (
                (
                    index -
                    (
                        (
                            (
                                (RowMax(index) / ONE)
                                - RowRatio(index)
                            ) 
                            - ONE
                        ) 
                        * columns
                    )
                ) 
                - (FlooredColumnRatio(index) * columns)
            );

        //Direction Searches
        /// <summary>
        /// Searches for a character in the North direction.
        /// </summary>
        /// <param name="index">The Index of the character.(Integer)</param>
        /// <returns>Character in the north direction.</returns>
        private static char SearchNorth(int index)
        => chars[index - columns];

        /// <summary>
        /// Checks to see if the method exists before continuing to Search North.
        /// If it doesn't it will return the ERROR character '!';
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>Character at Index</returns>
        private static char North(int index)
        => CharacterExists(index - columns)
            ? SearchNorth(index) : NOT_EXIST;

        /// <summary>
        /// Searches for a character in the East direction.
        /// </summary>
        /// <param name="index">The Index of the character.(Integer)</param>
        /// <returns>Character in the East direction.</returns>
        private static char SearchEast(int index)
        => chars[index + ONE];

        /// <summary>
        /// Checks to see if the method exists before continuing to Search East.
        /// If it doesn't it will return the ERROR character '!';
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>Character at Index</returns>
        private static char East(int index)
        => CharacterExists(index + ONE)
            ? SearchEast(index) : NOT_EXIST;

        /// <summary>
        /// Searches for a character in the South direction.
        /// </summary>
        /// <param name="index">The Index of the character.(Integer)</param>
        /// <returns>Character in the South direction.</returns>
        private static char SearchSouth(int index)
        => chars[index + columns];

        /// <summary>
        /// Checks to see if the method exists before continuing to Search North.
        /// If it doesn't it will return the ERROR character '!';
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>Character at Index</returns>
        private static char South(int index)
        => CharacterExists(index + columns)
            ? SearchSouth(index) : NOT_EXIST;

        /// <summary>
        /// Searches for a character in the West direction.
        /// </summary>
        /// <param name="index">The Index of the character.(Integer)</param>
        /// <returns>Character in the West direction.</returns>
        private static char SearchWest(int index)
        => chars[index - ONE];

        /// <summary>
        /// Checks to see if the method exists before continuing to Search North.
        /// If it doesn't it will return the ERROR character '!';
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>Character at Index</returns>
        public static char West(int index)
        => CharacterExists(index - ONE) 
            ? SearchWest(index) : NOT_EXIST;

        /// <summary>
        /// Searches for a character in the North East direction.
        /// </summary>
        /// <param name="index">The Index of the character.(Integer)</param>
        /// <returns>Character in the North East direction.</returns>
        private static char SearchNorthEast(int index)
        => chars[(index + ONE) - columns];

        /// <summary>
        /// Checks to see if the method exists before continuing to Search North East.
        /// If it doesn't it will return the ERROR character '!';
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>Character at Index</returns>
        private static char NorthEast(int index)
        => CharacterExists((index + ONE) - columns)
            ? SearchNorthEast(index) : NOT_EXIST;

        /// <summary>
        /// Searches for a character in the South East direction.
        /// </summary>
        /// <param name="index">The Index of the character.(Integer)</param>
        /// <returns>Character in the South East direction.</returns>
        private static char SearchSouthEast(int index)
        => chars[(index + ONE) + columns];

        /// <summary>
        /// Checks to see if the method exists before continuing to Search South East.
        /// If it doesn't it will return the ERROR character '!';
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>Character at Index</returns>
        private static char SouthEast(int index)
        => CharacterExists(index + ONE) 
            ? SearchSouthEast(index) : NOT_EXIST;

        /// <summary>
        /// Searches for a character in the South West direction.
        /// </summary>
        /// <param name="index">The Index of the character.(Integer)</param>
        /// <returns>Character in the South West direction.</returns>
        private static char SearchSouthWest(int index)
        => chars[(index - ONE) + columns];

        /// <summary>
        /// Checks to see if the method exists before continuing to Search North.
        /// If it doesn't it will return the ERROR character '!';
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>Character at Index</returns>
        private static char SouthWest(int index)
        => CharacterExists((index - ONE) + columns)
            ? SearchSouthWest(index) : NOT_EXIST;

        /// <summary>
        /// Searches for a character in the North West direction.
        /// </summary>
        /// <param name="index">The Index of the character.(Integer)</param>
        /// <returns>Character in the North West direction.</returns>
        private static char SearchNorthWest(int index)
        => chars[(index - ONE) - columns];

        /// <summary>
        /// Checks to see if the method exists before continuing to Search North.
        /// If it doesn't it will return the ERROR character '!';
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>Character at Index</returns>
        private static char NorthWest(int index)
        => CharacterExists((index - ONE) - columns) 
            ? SearchNorthWest(index) : NOT_EXIST;

        //Helpers
        /// <summary>
        /// Takes a index input, and converts it to ZeroBased.
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>The Zero Based Index</returns>
        private static int ConvertToZeroBased(int index)
        => index - ONE;

        /// <summary>
        /// Reverse the process of the Zero Based Conversion
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>Reversed Zero Based Index</returns>
        private static int ReverseZeroBased(int index)
        => index + ONE;

        /// <summary>
        /// Takes in a direction and checks whether or not it can be done
        /// with the certain directional criteria.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        private static bool IsDirectionValid(Directions direction, Directions criteria)
        {
            bool valid = false;

            for (int i = ZERO; i < directionalRows; i++)
            {
                if (criteria == CORNERS[i])
                {
                    for (int j = ZERO; j < cornerCols; j++)
                        if (direction == CORNERS_CRITERIA[i, j])
                        { valid = true; j = cornerCols; }
                }
                else if (criteria == DIRECTIONALS[i])
                {
                    for (int j = ZERO; j < directionCols; j++)
                        if (direction == DIRECTIONS_CRITERIA[i, j])
                        { valid = true; j = directionCols; }
                }
            }
            return valid;
        }

        /// <summary>
        /// This method uses the index and returns a criteria if there is one.
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The Criteria, if any</returns>
        private static Directions GetCriteria(int index)
        {
            Directions criteria = Directions.NONE;

            if (IsWallCheck(index))
            {
                if (IsWestWall(index))
                {
                    if (IsNorthWestCorner(index))
                    {
                        criteria = Directions.NORTH_WEST;
                    }
                    else if (IsSouthWestCorner(index))
                    {
                        criteria = Directions.SOUTH_WEST;
                    }
                    else
                    {
                        criteria = Directions.WEST;
                    }
                }
                else if (IsEastWall(index))
                {
                    if (IsNorthEastCorner(index))
                    {
                        criteria = Directions.NORTH_EAST;
                    }
                    else if (IsSouthEastCorner(index))
                    {
                        criteria = Directions.SOUTH_EAST;
                    }
                    else
                    {
                        criteria = Directions.EAST;
                    }
                }


            }
            else if (IsPoleCheck(index))
            {
                if (IsNorthPole(index))
                {
                    criteria = Directions.NORTH;
                }
                else if (IsSouthPole(index))
                {
                    criteria = Directions.SOUTH;
                }
            }

            return criteria;
        }

        /// <summary>
        /// Returns the Direction that relates to the integer assigned to it.
        /// </summary>
        /// <param name="integer">The integer</param>
        /// <returns>The correlating Direction.</returns>
        private static Directions GetDirectionByInteger(int integer)
        {
            Directions tempDirection = Directions.NORTH;

            switch(integer)
            {
                case 1:
                    tempDirection = Directions.NORTH;
                    break;
                case 2:
                    tempDirection = Directions.NORTH_EAST;
                    break;
                case 3:
                    tempDirection = Directions.EAST;
                    break;
                case 4:
                    tempDirection = Directions.SOUTH_EAST;
                    break;
                case 5:
                    tempDirection = Directions.SOUTH;
                    break;
                case 6:
                    tempDirection = Directions.SOUTH_WEST;
                    break;
                case 7:
                    tempDirection = Directions.WEST;
                    break;
                case 8:
                    tempDirection = Directions.NORTH_WEST;
                    break;       
            }
            return tempDirection;
        }

        /// <summary>
        /// Takes in a direction and character index and returns the wanted char.
        /// </summary>
        /// <param name="direction">The Direction for the Search.</param>
        /// <param name="index">The Index of the Character.</param>
        /// <returns>The character in the specified direction.</returns>
        private static char Search(Directions direction, int index)
        {
            char tempChar = ERROR;

            switch (direction)
            {
                case Directions.NORTH:
                    tempChar = North(index);
                    break;
                case Directions.NORTH_EAST:
                    tempChar = NorthEast(index);
                    break;
                case Directions.EAST:
                    tempChar = East(index);
                    break;
                case Directions.SOUTH_EAST:
                    tempChar = SouthEast(index);
                    break;
                case Directions.SOUTH:
                    tempChar = South(index);
                    break;
                case Directions.SOUTH_WEST:
                    tempChar = SouthWest(index);
                    break;
                case Directions.WEST:
                    tempChar = West(index);
                    break;
                case Directions.NORTH_WEST:
                    tempChar = NorthWest(index);
                    break;
                default:
                    //Not a direction
                    break;
            }

            return tempChar;
        }

        /// <summary>
        /// Protects the Search method by first validating the search.
        /// </summary>
        /// <param name="direction">The Direction</param>
        /// <param name="index">The Index</param>
        /// <returns>The Character</returns>
        private static char ProtectedSearch(Directions direction, int index)
        {
            if (GetCriteria(index) == Directions.NONE)
                return Search(direction, ConvertToZeroBased(index));
            else if (GetCriteria(index) != Directions.NONE)
            {
                if (IsDirectionValid(direction, GetCriteria(index)))
                {
                    return Search(direction, ConvertToZeroBased(index));
                }
                else
                    return NOT_EXIST;
            }
            else
                return ERROR;
                
        }
        /// <summary>
        /// Searches all eight directions.
        /// </summary>
        /// <returns>A Character array of all the searches.</returns>
        private static char[] SearchDirections(int index)
        {
            char[] chars = new char[NUM_DIRECTIONS];

            //Search Directions
            for (int x = ZERO; x < NUM_DIRECTIONS; x++)
            {
                chars[x] = ProtectedSearch(GetDirectionByInteger(ReverseZeroBased(x)), index);
            }

            return chars;
        }

        /// <summary>
        /// A more robust directional search that checks to see if the current index exists,
        /// and automatically converts the index into a ZeroBasedIndex.
        /// Returns an ERROR CHAR ARRAY '[ '!' ]' if the index doesn't exist.
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>A character array.</returns>
        public static char[] DirectionalSearch(int index)
        => CharacterExists(index)
            ? SearchDirections(index) : ERROR_CHAR_ARRAY;

        //Method Checkers
        /// <summary>
        /// This method checks the index to see if its valid or not.
        /// </summary>
        /// <param name="index">The Index of the possible character.</param>
        /// <returns>A Boolean</returns>
        public static bool CharacterExists(int index)
        => index >= ZERO && index <= characters;

        //Direction and Placement Checkers

        /// <summary>
        /// Checks the index to see if its at the West Wall.
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>A Bool</returns>
        public static bool IsWestWall(int index)
        => FindColumn(index) == ONE;

        /// <summary>
        /// Checks the index to see if its at the East Wall.
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>A Bool</returns>
        public static bool IsEastWall(int index)
        => FindColumn(index) == columns;

        /// <summary>
        /// This method checks to see if the index is at the left or right.
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>A Bool Value</returns>
        public static bool IsWallCheck(int index)
        => IsWestWall(index) || IsEastWall(index);

        /// <summary>
        /// This method checks to see if the index is at the top
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>A Bool Value</returns>
        public static bool IsNorthPole(int index)
        => FindRow(index) == ONE;

        /// <summary>
        /// This method checks to see if the index is at the bottom.
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>A Bool Value</returns>
        public static bool IsSouthPole(int index)
        => FindRow(index) == rows;

        /// <summary>
        /// This method checks to see if the index is at the top or bottom.
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>A Bool Value</returns>
        public static bool IsPoleCheck(int index)
        => IsNorthPole(index) || IsSouthPole(index);

        /// <summary>
        /// This methods checks to see if the index is the North West Corner
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>A Bool Value</returns>
        public static bool IsNorthWestCorner(int index)
        => IsNorthPole(index) && IsWestWall(index);

        /// <summary>
        /// This methods checks to see if the index is the North East Corner
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>A Bool Value</returns>
        public static bool IsNorthEastCorner(int index)
        => IsNorthPole(index) && IsEastWall(index);

        /// <summary>
        /// This methods checks to see if the index is the South West Corner
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>A Bool Value</returns>
        public static bool IsSouthWestCorner(int index)
        => IsSouthPole(index) && IsWestWall(index);

        /// <summary>
        /// This methods checks to see if the index is the South East Corner
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>A Bool Value</returns>
        public static bool IsSouthEastCorner(int index)
        => IsSouthPole(index) && IsEastWall(index);


        //Algorithm

        /// <summary>
        /// This method takes in a character and returns the first index
        /// of its kind.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <returns>The index.</returns>
        public static int FindFirstCharacter(char c)
        {
            int index = ZERO;

            for(int i = ZERO; i < wordSearch.Length; i++)
            {
                if (wordSearch[i] == c)
                {
                    index = ReverseZeroBased(i);
                    i = wordSearch.Length;
                }
            }

            return index;
        }

        /// <summary>
        /// This method takes in a character, and a starting index to start
        /// the search at.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <param name="startingIndex">The starting index.</param>
        /// <returns>The index</returns>
        public static int FindNextCharacter(char c, int startingIndex)
        {
            int index = ZERO;

            for (int i = startingIndex; i < wordSearch.Length; i++)
            {
                if (wordSearch[i] == c)
                {
                    index = ReverseZeroBased(i);
                    i = wordSearch.Length;
                }
            }

            return index;
        }

        /// <summary>
        /// The method checks the boundry of the index used in the
        /// character finder methods.
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>A Bool Value.</returns>
        public static bool IsBoundry(int index)
        => index > ZERO && index < wordSearch.Length;

        //Hurray for Recursion!

        /// <summary>
        /// This method combines the first two Find Character Methods.
        /// It uses recursion to find all instances of a letter, if any.
        /// </summary>
        /// <param name="c">The character</param>
        /// <returns>All Instances' indexes</returns>
        public static int[] FindAllInstancesOfCharacter(char c)
        {
            int[] instances = new int[wordSearch.Length];
            int length = ZERO;
            bool condition;
            int startingIndex;
            int nextIndex;

            startingIndex = FindFirstCharacter(c);

            if (IsBoundry(startingIndex))
            {
                instances[length] = startingIndex;
                length += ONE;

                nextIndex = FindNextCharacter(c, startingIndex);
                condition = IsBoundry(nextIndex);

                while(condition)
                {
                    instances[length] = nextIndex;
                    length += ONE;

                    nextIndex = FindNextCharacter(c, nextIndex);
                    condition = IsBoundry(nextIndex);
                }
            }

            return instances;
        }

        /// <summary>
        /// Takes a character array, and removes all blanks (NOT_EXIST)
        /// </summary>
        /// <param name="array">The array</param>
        /// <returns>Trimmed array</returns>
        public static char[] Trim(char[] array)
        {
            int length = array.Length;
            int actualLength = ZERO;

            foreach (char c in array)
                if (c != NOT_EXIST)
                    actualLength += ONE;

            char[] trimmedArray = new char[actualLength];
            for (int i = 0; i < length; i++)
                if (array[i] != NOT_EXIST)
                    trimmedArray[i] = array[i];

            return trimmedArray;

        }

        /// <summary>
        /// Takes a integer array, removes all ZERO's
        /// </summary>
        /// <param name="array">The array</param>
        /// <returns>Trimmed array</returns>
        public static int[] Trim(int[] array)
        {
            int length = array.Length;
            int actualLength = ZERO;

            foreach (int i in array)
                if (i > 0)
                    actualLength += ONE;

            int[] trimmedArray = new int[actualLength];
            for(int j = 0; j < length; j++)
                if (array[j] > 0)
                    trimmedArray[j] = array[j];

            return trimmedArray;
        }

        /*Pseudocode
         * Find the first character in the choosen word to search for.
         * 
         * Find first iteration, if any, inside the character array.
         * 
         * Find the next letter in the choosen word with the same directional
         * method.
         * 
         * Repeat until either the entire word has been found.
         * 
         * If the next letter found isn't a corresponding character in the word
         * that is being searched, cancel the process and move onto the next
         * iteration, if any, in the word search.
         * 
         * Else if the next letter is a corresponding character in the word using
         * the same directional method used, continue until either found or the 
         * first condtion is met.
         * 
         * If all else fails, that means that either a letter wasn't found, word
         * couldn't be found, or something else.
         *
         */

        private static void Main(string[] args)
        {
            //Testing
            char[] tests =
            [
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l',
                'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            ];

            //Test Passed
            foreach(char test in tests)
            {
                int[] array = Trim(FindAllInstancesOfCharacter(test));
                Print(test + COLON);
                PrintArray(array);
            }

            Console.WriteLine();
            Console.WriteLine("End Of Program?");
            Console.ReadKey(true);
        }

    }
}
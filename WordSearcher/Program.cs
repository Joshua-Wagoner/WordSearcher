
namespace WordSearcher
{

    /** *
     * DLM: 05/02/2024 
     * Program Name: WordSearcher
     * Author: Joshua M. Wagoner
     * Copyright @ 2024
     * 
     * Description:
     * This program will take a designated word search, a list of words,
     * and search the word search until all words are found.
     * 
     */

    /*
     * TODO:
     *  Finished: No
     *  Finish writing the boundary method, clean up and simplify every day.
     *  
     *  Finished: No
     *  Finishing writing the Find Row method. We are close!
     * 
     *  Finished: No
     *  Write method checkers as in what happens if the character is on a boundry.
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
            NORTH_WEST = 8
        }

        //Constants

        //Debugging
        public readonly static string EOL_MESSAGE = "EOL";
        public readonly static string NEW_LINE = "\n";
        public readonly static string COLON = ":";

        public readonly static char ERROR = '!';
        public readonly static char NOT_EXIST = ' ';
        public readonly static char[] ERROR_CHAR_ARRAY = ['!'];

        public readonly static int NUM_DIRECTIONS = 8;
        public readonly static int ROWS = 20;
        public readonly static int COLUMNS = 22;
        public readonly static int WORD_CHARACTER_LENGTH = 12;
        public readonly static int WORD_CHARACTER_ROWS = 7;
        public readonly static int ONE = 1;

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

        private readonly static int cRows = WORD_CHARACTER_ROWS;
        private readonly static int cColumns = WORD_CHARACTER_LENGTH;
        public readonly static char[,] wordChars = new char[cRows, cColumns];

        private readonly static int rows = ROWS;
        private readonly static int columns = COLUMNS;
        private readonly static int characters = rows * columns;

        //Debugging Methods
        public static void Print(string s)
        => Console.Write(s);

        //Finder
        /// <summary>
        /// Finds the index of a character by using the row and column.
        /// </summary>
        /// <param name="row">The row of the character. (Integer)</param>
        /// <param name="column">The column of the character. (Integer)</param>
        /// <returns>Index</returns>
        private static int FindIndex(int row, int column)
        => (row * columns) - (columns - column);

        //Might need to use class structure for this one.
        private static void FindRow(int index)
        {
            //Ratios
            int row = index / columns;
            int column = index / rows;

            //Temporary
            if(row >= 0 && row <= ONE)
                Print(row + string.Empty);
        }

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
        /// Searches all eight directions.
        /// </summary>
        /// <returns>A Character array of all the searches.</returns>
        private static char[] SearchDirections(int index)
        {
            char[] chars = new char[NUM_DIRECTIONS];

            //Search Directions
            for (int x = 0; x < NUM_DIRECTIONS; x++)
            {
                chars[x] = Search(GetDirectionByInteger(ReverseZeroBased(x)), index);
                Print(GetDirectionByInteger(ReverseZeroBased(x)) + COLON);
                Print(chars[x] + string.Empty + NEW_LINE);
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
        => CharacterExists(ConvertToZeroBased(index))
            ? SearchDirections(ConvertToZeroBased(index)) : ERROR_CHAR_ARRAY;

        /// <summary>
        /// A more robust directional search that checks to see if the current index at 
        /// the row and column exists, and automatically converts the index into a ZeroBasedIndex.
        /// Returns an ERROR CHAR ARRAY '[ '!' ]' if the index doesn't exist.
        /// </summary>
        /// <param name="row">The row of the character</param>
        /// <param name="column">The column of the character</param>
        /// <returns>A character array.</returns>
        public static char[] DirectionalSearch(int row, int column)
        => CharacterExists(ConvertToZeroBased(FindIndex(row, column))) 
            ? SearchDirections(ConvertToZeroBased(FindIndex(row, column))) : ERROR_CHAR_ARRAY;

        //Method Checkers
        /// <summary>
        /// This method checks the index to see if its valid or not.
        /// </summary>
        /// <param name="index">The Index of the possible character.</param>
        /// <returns>A Boolean</returns>
        public static bool CharacterExists(int index)
        => index >= 0 && index <= characters;

        //public static bool CheckWallBoundry(int index)
        //=> index >= ((row - ONE) * columns) && index <= (columns * row);

        //Algorithm

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

                FindRow(FindIndex(19, 22));
                Print(NEW_LINE);
            



            //Acknowledge EOP
            Console.WriteLine();
            Console.WriteLine("End Of Program?");
            Console.ReadKey(true);
        }

    }
}
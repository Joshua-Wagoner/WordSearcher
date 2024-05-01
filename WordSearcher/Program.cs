namespace WordSearcher
{

    /** *
     * DLM: 05/01/2024 
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
     * Rework all Search Methods, as the base formula has been changed.
     * North is already done and working.
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
        public readonly static string EOL_MESSAGE = "EOL";

        public readonly static char ERROR = '!';

        public readonly static int NUM_DIRECTIONS = 8;

        //Variables
        public readonly static string[] words =
            {
            "blizzard    ", "december    ", "february    ", "fireplace   ",
            "flannel     ", "flurries    ", "frigid      ", "frostbite   ",
            "frozen      ", "gloves      ", "hockey      ", "holidays    ",
            "hotchocolate", "icicle      ", "igloo       ", "jacket      ",
            "january     ", "longjohns   ", "mitts       ", "scarf       ",
            "shovel      ", "skating     ", "skiing      ", "sleigh      ",
            "slippery    ", "snowballs   ", "snowboarding", "snowflakes  ",
            "snowman     ", "showshoes   ","solstice     ", "sweater     ",
            "toboggan    ", "whiteout    ", "wintertime  "
        };
        public readonly static string wordSearch =
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

        public readonly static char[] chars = wordSearch.ToCharArray();

        public readonly static int cRows = 7;
        public readonly static int cColumns = 12;
        public readonly static char[,] wordChars = new char[cRows, cColumns];

        public readonly static int rows = 20;
        public readonly static int columns = 22;

        //Finders

        /// <summary>
        /// Finds the index of a character by using the row and column.
        /// </summary>
        /// <param name="row">The row of the character. (Integer)</param>
        /// <param name="column">The column of the character. (Integer)</param>
        /// <returns>Index</returns>
        public static int FindIndex(int row, int column)
        => (row * columns) - (columns- column);

        //Direction Searches

        /// <summary>
        /// Searches for a character in the North direction.
        /// </summary>
        /// <param name="index">The Index of the character.(Integer)</param>
        /// <returns>Character in the north direction.</returns>
        public static char SearchNorth(int index)
        => chars[index - columns];

        /// <summary>
        /// Searches for a character in the East direction.
        /// </summary>
        /// <param name="index">The Index of the character.(Integer)</param>
        /// <returns>Character in the East direction.</returns>
        public static char SearchEast(int index)
        => chars[index++];

        /// <summary>
        /// Searches for a character in the South direction.
        /// </summary>
        /// <param name="index">The Index of the character.(Integer)</param>
        /// <returns>Character in the South direction.</returns>
        public static char SearchSouth(int index)
        => chars[index - columns];

        /// <summary>
        /// Searches for a character in the West direction.
        /// </summary>
        /// <param name="index">The Index of the character.(Integer)</param>
        /// <returns>Character in the West direction.</returns>
        public static char SearchWest(int index)
        => chars[index--];

        /// <summary>
        /// Searches for a character in the North East direction.
        /// </summary>
        /// <param name="index">The Index of the character.(Integer)</param>
        /// <returns>Character in the North East direction.</returns>
        public static char SearchNorthEast(int index)
        => SearchNorth(SearchEast(index));

        /// <summary>
        /// Searches for a character in the South East direction.
        /// </summary>
        /// <param name="index">The Index of the character.(Integer)</param>
        /// <returns>Character in the South East direction.</returns>
        public static char SearchSouthEast(int index)
        => SearchSouth(SearchEast(index));

        /// <summary>
        /// Searches for a character in the South West direction.
        /// </summary>
        /// <param name="index">The Index of the character.(Integer)</param>
        /// <returns>Character in the South West direction.</returns>
        public static char SearchSouthWest(int index)
        => SearchSouth(SearchWest(index));

        /// <summary>
        /// Searches for a character in the North West direction.
        /// </summary>
        /// <param name="index">The Index of the character.(Integer)</param>
        /// <returns>Character in the North West direction.</returns>
        public static char SearchNorthWest(int index)
        => SearchNorth(SearchWest(index));

        //Helpers

        /// <summary>
        /// Takes a index input, and converts it to ZeroBased.
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>The Zero Based Index</returns>
        public static int ConvertToZeroBased(int index)
        => index-1;

        /// <summary>
        /// Returns the Direction that relates to the integer assigned to it.
        /// </summary>
        /// <param name="integer">The integer</param>
        /// <returns>The correlating Direction.</returns>
        public static Directions GetDirectionByInteger(int integer)
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
                    tempDirection = Directions.NORTH;
                    break;
                case 7:
                    tempDirection = Directions.NORTH_WEST;
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
        public static char Search(Directions direction, int index)
        {
            char tempChar = ERROR;

            switch (direction)
            {
                case Directions.NORTH:
                    tempChar = SearchNorth(index);
                    break;
                case Directions.NORTH_EAST:
                    tempChar = SearchNorthEast(index);
                    break;
                case Directions.EAST:
                    tempChar = SearchEast(index);
                    break;
                case Directions.SOUTH_EAST:
                    tempChar = SearchNorth(index);
                    break;
                case Directions.SOUTH:
                    tempChar = SearchNorth(index);
                    break;
                case Directions.SOUTH_WEST:
                    tempChar = SearchNorth(index);
                    break;
                case Directions.WEST:
                    tempChar = SearchNorth(index);
                    break;
                case Directions.NORTH_WEST:
                    tempChar = SearchNorth(index);
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
        public static char[] SearchDirections(int index)
        {
            char[] chars = new char[NUM_DIRECTIONS];

            //Search Directions
            for (int x = 1; x < NUM_DIRECTIONS; x++)
            {

                chars[x] = Search(GetDirectionByInteger(x), index);
            }

            return chars;
        }

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
            Console.Write(FindIndex(10, 3));
            Console.Write("=");
            Console.Write(chars[ConvertToZeroBased(FindIndex(10,3))]);
            Console.WriteLine();
            Console.Write("N=");
            Console.Write(SearchNorth(ConvertToZeroBased(FindIndex(10, 3))));
            

            //Acknowledge EOP
            Console.WriteLine();
            Console.WriteLine("End Of Program?");
            Console.ReadKey(true);
        }
    }
}
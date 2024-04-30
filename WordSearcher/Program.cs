internal class Program
{
    //Constants
    public readonly static string EOL_MESSAGE = "EOL";

    //Variables
    public readonly static string[] words =
        {
    "blizzard    ", "december    ", "february ", "fireplace", "flannel ",
    "flurries    ", "frigid   ", "frostbite", "frozen   ", "gloves  ",
    "hockey   ", "holidays ", "hotchocolate", "icicle", "igloo",
    "jacket", "january", "longjohns", "mitts", "scarf", "shovel",
    "skating", "skiing", "sleigh", "slippery", "snowballs",
    "snowboarding", "snowflakes", "snowman", "showshoes",
    "solstice", "sweater", "toboggan", "whiteout", "wintertime"
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
    public readonly static char[,] wordChars =
    {
        { }, { }, { }, { }, { },
        { }, { }, { }, { }, { },
        { }, { }, { }, { }, { },
        { }, { }, { }, { }, { },
        { }, { }, { }, { }, { },
        { }, { }, { }, { }, { },
        { }, { }, { }, { }, { }
    };

    public readonly static int rows = 20;
    public readonly static int columns = 22;

    //Finders
    public int FindIndex(int row, int column)
    => row * columns - (rows - column);

    public int FindRow(int index, int column)
    => (index + (rows - column)) / columns;

    public int FindColumns(int index, int row)
    => index - row * columns + rows;

    //Direction Searches
    public char SearchNorth(int index)
    => chars[index - rows];

    public char SearchEast(int index)
    => chars[index++];

    public char SearchSouth(int index)
    => chars[index + rows];

    public char SearchWest(int index)
    => chars[index--];

    public char SearchNorthEast(int index)
    => SearchNorth(SearchEast(index));

    public char SearchSouthEast(int index)
    => SearchSouth(SearchEast(index));

    public char SearchSouthWest(int index)
    => SearchSouth(SearchWest(index));

    public char SearchNorthWest(int index)
    => SearchNorth(SearchWest(index));

    //Helpers

    public static int ZeroBased(int index)
    => index--;

    private static void Main(string[] args)
    {

        Console.ReadKey(true);
    }
}
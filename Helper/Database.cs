public static class Database{ 
    static Dictionary<string, string> versions = new Dictionary<string, string>
    {
        {"aa:bb:cc:dd:ee", "001"},
        {"bb:cc:dd:ee:aa", "001"},
        {"cc:dd:ee:aa:bb:", "002"}
    };
}
using TreeGen;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var (rootDir, dirOnly, outputFile, showVersion, showHelp) = Cli.ParseArguments(args);

            if (showVersion)
            {
                Cli.PrintVersion();
                return;
            }

            if (showHelp)
            {
                // System.CommandLine tự động hiển thị trợ giúp khi -h/--help được gọi
                return;
            }

            if (!Directory.Exists(rootDir))
            {
                Console.WriteLine("The specified root directory doesn't exist.");
                Environment.Exit(1);
            }

            var tree = new DirectoryTree(rootDir, dirOnly, outputFile);
            tree.Generate();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Environment.Exit(1);
        }
    }
}
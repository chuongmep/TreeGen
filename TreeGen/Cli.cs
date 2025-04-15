using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Help;
using System.CommandLine.Parsing;
using System.Reflection;

namespace TreeGen;

public class Cli
{
    // get ver from assembly
    private static readonly string Version = Assembly.GetExecutingAssembly()
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "unknown";

    public static (string rootDir, bool dirOnly, string? outputFile, bool showVersion, bool showHelp) ParseArguments(string[] args)
    {
        var rootDirArgument = new Argument<string>(
            name: "ROOT_DIR",
            description: "Generate a full directory tree starting at ROOT_DIR",
            getDefaultValue: () => Directory.GetCurrentDirectory());

        var dirOnlyOption = new Option<bool>(
            aliases: new[] { "-d", "--dir-only" },
            description: "Generate a Directory-only Tree",
            getDefaultValue: () => false);

        var outputFileOption = new Option<string?>(
            aliases: new[] { "-o", "--output-file" },
            description: "Generate a full directory tree and save it to a file",
            getDefaultValue: () => null);

        var helpOption = new Option<bool>(
            aliases: new[] { "-h", "--help" },
            description: "Show this help message and exit",
            getDefaultValue: () => false);

        var versionOption = new Option<bool>(
            aliases: new[] { "-v", "--version" },
            description: "Show program's version number and exit",
            getDefaultValue: () => false);

        var rootCommand = new RootCommand("RP Tree, a directory tree generator")
        {
            rootDirArgument,
            dirOnlyOption,
            outputFileOption,
            helpOption,
            versionOption
        };

        string rootDir = Directory.GetCurrentDirectory();
        bool dirOnly = false;
        string? outputFile = null;
        bool showVersion = false;
        bool showHelp = false;

        rootCommand.SetHandler((rootDirValue, dirOnlyValue, outputFileValue, helpValue, versionValue) =>
        {
            rootDir = rootDirValue;
            dirOnly = dirOnlyValue;
            outputFile = outputFileValue;
            showHelp = helpValue;
            showVersion = versionValue;
        }, rootDirArgument, dirOnlyOption, outputFileOption, helpOption, versionOption);

        var parser = new CommandLineBuilder(rootCommand)
            .UseHelp(context =>
            {
                context.HelpBuilder.CustomizeLayout(_ =>
                    HelpBuilder.Default.GetLayout().Prepend(_ =>
                        context.Output.WriteLine("usage: treegen [-h] [-v] [-d] [-o [OUTPUT_FILE]] [ROOT_DIR]\n"))
                        .Append(_ =>
                            context.Output.WriteLine("Thanks for using RP Tree!")));
            })
            .Build();

        parser.Invoke(args);
        return (rootDir, dirOnly, outputFile, showVersion, showHelp);
    }

    public static void PrintVersion()
    {
        Console.WriteLine($"RP Tree version {Version}");
    }
}
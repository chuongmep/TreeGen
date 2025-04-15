using System.Text;

namespace TreeGen;

public class DirectoryTree
{
    private readonly string _rootDir;
    private readonly bool _dirOnly;
    private readonly string? _outputFile;
    private const string PIPE = "│   ";
    private const string ELBOW = "└── ";
    private const string TEE = "├── ";
    private const string SPACE_PREFIX = "    ";

    public DirectoryTree(string rootDir, bool dirOnly = false, string? outputFile = null)
    {
        _rootDir = rootDir;
        _dirOnly = dirOnly;
        _outputFile = outputFile;
    }

    public void Generate()
    {
        var output = new StringBuilder();
        output.AppendLine(_rootDir);

        GenerateTree(new DirectoryInfo(_rootDir), "", output);

        string result = output.ToString();
        if (!string.IsNullOrEmpty(_outputFile))
        {
            File.WriteAllText(_outputFile, result);
        }
        else
        {
            Console.WriteLine(result);
        }
    }

    private void GenerateTree(DirectoryInfo directory, string indent, StringBuilder output)
    {
        try
        {
            var items = GetDirectoryItems(directory).ToList();
            for (int i = 0; i < items.Count; i++)
            {
                bool isLast = i == items.Count - 1;
                string prefix = indent + (isLast ? ELBOW : TEE);
                output.AppendLine($"{prefix}{items[i].Name}");

                if (items[i] is DirectoryInfo dir)
                {
                    string nextIndent = indent + (isLast ? SPACE_PREFIX : PIPE);
                    GenerateTree(dir, nextIndent, output);
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            output.AppendLine($"{indent}{ELBOW}Access Denied");
        }
        catch (Exception ex)
        {
            output.AppendLine($"{indent}{ELBOW}Error: {ex.Message}");
        }
    }

    private IEnumerable<FileSystemInfo> GetDirectoryItems(DirectoryInfo directory)
    {
        try
        {
            if (_dirOnly)
            {
                return directory.GetDirectories()
                    .OrderBy(d => d.Name)
                    .Cast<FileSystemInfo>();
            }
            return directory.GetFileSystemInfos()
                .OrderBy(f => f is DirectoryInfo ? 0 : 1)
                .ThenBy(f => f.Name);
        }
        catch
        {
            return Enumerable.Empty<FileSystemInfo>();
        }
    }
}
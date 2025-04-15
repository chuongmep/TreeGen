# TreeGen

**TreeGen** is a command-line tool written in C# that generates a visual directory tree. It displays the structure of a directory in a tree-like format, with options to customize the output.

> RP Tree, a directory tree generator

## Features

- Generate a full directory tree starting from a specified directory.
- Option to display only directories (`-d`/`--dir-only`).
- Save the tree to a file (`-o`/`--output-file`).
- Display help (`-h`/`--help`) and version (`-v`/`--version`) information.
- Simple and intuitive CLI interface.

## Installation

```bash
dotnet tool install --global TreeGen
```
Run:
```bash
treegen --help
```
### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later.

### Build from Source
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/TreeGen.git
   cd TreeGen
   ```
2. Build the project:
   ```bash
   dotnet build
   ```
3. Run the application:
   ```bash
   dotnet run -- [options] [ROOT_DIR]
   ```

### Publish as a Standalone Executable
To run `treegen` without `dotnet run`:
1. Publish the tool:
   ```bash
   dotnet publish -c Release -r win-x64 --self-contained -o ./publish
   ```
   Replace `win-x64` with `linux-x64` or `osx-x64` for other platforms.
2. Run from the publish folder:
   ```bash
   ./publish/treegen -h
   ```

### Install as a Global Tool (Optional)
To run `treegen` from any directory:
1. Update `TreeGen.csproj` to include:
   ```xml
   <PropertyGroup>
     <PackAsTool>true</PackAsTool>
     <ToolCommandName>treegen</ToolCommandName>
     <PackageOutputPath>./nupkg</PackageOutputPath>
   </PropertyGroup>
   ```
2. Pack and install:
   ```bash
   dotnet pack
   dotnet tool install --global --add-source ./nupkg TreeGen
   ```
3. Run:
   ```bash
   treegen --help
   ```

### Note for Windows Users
The command `tree` may conflict with the built-in Windows `tree` command. Use `treegen` (the executable name) or `dotnet run -- [options]` to run TreeGen:
```bash
dotnet run -- -d ./test_dir
./publish/treegen -v
```

## Usage

```bash
usage: treegen [-h] [-v] [-d] [-o [OUTPUT_FILE]] [ROOT_DIR]

RP Tree, a directory tree generator

positional arguments:
  ROOT_DIR              Generate a full directory tree starting at ROOT_DIR

optional arguments:
  -h, --help            show this help message and exit
  -v, --version         show program's version number and exit
  -d, --dir-only        Generate a Directory-only Tree
  -o [OUTPUT_FILE], --output-file [OUTPUT_FILE]
                        Generate a full directory tree and save it to a file.

Thanks for using RP Tree!
```

## Examples

- Generate a tree for the current directory:
  ```bash
  treegen
  ```
  **Output**:
  ```
  .
  ├── file1.txt
  ├── folder1
  │   └── file2.txt
  └── folder2
      └── file3.txt
  ```

- Generate a directory-only tree:
  ```bash
  treegen -d ./test_dir
  ```
  **Output**:
  ```
  ./test_dir
  ├── folder1
  └── folder2
  ```

- Save the tree to a file:
  ```bash
  treegen -o tree.txt ./test_dir
  ```
  **Output**: Creates `tree.txt` with the tree structure.

- Show version:
  ```bash
  treegen -v
  ```
  **Output**:
  ```
  RP Tree version 1.0.0
  ```

## Contributing

Contributions are welcome! Please:
1. Fork the repository.
2. Create a feature branch:
   ```bash
   git checkout -b feature/your-feature
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add your feature"
   ```
4. Push to the branch:
   ```bash
   git push origin feature/your-feature
   ```
5. Open a Pull Request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Inspired by the [rptree](https://github.com/realpython/rptree) Python project.
- Built with [System.CommandLine](https://github.com/dotnet/command-line-api).

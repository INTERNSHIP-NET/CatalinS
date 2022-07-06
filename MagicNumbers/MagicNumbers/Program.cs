using System;
using System.IO;
using MagicNumbers.Modules;
class Program
{
    static public void Main(String[] args)
    {
        TestAllFiles();
    }

    static public void TestAllFiles()
    {
        var squareValidator = new MagicSquareValidator();

        var testsFolderName = "Tests";
        var relativeDirectory = Path.GetFullPath(Path.Combine(System.Environment.CurrentDirectory, @"..\..\..\"));
        var testsPath = Path.Combine(relativeDirectory, testsFolderName);

        DirectoryInfo testsFolder = new DirectoryInfo(testsPath);
        FileInfo[] files = testsFolder.GetFiles("*.txt");

        foreach (FileInfo file in files)
        {
            Console.WriteLine("The status of the square in the " + file.Name + " file is:");

            var filePath = testsPath + "/" + file.Name;
            squareValidator.IsMagicSquare(filePath);

            Console.WriteLine(" ");
        }
    }

    
}
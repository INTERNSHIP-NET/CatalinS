using System;
using System.IO;

class Program
{
    static public void Main(String[] args)
    {
        var CollectionStatus = IsMagicSquare("M8.txt");
    }

    static public bool IsMagicSquare(string relativePath)
    {
        try
        {
            var testPath = CreateInputPath(relativePath);
            var magicNumber = GetMagicNumber(testPath);
            Console.WriteLine("Numarul este patrat magic. Valoarea magica este " + magicNumber + ".");

            return true;
        }
        catch(FileNotFoundException)
        {
            Console.WriteLine("Fisierul introdus nu exista!");

            return false;
        }
        catch(InvalidOperationException)
        {
            Console.WriteLine("Numarul nu este patrat magic.");

            return false;
        }
        catch
        {
            Console.WriteLine("A strange thing happened! Please try again later!");

            return false;
        }
    }

    static public string CreateInputPath(string relativePath)
    {
        var relativeDirectory = Path.GetFullPath(Path.Combine(System.Environment.CurrentDirectory, @"..\..\..\"));
        var testPath = Path.Combine(relativeDirectory, "Tests", relativePath);

        if (!File.Exists(testPath))
        {
            throw new FileNotFoundException("The inserted file does not exist!");
        }
        else
        {
            return testPath;
        }
    }

    static public int GetMagicNumber(string TestPath)
    {
        var numberCollection = GetNumbersCollection(TestPath);

        var horizontalValue = GetHorizontalValue(numberCollection);
        var verticalValue = GetVerticalValue(numberCollection);

        if (horizontalValue == verticalValue)
        {
            return horizontalValue;
        }
        else
        {
            throw new InvalidOperationException("The collection of numbers received is not a magic square, so its vertical value could not be extracted!");
        }    
        
    }

    static public List<List<int>> GetNumbersCollection(string Path)
    {
        List<List<int>> CollectionOfNumbers = new List<List<int>>();
        using (StreamReader reader = new StreamReader(Path))
        {
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }

                List<int> RowOfCollection = line.Split(' ').Select(int.Parse).ToList();
                CollectionOfNumbers.Add(RowOfCollection);
            }
        }
        return CollectionOfNumbers;
    }

    static public int GetHorizontalValue(List<List<int>> NumbersCollection)
    {
        var rowValuesHashSet = new HashSet<int>();
        var rowValue = 0;

        foreach (var row in NumbersCollection)
        {
            rowValue = 0;
            foreach (var number in row)
            {
                rowValue += number;
            }

            rowValuesHashSet.Add(rowValue);
            if (rowValuesHashSet.Count > 1)
            {
                throw new InvalidOperationException("The collection of numbers received is not a magic square, so its horizontal value could not be extracted!");
            }
        }
        
        return rowValuesHashSet.First();
    }

    static public int GetVerticalValue(List<List<int>> NumbersCollection)
    {
        var columnValuesHashSet = new HashSet<int>();
        var columnValue = 0;

        for (var i = 0; i < NumbersCollection[0].Count; i++)
        {
            columnValue = 0;
            for (var j = 0; j < NumbersCollection.Count; j++)
            {
                columnValue += NumbersCollection[j][i];
            }

            columnValuesHashSet.Add(columnValue);
            if (columnValuesHashSet.Count > 1)
            {
                throw new InvalidOperationException("The collection of numbers received is not a magic square, so its value could not be extracted!");
            }
        }
        return columnValuesHashSet.First();
    }
}
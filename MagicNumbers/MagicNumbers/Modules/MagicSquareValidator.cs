using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MagicNumbers.Modules
{
    public class MagicSquareValidator
    {
        public bool IsMagicSquare(string filePath)
        {
            try
            {
                CheckInputPath(filePath);
                var magicNumber = GetMagicNumber(filePath);
                Console.WriteLine("This square is a magical one. The magic value is " + magicNumber + ".");

                return true;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The inserted file does not exist!");

                return false;
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("This square isn't a magical one.");

                return false;
            }
            catch
            {
                Console.WriteLine("A strange thing happened! Please try again later!");

                return false;
            }
        }

        private void CheckInputPath(string inputPath)
        {
            if (!File.Exists(inputPath))
            {
                throw new FileNotFoundException("The inserted file does not exist!");
            }
        }

        private int GetMagicNumber(string testPath)
        {
            var numberCollection = GetNumbersCollection(testPath);

            var directionsValues = new HashSet<int>();

            var horizontalValue = GetHorizontalValue(numberCollection);
            directionsValues.Add(horizontalValue);

            var verticalValue = GetVerticalValue(numberCollection);
            directionsValues.Add(verticalValue);

            var diagonalValue = GetDiagonalValue(numberCollection);
            directionsValues.Add(verticalValue);

            var maximNumberOfElements = 1;
            if (directionsValues.Count > maximNumberOfElements)
            {
                throw new InvalidOperationException("The collection of numbers received is not a magic square, so its value could not be extracted!");
            }

            return horizontalValue;
        }

        private List<List<int>> GetNumbersCollection(string path)
        {
            List<List<int>> collectionOfNumbers = new List<List<int>>();
            using (StreamReader reader = new StreamReader(path))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    List<int> rowOfCollection = line.Split(' ').Select(int.Parse).ToList();
                    collectionOfNumbers.Add(rowOfCollection);
                }
            }
            return collectionOfNumbers;
        }

        private int GetHorizontalValue(List<List<int>> numbersCollection)
        {
            var rowValuesHashSet = new HashSet<int>();
            var rowValue = 0;

            foreach (var row in numbersCollection)
            {
                rowValue = 0;
                foreach (var number in row)
                {
                    rowValue += number;
                }

                rowValuesHashSet.Add(rowValue);

                var maximNumberOfElements = 1;
                if (rowValuesHashSet.Count > maximNumberOfElements)
                {
                    throw new InvalidOperationException("The collection of numbers received is not a magic square, so its horizontal value could not be extracted!");
                }
            }

            return rowValuesHashSet.First();
        }

        private int GetVerticalValue(List<List<int>> numbersCollection)
        {
            var columnValuesHashSet = new HashSet<int>();
            var columnValue = 0;

            for (var i = 0; i < numbersCollection[0].Count; i++)
            {
                columnValue = 0;
                for (var j = 0; j < numbersCollection.Count; j++)
                {
                    columnValue += numbersCollection[j][i];
                }

                columnValuesHashSet.Add(columnValue);

                var maximNumberOfElements = 1;
                if (columnValuesHashSet.Count > maximNumberOfElements)
                {
                    throw new InvalidOperationException("The collection of numbers received is not a magic square, so its value could not be extracted!");
                }
            }
            return columnValuesHashSet.First();
        }

        private int GetDiagonalValue(List<List<int>> numbersCollection)
        {
            var diagonalValuesHashSet = new HashSet<int>();
            var upLeftCrossedDiagonalValue = 0;
            var downLeftCrossedDiagonalValue = 0;

            var columnsNumbers = numbersCollection[0].Count;
            var maxIndexOfSquare = numbersCollection[0].Count - 1;

            for (var i = 0; i < columnsNumbers; i++)
            {
                upLeftCrossedDiagonalValue += numbersCollection[i][i];
                downLeftCrossedDiagonalValue += numbersCollection[maxIndexOfSquare - i][i];
            }

            diagonalValuesHashSet.Add(upLeftCrossedDiagonalValue);
            diagonalValuesHashSet.Add(downLeftCrossedDiagonalValue);

            var maximNumberOfElements = 1;
            if (diagonalValuesHashSet.Count > maximNumberOfElements)
            {
                throw new InvalidOperationException("The collection of numbers received is not a magic square, so its diagonal value could not be extracted!");
            }

            return diagonalValuesHashSet.First();
        }
    }
}

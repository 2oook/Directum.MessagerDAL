namespace Directum.Refactoring
{
    public static class XmlHelper
    {
        public static string Func1(string input, string elementName, string attrName) // название метода ничего не говорит о его назначении
        {
            // метод отработает корректно только при валидном xml
            // отсутствуют проверки входных значений
            string[] lines = System.IO.File.ReadAllLines(input);
            string result = null;

            foreach (var line in lines)
            {
                var startElEndex = line.IndexOf(elementName); // название локальной переменной можно сделать понятнее

                if (startElEndex != -1)  // код имеет много вложенных блоков
                {
                    if (line[startElEndex - 1] == '<')
                    {
                        var endElIndex = line.IndexOf('>', startElEndex - 1);
                        var attrStartIndex = line.IndexOf(attrName, startElEndex, endElIndex - startElEndex + 1);

                        if (attrStartIndex != -1)
                        {
                            int valueStartIndex = attrStartIndex + attrName.Length + 2; // магическое число 2

                            while (line[valueStartIndex] != '"')
                            {
                                result += line[valueStartIndex];
                                valueStartIndex++;
                            }

                            break;
                        }
                    }
                }
            }

            return result;
        }


        public static string FindAttributeValue(string input, string elementName, string attrName)
        {
            string[] lines = System.IO.File.ReadAllLines(input);

            foreach (var line in lines)
            {
                int startElementIndex = line.IndexOf(elementName);

                if (startElementIndex == -1)
                    continue;

                if (CheckStartElement(line, startElementIndex))
                    continue;

                int startAttributeIndex = FindStartAttributeIndex(line, startElementIndex, attrName);

                if (startAttributeIndex == -1)
                    continue;

                return FindAttributeValue(line, FindValueStartIndex(startAttributeIndex, line));
            }

            return null;
        }

        private static string FindAttributeValue(string line, int valueStartIndex)
        {
            string result = null;

            while (line[valueStartIndex] != '"')
            {
                result += line[valueStartIndex++];

                if (line.Length == valueStartIndex)
                    throw new XmlIsIncorrectException(Constants.CanNotFindEndOfAttributeValueExceptionMessage);
            }

            return result;
        }

        private static bool CheckStartElement(string line, int startElementIndex) 
        {
            return line[startElementIndex - 1] != '<';
        }

        private static int FindEndElementIndex(string line, int startElementIndex) 
        {
            return line.IndexOf('>', startElementIndex - 1);
        }

        private static int FindStartAttributeIndex(string line, int startElementIndex, string attrName)
        {
            return line.IndexOf(
                attrName, 
                startElementIndex,
                FindCharactersCountToSearchStartAttributeIndex(line, startElementIndex));
        }

        private static int FindCharactersCountToSearchStartAttributeIndex(string line, int startElementIndex) 
        {
            int index = FindEndElementIndex(line, startElementIndex) - startElementIndex + 1;

            if (index == -1)
                throw new XmlIsIncorrectException(Constants.CanNotFindEndElementIndexExceptionMessage);

            return index;
        }

        private static int FindValueStartIndex(int startAttributeIndex, string line)
        {
            return line.IndexOf('"', startAttributeIndex) + 1;
        }
    }
}
using System.Text;

namespace kevinye_codingassessment_smartcommunications
{
    public class HaikuParser
    {
        char[] vowels = new char[] {'a', 'e', 'i', 'o', 'u' };
        char[] vowelsIncludingY = new char[] { 'a', 'e', 'i', 'o', 'u', 'y' };
        int[] expectedSyllableCount = new int[] { 5, 7, 5 };

        public bool IsHaiku(string haikuText, out string failureReason)
        {
            failureReason = "";

            // Trim endlines and spaces with Trim()
            string[] haikuLines = haikuText.Split(Environment.NewLine).Select(line => line.Trim().ToLower()).ToArray();

            // Based on the prompt, I am inferring that trailing/leading line breaks should be treated as additional lines, which causes the text to fail.
            if (haikuLines.Length != 3)
            {
                failureReason = "Haiku does not have three lines.";
                return false;
            }
            else
            {
                for (int lineNumber = 0; lineNumber < haikuLines.Length; lineNumber++)
                {
                    int syllables = 0;
                    string line = haikuLines[lineNumber];

                     
                    // Split words by space, strip all punctuation and numbers.
                    IEnumerable<string> words = line.Split(' ');
                    words.Select(word => RemoveNonLetters(word));

                    foreach(string word in words)
                    {
                        syllables += wordVowelCount(word, 0);
                    }

                    // Compare to expected syllables for line
                    if (syllables != expectedSyllableCount[lineNumber])
                    {
                        failureReason = $"Line {lineNumber + 1} contains {syllables} syllables instead of the expected {expectedSyllableCount[lineNumber]}.";
                        return false;
                    }
                }
            }
            return true;
        }

        private int wordVowelCount(string word, int currentVowelCount)
        {
            if (word.Length == 0)
            {
                return currentVowelCount;
            }

            else if (word.Length == 1)
            {
                // Explicitly check if 'e' is the last character; its behavior is unique.
                if (word[0] == 'e')
                {
                    return currentVowelCount == 0 ? currentVowelCount + 1 : currentVowelCount;
                }

                return vowelsIncludingY.Contains(word[0]) ? currentVowelCount + 1 : currentVowelCount;
            }
            else
            {
                if (vowels.Contains(word[0]))
                {
                    // Consecutive vowels always count as 1 vowel, so the next character never needs to be checked
                    return wordVowelCount(word.Substring(2), currentVowelCount + 1);
                }

                return wordVowelCount(word.Substring(1), currentVowelCount);
            }
        }

        private string RemoveNonLetters(string input)
        {
            var stringBuilder = new StringBuilder();

            foreach (char c in input)
            {
                if (char.IsLetter(c))
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }
    }
}

using System.Text;

/*
The engine reads a “poem” from a file and answers whether or not it is a valid haiku poem.

For the purposes of the exercise, a poem is a haiku poem if and only if:
It is exactly three lines long, and
The three lines contain 5, 7, and 5 syllables, respectively.

The number of syllables in a line is the sum of the syllables in each word in that line. 
The engine will use four “rules” to count the number of syllables in a word:

A vowel – A, E, I, O, or U – corresponds to a syllable in the word.

As an exception to (1), any pair of consecutive vowels only counts as one syllable, not two.
A “Y” that appears at the end of a word is also a vowel.

An “E” at the end of a word is “silent” and not a vowel if the word contains another vowel.
For example, “Madison” has 3 syllables (as the “a”, “i”, and “o” are separated by consonants), 
while “heart” has 1 syllable (by rule 2 - the “ea” are consecutive). 

Also, “your” has 1 syllable (“ou” are consecutive, by rule 2, and “Y” is not at then end, so not a vowel, by rule 3), 
but “lucky” has 2 syllables (due to the “u” and the “y” at the end).

Lastly, “code” has one syllable (because the "e" is silent and not a vowel, by rule 4), 
but “the” also has 1 syllable (the “e” is at the end, however it is the only vowel in the word).
The engine will output “Valid haiku” or “Not a haiku poem because <rule>”.

A sample input could be the three lines:

An old silent pond
A frog jumps into the pond
Splash! Silence again
*/

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

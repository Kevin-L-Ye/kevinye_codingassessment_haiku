using kevinye_codingassessment_smartcommunications;

namespace kevinye_codingassessment_haiku_test
{
    [TestClass]
    public class HaikuTests
    {
        [TestMethod]
        public void Haiku_Works_On_Happy_Path()
        {
            HaikuParser parser = new HaikuParser();
            string outputError;

            // Using string.Join() here is just a cute way to preserve spacing while using a string literal.
            string inputText = string.Join(Environment.NewLine, new string[]{
                @"a a a a a" ,
                "a a a a a a a" ,
                "a e i o u" ,
            });

            bool result = parser.IsHaiku(inputText, out outputError);

            Assert.IsTrue(result);
            Assert.AreEqual(outputError, String.Empty);
        }

        [TestMethod]
        public void Haiku_Strips_Punctuation_And_Numbers()
        {
            HaikuParser parser = new HaikuParser();
            string outputError;

            string inputText = string.Join(Environment.NewLine, new string[]{
                @"a!! a? ..a a ][][]\a" ,
                "a 234a a a a a a" ,
                "a e -=-=-i o u" ,
            });

            bool result = parser.IsHaiku(inputText, out outputError);

            Assert.IsTrue(result);
            Assert.AreEqual(outputError, String.Empty);
        }

        [TestMethod]
        public void Haiku_Checks_For_ThreeLines()
        {
            HaikuParser parser = new HaikuParser();
            string outputError;
            string inputText = @"";

            bool result = parser.IsHaiku(inputText, out outputError);

            Assert.IsFalse(result);
            Assert.AreEqual(outputError, "Haiku does not have three lines.");
        }

        [TestMethod]
        public void Haiku_Treats_Y_As_Vowel_When_Last_Letter()
        {
            HaikuParser parser = new HaikuParser();
            string outputError;

            string inputText = string.Join(Environment.NewLine, new string[]{
                @"a a a a bay" ,
                "a a a a a sunny " ,
                "a e i o y" ,
            });

            bool result = parser.IsHaiku(inputText, out outputError);

            Assert.IsTrue(result);
            Assert.AreEqual(outputError, String.Empty);
        }

        [TestMethod]
        public void Haiku_Does_Not_Treat_Y_As_Vowel_When_Not_Last_Letter()
        {
            HaikuParser parser = new HaikuParser();
            string outputError;

            string inputText = string.Join(Environment.NewLine, new string[]{
                @"a a a a yell" ,
                "a a a a a flypaper" ,
                "a e i o yyyb a" ,
            });

            bool result = parser.IsHaiku(inputText, out outputError);

            Assert.IsTrue(result);
            Assert.AreEqual(outputError, String.Empty);
        }

        [TestMethod]
        public void Haiku_Treats_Trailing_E_As_Vowel_When_Vowel_Count_Is_Zero()
        {
            HaikuParser parser = new HaikuParser();
            string outputError;

            string inputText = string.Join(Environment.NewLine, new string[]{
                @"a a a a the" ,
                "a a a a a a then" ,
                "a e i o be" ,
            });

            bool result = parser.IsHaiku(inputText, out outputError);

            Assert.IsTrue(result);
            Assert.AreEqual(outputError, String.Empty);
        }

        [TestMethod]
        public void Haiku_Does_Not_Treat_Trailing_E_As_Vowel_When_Vowel_Count_Is_Not_Zero()
        {
            HaikuParser parser = new HaikuParser();
            string outputError;

            string inputText = string.Join(Environment.NewLine, new string[]{
                @"a a a a able" ,
                "a a a a a a bee" ,
                "a e i o there" ,
            });

            bool result = parser.IsHaiku(inputText, out outputError);

            Assert.AreEqual(outputError, String.Empty);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Haiku_Treats_Consecutive_Vowels_As_One_Syllable()
        {
            HaikuParser parser = new HaikuParser();
            string outputError;

            string inputText = string.Join(Environment.NewLine, new string[]{
                @"aria boar hey moan" ,
                "triage a a a a a bee" ,
                "a e i o there" ,
            });

            bool result = parser.IsHaiku(inputText, out outputError);

            Assert.AreEqual(outputError, String.Empty);
            Assert.IsTrue(result);
        }
    }
}
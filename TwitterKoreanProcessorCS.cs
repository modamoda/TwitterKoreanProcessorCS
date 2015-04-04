/*
The MIT License (MIT)

Copyright (c) 2015 modamoda

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moda.Korean.TwitterKoreanProcessorCS
{
    using com.twitter.penguin.korean;
    using ikvm.extensions;

    public static class TwitterKoreanProcessorCS
    {
        static TwitterKoreanProcessorCS()
        {
            // http://sourceforge.net/p/ikvm/mailman/message/31712591/
            Dictionary<string, string> props = new Dictionary<string, string>();
            props.Add("file.encoding", "UTF-8");
            ikvm.runtime.Startup.setProperties(props);
        }

        public static string Normalize(string text)
        {
            string result = TwitterKoreanProcessor.normalize(text).toString();
            return result;
        }

        public static StemmedTextWithTokens Stem(string text)
        {
            var result = TwitterKoreanProcessor.stem(text);
            var temp = new StemmedTextWithTokens(result);

            return temp;
            throw new NotImplementedException();
        }

        public static IEnumerable<string> TokenizeToStrings(string text, bool normalize = true, bool stem = true, bool keepSpace = false)
        {
            var result = TwitterKoreanProcessor.tokenize(text, normalize, stem, keepSpace);

            List<string> results = new List<string>();
            for (int i = 0; i < result.size(); i++)
            {
                var midResult = result.apply(i) as com.twitter.penguin.korean.tokenizer.KoreanTokenizer.KoreanToken;
                string s = midResult.text();
                results.Add(s);
            }

            return results;
        }
    }


    public class StemmedTextWithTokens
    {
        public string Text { get; set; }
        public List<KoreanToken> Tokens { get; set; }

        public StemmedTextWithTokens(com.twitter.penguin.korean.stemmer.KoreanStemmer.StemmedTextWithTokens scalaObject)
        {
            this.Text = scalaObject.text().toString();
            this.Tokens = new List<KoreanToken>();

            scala.collection.Seq temps = scalaObject.tokens();
            for (int i = 0; i < temps.size(); i++)
            {
                var temp = temps.apply(i) as com.twitter.penguin.korean.tokenizer.KoreanTokenizer.KoreanToken;
                var koreanToken = new KoreanToken(temp);
                this.Tokens.Add(koreanToken);
            }
        }
    }

    public class KoreanToken
    {
        public string Text { get; set; }
        public KoreanPos Pos { get; set; }
        public bool Unknown { get; set; }

        public KoreanToken(com.twitter.penguin.korean.tokenizer.KoreanTokenizer.KoreanToken scalaToken)
        {
            this.Text = scalaToken.text().toString();
            this.Pos = (KoreanPos)scalaToken.pos().id();
            this.Unknown = scalaToken.unknown();
        }
    }

    public enum KoreanPos
    {
        // Word leved POS
        Noun, Verb, Adjective,
        Adverb, Determiner, Exclamation,
        Josa, Eomi, PreEomi, Conjunction,
        NounPrefix, VerbPrefix, Suffix, Unknown,

        // Chunk level POS
        Korean, Foreign, Number, KoreanParticle, Alpha,
        Punctuation, Hashtag, ScreenName,
        Email, URL, CashTag,

        // Functional POS
        Space, Others
    }
}

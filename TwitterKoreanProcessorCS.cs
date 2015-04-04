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

namespace Moda.Korean.TwitterKoreanProcessorCS
{
    using com.twitter.penguin.korean;
    using com.twitter.penguin.korean.tokenizer;
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
            var scalaResult = TwitterKoreanProcessor.stem(text);
            var result = new StemmedTextWithTokens(scalaResult);

            return result;
        }

        public static IEnumerable<KoreanToken> Tokenize(string text, bool normalize= true, bool stem = true, bool keepSpace = false)
        {
            var scalaResults = TwitterKoreanProcessor.tokenize(text, normalize, stem, keepSpace);
            List<KoreanToken> results = ScalaSeqConverter<KoreanToken, KoreanTokenizer.KoreanToken>(
                scalaResults, (scalaResult) => { return new KoreanToken(scalaResult); });

            return results;
        }

        public static IEnumerable<string> TokenizeToStrings(string text, bool normalize = true, bool stem = true, bool keepSpace = false)
        {
            var scalaResults = TwitterKoreanProcessor.tokenize(text, normalize, stem, keepSpace);
            List<string> results = ScalaSeqConverter<string, KoreanTokenizer.KoreanToken>(
                scalaResults, (scalaResult) => { return scalaResult.toString(); });

            return results;
        }

        public static List<KoreanSegment> TokenizeWithIndex(string text)
        {
            throw new NotImplementedException();
        }

        public static KoreanSegmentWithText TokenizeWithIndexWithStemmer(string text)
        {
            throw new NotImplementedException();
        }

        public static List<string> ExtractPhrases(string text, bool filterSpam = false)
        {
            var scalaResults = TwitterKoreanProcessor.extractPhrases(text, filterSpam);
            List<string> results = ScalaSeqStringConverter(scalaResults);
            
            return results;
        }

        private static List<T> ScalaSeqConverter<T, TScala>(scala.collection.Seq sequences, Func<TScala,T> convertFunc)
            where TScala : class
            where T : class
        {
            List<T> results = new List<T>();

            for (int i = 0; i < sequences.size(); i++)
            {
                TScala scalaResult = sequences.apply(i) as TScala;
                T result = convertFunc(scalaResult);
                results.Add(result);
            }

            return results;
        }


        private static List<string> ScalaSeqStringConverter(scala.collection.Seq sequences)
        {
            List<string> results = new List<string>();

            for (int i = 0; i < sequences.size(); i++)
            {
                var scalaResult = sequences.apply(i);
                string result = scalaResult.toString();
                results.Add(result);
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

    public class KoreanSegmentWithText
    {
        public string Text { get; set; }
        public List<KoreanSegment> Segments { get; set; }
    }

    public class KoreanSegment
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public KoreanToken token { get; set; }
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

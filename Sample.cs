using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moda.Korean.TwitterKoreanProcessorCS.Sample
{
    using Moda.Korean.TwitterKoreanProcessorCS;

    public class Sample
    {
        public string NormalizeSample()
        {
            string result = TwitterKoreanProcessorCS.Normalize("정규화(Normalize) 예제입니당~");

            // "정규화(Normalize) 예제입니다~"
            return result;
        }

        public string TokenizeSample1()
        {
            StringBuilder result = new StringBuilder();

            var tokens = TwitterKoreanProcessorCS.Tokenize("토큰화를 처리하는 예제입니다");
            foreach (var token in tokens)
            {
                result.AppendFormat(format: "{0}({1}) [{2},{3}] / ",
                    args: new object[] { token.Text, token.Pos.ToString(), token.Offset, token.Length });
            }

            // 토큰(ProperNoun) [0,2] / 화(Suffix) [2,1] / 를(Josa) [3,1] /  ... / 입니(Adjective) [12,2] / 다(Eomi) [14,1] /
            return result.ToString();
        }

        public string TokensToStringsSample1()
        {
            var tokens = TwitterKoreanProcessorCS.Tokenize("토큰화를 처리하는 예제입니다. 문자열화는 덤");
            var results = TwitterKoreanProcessorCS.TokensToStrings(tokens);

            // 토큰 / 화 / 를 / 처리 / 하는 / 예제 / 입니 / 다 / . / 문자열 / 화 / 는 / 덤
            return string.Join(" / ", results);
        }

        public string StemSample1()
        {
            StringBuilder result = new StringBuilder();

            var tokens = TwitterKoreanProcessorCS.Tokenize("토큰화 이후 어근화를 처리하는 예제입니다");
            var stemmedTokens = TwitterKoreanProcessorCS.Stem(tokens);

            foreach (var stemmedToken in stemmedTokens)
            {
                result.AppendFormat(format: "{0}({1}) [{2},{3}] / ",
                    args: new object[] { stemmedToken.Text, stemmedToken.Pos.ToString(), stemmedToken.Offset, stemmedToken.Length });
            }

            // 토큰(ProperNoun) [0,2] / 화(Suffix) [2,1] /  (Space) [3,1] / 이후(Noun) [4,2] / ... / 예제(Noun) [17,2] / 이다(Adjective) [19,3] /
            return result.ToString();
        }

        public string ExtractPhraseSample1()
        {
            StringBuilder result = new StringBuilder();

            var tokens = TwitterKoreanProcessorCS.Tokenize("토큰화 처리 이후 어구를 추출하는 예제입니당ㅇㅇㅇ");
            var phrases = TwitterKoreanProcessorCS.ExtractPhrases(tokens);

            foreach (var phrase in phrases)
            {
                result.AppendLine("---------");
                result.AppendFormat("{0} | ", phrase.Pos.ToString());
                foreach (var token in phrase.Tokens)
                {
                    result.AppendFormat(format: "{0}({1}) [{2},{3}] / ",
                        args: new object[] { token.Text, token.Pos.ToString(), token.Offset, token.Length });
                }
                result.AppendLine();
            }

            // Noun | 토큰(ProperNoun) [0,2] /
            // Noun | 처리(Noun) [4,2] /
            // ...
            // Noun | 어구(Noun) [10,2] /
            return result.ToString();
        }
    }
}

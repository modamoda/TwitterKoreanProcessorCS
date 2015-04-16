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

        public string StemSample()
        {
            StemmedTextWithTokens stemResult = TwitterKoreanProcessorCS.Stem("어근화를 처리하는 예제입니다");

            // "어근화를 처리하다 예제이다"
            return stemResult.Text;
        }

        public string TokenizeSample0()
        {
            var tokenizedResults = TwitterKoreanProcessorCS.TokenizeToStrings("토큰화의 기본설정은 정규화와 어근화를 진행하는것입니닼ㅋㅋ");

            // "토큰Noun, 화의Noun, 기본Noun, 설정Noun, 은Josa, 정규화Noun, 와Josa, 어근Noun, 화Suffix, 를Josa, 진행Noun, 하다Verb, 이다Adjective, ㅋㅋKoreanParticle"
            return string.Join(", ", tokenizedResults);
        }

        public string TokenizeSample1()
        {
            var tokenizedWithoutNormalizationResults = TwitterKoreanProcessorCS.TokenizeToStrings(text: "정규화를 하지 않은 토큰화 예제입니당", normalize: false);

            // "정규화Noun, 를Josa, 하다Verb, 않다Verb, 토큰Noun, 화Suffix, 예제Noun, 이다Adjective"
            return string.Join(", ", tokenizedWithoutNormalizationResults);
        }

        public string TokenizeSample2()
        {
            var tokenizedWithoutStemResults = TwitterKoreanProcessorCS.TokenizeToStrings(text: "어근화를 하지 않은 토큰화 예제입니당ㅇㅇㅇ", stem: false);

            // "어근Noun, 화Suffix, 를Josa, 하지Verb, 않은Verb, 토큰Noun, 화Suffix, 예제Noun, 입니Adjective, 다Eomi, ㅇㅇKoreanParticle"
            return string.Join(", ", tokenizedWithoutStemResults);
        }

        public string ExtractPhraseSample()
        {
            var phrases = TwitterKoreanProcessorCS.ExtractPhrases("어구추출기능도 가능해유ㅠㅠㅠ", filterSpam: true);

            // "어구추출기능, 가능행, 어구, 추출, 기능, 가능"
            return string.Join(", ", phrases);
        }
    }
}

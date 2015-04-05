# TwitterKoreanProcessorCS
C# interface to [twitter-korean-text](https://github.com/twitter/twitter-korean-text) by [Twitter](https://github.com/twitter/) / 트위터에서 제공하는 한국어 형태소 분석기인 [twitter-korean-text](https://github.com/twitter/twitter-korean-text)를 C#에서 사용 가능하도록 wrapping 한 라이브러리입니다.

## Usage
### Installation
Download from Nuget / Nuget을 이용하여 다운로드가 가능합니다.

    PM> Install-Package Moda.Korean.TwitterKoreanProcessorCS

### Sample Code
    var results = TwitterKoreanProcessorCS.TokenizeToStrings("형태소 분석을 합니닼ㅋㅋㅋㅋㅋㅋ");
    Console.WriteLine(string.Join(" / ", results));
    // 형태소Noun / 분석Noun / 을Josa / 하다Verb / ㅋㅋKoreanParticle

## Note
I really appreciate to Twtitter's contribution to Korean, who opensourced Korean morpheme analyzer / 한국어의 활용능력을 극대화 할 수 있도록 형태소 분석기를 오픈소스화 해 주신 트위터에 감사의 말씀을 드립니다.

## Build Status
[![Build status](https://ci.appveyor.com/api/projects/status/0q2fuf31ne2uehh2?svg=true)](https://ci.appveyor.com/project/modamoda/twitterkoreanprocessorcs)

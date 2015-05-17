# TwitterKoreanProcessorCS
C# interface to [twitter-korean-text](https://github.com/twitter/twitter-korean-text) by [Twitter](https://github.com/twitter/) / 트위터에서 제공하는 한국어 형태소 분석기인 [twitter-korean-text](https://github.com/twitter/twitter-korean-text)를 C#에서 사용 가능하도록 wrapping 한 라이브러리입니다.

## Compatibility
Currently wraps [twitter-korean-text 4.1](https://github.com/twitter/twitter-korean-text/releases/tag/korean-text-4.1) / 현재 이 프로젝트는 [twitter-korean-text 4.1](https://github.com/twitter/twitter-korean-text/releases/tag/korean-text-4.1)을 사용중입니다.

## Usage
### Installation
Download from Nuget / Nuget을 이용하여 다운로드가 가능합니다.

    PM> Install-Package Moda.Korean.TwitterKoreanProcessorCS

### Sample Code

    var results = TwitterKoreanProcessorCS.TokenizeToStrings("형태소 분석을 합니닼ㅋㅋㅋㅋㅋㅋ");
    Console.WriteLine(string.Join(" / ", results));
    // 형태소Noun / 분석Noun / 을Josa / 하다Verb / ㅋㅋKoreanParticle

Please see [`Sample.cs`](https://github.com/modamoda/TwitterKoreanProcessorCS/blob/master/Sample.cs) for more sample usages. / [`Sample.cs`](https://github.com/modamoda/TwitterKoreanProcessorCS/blob/master/Sample.cs) 파일에서 여러 예제를 확인하실 수 있습니다.

## How to Contribute
This project is focusing on _wrapping_. If you want to improve natural language processing quality, please visit [twitter-korean-text](https://github.com/twitter/twitter-korean-text). / 이 프로젝트는 wrapping에 주안점을 두고 있습니다. 자연어 처리능력 향상과 관련된 내용은 [twitter-korean-text](https://github.com/twitter/twitter-korean-text)에서 제안/공헌이 가능합니다.

To make a contribution to this project, you can:
- Discuss on [twitter-korean-text Google Groups](https://groups.google.com/forum/#!topic/twitter-korean-text/2YpG_BR2ZYM) / 구글 그룹스에 개설된 [twitter-korean-text](https://groups.google.com/forum/#!topic/twitter-korean-text/2YpG_BR2ZYM)에서 의견을 개진해 주세요.
- Open an issue (reporting bugs, suggestion of features/improvements) / 버그를 발견하셨거나 개선점을 제안하실 경우 issue를 생성해 주시면 감사드리겠습니다.
- Creating pull request (bugfix, sample codes) / 직접 버그를 해결하셨거나, 좋은 예제코드를 작성하신 경우 pull request를 생성해 주세요.

## Note
I really appreciate to [Will Hohyon Ryu](https://github.com/nlpenguin) and Twtitter, who are making great contribution to Korean by opensourcing Korean morpheme analyzer. / 한국어의 활용능력을 극대화 할 수 있도록 형태소 분석기를 오픈소스화 해 주신 트위터와 유호현님께 에 감사의 말씀을 드립니다.

## Build Status
[![Build status](https://ci.appveyor.com/api/projects/status/0q2fuf31ne2uehh2?svg=true)](https://ci.appveyor.com/project/modamoda/twitterkoreanprocessorcs)

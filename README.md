# voxsay2

音声合成製品 VOICEVOX、VOICEVOX Nemo、COEIROINK/v2、LMROID、SHAREVOX、ITVOICE、 のREST APIを呼び出して音声再生するCUIクライアント

2024-05-03 
元ネタの[voxsay](https://github.com/k896951/voxsay)も人によっては需要があるかもしれません。

## 使用方法

[こちら](https://github.com/k896951/voxsay/wiki)を参照してください。voxsayとほぼ同じです。

違いは
 - SAPI を使えない
 - 出力デバイスを指定できない
 - .NET8なのでLinux, macOSでも利用できる（かもしれない）
 - soxコマンドを使った再生ができる（指定時）

となります。

使用前に、各音声合成製品を起動します。
その後にvoxsay2.exeをオプション無しで実行するとヘルプが出ます。
```
f:\sandbox>voxsay2

voxsay command 2.0.1 (c)2024 by k896951

talk command line exsamples:
    voxsay -prodlist
    voxsay <-prod TTS> [Options1] -list
    voxsay <-prod TTS> [Options1] [-save FILENAME] <-index N> [Options2] -t  TALKTEXT
    voxsay <-prod TTS> [Options1] [-save FILENAME] <-index N> [Options2] [ -mf | -sf ] TEXTFILE

sing command line exsamples (VOICEVOX ONLY):
    voxsay -prod voicevox -renderingmode sing [Options1] -teacherlist
    voxsay -prod voicevox -renderingmode sing [Options1] -list
    voxsay -prod voicevox -renderingmode sing [Options1] [-save FILENAME] <-index N> [-teacherindex N] [Options2] -t  TALKTEXT
    voxsay -prod voicevox -renderingmode sing [Options1] [-save FILENAME] <-index N> [-teacherindex N] [Options2] [ -mf | -sf ] TEXTFILE

Note:
    * The "-renderingmode sing" option is only for VOICEVOX.


-t,-mf,-sf and -save combination:
    -save sample.wav -t text       : Output sample.wav
    -save sample.wav -mf textfile  : Output sample.wav
    -save sample.wav -sf textfile  : Output sample_000001.wav, sample_000002.wav, …　Outputs a wav file for the number of lines in the textfile.


help command line for Options1, Options2:
    voxsay -help talk
    voxsay -help sing

f:\sandbox>
```

ローカルで稼働している製品一覧を確認します。
```
f:\sandbox>voxsay2 -prodlist
product: sharevox
product: voicevox
product: coeiroinkv2

f:\sandbox>
```

SHAREVOXの話者一覧でインデクスを確認します。
```
f:\sandbox>voxsay2 -prod sharevox -list
index: 0,  speaker:小春音アミ（ノーマル）
index: 1,  speaker:小春音アミ（喜び）
index: 2,  speaker:小春音アミ（怒り）
index: 3,  speaker:小春音アミ（悲しみ）
index: 4,  speaker:つくよみちゃん（おしとやか）
index: 5,  speaker:白痴ー/黒聡鵜月（虚偽）
index: 6,  speaker:Yくん/開発者（ノーマル）
index: 7,  speaker:小春音アミ（ノーマルv2）
index: 8,  speaker:小春音アミ（喜びv2）
index: 9,  speaker:小春音アミ（怒りv2）
index: 10,  speaker:小春音アミ（悲しみv2）
index: 11,  speaker:つくよみちゃん（おしとやかv2）
index: 12,  speaker:Yくん/開発者（ノーマルv2）
index: 13,  speaker:白痴ー/黒聡鵜月（虚偽v2）
index: 14,  speaker:小春音アミ（ノーマルv3）
index: 15,  speaker:小春音アミ（喜びv3）
index: 16,  speaker:小春音アミ（怒りv3）
index: 17,  speaker:小春音アミ（悲しみv3）
index: 18,  speaker:つくよみちゃん（おしとやかv3）
index: 19,  speaker:Yくん/開発者（ノーマルv3）
index: 20,  speaker:白痴ー/黒聡鵜月（虚偽v3）
index: 21,  speaker:らごぱすブラック（ノーマル）
index: 22,  speaker:らごぱすホワイト（ノーマル）
index: 23,  speaker:風花ゆき（ノーマル）
index: 24,  speaker:安倍広葉（ノーマル）
index: 25,  speaker:鈴乃（ノーマル）

f:\sandbox>
```

鈴乃（ノーマル）に呟いてもらいます。

```
f:\sandbox>voxsay2 -prod sharevox -index 25 -t 早く寝てください！

f:\sandbox>
```

音量が大きい気がしたので下げます。

```
f:\sandbox>voxsay2 -prod sharevox -index 25 -volume 0.5 -t 早く寝てください！

f:\sandbox>
```

オプション -prod voicevox, -renderingmode sing, を指定すると、VOICEVOX 0.16.1で利用可能になった歌唱APIを使って歌わせることができます。

オプション -teacherlist で歌唱指導歌手のインデクスを確認します。
```
f:\sandbox>voxsay2 -prod voicevox -renderingmode sing -teacherlist
index: 6000,  speaker:波音リツ（ノーマル）

f:\sandbox>
```

オプション -list で歌手のインデクスを確認します。-renderingmode talk の時と番号が異なるので注意してください。
```
f:\sandbox>voxsay2 -prod voicevox -renderingmode sing -renderingmode sing -list
index: 3000,  speaker:四国めたん（あまあま）
index: 3001,  speaker:ずんだもん（あまあま）
index: 3002,  speaker:四国めたん（ノーマル）
index: 3003,  speaker:ずんだもん（ノーマル）
index: 3004,  speaker:四国めたん（セクシー）
index: 3005,  speaker:ずんだもん（セクシー）
index: 3006,  speaker:四国めたん（ツンツン）
index: 3007,  speaker:ずんだもん（ツンツン）
index: 3008,  speaker:春日部つむぎ（ノーマル）
index: 3009,  speaker:波音リツ（ノーマル）
index: 3010,  speaker:雨晴はう（ノーマル）
index: 3011,  speaker:玄野武宏（ノーマル）
index: 3012,  speaker:白上虎太郎（ふつう）
index: 3013,  speaker:青山龍星（ノーマル）
index: 3014,  speaker:冥鳴ひまり（ノーマル）
index: 3015,  speaker:九州そら（あまあま）
index: 3016,  speaker:九州そら（ノーマル）
index: 3017,  speaker:九州そら（セクシー）
index: 3018,  speaker:九州そら（ツンツン）
index: 3020,  speaker:もち子さん（ノーマル）
index: 3021,  speaker:剣崎雌雄（ノーマル）
index: 3023,  speaker:WhiteCUL（ノーマル）
index: 3024,  speaker:WhiteCUL（たのしい）
index: 3025,  speaker:WhiteCUL（かなしい）
index: 3026,  speaker:WhiteCUL（びえーん）
index: 3027,  speaker:後鬼（人間ver.）
index: 3028,  speaker:後鬼（ぬいぐるみver.）
index: 3029,  speaker:No.7（ノーマル）
index: 3030,  speaker:No.7（アナウンス）
index: 3031,  speaker:No.7（読み聞かせ）
index: 3032,  speaker:白上虎太郎（わーい）
index: 3033,  speaker:白上虎太郎（びくびく）
index: 3034,  speaker:白上虎太郎（おこ）
index: 3035,  speaker:白上虎太郎（びえーん）
index: 3037,  speaker:四国めたん（ヒソヒソ）
index: 3038,  speaker:ずんだもん（ヒソヒソ）
index: 3039,  speaker:玄野武宏（喜び）
index: 3040,  speaker:玄野武宏（ツンギレ）
index: 3041,  speaker:玄野武宏（悲しみ）
index: 3042,  speaker:ちび式じい（ノーマル）
index: 3043,  speaker:櫻歌ミコ（ノーマル）
index: 3044,  speaker:櫻歌ミコ（第二形態）
index: 3045,  speaker:櫻歌ミコ（ロリ）
index: 3046,  speaker:小夜/SAYO（ノーマル）
index: 3047,  speaker:ナースロボ＿タイプＴ（ノーマル）
index: 3048,  speaker:ナースロボ＿タイプＴ（楽々）
index: 3049,  speaker:ナースロボ＿タイプＴ（恐怖）
index: 3065,  speaker:波音リツ（クイーン）
index: 3066,  speaker:もち子さん（セクシー／あん子）
index: 3075,  speaker:ずんだもん（ヘロヘロ）
index: 3076,  speaker:ずんだもん（なみだめ）
index: 3077,  speaker:もち子さん（泣き）
index: 3078,  speaker:もち子さん（怒り）
index: 3079,  speaker:もち子さん（喜び）
index: 3080,  speaker:もち子さん（のんびり）
index: 3081,  speaker:青山龍星（熱血）
index: 3082,  speaker:青山龍星（不機嫌）
index: 3083,  speaker:青山龍星（喜び）
index: 3084,  speaker:青山龍星（しっとり）
index: 3085,  speaker:青山龍星（かなしみ）
index: 6000,  speaker:波音リツ（ノーマル）

f:\sandbox>
```
春日部つむぎにドレミファソラシドを歌ってもらいましょう。
```
f:\sandbox>voxsay2 -prod voicevox -renderingmode sing -index 3008 -teacherindex 6000 -t O4CDEFGABO5C

f:\sandbox>
```
-t オプションで MMLを指定する事で歌唱が可能になります。ただし正確な実装ではありません。それらしいように仕上げただけです。


### 定義ファイル

voxsay2.exeと同じフォルダに作成したJSONファイル voxsay2conf.json に定義をします。
```
{
  "defaultSetting": {
    "prod": "voicevox",
    "host": null,
    "port": null,
    "speed": null,
    "pitch": null,
    "intonation": null,
    "volume": null,
    "prephonemelength": 0.50,
    "postphonemelength": 0.50,
    "samplingrate": 24000,
    "index": null,
    "renderingmode": "sing",
    "teacherindex": 6000, 
    "mf": null,
    "sf": null
  },

  "soundSetting": {
    "method": "dotnet",
    "command": "sox",
    "audiodriver": "waveaudio",
    "frontopts": [
      "-q"
    ],
    "rearopts": [
      "-d"
    ]
  }
}
```
この例だと、オプションを指定して上書きしない限り、voxsay2 は
```
-prod voicevox -prephonemelength 0.50 -postphonemelength 0.50 -samplingrate 24000 -renderingmode sing -teacherindex 6000
```
が指定されたものとして動作します。

#### 再生方法指定

voxsay2conf.jsonのプロパティ soundSetting の設定は、音声再生方法の指定になります。

"method"が"dotnet"の時は.NETがSystem.Windows.Extensionsで提供する System.Media.SoundPlayerクラスを使って再生します。
```
  "soundSetting": {
    "method": "dotnet",
    "audiodriver": "",
    "command": "",
    "frontopts": [],
    "rearopts": []
  }
```

現状、System.Media.SoundPlayerクラスはWindowsでのみ機能すると警告されていますが私はそれを確認できません。

その為、"method"を"sox"にした際は、外部コマンド sox を利用した再生を行うようにしてあります。
```
  "soundSetting": {
    "method": "sox",
    "command": "sox",
    "audiodriver": "waveaudio",
    "frontopts": [
      "-q"
    ],
    "rearopts": [
      "-d"
    ]
  }
```

この例は、取得した音声ファイルを以下のコマンドラインでバックグラウンド実行します。
```
sox -q 音声ファイル -d
```

WindowsでもWin版soxコマンドをインストールして利用が可能です。
```
  "soundSetting": {
    "method": "sox",
    "command": "D:\\Program Files (x86)\\sox-14-4-2\\sox.exe",
    "audiodriver": "waveaudio",
    "frontopts": [
      "-q"
    ],
    "rearopts": [
      "-d"
    ]
  }
```

この例は、取得した音声ファイルを以下のコマンドラインでバックグラウンド実行します。
```
D:\Program Files (x86)\sox-14-4-2\sox.exe -q 音声ファイル -d
```


## 使用しているサードパーティライブラリとライセンス

### Microsoft

以下は MITライセンスで提供されています。

- System.Windows.Extensions	8.0.0

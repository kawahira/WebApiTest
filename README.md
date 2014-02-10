httpTest
========

Http経由のテストコードを作成するための汎用処理とその処理群のテストコードです。

テストアプリの使い方

consoleConnection.exe http://google.co.jp

とするとリクエストしたURLのレスポンスデータをconsole表示します。
またエラーレベルに StatusCodeを返します。
（なので正常終了時に200が返ってくる仕様です）

Jenkinsなどで利用する場合には注意が必要です。

# Docker構成
```
ロードバランサ (Nginx)
    ┣─ REST APIサーバー1
    │   ┣─ DB(MySQL)
    │   ┗─ Redis(Pub/Sub)
    ┣─ REST APIサーバー2
    │   ┗─ DB(MySQL)
    │   ┗─ Redis(Pub/Sub)
    ┣─ MagicOnionサーバー1
    ┗─ MagicOnionサーバー2

日次バッチ
┣─ DB(MySQL)
└─ S3(localのみ互換のストレージを別途立ててエミュレート)

```
# 各サーバー説明
## REST APIサーバー
### ディレクトリ
* api-server

### 用途
* 業務チェック
* DBへの検索・保存(グループ、移動情報、各オブジェクト)
* 認証

## 日次バッチ
### ディレクトリ
* daily-batch

### 用途
* 日次でDBからデータを読んでマップデータをS3にアップロード

## MagicOnionサーバー
### ディレクトリ
* realtime-server

### 用途
* 同じ接続同士のユーザーにPtoPでデータを通知
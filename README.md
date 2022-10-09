#XFreeCell
新接龍, X是指仿XP樣式，因為XP版的無法很好的隨著螢幕大小縮放，所以試著寫看看。

1.1
修正
* 連點2下選牌有bug
* 遊戲失敗選相同排局，但還是要讓我挑一個號碼開始
* 結束:先跳出要不要重完畫面，而不是先清空牌面
* StripMenu顯示剩餘張數(float-right)
* 程式放到桌面沒有icon
* 點選取消，目前點擊間隔1秒為取消，似乎太長，改成0.8秒試試

## TODO
雖然有想比照AboutGameDialogForm直接從ClientSize.Width 來決定顯示視窗的大小
來調整其它的Dialg，但先擱置。 原本用_raito，但resize也沒去更新

## feature
* 記錄遊戲完成 / 賽季
* 自動更新
* 偵測本局是否可完成，或自動完成功能


## bug
* "推估是否完成的機制"有bug，似乎沒測到可移到homecells的

## minor
* 移到不能移的Column，需要顯示此步犯規
* 點了卡片，遊標移到Foundations，Cousor會切換成ArrowUp
* 移動卡頓感太重
* 牌色不好看，使用者懷念舊的普克牌花色
* A無法顯示花色
* 自動往Homecells過於自動
* menu字太小



## 建置指令
dotnet publish -r win-x64 -c Release -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:UseAppHost=true --self-contained

deck card images from 
+ https://github.com/crobertsbmw/deckofcards
規則與參考命名
+ https://www.wikihow.com/Play-FreeCell-Solitaire
png 轉 icon 
https://www.icoconverter.com/
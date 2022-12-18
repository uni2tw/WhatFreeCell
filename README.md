#XFreeCell
新接龍, X是指仿XP樣式，因為XP版的無法很好的隨著螢幕大小縮放，所以試著寫看看。

## 1.1
### 修正
* 連點2下選牌有bug
* 遊戲失敗選相同排局，但還是要讓我挑一個號碼開始
* 結束:先跳出要不要重完畫面，而不是先清空牌面
* StripMenu顯示剩餘張數(float-right)
* 程式放到桌面沒有icon
* 點選取消，目前點擊間隔1秒為取消，似乎太長，改成0.8秒試試
* 自動完成過於自動, 未考慮如果牌還被foundactions需要的情境
* menu字太小

---

## 1.2
### Improvement
* 移動卡頓感太重
* 點選牌，遊標移到其它Column，滑鼠游標要變更，以提示可移動

### Bug
* "推估是否可完成的機制"有bug，似乎沒測到可移到homecells的
* 移動失敗的應該要取消選取

---

## 1.3
* 會記錄遊戲記錄

### bug
* 修正GetPossibleSituationsg，沒有計算移到 homecells, 造成判斷遊戲結束的邏輯有錯誤



# Future



## Task
* 錄遊戲完成/時間/花費手續
* 賽季
* 自動更新
* 需偵測本局是否可完成，或自動完成功能
* 錄遊戲完成/時間/花費手續

# Minor
* 移到不能移的Column，需要顯示此步犯規 (low)
* 牌色不好看，使用者懷念舊的普克牌花色  (low)
* A無法顯示花色 (low)
* 雖然有想比照AboutGameDialogForm直接從ClientSize.Width 來決定顯示視窗的大小來調整其它的Dialg，但先擱置。 原本用_raito，但resize也沒去更新  (low)
TODO
收攏 move 並記錄 tracks




---

要知道是否重複玩 過去記錄，挑戰過去  
要知道最近玩了哪一關  
留下隻字片語，提醒下一次玩的自己
評分

RSA
-- https://www.zhihu.com/question/25912483  
-- https://www.c-sharpcorner.com/UploadFile/75a48f/rsa-algorithm-with-C-Sharp2/

## 建置指令
- dotnet publish -r win-x64 -c Release -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:UseAppHost=true --self-contained
 - -p:PublishTrimmed=true 上次用會失敗

deck card images from 
+ https://github.com/crobertsbmw/deckofcards
規則與參考命名
+ https://www.wikihow.com/Play-FreeCell-Solitaire
png 轉 icon 
+ https://www.icoconverter.com/

關於 refresh 不要亂用
+ http://www.vb-helper.com/tip_refresh_versus_invalidate.html
#XFreeCell
就仿xp的新接龍

## TODO
雖然有想比照AboutGameDialogForm直接從ClientSize.Width 來決定顯示視窗的大小
來調整其它的Dialg，但先擱置。 原本用_raito，但resize也沒去更新

## bug
* 結束:先跳出要不要重完畫面，而不是先清空牌面
* 推估完成有bug，似乎沒測到移到homecells的
* 連點2下選牌有bug

## 建置指令
dotnet publish -r win-x64 -c Release -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:UseAppHost=true --self-contained

deck card images from 
+ https://github.com/crobertsbmw/deckofcards
規則與參考命名
+ https://www.wikihow.com/Play-FreeCell-Solitaire
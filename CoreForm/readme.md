當點擊card時，要觸發所屬的zone的holdeclick事件


遊戲流程
點擊wait區的slot
	wait無任何slot有選取牌
		被選取的slot最後一張牌已被選取
			(a)slot最後一張牌會被取消選取
		被選取的slot最後一張牌未被選取
			(a)該slot最後一張牌會被選取
	wait其它slot有選取牌(selectedSlot有值)
		(i)條件允許
			(a)選目前點選的牌，搬移至先前選取牌的後面
		(i)條件不允許
			(a)顯示此步犯規，並發出警示聲
			
	
雙擊wait區的slot 
	(i)wait無任何slot有選取牌 或有牌被選取且跟目前雙擊是同一個slot
		(i)如temp區未滿
			(a)點選的牌移至temp區		
		(i)如temp區已滿
			(a)slot最後一張牌會被取消選取
	(e)沒事發生

(w)點擊或雙擊wait區後，(a)自動將wait可能完成的牌，放置完成區



點擊temp區的slot


點擊complete區slot
 不回應

deck card images from 
https://github.com/crobertsbmw/deckofcards
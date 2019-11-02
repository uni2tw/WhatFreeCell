using System;
using System.Drawing;
using System.IO;

namespace CoreForm
{
    /// <summary>
    /// 一張牌
    /// </summary>
    public class Card
    {
        public int Number { get; set; }
        public CardSuit Suit { get; set; }
        public Image Image { get; set; }

        public override string ToString()
        {
            if (Suit == CardSuit.Spare)
            {
                return "黑桃 " + Number;
            }
            if (Suit == CardSuit.Heart)
            {
                return "紅心 " + Number;
            }
            if (Suit == CardSuit.Diamond)
            {
                return "方塊 " + Number;
            }
            if (Suit == CardSuit.Club)
            {
                return "梅花 " + Number;
            }
            throw new Exception("Card資料不正確");
        }

        public string GetImageFileName()
        {
            return string.Format("{0}-{1}.png", Suit, Number.ToString("00"));
        }

        public void ReloadImage()
        {
            var assembly = System.Reflection.Assembly.GetEntryAssembly();
            Stream resource = assembly
                .GetManifestResourceStream("CoreForm.assets.img." + GetImageFileName());
            Image img = Image.FromStream(resource);
            this.Image = img;
        }
    }
   
}

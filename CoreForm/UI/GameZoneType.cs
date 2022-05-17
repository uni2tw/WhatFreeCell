using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CoreForm.UI
{
    public enum GameZoneType
    {
        None, 
        /// <summary>
        /// 左上，暫存交換用
        /// </summary>
        Temp, 
        Completion, 
        /// <summary>
        /// 下方，工作區
        /// </summary>
        Waiting
    }
}

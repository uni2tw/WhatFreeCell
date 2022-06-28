namespace FreeCellSolitaire.Core.GameModels
{
    public enum ColumnType
    {
        None,
        /// <summary>
        /// 左上，暫存交換用
        /// </summary>
        Foundation,
        FreeCell,
        /// <summary>
        /// 下方，工作區
        /// </summary>
        Tableau
    }
}

/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using System;
using System.Collections.Generic;

namespace FancyScrollView.Example08_1
{
    class Context : FancyGridViewContext
    {
        public int SelectedIndex = -1;
        public Action<int> OnCellClicked;
        /// <summary>
        /// 达到选择上限
        /// </summary>
        public bool IsLimitSelect;
        //已选择的所有id
        public List<int> SelectIndexList = new List<int>();
    }
}

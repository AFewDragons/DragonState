using System;
using System.Collections.Generic;

namespace AFewDragons
{
    [Serializable]
    public class DragonStateComparisonList
    {
        public List<DragonStateComparison> Items;

        public bool Check()
        {
            foreach (var item in Items)
            {
                if (!item.Check())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
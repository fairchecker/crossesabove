using System.Collections.Generic;

namespace Interfaces
{
    public interface IBuffable
    {
        List<IBuff> Buffs { get; set; }
        
        public IBuff FindBuffByType(IBuff.BuffType buffType);
        public void AddBuff(IBuff buff);
        public void RemoveBuff(IBuff buff);
    }
}
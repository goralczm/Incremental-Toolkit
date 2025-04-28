using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public interface ISavable
    {
        public List<(string, object)> SaveData();
    }
}

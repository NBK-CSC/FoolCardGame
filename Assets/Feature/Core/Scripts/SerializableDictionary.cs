using System;
using System.Collections.Generic;
using UnityEngine;

namespace FoolCardGame.Core
{
    /// <summary>
    /// Сирилизуемый словарь
    /// </summary>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    /// <typeparam name="TValue">Тип значения</typeparam>
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<KeyPairValue<TKey, TValue>> _dictonary = new List<KeyPairValue<TKey, TValue>>();

        public void OnBeforeSerialize() => Clear();
        
        public void OnAfterDeserialize()
        {
            Clear();
            
            for (int i = 0; i < _dictonary.Count; i++)
            {
                var kvp = _dictonary[i];
                if (!ContainsKey(kvp.Key))
                    Add(kvp.Key, kvp.Value);
                else
                    throw new Exception($"Повторяются ключи в словаре");
            }
        }
    }
}
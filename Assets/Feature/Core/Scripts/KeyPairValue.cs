using System;
using UnityEngine;

namespace FoolCardGame.Core
{
    /// <summary>
    /// Сущность ключ значения для серилизованного словаря
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    [Serializable]
    public class KeyPairValue<K, V>
    {
        [SerializeField] private K _key;
        [SerializeField] private V _value;
        
        public K Key => _key;

        public V Value => _value;
        
        public KeyPairValue(K key, V value)
        {
            _key = key;
            _value = value;
        }
    }
}
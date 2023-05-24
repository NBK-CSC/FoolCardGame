using System;
using System.Collections.Generic;
using FoolCardGame.Behaviours;
using FoolCardGame.Network;
using FoolCardGame.Player.Abstractions.Views;
using UnityEngine;

namespace FoolCardGame.Rooms.Controllers
{
    /// <summary>
    /// Контроллер списка игроков
    /// </summary>
    public class PlayerListController
    {
        private readonly List<AbstractPlayerView> _list;
        private readonly PoolMono<AbstractPlayerView> _pool;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="count">Количество</param>
        /// <param name="prefab">Префаб игрока</param>
        /// <param name="parent">Родитель игрока</param>
        public PlayerListController(int count, AbstractPlayerView prefab, Transform parent)
        {
            _pool = new PoolMono<AbstractPlayerView>(count, prefab, parent);
            _list = new List<AbstractPlayerView>();
            
            for (int i = 0; i < count; i++)
                _list.Add(_pool.GetFreeObject(false));
        }

        /// <summary>
        /// Обновить лист игроков
        /// </summary>
        /// <param name="players">Лист игроков</param>
        public void UpdateList(IList<ClientData> players)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (i >= players.Count)
                {
                    _list[i].gameObject.SetActive(false);
                    continue;
                }

                _list[i].gameObject.SetActive(true);
                _list[i].SetName(players[i].Id);
                
                _list[i].SetStatus(players[i].State);
            }
        }
    }
}
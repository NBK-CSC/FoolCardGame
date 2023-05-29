using System.Collections.Generic;
using System.Linq;
using DarkRift;

namespace FoolCardGame.Network
{
    /// <summary>
    /// Данные комнаты
    /// </summary>
    public struct RoomData : IDarkRiftSerializable
    {
        public RoomConfig Config;
        public List<ClientData> Clients;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="config">Конфиг комнаты</param>
        public RoomData(RoomConfig config)
        {
            Config = config;
            Clients = new List<ClientData>(Config.MaxSlots);
        }
    
        public void Deserialize(DeserializeEvent e)
        {
            Config = e.Reader.ReadSerializable<RoomConfig>();
            Clients = e.Reader.ReadSerializables<ClientData>().ToList();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Config);
            e.Writer.Write(Clients.ToArray());
        }
    }
}
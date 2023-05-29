using DarkRift;

namespace FoolCardGame.Network
{
    /// <summary>
    /// Структура комнаты
    /// </summary>
    public struct RoomConfig : IDarkRiftSerializable
    {
        public string Id;
        public string Name;
        public byte Slots;
        public byte MaxSlots;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">Имя комнаты</param>
        /// <param name="slots">Количество слотов</param>
        /// <param name="maxSlots">Максимальное количество слотов</param>
        public RoomConfig(string id, string name, byte slots, byte maxSlots)
        {
            Id = id;
            Name = name;
            Slots = slots;
            MaxSlots = maxSlots;
        }

        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadString();
            Name = e.Reader.ReadString();
            Slots = e.Reader.ReadByte();
            MaxSlots = e.Reader.ReadByte();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.Write(Name);
            e.Writer.Write(Slots);
            e.Writer.Write(MaxSlots);
        }
    }
}
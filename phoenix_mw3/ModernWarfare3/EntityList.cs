using Phoenix.MemorySystem;
using Phoenix.ModernWarfare3.Structs;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.ModernWarfare3
{
    class EntityList
    {
        public List<Entity> Players;
		public List<ClientInfo> Clients;

        public Entity GetPlayerByIndex(int index)
        {
            return Players == null ? new Entity() { ClientNum = -1 } : Players.FirstOrDefault(player => player.ClientNum == index);
        }
		
        public Entity GetLocalPlayer()
        {
			var localPlayerIndex = Phoenix.Memory.Read<int>(SignatureManager.GetLocalIndex() + 0x150);
            return Players == null ? new Entity() : Players.FirstOrDefault(player => player.ClientNum == localPlayerIndex);
        }

		public ClientInfo GetLocalClient()
		{

			var localPlayerIndex = Phoenix.Memory.Read<int>(SignatureManager.GetLocalIndex() + 0x150);
			return Clients[localPlayerIndex];
		}
    }
}

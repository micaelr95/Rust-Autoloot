using Oxide.Core.Libraries.Covalence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("Autoloot", "micaelr95", 0.1)]
    [Description("Autoloot dead bodies")]
    public class Autoloot : RustPlugin
    {
        void OnLootEntity(BasePlayer player, BaseEntity entity)
        {
            LootableCorpse corpse = entity as LootableCorpse;
            if (!corpse) return;

            foreach (ItemContainer container in corpse.containers)
            {
                foreach (Item item in container.itemList.ToList())
                {
                    player.inventory.GiveItem(item);
                }
            }
        }
    }
}

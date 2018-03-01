using Oxide.Core.Libraries.Covalence;
using Oxide.Game.Rust.Cui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("Autoloot", "micaelr95", 0.1)]
    [Description("Autoloot entity")]
    public class Autoloot : RustPlugin
    {
        bool canLoot = false;
        BasePlayer p;
        BaseEntity e;

        void OnFrame()
        {
            if (canLoot)
            {
                if (e as LootableCorpse)
                {
                    var corpse = e as LootableCorpse;
                    foreach (var container in corpse.containers)
                    {
                        foreach (var item in container.itemList.ToList())
                        {
                            p.inventory.GiveItem(item);
                        }
                    }
                }
                else
                {
                    if (e as StorageContainer)
                    {
                        var container = e as StorageContainer;
                        foreach (var item in container.inventory.itemList.ToList())
                        {
                            p.inventory.GiveItem(item);
                        }
                    }
                }
                canLoot = false;
            }
        }

        void OnLootEntity(BasePlayer player, BaseEntity entity)
        {
            p = player;
            e = entity;

            CuiElementContainer elements = new CuiElementContainer();
            var panel = elements.Add(new CuiPanel
            {
                Image =
                {
                    Color = "0.1 0.1 0.1 0.6"
                },
                RectTransform =
                {
                    AnchorMin = "0.86 0.92",
                    AnchorMax = "0.97 0.98"
                }
            }, "Overall", "panel");

            elements.Add(new CuiButton
            {
                Button =
                {
                    Command = "corpse.loot",
                    Color = "0.8 0.8 0.8 0.2"
                },
                Text =
                {
                    Text = "AutoLoot",
                    FontSize = 22,
                    Align = TextAnchor.MiddleCenter
                }
            }, panel);
            CuiHelper.AddUi(player, elements);
        }

        void OnLootEntityEnd(BasePlayer player, BaseCombatEntity entity)
        {
            CuiHelper.DestroyUi(player, "panel");
        }

        [ConsoleCommand("corpse.loot")]
        void CanLoot()
        {
            canLoot = true;
        }
    }
}

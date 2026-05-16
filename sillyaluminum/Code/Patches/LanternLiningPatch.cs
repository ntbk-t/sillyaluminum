using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace SillyAluminum{
    [HarmonyPatch(typeof(BELantern))]
    [HarmonyPatch("Interact", MethodType.Normal)]
    public class PatchLanternLining{

        public static void Postfix(ref bool __result, ref BELantern __instance, IPlayer byPlayer){
            if(__result) return;

            ItemSlot slot = byPlayer.InventoryManager.ActiveHotbarSlot;
            if (slot.Empty){
                return;
            }

            CollectibleObject obj = slot.Itemstack!.Collectible;

            if (__instance.lining == null || (__instance.lining == "plain" && obj is ItemMetalPlate && (obj.Variant["metal"] == "aluminum"))){
                __instance.lining = obj.Variant["metal"];
                if (__instance.Api.Side == EnumAppSide.Client)
                {
                    (byPlayer as IClientPlayer).TriggerFpAnimation(EnumHandInteract.HeldItemInteract);
                }
                __instance.Api.World.PlaySoundAt(new AssetLocation("sounds/block/plate"), __instance.Pos, -0.4, byPlayer);
                slot.TakeOut(1);
                __instance.MarkDirty(redrawOnClient: true);
                __result = true;
                return;
            }
        }

        // public static void Prefix(BlockEntityContainer be, IPlayer byPlayer){
        //     be.Api.World.Logger.Event("Fired patch (be)");
        //     byPlayer.Entity.Api.World.Logger.Event("Fired patch (pe)");
        // }

        // public static void Postfix(ref bool __result, BlockEntityContainer be, ItemSlot slot, IPlayer byPlayer){
        //     be.Api.World.Logger.Event("Fired patch (be), returned " + __result);
        //     byPlayer.Entity.Api.World.Logger.Event("Fired patch (pe), returned " + __result);

        //     if(!__result){
        //         return;
        //     }

        //     string lining = slot.Itemstack!.Attributes.GetString("lining");
        //     ItemSlot handSlot = byPlayer.InventoryManager.ActiveHotbarSlot;
        //     if (handSlot.Empty){
        //         __result = false;
        //         return;
        //     }
        //     CollectibleObject obj = handSlot.Itemstack!.Collectible;

        //     bool flag = lining == null;
        //     if (!flag){
        //         bool flag2 = lining == "plain" && obj is ItemMetalPlate;
        //         if (flag2)
        //         {
        //             bool flag3;
        //             switch (obj.Variant["metal"])
        //             {
        //             case "aluminum":
        //                 flag3 = true;
        //                 break;
        //             default:
        //                 flag3 = false;
        //                 break;
        //             }
        //             flag2 = flag3;
        //         }
        //         flag = flag2;
        //     }
        //     if (flag)
        //     {
        //         slot.Itemstack!.Attributes.SetString("lining", obj.Variant["metal"]);
        //         (byPlayer as IClientPlayer)?.TriggerFpAnimation(EnumHandInteract.HeldItemInteract);
        //         be.Api.World.PlaySoundAt(new AssetLocation("sounds/block/plate"), be.Pos, -0.4, byPlayer);
        //         handSlot.TakeOut(1);
        //         be.MarkDirty(redrawOnClient: true);
        //         __result = true;
        //         return;
        //     }
        //     __result = false;
        //     return;
        // }
    }
}
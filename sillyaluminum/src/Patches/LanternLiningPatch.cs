using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace SillyAluminum;

//This is for everything that's not the 2x2 ground storage
[HarmonyPatch(typeof(BELantern))]
[HarmonyPatch("Interact", MethodType.Normal)]
internal class PatchBELanternLining {
    public static void Postfix(ref bool __result, ref BELantern __instance, IPlayer byPlayer) {
        if(__result) return;

        ItemSlot slot = byPlayer.InventoryManager.ActiveHotbarSlot;
        if (slot.Empty) return;

        CollectibleObject obj = slot.Itemstack!.Collectible;

        if (__instance.lining == null || (__instance.lining == "plain" && obj is ItemMetalPlate && (obj.Variant["metal"] == "aluminum"))) {
            __instance.lining = obj.Variant["metal"];
            if (__instance.Api.Side == EnumAppSide.Client) {
                ((IClientPlayer) byPlayer).TriggerFpAnimation(EnumHandInteract.HeldItemInteract);
            }
            __instance.Api.World.PlaySoundAt(new AssetLocation("sounds/block/plate"), __instance.Pos, -0.4, byPlayer);
            slot.TakeOut(1);
            __instance.MarkDirty(redrawOnClient: true);
            __result = true;
            return;
        }
    }
}

//This is for only the 2x2 ground storage
[HarmonyPatch(typeof(BlockLantern))]
[HarmonyPatch("OnContainedInteractStart", MethodType.Normal)]
public class PatchBlockLanternLining {
    public static void Postfix(ref bool __result, BlockEntityContainer be, ItemSlot slot, IPlayer byPlayer, BlockSelection blockSel) {
        if(__result) return;

        ItemSlot handSlot = byPlayer.InventoryManager.ActiveHotbarSlot;
        if (handSlot.Empty) return;

        CollectibleObject obj = handSlot.Itemstack!.Collectible;
        string lining = slot.Itemstack!.Attributes.GetString("lining");

        bool flag = lining == null;
        if (!flag) {
            bool flag2 = lining == "plain" && obj is ItemMetalPlate;
            if (flag2) {
                var flag3 = obj.Variant["metal"] switch {
                    "aluminum" => true,
                    _ => false,
                };
                flag2 = flag3;
            }
            flag = flag2;
        }

        if (flag) {
            slot.Itemstack!.Attributes.SetString("lining", obj.Variant["metal"]);
            (byPlayer as IClientPlayer)?.TriggerFpAnimation(EnumHandInteract.HeldItemInteract);
            be.Api.World.PlaySoundAt(new AssetLocation("sounds/block/plate"), be.Pos, -0.4, byPlayer);
            handSlot.TakeOut(1);
            be.MarkDirty(redrawOnClient: true);
            __result = true;
            return;
        }
    }
}
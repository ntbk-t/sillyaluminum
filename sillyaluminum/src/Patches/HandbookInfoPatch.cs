using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

//This code is absolutely not scalable at all in any way
//It's meant to work for exactly 1 interaction between 2 known items and that's it
//That interaction alone is hardcoded too
namespace SillyAluminum;

[HarmonyPatch(typeof(CollectibleBehaviorHandbookTextAndExtraInfo))]
[HarmonyPatch("addCreatedByInfo", MethodType.Normal)]
public class PatchAluminumPowderCraftingInfo{
    public static void Postfix(
        ref bool __result,
        CollectibleBehaviorHandbookTextAndExtraInfo __instance,
        ICoreClientAPI capi,
        ItemStack[] allStacks,
        ActionConsumable<string> openDetailPageFor,
        ItemStack stack,
        List<RichTextComponentBase> components
    ) {
        if (stack.Collectible.Code != "sillyaluminum:powder-aluminum") return;

        if (!components.Any(comp => comp is RichTextComponent { DisplayText: "Created by\n" })) {
            CollectibleBehaviorHandbookTextAndExtraInfo.AddHeading(components, capi, "Created by", ref __result);
            components.Add(new ClearFloatTextComponent(capi, 3));
            CollectibleBehaviorHandbookTextAndExtraInfo.AddSubHeading(
                components,
                capi,
                openDetailPageFor,
                "Submerging in water",
                "sillyaluminum:aluminummaking"
            );
            
            List<ItemStack> saltyAluminumPowderList = [];
            for (int i = 0; i < allStacks.Length; i++) {
                if(allStacks[i].Collectible.Code == "sillyaluminum:powder-saltyaluminum") {
                    saltyAluminumPowderList.Add(allStacks[i]);
                    break;
                }
            }

            __instance.AddSlideShowComponent(components, capi, saltyAluminumPowderList, openDetailPageFor, false);
        }
    }
}

[HarmonyPatch(typeof(CollectibleBehaviorHandbookTextAndExtraInfo))]
[HarmonyPatch("addIngredientForInfo", MethodType.Normal)]
public class PatchSaltyAluminumPowderCraftingInfo {
    public static void Postfix(
        ref bool __result,
        CollectibleBehaviorHandbookTextAndExtraInfo __instance,
        ICoreClientAPI capi,
        ItemStack[] allStacks,
        ActionConsumable<string> openDetailPageFor,
        ItemStack stack,
        List<RichTextComponentBase> components
    ) {
        if (stack.Collectible.Code != "sillyaluminum:powder-saltyaluminum") return;

        if (!components.Any(comp => comp is RichTextComponent { DisplayText: "Ingredient for\n" })) {
            CollectibleBehaviorHandbookTextAndExtraInfo.AddHeading(components, capi, "Ingredient for", ref __result);
            components.Add(new ClearFloatTextComponent(capi, 3));

            List<ItemStack> aluminumPowderList = [];
            for (int i = 0; i < allStacks.Length; i++) {
                if(allStacks[i].Collectible.Code == "sillyaluminum:powder-aluminum") {
                    aluminumPowderList.Add(allStacks[i]);
                    break;
                }
            }

            __instance.AddSlideShowComponent(components, capi, aluminumPowderList, openDetailPageFor, false);
        }

    }   
}
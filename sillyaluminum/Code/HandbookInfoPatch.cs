using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace SillyAluminum;
#nullable enable

public partial class HandbookInfoPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(CollectibleBehaviorHandbookTextAndExtraInfo), "addCreatedByInfo")]
    public static void PatchSaltyAluminumCraftingInfo(
        CollectibleBehaviorHandbookTextAndExtraInfo __instance,
        ICoreClientAPI capi,
        ItemStack[] allStacks,
        ActionConsumable<string> openDetailPageFor,
        ItemStack stack,
        List<RichTextComponentBase> components)
    {
        if (stack.Collectible is not ItemPowderSaltyAluminum) return;
        // Core.MaxFuelBurnTemp ??= allStacks
        //     .Where(s => s.Collectible.CombustibleProps?.BurnTemperature > 0)
        //     .OrderByDescending(s => s.Collectible.CombustibleProps.BurnTemperature)
        //     .FirstOrDefault()?.Collectible.CombustibleProps?.BurnTemperature ?? 0;
        // if (stack.Collectible.CombustibleProps?.MeltingPoint > Core.MaxFuelBurnTemp) return;
        // var moldStacks = allStacks.Where(s =>
        //         s.Collectible is BlockToolMold &&
        //         GetStackForVariant(capi, s, stack.Collectible.LastCodePart()) != null)
        //     .OrderBy(s => s.Collectible.Code.Domain == "game" ? -100 : 0)
        //     .ThenBy(s => s.ItemAttributes["requiredUnits"].AsInt(100))
        //     .ToArray();
        var haveText = true;//components.Count > 0;
        //if (moldStacks.Length <= 0) return;
        CollectibleBehaviorHandbookTextAndExtraInfo.AddHeading(components, capi, "THIS IS A TEST HEADING", ref haveText);
        
        // Array.ForEach(moldStacks, s =>
        //     s.StackSize = ToolMoldUnitsPatch.GetPatchedRequiredUnits(capi, s.Block, stack));
        // AddAlignedSlideshows(capi, openDetailPageFor, components, moldStacks.ToList());
    }
}
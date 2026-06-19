using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.Util;
using Vintagestory.GameContent;

namespace SillyAluminum;


[HarmonyPatch(typeof(ItemIngot))]
[HarmonyPatch("GetRequiredAnvilTier", MethodType.Normal)]
public class GetRequiredMetalTierPatch {
    public static void Prefix(
        AssetLocation ___Code,
        RelaxedReadOnlyDictionary<string, string> ___Variant,
        ICoreAPI ___api
    ) {
        if (___Variant["metal"] == null) {
            ___api.Logger.Error("Found ingot with null metal variant! code: {0}", ___Code.ToString());
        }
    }
}
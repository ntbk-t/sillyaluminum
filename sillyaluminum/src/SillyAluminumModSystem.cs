using Vintagestory.API.Common;
using HarmonyLib;

namespace sillyaluminum;

public class SillyAluminumModSystem : ModSystem {
    private Harmony? harmony;
    public override bool ShouldLoad(EnumAppSide forSide) {
        return true;
    }

    public override void Start(ICoreAPI api) {
        base.Start(api);
        api.Logger.Event("Loaded mod " + Mod.Info.ModID);
        api.RegisterItemClass(Mod.Info.ModID + ".ItemNuggetSodium", typeof(ItemNuggetSodium));
        api.RegisterItemClass(Mod.Info.ModID + ".ItemIngotSodium", typeof(ItemIngotSodium));
        api.RegisterItemClass(Mod.Info.ModID + ".ItemPowderSaltyAluminum", typeof(ItemPowderSaltyAluminum));

        api.RegisterEntity(Mod.Info.ModID + ".EntityThrownIngotSodium", typeof(EntityThrownIngotSodium));

        harmony = new Harmony(Mod.Info.ModID);
        harmony.PatchAll();
    }

    public override void Dispose() {
        harmony?.UnpatchAll(Mod.Info.ModID);
    }
}

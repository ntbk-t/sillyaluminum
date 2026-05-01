using Vintagestory.API.Common;

namespace sillyaluminum;

public class SillyAluminumModSystem : ModSystem {
    public override bool ShouldLoad(EnumAppSide forSide){
        return true;
    }

    public override void Start(ICoreAPI api){
        base.Start(api);
        api.Logger.Event("Loaded mod " + Mod.Info.ModID);
        api.RegisterItemClass(Mod.Info.ModID + ".ItemNuggetSodium", typeof(ItemNuggetSodium));
        api.RegisterItemClass(Mod.Info.ModID + ".ItemIngotSodium", typeof(ItemIngotSodium));
    }
}

using sillyaluminum;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

internal class EntityThrownIngotSodium : EntityThrownStone {
    public override void OnCollideWithLiquid() {
        base.OnCollideWithLiquid();

        // TODO: actually check what liquid we're colliding with

        // TODO: copy paste :(
        var metalbitSodium = World.GetItem(new AssetLocation("metalbit-sodium"));
        var nuggets = World.Rand.NextInt64() % 10 + 10;
		for (var i = 0; i < nuggets; i++) {
			var nugget = (EntityItem?) World.SpawnItemEntity(
				new ItemStack(metalbitSodium, 1),
				Pos.XYZ,
				new Vec3d(
					(World.Rand.NextDouble() - 0.5) * 0.25,
					(World.Rand.NextDouble() * 0.5) + 0.25,
					(World.Rand.NextDouble() - 0.5) * 0.25
				)
			);
			if (nugget == null) continue;
            nugget.Itemstack.Item.SetTemperature(nugget.World, nugget.Itemstack, 800);
			nugget.IsOnFire = true;
			nugget.Attributes.SetBool("burnOnWaterExit", true);
		}
        Die();
    }
}
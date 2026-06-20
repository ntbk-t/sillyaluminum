using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.GameContent;
using Vintagestory.API.MathTools;

internal class ItemIngotSodium : ItemIngot {
	Item? metalbit_sodium = null;

    public override void OnLoaded(ICoreAPI api) {
        base.OnLoaded(api);
		metalbit_sodium = api.World.GetItem(new AssetLocation("metalbit-sodium")); //Used to be sillyaluminum:metalbit-sodium
    }

    public override void OnGroundIdle(EntityItem entityItem) {
		base.OnGroundIdle(entityItem);

		if (api.Side != EnumAppSide.Server) return;
		if (!entityItem.Swimming) return;

		if (api.World.Rand.NextDouble() >= 0.2) {
			if (api.World.Rand.NextDouble() < 0.2) {
				api.World.SpawnCubeParticles(
					entityItem.Pos.XYZ,
					entityItem.Itemstack.Clone(),
					0.1f,
					2,
					0.2f + (float)api.World.Rand.NextDouble() / 5f
				);
			}
			return;
		}

		api.World.SpawnCubeParticles(entityItem.Pos.XYZ, entityItem.Itemstack.Clone(), 0.1f, 80, 0.3f);
		((IServerWorldAccessor)api.World).CreateExplosion(entityItem.Pos.AsBlockPos, EnumBlastType.EntityBlast, 0, 12.5, 1f);

		// if only Rand.NextInRange...
		long nuggets = api.World.Rand.NextInt64() % 10 + 10;
		for (int i = 0; i < nuggets; i++) {
			EntityItem? nugget = (EntityItem?) api.World.SpawnItemEntity(
				new ItemStack(metalbit_sodium, entityItem.Itemstack.StackSize),
				entityItem.Pos.XYZ,
				new Vec3d(
					entityItem.Pos.Motion.X + (api.World.Rand.NextDouble() - 0.5) * 0.25,
					(api.World.Rand.NextDouble() * 0.5) + 0.25,
					entityItem.Pos.Motion.Z + (api.World.Rand.NextDouble() - 0.5) * 0.25
				)
			);
			if (nugget == null) continue;
            nugget.Itemstack.Item.SetTemperature(nugget.World, nugget.Itemstack, 800);
			nugget.IsOnFire = true;
			nugget.Attributes.SetBool("burnOnWaterExit", true);
		}
		entityItem.Die();
	}
}
using System;
using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.GameContent;
using Vintagestory.API.Datastructures;
using System.Security.Cryptography;
using Vintagestory.API.MathTools;

//Not ItemIngot since that only has anvil stuff that we don't care about here
internal class ItemIngotSodium : Item {
	Item? metalbit_sodium = null;

    public override void OnLoaded(ICoreAPI api) {
        base.OnLoaded(api);
		metalbit_sodium = api.World.GetItem(new AssetLocation("sillyaluminum:metalbit-sodium"));
    }

    public string GetMetalType() {
		return LastCodePart();
	}

    public override void OnGroundIdle(EntityItem entityItem) {
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
		}

		api.World.SpawnCubeParticles(entityItem.Pos.XYZ, entityItem.Itemstack.Clone(), 0.1f, 80, 0.3f);
		((IServerWorldAccessor)api.World).CreateExplosion(entityItem.Pos.AsBlockPos, EnumBlastType.OreBlast, 4.5, 12, 1f);

		// if only Rand.NextInRange...
		var nuggets = api.World.Rand.NextInt64() % 10 + 10;
		for (var i = 0; i < nuggets; i++) {
			var nugget = (EntityItem?) api.World.SpawnItemEntity(
				new ItemStack(metalbit_sodium, 1),
				entityItem.Pos.XYZ,
				new(
					(api.World.Rand.NextDouble() - 0.5) * 0.25,
					(api.World.Rand.NextDouble() * 0.5) + 0.25,
					(api.World.Rand.NextDouble() - 0.5) * 0.25
				)
			);
			// TODO: better way to handle this
			if (nugget == null) return;
            nugget.Itemstack.Item.SetTemperature(nugget.World, nugget.Itemstack, 800);
			nugget.IsOnFire = true;
			entityItem.Attributes.SetBool("burnOnWaterExit", true);
		}
		entityItem.Die();
	}
}
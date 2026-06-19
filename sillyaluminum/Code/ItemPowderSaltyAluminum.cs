using System;
using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.GameContent;
using Vintagestory.API.MathTools;

//Not ItemIngot since that only has anvil stuff that we don't care about here
internal class ItemPowderSaltyAluminum : Item {
	Item? powder_aluminum = null;

    public override void OnLoaded(ICoreAPI api) {
        base.OnLoaded(api);
		powder_aluminum = api.World.GetItem(new AssetLocation("sillyaluminum:powder-aluminum"));
    }

    public override void OnGroundIdle(EntityItem entityItem) {
		base.OnGroundIdle(entityItem);

		if (api.Side != EnumAppSide.Server) return;
		if (!entityItem.Swimming) return;

		if (api.World.Rand.NextDouble() >= 0.025) {
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

		api.World.SpawnItemEntity(
            new ItemStack(powder_aluminum, entityItem.Itemstack.StackSize),
            entityItem.Pos.XYZ,
            entityItem.Pos.Motion
        );
		entityItem.Die();
	}
}
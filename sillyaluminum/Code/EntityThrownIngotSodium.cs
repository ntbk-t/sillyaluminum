using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

internal class EntityThrownIngotSodium : EntityThrownStone {
    private float explosionTimer;

    public override void Initialize(EntityProperties properties, ICoreAPI api, long InChunkIndex3d) {
        base.Initialize(properties, api, InChunkIndex3d);

        explosionTimer = World.Rand.NextSingle() / 2; // TODO: number tweaking :]
    }

    public override void OnGameTick(float dt) {
        base.OnGameTick(dt);

        if (Api.Side != EnumAppSide.Server) return;
        if (!Swimming) return;   

        // TODO: actually check what liquid we're colliding with

        // TODO: copy paste :(

        var metalBitSodium = World.GetItem(new AssetLocation("metalbit-sodium"));

        explosionTimer -= dt;
        if (explosionTimer > 0) {
			if (World.Rand.NextDouble() < 0.2) {
				World.SpawnCubeParticles(
					Pos.XYZ,
					new(metalBitSodium, 1),
					0.1f,
					2,
					0.2f + (float)World.Rand.NextDouble() / 5f
				);
			}
            return;
		}

        World.SpawnCubeParticles(Pos.XYZ, new(metalBitSodium, 1), 0.1f, 80, 0.3f);
		((IServerWorldAccessor) World).CreateExplosion(Pos.AsBlockPos, EnumBlastType.OreBlast, 4.5, 12, 1f);

        
        var nuggets = World.Rand.NextInt64() % 10 + 10;
		for (var i = 0; i < nuggets; i++) {
			var nugget = (EntityItem?) World.SpawnItemEntity(
				new(metalBitSodium, 1),
				Pos.XYZ,
				new Vec3d(
					Pos.Motion.X + (World.Rand.NextDouble() - 0.5) * 0.25,
					(World.Rand.NextDouble() * 0.5) + 0.25,
					Pos.Motion.Z + (World.Rand.NextDouble() - 0.5) * 0.25
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
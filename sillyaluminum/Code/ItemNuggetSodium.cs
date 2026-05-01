using System;
using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.GameContent;
using Vintagestory.API.Datastructures;
using System.Security.Cryptography;

internal class ItemNuggetSodium : ItemNugget{
    public override void OnGroundIdle(EntityItem entityItem){
		if (api.Side != EnumAppSide.Server){
			return;
		}

		if(!entityItem.Swimming){
			if(entityItem.Attributes.GetAsBool("burnOnWaterExit")){
				entityItem.Attributes.RemoveAttribute("burnOnWaterExit");
				entityItem.IsOnFire = true;
				if(GetTemperature(entityItem.World, entityItem.Itemstack) < 800){
					SetTemperature(entityItem.World, entityItem.Itemstack, 800);
				}
			}
			return;
		}

		if (api.World.Rand.NextDouble() < 0.04){
			api.World.SpawnCubeParticles(entityItem.Pos.XYZ, entityItem.Itemstack.Clone(), 0.1f, 80, 0.3f);
            //api.World.SpawnParticles(2, 255, entityItem.Pos.XYZ, entityItem.Pos.XYZ, )
            ((IServerWorldAccessor)api.World).CreateExplosion(entityItem.Pos.AsBlockPos, EnumBlastType.EntityBlast, 0.0, 2.5, 1f);
			entityItem.Attributes.SetBool("burnOnWaterExit", true);
			if(api.World.Rand.NextDouble() < 0.25){
				entityItem.Die();
			}else{
				entityItem.Pos.Motion.X += (api.World.Rand.NextDouble() - 0.5) * 0.25;
				entityItem.Pos.Motion.Z += (api.World.Rand.NextDouble() - 0.5) * 0.25;
				entityItem.Pos.Motion.Y += (api.World.Rand.NextDouble() * 0.5) + 0.25;
			}
		}else if (api.World.Rand.NextDouble() < 0.2){
			api.World.SpawnCubeParticles(entityItem.Pos.XYZ, entityItem.Itemstack.Clone(), 0.1f, 2, 0.2f + (float)api.World.Rand.NextDouble() / 5f);
		}
	}
}
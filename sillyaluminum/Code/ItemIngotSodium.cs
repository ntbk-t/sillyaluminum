using System;
using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.GameContent;
using Vintagestory.API.Datastructures;
using System.Security.Cryptography;

internal class ItemIngotSodium : Item{ //Not ItemIngot since that only has anvil stuff that we don't care about here

    public string GetMetalType(){
		return LastCodePart();
	}

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

		if (api.World.Rand.NextDouble() < 0.20){
			api.World.SpawnCubeParticles(entityItem.Pos.XYZ, entityItem.Itemstack.Clone(), 0.1f, 80, 0.3f);
            //api.World.SpawnParticles(2, 255, entityItem.Pos.XYZ, entityItem.Pos.XYZ, )
            ((IServerWorldAccessor)api.World).CreateExplosion(entityItem.Pos.AsBlockPos, EnumBlastType.OreBlast, 4.5, 25, 1f);
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
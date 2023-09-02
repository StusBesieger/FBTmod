using System;
using System.Collections.Generic;
using Modding;
using Modding.Blocks;
using UnityEngine;
using StusFBT;

namespace StusFBT
{
	public class Mod : ModEntryPoint
	{
		public override void OnLoad()
		{
			mod = new GameObject("StusFBTcontroller");
			SingleInstance<FBTBlockSelector>.Instance.transform.parent = mod.transform;
			UnityEngine.Object.DontDestroyOnLoad(mod);
		}
		public static void Log(string msg)
       {
		Debug.Log("stus FBTmod : " + msg);
       }
		public static void Warning(string msg)
       {
		Debug.LogWarning("stus FBTmod : " + msg);
       }
		public static void Error(string msg)
       {
		Debug.LogError("stus FBTmod : " + msg);
       }
	}

	//ブロックにスクリプトを追加
	public class BlocksSelector : SingleInstance<FBTBlockSelector>
	{
		public Dictionary<int, Type> BlockDict = new Dictionary<int, Type>
		{
			//ブロックIDとスクリプト
			//弾薬庫ブロック（未作成）
			//{, tyoeof(ExplosionScript)},

		};
		public override string Name
		{
			get
			{
				return "Stus FBT BlocksSelector";
			}
		}
		//メソッド
		public void Start()
		{
			Events.OnBlockInit += new AddScript;
		}
		public void AddScript(Block block)
		{
			BlockBehaviour internalObject = block.BuildingBlock.InternalObject;
			if (BlockDict.ContainsKey(internalObject.BlockID))
			{
				Type type = BlockDict[internalObject.BlockID];
				try
				{
					//貼り付けている場合
					if (internalObject.GetComponent(type) == null)
					{
						internalObject.gameObject.AddComponent(type);
						Mod.Log("Added Script");
					}
				}
				catch
				{
					Mod.Error("AddScript Error!");
				}
				return;
			}
		}
	}

	//ブロックhpが0になったら爆発するスクリプト
	public class ExplosionScript : Block
	{
		private BlockBehaviour block;
		void Start()
		{
			block = base.GetComponent<BlockBehaviour>();
		}
		void FixedUpdate()
		{
			if( Health <= 0.01)
			{
				float hp;
				if (block.Prefab.hasHealthBar)
				{
					hp = block.BlockHealth.health;
					if(hp <= 0.01)
					Explodey();
				}
			}
		}
		private void Explodey()
		{
			GameObject gameObject = (gameObject)object.Instantiate(PrefabMaster.BlockPrefabs[23].gameObject, base.transform.position, base.transform.rotation, base.transform)
			GetComponent<ExplodeOmCollideBlock>();
			eocb =gameObject.GetComponent<ExplodeOnCollideBock>();
			ecob.radius =7f;
			ecob.Simphysics = true;
			ecob.Explodey();
		}
	}
}

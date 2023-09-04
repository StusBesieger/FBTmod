using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using Modding.Blocks;
using UnityEngine;
using FBTcore;

namespace FBTcore
{
	public class Mod : ModEntryPoint
	{
		GameObject mod;
		public override void OnLoad()
		{
			mod = new GameObject("StusFBTcontroller");
			SingleInstance<BlockSelector>.Instance.transform.parent = mod.transform;
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
	public class BlockSelector : SingleInstance<BlockSelector>
	{
		public Dictionary<string, Type> BlockDict = new Dictionary<string, Type>
		{
			//ブロックIDとスクリプト
			//弾薬庫ブロック（未作成）
			{"a3567e7a-50bd-41f4-87cc-1b93a3e93852-6", typeof(ExplosionScript)},

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
			Events.OnBlockInit += new Action<Block>(AddScript);
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
	public class ExplosionScript : MonoBehaviour
	{
		private BlockBehaviour block;
		//爆発ステータス
		public float radius = 4.0F;
		public float power = 10000.0F;
		void Start()
		{
			block = base.GetComponent<BlockBehaviour>();
		}
		void FixedUpdate()
		{

			float hp;
			if (block.Prefab.hasHealthBar)
			{
				hp = block.BlockHealth.health;
				if (hp <= 0.01)
					Explodey();
			}
		}
		private void Explodey()
		{
				Vector3 explosionPos = transform.position;
				Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
				foreach (Collider hit in colliders)
				{
					Rigidbody rb = hit.GetComponent<Rigidbody>();

					if (rb != null)
						rb.AddExplosionForce(power, explosionPos, radius, 3.0F);

				}
			Destroy(this.gameObject);
		}
	}
}

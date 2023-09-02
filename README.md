方針
弾薬ブロック
hpが0になると爆発する(燃焼付き)
爆発半径は9、breakforceは2000ぐらい

エンジンブロック
hpが0になると爆発する(燃焼付き）
爆発半径は4、breakforceは500くらい
馬力の値を400~1000までを設定できるようにし、馬力をもとにエンジンブロックの重量や
設置されたコグやホイールなどの動力元のブロックのトルクの値と最大回転速度値を決定する¹
・重量=馬力*0.1
・トルク値: 未定
・最大回転速度=馬力*0.8

コグやホイールの回転するためのトルクを0から上げていく挙動から一定値に変更させる¹

¹難しい場合機体全体の重量とエンジンブロックの馬力から最大回転速度を決める
・最大回転速度=馬力/重量



using System;
using System.Collections.Generic;
using Modding;
using Modding.Blocks;
using UnityEngine;

namespace blockstate
{
	public class Mod : ModEntryPoint
	{
		public override void OnLoad()
		{
			mod = new GameObject("StusFBTcontroller")
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
			{, tyoeof(ExplosionScript)},

		};
		public override string Name
		{
			get
			{
				return "Stus FBT BlocksSelector";
			}
		}
		//メソッド
		public void Awake()
		{
			Events.OnBlockInit += new Action<Block>(AddScript);
		}
		public void AddScript(Block block)
		{
			BlockBehaciour BlockBehaviour internalObject = block.BuildingBlock.InternalObject;
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

	//hpが0になったら爆発するスクリプト
	public class ExplosionScript : MonoBehaviour
	{
		public float Health { get; } 

		//hpが0になった時爆発させる
		if( == 0)
		{
			//爆発を生成する？
		eocb = gameObject.GetComponent<ExplodeOnCollideBlock>();
		eocb.radius = 9f;
		eocb.SimPhysics = true;
		eocb.Explodey();
		}
		
	}
}

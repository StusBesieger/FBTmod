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



	//ブロックhpが0になったら爆発するスクリプト
	public class ExplosionScript : BlockScript
	{
		private BlockBehaviour block;
		public float power = 50.0F;
		public float radius = 5.0f;
		public float upward = 1.0f;

		//爆発ステータス

		void Start()
		{
			Debug.Log("stus FBTmod : ");
			block = base.GetComponent<BlockBehaviour>();
		}
		void FixedUpdate()
		{

			float hp;
			if (this.gameObject.activeSelf)
			{
				hp = block.BlockHealth.health;
				if (hp <= 0.01)
					Explode();

			}
		}
		//爆発
		void Explode()
		{
			Debug.Log("stus FBTmod2 : ");
			Collider[] Colliders = Physics.OverlapSphere(transform.position, 5.0f);
			foreach(Collider cube in Colliders)
			{
				if (cube.GetComponent<Rigidbody>())
				{
					cube.GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, radius, upward, ForceMode.Impulse);
					Debug.Log("stus FBTmod3 : ");
					this.gameObject.SetActive(false);
				}
			}
		}
	}
}

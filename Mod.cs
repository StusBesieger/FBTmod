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
		public float radius = 5.0F;
		public float power = 1000.0F;
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
			Vector3 explosionPos = transform.position;
			Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
			foreach (Collider hit in colliders)
			{
				Rigidbody rb = hit.GetComponent<Rigidbody>();

				if (rb != null)
					rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
				this.gameObject.SetActive(false);
			}
		}
	}
}

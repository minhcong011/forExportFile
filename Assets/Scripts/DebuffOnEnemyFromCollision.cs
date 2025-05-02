// dnSpy decompiler from Assembly-CSharp.dll class: DebuffOnEnemyFromCollision
using System;
using UnityEngine;

public class DebuffOnEnemyFromCollision : MonoBehaviour
{
	private void Start()
	{
		this.EffectSettings.CollisionEnter += this.EffectSettings_CollisionEnter;
	}

	private void EffectSettings_CollisionEnter(object sender, CollisionInfo e)
	{
		if (this.Effect == null)
		{
			return;
		}
		Collider[] array = Physics.OverlapSphere(base.transform.position, this.EffectSettings.EffectRadius, this.EffectSettings.LayerMask);
		foreach (Collider collider in array)
		{
			Transform transform = collider.transform;
			Renderer componentInChildren = transform.GetComponentInChildren<Renderer>();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Effect);
			gameObject.transform.parent = componentInChildren.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.GetComponent<AddMaterialOnHit>().UpdateMaterial(collider.transform);
		}
	}

	private void Update()
	{
	}

	public EffectSettings EffectSettings;

	public GameObject Effect;
}

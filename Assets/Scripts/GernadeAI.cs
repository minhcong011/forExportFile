// dnSpy decompiler from Assembly-CSharp.dll class: GernadeAI
using System;
using UnityEngine;

public class GernadeAI : MonoBehaviour
{
	private void Start()
	{
		this.body = base.GetComponent<Rigidbody>();
		base.Invoke("Damage", this.lifeTime);
		this.body.AddForce(base.transform.up * 45f, ForceMode.Impulse);
		this.body.AddTorque(base.transform.right * 30f, ForceMode.Impulse);
	}

	public void setTarget(Vector3 t)
	{
		this.target = t;
		this.target = new Vector3(t.x, t.y, t.z - 1f);
	}

	public void Damage()
	{
		if (this.effect)
		{
			GameObject obj = UnityEngine.Object.Instantiate<GameObject>(this.effect, base.transform.position, base.transform.rotation);
			UnityEngine.Object.Destroy(obj, 2f);
		}
		foreach (Collider collider in Physics.OverlapSphere(base.transform.position, this.range))
		{
			if (collider)
			{
				if (collider.gameObject.tag == "Player" && collider.gameObject == Singleton<GameController>.Instance.refAllController.getCurrentController().gameObject)
				{
					collider.gameObject.GetComponent<PlayerHealth>().DamagePlayer(this.damage, false);
				}
			}
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void Update()
	{
		base.transform.position = Vector3.Lerp(base.transform.position, this.target, 1.5f * Time.deltaTime);
	}

	public float lifeTime = 3f;

	public float range = 5f;

	public GameObject effect;

	public float force = 40f;

	public float damage = 3f;

	private bool destroyed;

	private Rigidbody body;

	private Vector3 target;
}

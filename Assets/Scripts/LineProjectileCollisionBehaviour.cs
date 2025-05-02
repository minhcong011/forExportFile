// dnSpy decompiler from Assembly-CSharp.dll class: LineProjectileCollisionBehaviour
using System;
using UnityEngine;

public class LineProjectileCollisionBehaviour : MonoBehaviour
{
	private void GetEffectSettingsComponent(Transform tr)
	{
		Transform parent = tr.parent;
		if (parent != null)
		{
			this.effectSettings = parent.GetComponentInChildren<EffectSettings>();
			if (this.effectSettings == null)
			{
				this.GetEffectSettingsComponent(parent.transform);
			}
		}
	}

	private void Start()
	{
		this.t = base.transform;
		if (this.EffectOnHit != null)
		{
			this.tEffectOnHit = this.EffectOnHit.transform;
			this.effectOnHitParticles = this.EffectOnHit.GetComponentsInChildren<ParticleSystem>();
		}
		if (this.ParticlesScale != null)
		{
			this.tParticleScale = this.ParticlesScale.transform;
		}
		this.GetEffectSettingsComponent(this.t);
		if (this.effectSettings == null)
		{
			UnityEngine.Debug.Log("Prefab root or children have not script \"PrefabSettings\"");
		}
		if (this.GoLight != null)
		{
			this.tLight = this.GoLight.transform;
		}
		this.InitializeDefault();
		this.isInitializedOnStart = true;
	}

	private void OnEnable()
	{
		if (this.isInitializedOnStart)
		{
			this.InitializeDefault();
		}
	}

	private void OnDisable()
	{
		this.CollisionLeave();
	}

	private void InitializeDefault()
	{
		this.hit = default(RaycastHit);
		this.frameDroped = false;
	}

	private void Update()
	{
		if (!this.frameDroped)
		{
			this.frameDroped = true;
			return;
		}
		Vector3 vector = this.t.position + this.t.forward * this.effectSettings.MoveDistance;
		RaycastHit raycastHit;
		if (Physics.Raycast(this.t.position, this.t.forward, out raycastHit, this.effectSettings.MoveDistance + 1f, this.effectSettings.LayerMask))
		{
			this.hit = raycastHit;
			vector = raycastHit.point;
			if (this.oldRaycastHit.collider != this.hit.collider)
			{
				this.CollisionLeave();
				this.oldRaycastHit = this.hit;
				this.CollisionEnter();
				if (this.EffectOnHit != null)
				{
					foreach (ParticleSystem particleSystem in this.effectOnHitParticles)
					{
						particleSystem.Play();
					}
				}
			}
			if (this.EffectOnHit != null)
			{
				this.tEffectOnHit.position = this.hit.point - this.t.forward * this.effectSettings.ColliderRadius;
			}
		}
		else if (this.EffectOnHit != null)
		{
			foreach (ParticleSystem particleSystem2 in this.effectOnHitParticles)
			{
				particleSystem2.Stop();
			}
		}
		if (this.EffectOnHit != null)
		{
			this.tEffectOnHit.LookAt(this.hit.point + this.hit.normal);
		}
		if (this.IsCenterLightPosition && this.GoLight != null)
		{
			this.tLight.position = (this.t.position + vector) / 2f;
		}
		foreach (LineRenderer lineRenderer in this.LineRenderers)
		{
			lineRenderer.SetPosition(0, vector);
			lineRenderer.SetPosition(1, this.t.position);
		}
		if (this.ParticlesScale != null)
		{
			float x = Vector3.Distance(this.t.position, vector) / 2f;
			this.tParticleScale.localScale = new Vector3(x, 1f, 1f);
		}
	}

	private void CollisionEnter()
	{
		if (this.EffectOnHitObject != null && this.hit.transform != null)
		{
			AddMaterialOnHit componentInChildren = this.hit.transform.GetComponentInChildren<AddMaterialOnHit>();
			this.effectSettingsInstance = null;
			if (componentInChildren != null)
			{
				this.effectSettingsInstance = componentInChildren.gameObject.GetComponent<EffectSettings>();
			}
			if (this.effectSettingsInstance != null)
			{
				this.effectSettingsInstance.IsVisible = true;
			}
			else
			{
				Transform transform = this.hit.transform;
				Renderer componentInChildren2 = transform.GetComponentInChildren<Renderer>();
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EffectOnHitObject);
				gameObject.transform.parent = componentInChildren2.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.GetComponent<AddMaterialOnHit>().UpdateMaterial(this.hit);
				this.effectSettingsInstance = gameObject.GetComponent<EffectSettings>();
			}
		}
		this.effectSettings.OnCollisionHandler(new CollisionInfo
		{
			Hit = this.hit
		});
	}

	private void CollisionLeave()
	{
		if (this.effectSettingsInstance != null)
		{
			this.effectSettingsInstance.IsVisible = false;
		}
	}

	public GameObject EffectOnHit;

	public GameObject EffectOnHitObject;

	public GameObject ParticlesScale;

	public GameObject GoLight;

	public bool IsCenterLightPosition;

	public LineRenderer[] LineRenderers;

	private EffectSettings effectSettings;

	private Transform t;

	private Transform tLight;

	private Transform tEffectOnHit;

	private Transform tParticleScale;

	private RaycastHit hit;

	private RaycastHit oldRaycastHit;

	private bool isInitializedOnStart;

	private bool frameDroped;

	private ParticleSystem[] effectOnHitParticles;

	private EffectSettings effectSettingsInstance;
}

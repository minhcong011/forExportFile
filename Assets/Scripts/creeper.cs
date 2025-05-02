// dnSpy decompiler from Assembly-CSharp.dll class: creeper
using System;
using UnityEngine;
using UnityEngine.AI;

public class creeper : MonoBehaviour
{
	private void Awake()
	{
		this.animator = base.GetComponent<Animator>();
		this.agent = base.GetComponent<NavMeshAgent>();
		this.timer = this.wanderTimer;
		this.animator = base.transform.GetComponent<Animator>();
		this.player = GameObject.FindGameObjectWithTag("Player");
		this.myaudio = base.GetComponent<AudioSource>();
	}

	private void Start()
	{
		this.startposition = base.transform.position;
		this.oldhitpoints = this.hitpoints;
	}

	private void Update()
	{
		Vector3 velocity = this.agent.velocity;
		this.animator.SetFloat("speed", velocity.magnitude / this.walkspeed);
		if (this.oldhitpoints != this.hitpoints)
		{
			this.animator.SetBool("gethit", true);
			this.oldhitpoints = this.hitpoints;
		}
		else
		{
			this.animator.SetBool("gethit", false);
		}
		if (this.hitpoints <= 0f)
		{
			UnityEngine.Object.Instantiate<Transform>(this.spawnobject, base.transform.position, base.transform.rotation);
			Collider[] array = Physics.OverlapSphere(this.hitreactionVector, 1f);
			foreach (Collider collider in array)
			{
				if (collider.GetComponent<Rigidbody>() != null)
				{
					Rigidbody component = collider.GetComponent<Rigidbody>();
					component.AddForceAtPosition((component.transform.position - this.hitreactionVector).normalized * 120f, this.hitreactionVector);
				}
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else if (this.player != null)
		{
			Vector3 vector = this.player.transform.position + new Vector3(0f, 0.5f, 0f) - base.transform.position;
			Vector3 forward = base.transform.forward;
			float num = Vector3.Angle(vector, forward);
			if (num < this.viewangle && vector.magnitude <= this.viewdistance)
			{
				if (vector.magnitude <= this.attackdistance)
				{
					this.agent.Stop();
					this.animator.SetBool("attack", true);
				}
				else
				{
					this.agent.speed = this.runspeed;
					this.agent.Resume();
					this.agent.SetDestination(this.player.transform.position);
					Quaternion b = Quaternion.LookRotation(vector, Vector3.up);
					b.x = 0f;
					b.z = 0f;
					base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, Time.deltaTime * 3f);
					this.animator.SetBool("attack", false);
				}
			}
			else
			{
				this.dowander();
				this.animator.SetBool("attack", false);
			}
		}
		else
		{
			this.dowander();
			this.animator.SetBool("attack", false);
		}
	}

	public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
	{
		Vector3 vector = UnityEngine.Random.insideUnitSphere * dist;
		vector += origin;
		NavMeshHit navMeshHit;
		NavMesh.SamplePosition(vector, out navMeshHit, dist, layermask);
		return navMeshHit.position;
	}

	private void Damage(float damage)
	{
		if (!this.myaudio.isPlaying && this.hitpoints >= 0f)
		{
			int num = UnityEngine.Random.Range(1, this.hurtSounds.Length);
			this.myaudio.clip = this.hurtSounds[num];
			this.myaudio.pitch = 0.9f + 0.1f * UnityEngine.Random.value;
			this.myaudio.Play();
			this.hurtSounds[num] = this.hurtSounds[0];
			this.hurtSounds[0] = this.myaudio.clip;
		}
		this.hitpoints -= damage;
	}

	private void Attack()
	{
		if (this.player != null)
		{
			this.player.transform.SendMessageUpwards("Damage", 50f, SendMessageOptions.DontRequireReceiver);
		}
	}

	private void hitvector(Vector3 davector)
	{
		this.hitreactionVector = davector;
	}

	private void dowander()
	{
		this.agent.Resume();
		Vector3 forward = base.transform.forward;
		Ray ray = new Ray(base.transform.position + new Vector3(0f, 1f, 0f), forward);
		RaycastHit raycastHit = default(RaycastHit);
		Vector3 direction = base.transform.InverseTransformDirection(base.transform.right);
		Ray ray2 = new Ray(base.transform.position + new Vector3(0f, 1f, 0f), direction);
		RaycastHit raycastHit2 = default(RaycastHit);
		Vector3 direction2 = base.transform.InverseTransformDirection(-base.transform.right);
		Ray ray3 = new Ray(base.transform.position + new Vector3(0f, 1f, 0f), direction2);
		RaycastHit raycastHit3 = default(RaycastHit);
		if (Physics.Raycast(ray, out raycastHit, this.walltreshold, this.mask))
		{
			if (Physics.Raycast(ray2, out raycastHit2, this.walltreshold, this.mask))
			{
				Vector3 b = base.transform.localEulerAngles + new Vector3(0f, -50f, 0f);
				base.transform.localEulerAngles = Vector3.Lerp(base.transform.localEulerAngles, b, 2f * Time.deltaTime);
			}
			else
			{
				this.agent.updateRotation = false;
				Vector3 b2 = base.transform.localEulerAngles + new Vector3(0f, 50f, 0f);
				base.transform.localEulerAngles = Vector3.Lerp(base.transform.localEulerAngles, b2, 2f * Time.deltaTime);
			}
			this.agent.updateRotation = false;
		}
		if (Physics.Raycast(ray, out raycastHit, this.walltreshold, this.mask) && Physics.Raycast(ray2, out raycastHit2, this.walltreshold, this.mask))
		{
			this.agent.updateRotation = false;
			Vector3 b3 = base.transform.localEulerAngles + new Vector3(0f, -50f, 0f);
			base.transform.localEulerAngles = Vector3.Lerp(base.transform.localEulerAngles, b3, 2f * Time.deltaTime);
		}
		else if (Physics.Raycast(ray, out raycastHit, this.walltreshold, this.mask) && Physics.Raycast(ray3, out raycastHit3, this.walltreshold, this.mask))
		{
			this.agent.updateRotation = false;
			Vector3 b4 = base.transform.localEulerAngles + new Vector3(0f, 50f, 0f);
			base.transform.localEulerAngles = Vector3.Lerp(base.transform.localEulerAngles, b4, 2f * Time.deltaTime);
		}
		else
		{
			this.agent.updateRotation = true;
		}
		this.timer += Time.deltaTime;
		this.agent.speed = this.walkspeed;
		if (this.timer >= this.wanderTimer)
		{
			Vector3 destination = creeper.RandomNavSphere(this.startposition, this.wanderRadius, -1);
			this.agent.SetDestination(destination);
			this.timer = 0f;
		}
	}

	public float wanderRadius;

	public float wanderTimer;

	public float hitpoints = 100f;

	public Transform spawnobject;

	private Transform target;

	private NavMeshAgent agent;

	public float walkspeed = 2f;

	public float runspeed = 5f;

	private float timer;

	private Animator animator;

	private GameObject player;

	public float viewangle = 60f;

	public float viewdistance = 20f;

	public float attackdistance = 1.5f;

	private AudioSource myaudio;

	public AudioClip[] hurtSounds;

	private float oldhitpoints;

	public LayerMask mask;

	public float walltreshold = 2f;

	private Vector3 hitreactionVector;

	private Vector3 startposition;
}

// dnSpy decompiler from Assembly-CSharp.dll class: CommonAnimalModel
using System;
using UnityEngine;

public class CommonAnimalModel : MonoBehaviour
{
	public void Start()
	{
		string[] array = this.refModelMeta.modelMetaId.Split(new char[]
		{
			'_'
		});
		this.animalID = (int)Convert.ToInt16(array[0]);
		this.typeId = (int)Convert.ToInt16(array[1]);
		this.behaviourId = (int)Convert.ToInt16(array[2]);
		if (this.behaviourId == 1)
		{
			this.animalBehaviour = CommonAnimalModel.Behaviour.simple;
			this.canFollowPlayer = false;
			this.canFollowAnimal = false;
		}
		else if (this.behaviourId == 2)
		{
			this.animalBehaviour = CommonAnimalModel.Behaviour.attackOnPlayer;
			this.canFollowPlayer = true;
			this.canFollowAnimal = false;
		}
		else if (this.behaviourId == 3)
		{
			this.animalBehaviour = CommonAnimalModel.Behaviour.bothAttacking;
			this.canFollowPlayer = true;
			this.canFollowAnimal = true;
		}
		else if (this.behaviourId == 4)
		{
			this.animalBehaviour = CommonAnimalModel.Behaviour.attackOnAnimalsOnly;
			this.canFollowPlayer = false;
			this.canFollowAnimal = true;
		}
	}

	public void setInitials(AnimalModelMeta meta, Vector3 destPt, int pathIndexRef = -1)
	{
		this.refModelMeta = new AnimalModelMeta();
		UnityEngine.Debug.Log("Coming");
		this.refModelMeta.CopyMeta(meta);
		this.health = (float)meta.health;
		string[] array = this.refModelMeta.modelMetaId.Split(new char[]
		{
			'_'
		});
		this.animalID = (int)Convert.ToInt16(array[0]);
		this.typeId = (int)Convert.ToInt16(array[1]);
		this.behaviourId = (int)Convert.ToInt16(array[2]);
		this.DestinationPos = destPt;
		if (this.behaviourId == 1)
		{
			this.animalBehaviour = CommonAnimalModel.Behaviour.simple;
			this.canFollowPlayer = false;
			this.canFollowAnimal = false;
		}
		else if (this.behaviourId == 2)
		{
			this.animalBehaviour = CommonAnimalModel.Behaviour.attackOnPlayer;
			this.canFollowPlayer = true;
			this.canFollowAnimal = false;
		}
		else if (this.behaviourId == 3)
		{
			this.animalBehaviour = CommonAnimalModel.Behaviour.bothAttacking;
			this.canFollowPlayer = true;
			this.canFollowAnimal = true;
		}
		else if (this.behaviourId == 4)
		{
			this.animalBehaviour = CommonAnimalModel.Behaviour.attackOnAnimalsOnly;
			this.canFollowPlayer = false;
			this.canFollowAnimal = true;
		}
		this.pathIndex = pathIndexRef;
	}

	public AnimalModelMeta refModelMeta;

	public float health = 100f;

	public AudioClip[] deadClips;

	public AudioClip[] hitSound;

	public AudioClip[] runSound;

	public AudioClip[] attackSound;

	public AudioClip[] idleSound;

	public int animalID = 1;

	public int typeId = 1;

	public int behaviourId = 1;

	public int pathIndex = -1;

	public Vector3 startingPos;

	public Vector3 DestinationPos;

	public Vector3 randDestinationPos;

	public bool canFollowPlayer = true;

	public bool canFollowAnimal;

	public CommonAnimalModel.State animalState;

	public CommonAnimalModel.Behaviour animalBehaviour;

	public enum State
	{
		idle,
		walking,
		running,
		attacking,
		eating,
		followingAnimal,
		dead
	}

	public enum Behaviour
	{
		simple,
		attackOnPlayer,
		bothAttacking,
		attackOnAnimalsOnly
	}
}

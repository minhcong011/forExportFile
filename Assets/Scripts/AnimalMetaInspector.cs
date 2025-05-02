// dnSpy decompiler from Assembly-CSharp.dll class: AnimalMetaInspector
using System;
using UnityEngine;

public class AnimalMetaInspector : MonoBehaviour
{
	private void Awake()
	{
		this.animalTransform = base.gameObject.transform;
	}

	private void Start()
	{
	}

	public void CopyIt(AnimalMetaInspector meta)
	{
		this.model = new AnimalModelMeta();
		this.model.CopyMeta(meta.model);
		this.stopPoint = meta.stopPoint;
		this.path = meta.path;
		this.animalTransform = meta.transform;
		this.destinationPt = meta.destinationPt;
	}

	[SerializeField]
	public AnimalModelMeta model;

	public int[] stopPoint = new int[]
	{
		2,
		4
	};

	public PathManager path;

	public Transform animalTransform;

	public Transform destinationPt;
}

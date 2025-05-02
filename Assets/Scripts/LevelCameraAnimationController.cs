// dnSpy decompiler from Assembly-CSharp.dll class: LevelCameraAnimationController
using System;
using UnityEngine;

public class LevelCameraAnimationController : MonoBehaviour
{
	private void Awake()
	{
		this.refLevelOrganiser = base.GetComponent<LevelOrganiser>();
	}

	private void Start()
	{
	}

	public void setAnimation()
	{
		this.listOfObjects[0].SetActive(true);
		this.listOfObjects[0].GetComponent<IntroCharacterMovement>().MoveOnPath(this.refLevelOrganiser.locationWavesMeta.getCurrentLocationMeta().playerLevelMeta.path[0]);
		this.cam.transform.position = this.listOfObjects[0].transform.position;
		this.cam.SetActive(true);
	}

	public void animationComplete()
	{
		this.cam.SetActive(false);
		this.listOfObjects[0].SetActive(false);
	}

	public GameObject[] listOfObjects;

	public LevelOrganiser refLevelOrganiser;

	public GameObject cam;

	public int id;
}

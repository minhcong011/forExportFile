using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
	public GameObject[] characters;

	[HideInInspector]
	public GameObject activeChild;

	private void OnTriggerEnter(Collider other)
	{
		if (!(other.transform.tag == "Btn"))
		{
			return;
		}
		characters[int.Parse(other.transform.name)].SetActive(value: true);
		characters[0].SetActive(value: false);
		other.gameObject.GetComponent<Button>().interactable = false;
		other.gameObject.GetComponent<BoxCollider>().enabled = false;
		base.gameObject.GetComponent<BoxCollider>().enabled = false;
		foreach (Transform item in base.transform)
		{
			if (item.gameObject.activeSelf)
			{
				activeChild = item.gameObject;
				break;
			}
		}
		Debug.Log(activeChild.name);
	}
}

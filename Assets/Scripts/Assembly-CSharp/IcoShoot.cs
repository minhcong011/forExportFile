using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200007D RID: 125
public class IcoShoot : MonoBehaviour
{
	// Token: 0x06000223 RID: 547 RVA: 0x0000F55C File Offset: 0x0000D75C
	private void Start()
	{
		this.PlayerC = GameObject.Find("FirstPersonCharacter");
		this.BulletPos = GameObject.Find("BulletPos");
	}

	// Token: 0x06000224 RID: 548 RVA: 0x0000F580 File Offset: 0x0000D780
	public void Shoot()
	{
		if (VariblesGlobal.PlayerHide != 1)
		{
			if (VariblesGlobal.Ammo > 0)
			{
				VariblesGlobal.Ammo--;
				GameObject.Find("IcoBulletText").GetComponent<Text>().text = string.Concat(VariblesGlobal.Ammo);
				GameObject gameObject = Object.Instantiate(Resources.Load("bullet")) as GameObject;
				gameObject.transform.position = this.BulletPos.transform.position;
				gameObject.GetComponent<Rigidbody>().AddForce(this.PlayerC.transform.forward * 1300f);
				(Object.Instantiate(Resources.Load("Sound/electricsound")) as GameObject).transform.position = base.gameObject.transform.position;
				return;
			}
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text1;
				GameObject gameObject2 = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject2.name = "infobox";
				gameObject2.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
		}
	}

	// Token: 0x04000250 RID: 592
	private GameObject PlayerC;

	// Token: 0x04000251 RID: 593
	private GameObject BulletPos;
}

using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000077 RID: 119
public class IcoBankaEnergy : MonoBehaviour
{
	// Token: 0x06000210 RID: 528 RVA: 0x0000F130 File Offset: 0x0000D330
	private void Start()
	{
		this.TextBankaEnergy = GameObject.Find("TextBankaEnergy");
		this.TextBankaEnergy.GetComponent<Text>().text = string.Concat(VariblesGlobal.BankaEnergy);
	}

	// Token: 0x06000211 RID: 529 RVA: 0x0000F164 File Offset: 0x0000D364
	public void StartEnergy()
	{
		if (VariblesGlobal.BankaEnergyTime <= 0f)
		{
			VariblesGlobal.BankaEnergy--;
			VariblesGlobal.BankaEnergyTime = 60f;
			this.TextBankaEnergy.GetComponent<Text>().text = string.Concat(VariblesGlobal.BankaEnergy);
			Object.Instantiate(Resources.Load("Sound/SoundDrink"));
			if (VariblesGlobal.BankaEnergy <= 0)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x04000247 RID: 583
	private GameObject TextBankaEnergy;
}

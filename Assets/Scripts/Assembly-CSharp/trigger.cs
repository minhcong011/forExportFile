using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200001A RID: 26
public class trigger : MonoBehaviour
{
	// Token: 0x0600005C RID: 92 RVA: 0x0000436C File Offset: 0x0000256C
	private void Start()
	{
		this.Engine = GameObject.Find("BMWX54").GetComponent<AudioSource>();
		base.GetComponent<MeshRenderer>().enabled = false;
		GameObject.Find("FinishText").GetComponent<Text>().text = VariblesGlobalText.Text29;
		GameObject.Find("TextRate").GetComponent<Text>().text = VariblesGlobalText.Text30;
		GameObject.Find("TextMain").GetComponent<Text>().text = VariblesGlobalText.Text31;
		this.Panel = GameObject.Find("Panel");
		this.Panel2 = GameObject.Find("Panel2");
		this.ImageText = GameObject.Find("ImageText");
		this.Panel2.SetActive(false);
		this.Panel.SetActive(false);
	}

	// Token: 0x0600005D RID: 93 RVA: 0x0000442D File Offset: 0x0000262D
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "Player")
		{
			this.Panel.SetActive(true);
			this.State2 = 1;
		}
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00004459 File Offset: 0x00002659
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			this.Panel.SetActive(true);
			this.State2 = 1;
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00004480 File Offset: 0x00002680
	private void Update()
	{
		if (this.State2 == 1)
		{
			if (this.Engine.volume > 0f)
			{
				this.Engine.volume -= Time.deltaTime;
			}
			if (this.State1 == 0)
			{
				this.Timer2 -= Time.deltaTime;
				if (this.Timer2 <= 0f)
				{
					this.State1 = 1;
					Object.Destroy(this.ImageText);
					this.Panel2.SetActive(true);
				}
			}
		}
	}

	// Token: 0x040000A2 RID: 162
	private GameObject Panel;

	// Token: 0x040000A3 RID: 163
	private GameObject Panel2;

	// Token: 0x040000A4 RID: 164
	private GameObject ImageText;

	// Token: 0x040000A5 RID: 165
	private float Timer2 = 5f;

	// Token: 0x040000A6 RID: 166
	private int State1;

	// Token: 0x040000A7 RID: 167
	private int State2;

	// Token: 0x040000A8 RID: 168
	private AudioSource Engine;
}

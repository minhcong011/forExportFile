using UnityEngine;

namespace UnityStandardAssets.Effects
{
	// Token: 0x02000022 RID: 34
	public class SmokeParticles : MonoBehaviour
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00004659 File Offset: 0x00002859
		private void Start()
		{
			base.GetComponent<AudioSource>().clip = this.extinguishSounds[Random.Range(0, this.extinguishSounds.Length)];
			base.GetComponent<AudioSource>().Play();
		}

		// Token: 0x0400009B RID: 155
		public AudioClip[] extinguishSounds;
	}
}

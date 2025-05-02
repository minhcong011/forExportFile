// dnSpy decompiler from Assembly-CSharp.dll class: PlayerHealth
using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	private void Awake()
	{
	}

	public void resetPlayerHealth()
	{
		this.health = 200f;
	}

	private void Start()
	{
		this.player_die = false;
		this.health = 100f;
	}

	public void DamagePlayer(float damagValue, bool via_tank)
	{
		if (this.player_die)
		{
			return;
		}
		float num = 0f;
		if (via_tank)
		{
			if (this.selectedType.Equals(PlayerHealth.PlayerType.character))
			{
				num = 3f;
			}
			else if (this.selectedType.Equals(PlayerHealth.PlayerType.tank))
			{
				num = damagValue;
			}
		}
		else
		{
			num = damagValue;
		}
		this.health -= num;
		Singleton<GameController>.Instance.gameSceneController.SetPlayersHealth(num);
	}

	public void Damage(int damaghValue, bool last_Lion)
	{
		this.health -= (float)damaghValue;
	}

	public void DamagePlayer1(float decreasingAmount)
	{
		Singleton<GameController>.Instance.gameSceneController.SetPlayersHealth(decreasingAmount);
	}

	public float health;

	private bool player_die;

	public Animator playerAnimator;

	public PlayerHealth.PlayerType selectedType;

	public enum PlayerType
	{
		character,
		tank
	}
}

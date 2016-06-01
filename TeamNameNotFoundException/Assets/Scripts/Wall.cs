using UnityEngine;
using System.Collections;
public class Wall : HittableObject
{
	public AudioClip chopSound1;				
	public AudioClip chopSound2;				
	public Sprite dmgSprite;					
	public int health = 3;						
	private SpriteRenderer spriteRenderer;		

	void Awake ()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
			
	public void DamageWall (int loss)
	{
		spriteRenderer.sprite = dmgSprite;
		Damaged (loss);
	}

	public override void Damaged (int damage)
	{
		health -= damage;
		if(health <= 0)
		{
			SoundManager.instance.PlaySingle(chopSound1);
			Destroy(gameObject, chopSound1.length / 8);
		}
		else
		{
			SoundManager.instance.PlaySingle(chopSound2);
		}
	}
}

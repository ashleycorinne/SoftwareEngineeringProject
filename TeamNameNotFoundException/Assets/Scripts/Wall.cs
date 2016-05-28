using UnityEngine;
using System.Collections;
public class Wall : HittableObject
{
	public AudioClip chopSound1;				
	public AudioClip chopSound2;				
	public Sprite dmgSprite;					
	public int health = 2;						
	private SpriteRenderer spriteRenderer;		

	void Awake ()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
			
//	public void DamageWall (int loss)
//	{
//		//SoundManager.instance.RandomizeSfx (chopSound1, chopSound2);
//		spriteRenderer.sprite = dmgSprite;
//		this.health -= loss;
//		if(this.health <= 0)
//			gameObject.SetActive (false);
//	}

	public override void Damaged(int damage)
	{
		spriteRenderer.sprite = dmgSprite;
		health -= damage;
		if(health <= 0)
		{
			gameObject.SetActive (false);
		}
	}
}

using UnityEngine;
using System.Collections;
public class Wall : MonoBehaviour
{
	public AudioClip chopSound1;				
	public AudioClip chopSound2;				
	public Sprite dmgSprite;					
	public int hp = 3;						
	private SpriteRenderer spriteRenderer;		

	void Awake ()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
			
	public void DamageWall (int loss)
	{
		spriteRenderer.sprite = dmgSprite;
		this.hp -= loss;
		if(this.hp <= 0)
        {
            SoundManager.instance.PlaySingle(chopSound1);
            //gameObject.SetActive(false);
            Destroy(gameObject, chopSound1.length);
        }
        else
        {
            SoundManager.instance.PlaySingle(chopSound2);
        }
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class Player : MovingObject
{
    public float restartLevelDelay = 1f;
    public int batteryPoints = 10;
    public float redbullPoints = 10f;
	public int enemyDamage = 2;
    public int wallDamage = 1;
    public Text BatteryText;
    public Text PlusMinusBatteryText;
    public AudioClip moveSound1;
    public AudioClip moveSound2;
    public AudioClip eatSound1;
    public AudioClip drinkSound1;
    public AudioClip gameOverSound;
    public AudioClip hurtSound;
    public AudioClip teleportSound;
    public AudioClip lowBattery;
    public AudioClip playerAttack;

    private Animator animator;
    private int battery;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        battery = GameManager.instance.playerBattery;
        BatteryText.text = battery + "%";
        PlusMinusBatteryText.text = "";
        if(!string.Equals(ApplicationData.characterName, "Ashley", System.StringComparison.CurrentCultureIgnoreCase))
            animator.runtimeAnimatorController = Resources.Load("Animations/AnimatorControllers/" + ApplicationData.characterName) as RuntimeAnimatorController;
        base.Start();
    }

    private void OnDisable()
    {
        GameManager.instance.playerBattery = battery;
    }

    private void Update()
    {
        if (!GameManager.instance.playersTurn)
            return;
        int horizontal = 0;
        int vertical = 0;
        #if UNITY_STANDALONE || UNITY_WEBPLAYER
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));
        if (horizontal != 0)
        {
            vertical = 0;
        }
        #endif
        if (horizontal != 0 || vertical != 0)
        {
			AttemptMove<HittableObject>(horizontal, vertical);
            animator.SetTrigger("playerWalk");
        }
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        if (battery <= 10)
        {
            SoundManager.instance.PlaySingle(lowBattery);
        }
        battery--;
        BatteryText.text = battery + "%";
        PlusMinusBatteryText.text = "";
        base.AttemptMove<T>(xDir, yDir);
        RaycastHit2D hit;
        if (Move(xDir, yDir, out hit))
        {
            SoundManager.instance.RandomizeSfx (moveSound1, moveSound2);
        }
        CheckIfGameOver();
        GameManager.instance.playersTurn = false;
    }

    protected override void OnCantMove<T>(T component)
    {
        animator.SetTrigger("playerChop");
        
		if(component is Wall) 
		{
			Wall hitWall = component as Wall;
			hitWall.DamageWall(wallDamage);
		} else if(component is Enemy) 
		{
			Enemy enemy = component as Enemy;
            SoundManager.instance.RandomizeSfx(playerAttack);
            enemy.Damaged (enemyDamage);
		}
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            SoundManager.instance.RSfx(teleportSound);
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Food")
        {
            battery += batteryPoints;
            PlusMinusBatteryText.text = "+" + batteryPoints;
            BatteryText.text = battery + "%";
            SoundManager.instance.RandomizeSfx (eatSound1, eatSound1);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Soda")
        {
            SoundManager.instance.RandomizeSfx (drinkSound1, drinkSound1);
            battery += batteryPoints;
            PlusMinusBatteryText.text = "+" + 5;
            BatteryText.text = battery + "%";
            this.setMoveTime(redbullPoints);
            other.gameObject.SetActive(false);
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }


    public void LoseBattery(int loss)
    {
		Damaged (loss);
    }

	public override void Damaged (int loss)
	{
		animator.SetTrigger("playerHit");
		SoundManager.instance.PlaySingle(hurtSound);
		battery -= loss;
		PlusMinusBatteryText.text = "-" + loss;
		BatteryText.text = battery + "%";
		CheckIfGameOver();
	}

    private void CheckIfGameOver()
    {
        if (battery <= 0)
        {
            SoundManager.instance.PlaySingle (gameOverSound);
            SoundManager.instance.musicSource.Stop();
            GameManager.instance.GameOver();
        }
    }

}

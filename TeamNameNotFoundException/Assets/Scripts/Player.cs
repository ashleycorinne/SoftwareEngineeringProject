using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MovingObject
{
    public float restartLevelDelay = 1f;
    public int batteryPoints = 10;
    public double redbullPoints = .01;
    public int wallDamage = 1;
    public Text BatteryText;
    public AudioClip moveSound1;
    public AudioClip moveSound2;
    public AudioClip eatSound1;
    public AudioClip eatSound2;
    public AudioClip drinkSound1;
    public AudioClip drinkSound2;
    public AudioClip gameOverSound;

    private Animator animator;
    private int battery;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        battery = GameManager.instance.playerBattery;
        BatteryText.text = battery + "%";
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
            AttemptMove<Wall>(horizontal, vertical);
            animator.SetTrigger("playerWalk");
        }
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        battery--;
        BatteryText.text = battery + "%";
        base.AttemptMove<T>(xDir, yDir);
        RaycastHit2D hit;
        if (Move(xDir, yDir, out hit))
        {
            //	SoundManager.instance.RandomizeSfx (moveSound1, moveSound2);
        }
        CheckIfGameOver();
        GameManager.instance.playersTurn = false;
    }

    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("playerChop");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Food")
        {
            battery += batteryPoints;
            BatteryText.text = "+" + batteryPoints + "        " + battery + "%";
            //	SoundManager.instance.RandomizeSfx (eatSound1, eatSound2);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Soda")
        {
            //redbull will speed character for amount of time
            
            //		SoundManager.instance.RandomizeSfx (drinkSound1, drinkSound2);
            other.gameObject.SetActive(false);
        }
    }

    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }


    public void LoseBattery(int loss)
    {
        animator.SetTrigger("playerHit");
        battery -= loss;
        BatteryText.text = "-" + loss + "        " + battery + "%";
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        //Check if food point total is less than or equal to zero.
        if (battery <= 0)
        {
            //Call the PlaySingle function of SoundManager and pass it the gameOverSound as the audio clip to play.
            		SoundManager.instance.PlaySingle (gameOverSound);

            //Stop the background music.
            //	SoundManager.instance.musicSource.Stop();

            //Call the GameOver function of GameManager.
            GameManager.instance.GameOver();
        }
    }
}

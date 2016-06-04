using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
	{
		public float levelStartDelay = 2f;						
		public float turnDelay = 0.1f;							
		public int playerBattery = 100;
        private GameObject Tom;
        public static GameManager instance = null;				
		[HideInInspector] public bool playersTurn = true;			
		private Text levelText;									
		private GameObject levelImage;
        public AudioClip cheatSound;				
		private BoardManager boardScript;					
		private int level = 1;									
		private List<Enemy> enemies;							
		private bool enemiesMoving;								
		private bool doingSetup = true;
        static bool firstRun = true;
    public GameObject bossBackground;

    //Cheat code
    private string[] cheatCode;
        private int index;
    private bool cheatEnabled;
    private GameObject cheatText;


    void Awake()
		{
			if (instance == null)
				instance = this;
			else if (instance != this)
				Destroy(gameObject);	
			DontDestroyOnLoad(gameObject);
			enemies = new List<Enemy>();
			boardScript = GetComponent<BoardManager>();
            InitGame();
            cheatCode = new string[] { "i", "d", "k", "f", "a" };
            index = 0;
    }

    void OnLevelWasLoaded(int index)
		{
            if (firstRun)
            {
                firstRun = false;
                return;
            }
            level++;
			InitGame();
		}
		
		 void InitGame()
		{
			doingSetup = true;
            cheatEnabled = false;
			levelImage = GameObject.Find("LevelImage");
			levelText = GameObject.Find("LevelText").GetComponent<Text>();
            Tom = GameObject.Find("Tom");
            cheatText = GameObject.Find("CheatText");
            bossBackground = GameObject.Find("BossBackground");
            cheatText.SetActive(false);
            levelText.text = "Level " + level;
            levelImage.SetActive(true);
            Invoke("HideLevelImage", levelStartDelay);
            enemies.Clear();
            Tom.SetActive(false);
            bossBackground.SetActive(false);
            if (level != 10)
            {
                boardScript.SetupScene(level);
                if (level == 1)
                {
                    Tom.SetActive(true);
                }
            }
            else
            {
                bossBackground.SetActive(true);
                boardScript.SetupBossScene();
            }
    }


    void HideLevelImage()
		{
			levelImage.SetActive(false);	
			doingSetup = false;
		}
		
		void Update()
		{
            if (Input.anyKeyDown)
            {
                if (!cheatEnabled && Input.GetKeyDown(cheatCode[index]))
                {
                    index++;
                if (index == cheatCode.Length)
                    cheatEnabled = true;
                }
                else {
                    index = 0;
                }
            }
            if (cheatEnabled)
            {
                SoundManager.instance.PlaySingle(cheatSound);
                cheatText.SetActive(true);
                level = 9;
            }
            if (playersTurn || enemiesMoving || doingSetup)
                return;
            StartCoroutine(MoveEnemies());
		}

		public void AddEnemyToList(Enemy script)
		{
			enemies.Add(script);
		}
		
		public void GameOver()
		{
			levelText.text = "Your laptop died! You are stuck in the computer forever. GAME OVER.";
			levelImage.SetActive(true);
			enabled = false;
		}
		
		IEnumerator MoveEnemies()
		{
			enemiesMoving = true;	
			yield return new WaitForSeconds(turnDelay);
			if (enemies.Count == 0) 
			{
				yield return new WaitForSeconds(turnDelay);
			}
			for (int i = 0; i < enemies.Count; i++)
			{
				if(enemies[i].health > 0) {
					enemies[i].MoveEnemy();
					yield return new WaitForSeconds(enemies[i].moveTime);
				}
			}
			playersTurn = true;
			enemiesMoving = false;
		}

}



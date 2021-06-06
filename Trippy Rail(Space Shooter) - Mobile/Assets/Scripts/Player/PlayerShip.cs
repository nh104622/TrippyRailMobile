//==============================================================================	
//PlayerShip Script
//
//==============================================================================	
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerShip : MonoBehaviour {
	//==========================================================================	
	public GameObject explosion;
	
	public int score = 0;
	public GameObject menuHolder;
	public int collisionDamage = 5;
	public float flickerTimeMax = 1.0f;
	public float flickerRate = 0.1f;
	public GameObject shipModel;

	private GUIStyle style;
//	private Interface menu;
	private bool damaged = false;
	private float flickerTimer;
	private float flickerInterval;

	private bool invulnerable;
	public AudioClip hitSound;
	public AudioClip destroySound;
	
	//--------------------------------------------------------------------------
	//Health Bar Attributes
	public int maxHealth = 50;
	public int health;
	private Rect box = new Rect(200, 200, 200, 20);
	private Texture2D frame;
	private Texture2D background;
	private Texture2D foreground;
	private bool paused=false;
//	private float rotateSpeed=1.0f;
	private bool rotated=false;

    public GameObject joystick;

    public GameObject ship; //for model

	public Color damageFlash = Color.red;
	private Color originalColor; //initialized to color of material attached
	private Material myMaterial; //keep a reference for optimization
	public float flashTime = .3f; //how long the flash shouldlast when taking damage
//	private float currFlashTime; //initialized to a value greater than flash time
//	private bool hit=false;

	public GameObject pMenu;	//pause menu
    public GUIStyle textStyle;
    public GUIStyle statStyle;


	//==========================================================================
	void Start(){
		//renderer.material.color = Color.blue;
//		currFlashTime = flashTime;
		myMaterial = shipModel.GetComponent<Renderer>().material;
		originalColor = myMaterial.color;






//		menu = menuHolder.GetComponent<Interface>();
		health = maxHealth;
		flickerTimer = 0.0f;
		flickerInterval = 0.0f;
		invulnerable = false;
		
		frame 	   = new Texture2D(1, 1, TextureFormat.RGB24, false);
		background = new Texture2D(1, 1, TextureFormat.RGB24, false);
		foreground = new Texture2D(1, 1, TextureFormat.RGB24, false);
		frame.SetPixel(0, 0, Color.black);
		background.SetPixel(0, 0, Color.clear);
		foreground.SetPixel(0, 0, Color.red);
		frame.Apply();
		background.Apply();
		foreground.Apply();
		paused = false;
	}
	

	//--------------------------------------------------------------------------
	void Update() {

		if (Input.GetButtonDown ("Pause")) {
            Pause();
			
		}




		if (Input.GetButtonDown ("Rotate")) {
            Rotate();
		}




	//	if( health <= 0 ){
	//		Kill();
	//	}

		if(damaged){
			DamageFlash();
		}
		
		if (health < 0){ 
			health = 0;
		}
		else if (health > maxHealth){ 
			health = maxHealth;
		}


	}


    public void Rotate()
    {
        Vector3 rotationVector2 = transform.rotation.eulerAngles;
        if (rotated)
        {
            rotationVector2.z = -90;


        }
        else {
            rotationVector2.z = 90;

        }
        transform.Rotate(rotationVector2);
        rotated = !rotated;
    }



    public void Pause()
    {
        if (Time.timeScale == 0)
        {

            pMenu.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else {
            pMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

	/*public void DamageFlash()
	{

		if (flashTime > currFlashTime)
		{
			Color currFlashColor = Color.Lerp (damageFlash,originalColor,currFlashTime/flashTime);
			foreach(Renderer r in shipModel.GetComponentsInChildren<Renderer>()){
				r.renderer.material.color=currFlashColor;
			}
			//myMaterial.color = currFlashColor;
			currFlashTime += Time.deltaTime;
		}
		else { 
			foreach(Renderer r in shipModel.GetComponentsInChildren<Renderer>()){
				r.renderer.material.color=originalColor;
			}
			hit=false;
			//myMaterial.color = originalColor;
		}
	}
*/
	
	//--------------------------------------------------------------------------
	void OnGUI() {
		//if(menu.menuDown){
			GUI.contentColor = Color.red;
            
			GUI.Label(new Rect(0, 1, 250, 250), "Total Score: " + score, textStyle);   //top left corner of screen
		
			if (paused) {
				GUI.Box (new Rect(Screen.width/2-150,Screen.height/2, 400, 200), "PAUSED");
            if (GUI.Button(new Rect(Screen.width / 2 + 100, Screen.height / 2 + 50, 100, 50), "Reset"))
                SceneManager.LoadScene("Scene"); ;
				if (GUI.Button (new Rect(Screen.width/2-100,Screen.height/2+50,100,50), "Quit"))
					Application.Quit ();

			}

			GUI.contentColor = Color.white;
			GUI.Label(new Rect(0, 25, 150, 100), "Speed ", statStyle);
			GUI.DrawTexture(new Rect(0, 95, box.width + 4, box.height + 4), frame, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect(0, 95, box.width, box.height), background, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect(0, 95, box.width*health/maxHealth, box.height), foreground, ScaleMode.StretchToFill);
		//}
	}
	
	//--------------------------------------------------------------------------
	void OnTriggerEnter(Collider other){
		if(!invulnerable){
			if(other.tag == "Indestructible" || other.tag == "Destructible" || other.tag == "EnemyBullet") {
				ApplyDamage(collisionDamage);
				damaged = true;
				invulnerable = true;
			}
		}

		if (other.tag == "item")
						Destroy (other.gameObject);

	}
	
	//--------------------------------------------------------------------------
	//Reduces health by incoming damage value.
	void ApplyDamage(int damage) {
		health -= damage;
		//hit = true;
		GetComponent<AudioSource>().PlayOneShot (hitSound);
		if (health <= 0) {
			Kill ();
		}
	}

	//--------------------------------------------------------------------------
	public void Heal(int mod){

		health += mod;

	}
	
	//--------------------------------------------------------------------------
	//Handle death animations/effects and remove object when destroyed.
	void Kill() {

        
		GetComponent<AudioSource>().clip = destroySound;
		GameObject destruction = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        GetComponent<Renderer>().enabled = false;
        ship.SetActive(false);
		GetComponent<AudioSource>().Play ();
		Destroy(this.gameObject,GetComponent<AudioSource>().clip.length);
	}
	
	//--------------------------------------------------------------------------
	public void SetScore (int inScore) {
		score += inScore;
	}
	
	//--------------------------------------------------------------------------
	void DamageFlash(){
	
		foreach(Renderer r in shipModel.GetComponentsInChildren<Renderer>()){
			r.GetComponent<Renderer>().material.color=Color.red;
		}

		//shipModel.renderer.material.color = Color.red;
		flickerTimer += Time.deltaTime;
		if(flickerTimer < flickerTimeMax){
			flickerInterval += Time.deltaTime;
			if(flickerInterval >= flickerRate){
				if(GetComponent<Renderer>().material.color == originalColor){
					foreach(Renderer r in shipModel.GetComponentsInChildren<Renderer>()){
						r.GetComponent<Renderer>().material.color=Color.red;
					}
					//shipModel.renderer.material.color = Color.red;
				}
				else{
					foreach(Renderer r in shipModel.GetComponentsInChildren<Renderer>()){
						r.GetComponent<Renderer>().material.color=originalColor;
					}
					//shipModel.renderer.material.color = baseColor;
				}
				flickerInterval = 0.0f;
			}
		}
		else{
			damaged = false;
			flickerTimer = 0.0f;
			foreach(Renderer r in shipModel.GetComponentsInChildren<Renderer>()){
				r.GetComponent<Renderer>().material.color=originalColor;
			}
			//shipModel.renderer.material.color = baseColor;
			invulnerable = false;
		}
	}
	//--------------------------------------------------------------------------
}
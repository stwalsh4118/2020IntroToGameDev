using UnityEngine;

public class Player : MonoBehaviour
{

    public float IFrames = 1f;
    public float count = 0;
    public bool Invuln = false;

    public PlayerState localPlayerData = new PlayerState();


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        count -= Time.deltaTime;
        if (count < 0)
        {
            Invuln = false;
            GetComponentInParent<playerMovement>().animator.SetBool("OnHit", Invuln);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.parent.GetComponent<playerMovement>().isRolling == false && !Invuln)
        {
            if ((other.gameObject.tag == "Donut") || (other.gameObject.tag == "Pizza") || (other.gameObject.tag == "Bone")
             || (other.gameObject.tag == "Scythe") || (other.gameObject.tag == "Green"))
            {
                Invuln = true;
                GetComponentInParent<playerMovement>().animator.SetBool("OnHit", Invuln);
                count = IFrames;
                HealthBar.playerHP.TakeDamage();
            }
        }
    }

    public void dropAbility()
    {
        if (localPlayerData.ability != null)
        {
            GameObject dropdAbility = new GameObject();
            dropdAbility.transform.position = transform.position;
            dropdAbility.transform.localScale = new Vector3(1f, 1f, 0f);
            dropdAbility.AddComponent<SpriteRenderer>();
            dropdAbility.AddComponent<droppedAbility>();
            dropdAbility.GetComponent<droppedAbility>().droppedAbil = localPlayerData.ability;
            dropdAbility.GetComponent<droppedAbility>().UpdateAbility();
        }
        
    }

    public void SavePlayerData()
    {
        StateManager.Instance.GlobalPlayerData = localPlayerData;
        Debug.Log("saving");
    }

    public void LoadPlayerData()
    {
        localPlayerData = StateManager.Instance.GlobalPlayerData;
    }
}

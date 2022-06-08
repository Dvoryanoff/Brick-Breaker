using UnityEngine;

public class Brick : MonoBehaviour
{
    public int health { get; private set; }
    public Sprite[] state;
    public bool unbreackable;
    public int points = 100;
    
    public SpriteRenderer spriteRenderer { get; private set; }

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ResetBrick();
    }

    public void ResetBrick()
    {
        this.gameObject.SetActive(true);
        if (!this.unbreackable)
        {
            this.health = this.state.Length;
            this.spriteRenderer.sprite = this.state[this.health - 1];
        }
    }

    private void Hit()
    {
        if (this.unbreackable)
            return;
        this.health--;

        if (this.health <= 0)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.spriteRenderer.sprite = this.state[this.health - 1];
        }

        FindObjectOfType<GameManager>().Hit(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            Hit();
        }
    }
}
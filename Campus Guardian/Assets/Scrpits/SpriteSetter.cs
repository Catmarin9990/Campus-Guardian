using UnityEngine;
using UnityEngine.UI;

public class SpriteSetter : MonoBehaviour
{
    private Image sprite;
    [SerializeField] private Sprite GirlSprite;
    private GameController gameController;
    void Start()
    {
        gameController = FindAnyObjectByType<GameController>();
        sprite = GetComponent<Image>();
        if (gameController.CurrentStudnet.isGirl)
            sprite.sprite = GirlSprite;
    }
}

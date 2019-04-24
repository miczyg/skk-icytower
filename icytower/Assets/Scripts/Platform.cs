using Assets.Scripts;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Constants.Tags.Finish)
        {
            GameController.Instance.Scored();
        }
    }
}

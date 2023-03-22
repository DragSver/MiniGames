using UnityEngine;

namespace MiniGames.WolfAndEggs
{
    public class CatchZone : MonoBehaviour
    {
        public bool ThisBasket;
        public bool ThisEgg;

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.GetComponent<Basket>())
                ThisBasket = true;
            else if (collider.gameObject.GetComponent<Egg>())
                ThisEgg = true;
            // if (ThisEgg & ThisBasket) 
            //     collider.gameObject.GetComponent<Egg>().Catch();
        }
        public void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.gameObject.GetComponent<Basket>())
                ThisBasket = false;
        }
    }
}
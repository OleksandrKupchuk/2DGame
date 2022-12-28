using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public bool IsGround { get; private set; }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            IsGround = true;
            print("on ground");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            IsGround = false;
            print("in air");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : Character
{
    private Idle _idle;
    private void Start()
    {
        _idle = new Idle();
        _idle.EnterState(this);
    }
}

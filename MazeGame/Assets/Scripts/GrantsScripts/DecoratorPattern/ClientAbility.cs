using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientAbility : MonoBehaviour
{
    private List<IAbility> _abilities;

    public static void UseAbility(IAbility ability)
    {
        ability.activateAbility();
    }
}

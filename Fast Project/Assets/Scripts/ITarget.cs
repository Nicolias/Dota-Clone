using Characters;
using UnityEngine;

public interface ITarget
{
    public GameObject GameObject { get; }
    public SideType Side { get; }
}
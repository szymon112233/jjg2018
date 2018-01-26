using UnityEngine;

public class Employee : MonoBehaviour {

    [SerializeField] protected Ability programingSpeed;
    [SerializeField] protected Ability musicSpeed;
    [SerializeField] protected Ability artSpeed;
    [SerializeField] protected Ability testSpeed;

    private Game _game;

    protected virtual void Update()
    {
        if (_game != null)
        {
            WorkOnGame();
        }
            
    }

    protected virtual void WorkOnGame()
    {

    }

}

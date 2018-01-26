using UnityEngine;

public class Employee : MonoBehaviour {

    protected Ability programing;
    protected Ability musicSpeed;
    protected Ability artSpeed;
    protected Ability testSpeed;

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

    public void SetGame(Game game, )
}

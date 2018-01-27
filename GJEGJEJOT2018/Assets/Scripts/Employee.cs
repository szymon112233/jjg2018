using UnityEngine;

public class Employee : MonoBehaviour {

    [SerializeField] protected Ability programming;
    [SerializeField] protected Ability music;
    [SerializeField] protected Ability art;
    [SerializeField] protected Ability testing;

    private Game _game;
    private TaskEnum _gameTask;
    private Ability _currAbility;

    protected virtual void Update()
    {
        if (_game != null)
        {
            WorkOnGame();
        }
    }

    protected virtual void WorkOnGame()
    {
        _game.WorkOn(_currAbility.Value);
    }

    public virtual void Build()
    {
        programming.GenerateValue();
        music.GenerateValue();
        art.GenerateValue();
        testing.GenerateValue();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Game.tagName))
        {
            Debug.Log("GAME RECEIVED: " + collision.gameObject.GetComponent<Game>().Name);
            RemoveGame();
            AddGame(collision.gameObject.GetComponent<Game>());
        }
    }

    public void AddGame(Game game)
    {
        _game = game;
        _currAbility = GetAbility(game.CurrTask);
    }

    public void RemoveGame()
    {
        _game = null;
        _currAbility = null;
    }

    private Ability GetAbility(TaskEnum task)
    {
        switch (task)
        {
            case TaskEnum.ART:
                return this.art;
            case TaskEnum.MUSIC:
                return this.music;
            case TaskEnum.PROGRAMMING:
                return this.programming;
            case TaskEnum.TESTING:
                 return this.testing;
            default:
                return new Ability();
        }
    }
}

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
        _game.WorkOn(_gameTask, _currAbility.Value);
    }

    public virtual void Build()
    {
        programming.GenerateValue();
        music.GenerateValue();
        art.GenerateValue();
        testing.GenerateValue();
    }

    public void AddGame(Game game, TaskEnum task)
    {
        _game = game;
        _currAbility = GetAbility(task);
        _gameTask = task;
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

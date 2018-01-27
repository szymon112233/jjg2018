using UnityEngine;

public class Employee : MonoBehaviour {

    [SerializeField] protected Ability programming;
    [SerializeField] protected Ability music;
    [SerializeField] protected Ability art;
    [SerializeField] protected Ability testing;

    [SerializeField] private BarsController bars;

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
        bool isEnd = _game.WorkOn(_currAbility.Value, bars.progressBar);

        if (isEnd)
        {
            RemoveGame();
        }
    }

    public virtual void Build()
    {
        bars.objectToFollow = this.transform;
        for (int i = 0; i <= (int)TaskEnum.TESTING; i++)
            GenerateValue((TaskEnum)i);
	}

    private void GenerateValue(TaskEnum task)
    {
        var ability = GetAbility(task);
        ability.GenerateValue();
        bars.GetBar(task).ChangeValue(ability.Value);
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
		if (_game)
			GetComponent<SocketAttacher>().AttachToGameSocket(_game);
	}

    public void RemoveGame()
    {
		if(_game != null)
		{
			SocketAttacher socketAttacher = GetComponent<SocketAttacher>();
			socketAttacher.DetachFromGameSocket(_game);
			socketAttacher.MoveToDropSocket(_game);
		}
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

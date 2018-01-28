using UnityEngine;

public class Employee : MonoBehaviour
{
    private static RectTransform _mainCanvas;
    private static RectTransform MainCanvas
    {
        get
        {
            if (_mainCanvas == null)
                _mainCanvas = GameObject.FindGameObjectWithTag(GameDisplay.tagName).GetComponent<RectTransform>();
            return _mainCanvas;
        }
    }

    [SerializeField] private GameObject BarsPrefab;
    [SerializeField] private AudioSource whoopieAudio;
    [SerializeField] private AudioSource typingAudio;


    [SerializeField] protected Ability programming;
    [SerializeField] protected Ability music;
    [SerializeField] protected Ability art;
    [SerializeField] protected Ability testing;

	private Animator animator;
    private BarsController bars;

	private Game _game;
    private TaskEnum _gameTask;
    private Ability _currAbility;
	private float productivity = 100;

    private void Awake()
    {
        Build();
    }

    protected virtual void Update()
    {
        if (_game != null)
        {
            WorkOnGame();
        }
		else
		if (productivity%5 < 0.4f && Random.Range(0.0f, 80.0f) > productivity)
		{
			animator.SetTrigger("Bored");
			EndBoredom();
		}
		else
		{
			productivity -= Time.deltaTime*Random.Range(1f, 3f);
		}
	}

    protected virtual void WorkOnGame()
    {
        bool isEnd = _game.WorkOn(_currAbility.Value, bars.progressBar);
        if (!typingAudio.isPlaying)
        {
            float randomDelay = Random.value;
            typingAudio.PlayDelayed(randomDelay);
        }
            

        if (isEnd)
        {
            typingAudio.Stop();
			animator.SetTrigger("Finished");
            whoopieAudio.PlayDelayed(0.5f);
            RemoveGame();
			EndBoredom();

		}
	}
	public void EndBoredom()
	{
			productivity = 100;
	}

	public virtual void Build()
    {
        bars = Instantiate(BarsPrefab, MainCanvas.transform).GetComponent<BarsController>();
        bars.targetCanvas = MainCanvas;
        bars.objectToFollow = this.transform;
        for (int i = 0; i <= (int)TaskEnum.TESTING; i++)
            GenerateValue((TaskEnum)i);
		animator = transform.parent.GetComponentInChildren<Animator>();
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
		if (_currAbility.Value < 0.3f)
			animator.SetBool("Unhappy",true);
		animator.SetBool("Working", true);
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
		animator.SetBool("Unhappy", false);
		animator.SetBool("Working", false);

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

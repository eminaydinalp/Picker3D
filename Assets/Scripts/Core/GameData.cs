public class GameData : Singleton<GameData>
{
    public GameMode GameMode = GameMode.Normal;
    public bool IsStart { get; set; }
    private int _currentLevel;

    public int CurrentLevel
    {
        get { return _currentLevel; }
        set 
        { 
            _currentLevel = value;
        }
    }
    private int _infinityLevel;

    public int InfinityLevel
    {
        get { return _infinityLevel; }
        set { _infinityLevel = value; }
    }

    private int _cache;

    public int Cache
    {
        get { return _cache; }
        set { _cache = value; }
    }

    private bool _isLevelEnd;
    public bool IsLevelEnd
    {
        get { return _isLevelEnd; } // level bitti
        set { _isLevelEnd = value; }
    }
    
    private int _score;

    public int Score
    {
        get { return _score; }
        set 
        { 
            _score = value; 
            FindObjectOfType<GameplayCanvas>().IncreaseScore(_score);
        }
    }

    private int _collectedObjectCount;
    public int CollectedObjectCount
    {
        get { return _collectedObjectCount; }
        set 
        { 
            _collectedObjectCount = value;
        }
    }
    private int[] _collectedObjectCountForPassLevelArray = new int[] {5,6,7,8,9,10,10,10,11,11};

    public int CollectedObjectCountForPassLevel
    {
        get { return _collectedObjectCountForPassLevelArray[_currentLevel];}
    }

    public int CollectedObjectCountForPassLevelForInfinity
    {
        get { return _collectedObjectCountForPassLevelArray[_infinityLevel]; }
    }
}

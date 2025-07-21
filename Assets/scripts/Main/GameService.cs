using Puzzle.Board;
using Puzzle.Events;
using Puzzle.GameCamera;
using Puzzle.Gaps;
using Puzzle.Level;
using Puzzle.Sound;
using Puzzle.Spider;
using Puzzle.UI;
using Puzzle.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        //Services
        public BoardService BoardService {  get; private set; }
        public SpiderService SpiderService { get; private set; }
        public GapService GapService { get; private set; }
        public EventService EventService { get; private set; }
        public LevelService LevelService { get; private set; }
        public SoundService SoundService { get; private set; }

        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;

        [SerializeField] private CameraService cameraService;
        public CameraService CameraService => cameraService;

        //Prefabs
        [SerializeField] private GameObject gapRingPrefab;
        [SerializeField] private RingView ringPrefab;
        [SerializeField] private GameObject turnImage;
        [SerializeField] private SpiderView spiderView;
        [SerializeField] private GapView gapPrefab;

        //Scriptable Objects
        [SerializeField] private List<LevelScriptableObject> levelSOList;
        [SerializeField] private SoundScriptableObject soundScriptableObject;

        //Scene References
        [SerializeField] private AudioSource bgmSource;
        [SerializeField] private AudioSource sfxSource;

        protected override void Awake()
        {
            base.Awake();

            EventService = new EventService();
            SoundService = new SoundService(soundScriptableObject, sfxSource, bgmSource);
            LevelService = new LevelService(levelSOList);
            BoardService = new BoardService(gapRingPrefab, ringPrefab, turnImage);
            GapService = new GapService(gapPrefab);
            SpiderService = new SpiderService(spiderView);
        }

        private void Start() => UIService.CreateLevelSelectionUI(levelSOList.Count);
    }
}
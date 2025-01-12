using System.Collections.Generic;
using UnityEngine;
using utils;

namespace ChessClub
{
    public class GameManager : MonoBehaviour,IHpManager
    {
        public GameObject playerPrefab;
        public GameObject bossPrefab;
        public GameData gameData;
        public GameObject mapPrefab;
        private GameObject _boss;
        private HpManager _hpManager;
        private GameObject _map;
        private GameObject _player;
        public GameObject wallPrefab;
        public List<GameObject> walls;
        public List<Vector2> udlr;


        // Start is called before the first frame update
        private void Start()
        {
            udlr = new List<Vector2>();
            udlr.Add(Vector2.up);
            udlr.Add(Vector2.down);
            udlr.Add(Vector2.left);
            udlr.Add(Vector2.right);
            Cursor.visible = false;
            _map = Instantiate(mapPrefab, gameData.mapCenter, transform.rotation);

            _boss = Instantiate(bossPrefab, gameData.bossInitPosition, transform.rotation);
            _player = Instantiate(playerPrefab, gameData.playerInitPosition, transform.rotation);
            _hpManager = new HpManager();
            var playerIhp = _player.GetComponent<Ihp>();
            playerIhp.SetHpManager(_hpManager);
            playerIhp.SetHp(gameData.playerInitHp);
            var bossIhp = _boss.GetComponent<Ihp>();
            bossIhp.SetHpManager(_hpManager);
            bossIhp.SetHp(gameData.bossInitHp);
            gameData.mapSize = _map.GetComponent<SpriteRenderer>().size;
            gameData.wallWidth = gameData.mapSize.x * 0.265625f*2.0f;//magic number by artist
                walls = new List<GameObject>();

            foreach (var dir in udlr)
            {
                GameObject newWall = Instantiate(wallPrefab);
                newWall.transform.position = new Vector2(gameData.mapSize.x * 0.5f*dir.x, gameData.mapSize.y * 0.5f*dir.y);
                newWall.GetComponent<BoxCollider2D>().size =new Vector2(gameData.wallWidth*Mathf.Abs(dir.x)+gameData.mapSize.x*Mathf.Abs(dir.y),gameData.mapSize.y*Mathf.Abs(dir.x) + gameData.wallWidth*Mathf.Abs(dir.y));
                walls.Add(newWall);
            }
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
        }

        public HpManager GetHpManager()
        {
            return _hpManager;
        }
    }
}
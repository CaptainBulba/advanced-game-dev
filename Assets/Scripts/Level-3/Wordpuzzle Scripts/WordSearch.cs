using UnityEngine;
using System.Collections.Generic;

public class WordSearch : MonoBehaviour {
	
    public bool useWordpool; //to add .txt file
    public TextAsset wordpool; // fetch words from .txt file
    public string[] words; // array to hold words from .txt file
    public int maxWordCount; // max number of words we want 
	public int maxWordLetters; // max charcter of word that we want to use
    public int gridX, gridY;
    public float spacing; // spac between tiles

    private LevelController levelController;
    private GameObject puzzleButton;


    public GameObject tile, background, current;
    public Color defaultTint, mouseoverTint, identifiedTint;
    public bool ready = false, correct = false;
    public string selectedString = "";

    public List<GameObject> selected = new List<GameObject>(); //to make a list of latter that mouse hovers on
    
    
    private List<GameObject> tiles = new List<GameObject>();
    private float sensitivity = 0.9f;
    private GameObject temporary, backgroundObject;
    private int identified = 0;

    private string[,] matrix; //to locate character string from grid

    private Dictionary<string, bool> word = new Dictionary<string, bool>();
    private Dictionary<string, bool> insertedWords = new Dictionary<string, bool>();
    
    private string[] letters = new string[26]
	{"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
    
    private Ray ray;
    private RaycastHit hit;
    private int mark = 0;

    private static WordSearch instance; //made instance of the class

    public static WordSearch Instance {
        get {
			return instance;
		}
    }

	void Awake() {
        instance = this;
    }

    void Start() 
    {
        //finding the number of words dnd addeing it in words array
        List<string> findLength = new List<string>(); 
        int count = 0;

        puzzleButton = GetComponent<PrefabSettings>().GetButton();
        levelController = GetComponent<PrefabSettings>().GetLevelController();

        if (useWordpool) 
        {
            words = wordpool.text.Split(';');
        } 
        else
        {
            maxWordCount = words.Length;
        }

        if (maxWordCount <= 0) {
            maxWordCount = 1;
        }

        Mix(words); //juggle the words

        Mathf.Clamp(maxWordLetters, 0, gridY < gridX ? gridX : gridY);
       
        while (findLength.Count < maxWordCount + 1) {
            if (words[count].Length <= maxWordLetters) {
                findLength.Add(words[count]);
            } 
			count++;
        }

        //adding words in dictoney and making all to uppercase
        for (int i = 0; i < maxWordCount; i++) {
            if (!word.ContainsKey(findLength[i].ToUpper()) && !word.ContainsKey(findLength[i])) {
                    word.Add(findLength[i], false);
            }
        }

        Mathf.Clamp01(sensitivity);
        matrix = new string[gridX, gridY];

        //create grid with words on background
        InstantiateBG();

        //adding tiles or box
        for (int i = 0; i < gridX; i++) {
            for (int j = 0; j < gridY; j++) {
                temporary = Instantiate(tile, new Vector3(i * 1 * tile.transform.localScale.x * spacing, 10, j * 1 * tile.transform.localScale.z * spacing), Quaternion.identity) as GameObject;
                temporary.name = "tile-" + i.ToString() + "-" + j.ToString();
                temporary.transform.eulerAngles = new Vector3(90, 0, 0);
                temporary.transform.parent = backgroundObject.transform;
                BoxCollider boxCollider = temporary.GetComponent<BoxCollider>() as BoxCollider;
                boxCollider.size = new Vector3(sensitivity, 1, sensitivity);
                temporary.GetComponent<Letters>().letter.text = "";
                temporary.GetComponent<Letters>().gridX = i;
                temporary.GetComponent<Letters>().gridY = j;
                tiles.Add(tile);
                matrix[i, j] = "";
            }
        }
        CenterBG();
        InsertWords();
        FillRemaining();      
    }

    //transforms for background
    private void CenterBG() {
        backgroundObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2), 200));
        backgroundObject.transform.eulerAngles = new Vector3(-90, 0, 0);
    }

    //create grid with words on background
    private void InstantiateBG() {
		if (gridX % 2 != 0 && gridY % 2 == 0) 
        {
			backgroundObject = Instantiate (background, new Vector3 ((tile.transform.localScale.x * spacing)
			* (gridX / 2), 1, (tile.transform.localScale.z * spacing)
			* (gridY / 2) - (tile.transform.localScale.z * spacing)), Quaternion.identity) as GameObject;
		} 
        else if (gridX % 2 == 0 && gridY % 2 != 0)
        {
			backgroundObject = Instantiate (background, new Vector3 ((tile.transform.localScale.x * spacing) * (gridX / 2)
			- (tile.transform.localScale.x * spacing), 1, (tile.transform.localScale.z * spacing) * (gridY / 2)), Quaternion.identity) as GameObject;
		} 
        else 
        {
			backgroundObject = Instantiate(background, new Vector3 ((tile.transform.localScale.x * spacing) * (gridX / 2) -
				(tile.transform.localScale.x * spacing), 1, (tile.transform.localScale.z * spacing) * (gridY / 2) - (tile.transform.localScale.z * spacing)), Quaternion.identity) as GameObject;
		}
        backgroundObject.transform.localScale = new Vector3(((tile.transform.localScale.x * spacing) * gridX), 1, ((tile.transform.localScale.x * spacing) * gridY));
   }

    void Update() {
        if (Input.GetMouseButton(0)) 
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) 
            {
                current = hit.transform.gameObject;
            }
            ready = true;
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) 
            {
                current = hit.transform.gameObject;
            }
            Verify();

            if (identified == word.Count)
            {
                levelController.LaunchMainScreen(puzzleButton);
               
            }
        }        
    }

	private void Verify() {
        if (!correct) 
        {
            foreach (KeyValuePair<string, bool> p in insertedWords) {
                if (selectedString.ToLower() == p.Key.Trim().ToLower()) {
                    foreach (GameObject g in selected) {
                        g.GetComponent<Letters>().identified = true;
                    }
                    correct = true;
                }             
            }
        }

        if (correct) {
            insertedWords.Remove(selectedString);       

			if (word.ContainsKey (selectedString)) {
				insertedWords.Add (selectedString, true);
			}
            identified++;
        }
        ready = false;
        selected.Clear();
        selectedString = "";
        correct = false;

    }

    // adding words in the background
    private void InsertWords() {
        System.Random rn = new System.Random();
        foreach (KeyValuePair<string, bool> p in word) {
            string s = p.Key.Trim();
            bool placed = false;
            while (placed == false) {
                int row = rn.Next(gridX);
                int column = rn.Next(gridY);
                int directionX = 0;
                int directionY = 0;
                while (directionX == 0 && directionY == 0) {
                    directionX = rn.Next(3) - 1;
                    directionY = rn.Next(3) - 1;
                }                
                placed = InsertWord(s.ToUpper(), row, column, directionX, directionY);
                mark++;
                if (mark > 100) {
                    break;
                }
            }
        }
    }

    private bool InsertWord(string word, int row, int column, int directionX, int directionY) {
        if (directionX > 0) {
            if (row + word.Length >= gridX) {
                return false;
            }
        }
        if (directionX < 0) {
            if (row - word.Length < 0) {
                return false;
            }
        }
        if (directionY > 0) {
            if (column + word.Length >= gridY) {
                return false;
            }
        }
        if (directionY < 0) {
            if (column - word.Length < 0) {
                return false;
            }
        }

		if (((0 * directionY) + column) == gridY - 1) {
			return false;
		}
	
        for (int i = 0; i < word.Length; i++) {
			if (!string.IsNullOrEmpty (matrix [(i * directionX) + row, (i * directionY) + column])) {
				return false;
			}
        }

        insertedWords.Add(word, false);
        char[] w = word.ToCharArray();
        for (int i = 0; i < w.Length; i++) {
            matrix[(i * directionX) + row, (i * directionY) + column] = w[i].ToString();
            GameObject.Find("tile-" + ((i * directionX) + row).ToString() + "-" + ((i * directionY) + column).ToString()).GetComponent<Letters>().letter.text = w[i].ToString();
        }
        return true;
    }

    //filling empty tiles with other latters
    private void FillRemaining() {
        for (int i = 0; i < gridX; i++) {
            for (int j = 0; j < gridY; j++) {
                if (matrix[i, j] == "") {
                    matrix[i, j] = letters[UnityEngine.Random.Range(0, letters.Length)];
                    GameObject.Find("tile-" + i.ToString() + "-" + j.ToString()).GetComponent<Letters>().letter.text = matrix[i, j];
                }
            }
        }
    }

    //mixing words
    private void Mix(string[] words) {
        for (int t = 0; t < words.Length; t++) {
            string tmp = words[t];
            int r = UnityEngine.Random.Range(t, words.Length);
            words[t] = words[r];
            words[r] = tmp;
        }
    }

    void OnGUI() {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.EndHorizontal();
   
        foreach (KeyValuePair<string, bool> p in insertedWords) {
            GUILayout.BeginHorizontal();
            GUILayout.Label("   " + p.Key);
            if (p.Value) {
                GUILayout.Label("*");
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }
}
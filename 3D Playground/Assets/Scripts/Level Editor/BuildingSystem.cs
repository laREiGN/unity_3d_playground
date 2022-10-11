using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem self;

     public GridLayout gridLayout;
     private Grid grid;

     [SerializeField] private Tilemap tilemap;
     [SerializeField] private TileBase emptyTile;

    [Header ("Placeable Prefabs")]
     public GameObject treePrefab;
     public GameObject smallStonePrefab;
     public GameObject bigStonePrefab;

     private PlaceableObject objectToPlace;


    #region Unity Methods

    private void Awake()
    {
        self = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InitializeWithObject(treePrefab);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InitializeWithObject(smallStonePrefab);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InitializeWithObject(bigStonePrefab);
        }
    }

    #endregion

    #region Utils

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit)){
            return raycastHit.point;
        } else {
            return Vector3.zero;
        }
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPosition = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPosition);
        return new Vector3(0, position.y, position.z);
    }

    #endregion

    #region Building Placement

    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);

        GameObject gameObject = Instantiate(prefab, position, Quaternion.identity);
        objectToPlace = gameObject.GetComponent<PlaceableObject>();
        gameObject.AddComponent<ObjectDrag>();
        
    }

    #endregion
}

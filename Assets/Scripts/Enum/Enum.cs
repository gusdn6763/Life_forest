
/// <summary>
/// 도로의 연결을 돕는 열겨형 타입
/// </summary>
public enum RayPoint
{
    NONE,
    LEFT,
    RIGHT,
    TOP,
    BOTTOM
};

/// <summary>
/// 어떠한 건물인지 참조할 타입
/// </summary>
public enum BuildingType
{
    NONE,
    HOUSE,
    STORE,
    COMPANY,
    NATURE
}

/// <summary>
/// 건물의 방향을 정하는 타입
/// </summary>
public enum Dir
{
    Down,
    Left,
    Up,
    Right,
}

/// <summary>
/// 
/// </summary>
public enum CellType
{
    Empty,
    Road,
    Structure,
    SpecialStructure,
    None
}

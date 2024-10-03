using UnityEngine;

// 기본 Tile 클래스
public class Tile : MonoBehaviour
{
    public virtual Tile GetNextTile(Vector2 direction)
    {
        return null; // 기본 구현은 null 반환
    }
}

// 왼쪽 아래로 가는 타일
public class DownLTile : Tile
{
    public Tile downTile; // 아래로 연결된 타일
    public Tile leftTile; // 왼쪽으로 연결된 타일

    public override Tile GetNextTile(Vector2 direction)
    {
        if (direction == Vector2.down && downTile != null)
            return downTile;
        else if (direction == Vector2.left && leftTile != null)
            return leftTile;

        return null;
    }
}

// 오른쪽 아래로 가는 타일
public class DownRTile : Tile
{
    public Tile downTile;
    public Tile rightTile;

    public override Tile GetNextTile(Vector2 direction)
    {
        if (direction == Vector2.down && downTile != null)
            return downTile;
        else if (direction == Vector2.right && rightTile != null)
            return rightTile;

        return null;
    }
}

// 수평 타일
public class HorizontalTile : Tile
{
    public Tile leftTile;
    public Tile rightTile;

    public override Tile GetNextTile(Vector2 direction)
    {
        if (direction == Vector2.left && leftTile != null)
            return leftTile;
        else if (direction == Vector2.right && rightTile != null)
            return rightTile;

        return null;
    }
}

// 위쪽 왼쪽으로 가는 타일
public class UpLTile : Tile
{
    public Tile upTile;
    public Tile leftTile;

    public override Tile GetNextTile(Vector2 direction)
    {
        if (direction == Vector2.up && upTile != null)
            return upTile;
        else if (direction == Vector2.left && leftTile != null)
            return leftTile;

        return null;
    }
}

// 위쪽 오른쪽으로 가는 타일
public class UpRTile : Tile
{
    public Tile upTile;
    public Tile rightTile;

    public override Tile GetNextTile(Vector2 direction)
    {
        if (direction == Vector2.up && upTile != null)
            return upTile;
        else if (direction == Vector2.right && rightTile != null)
            return rightTile;

        return null;
    }
}

// 수직 타일
public class VerticalTile : Tile
{
    public Tile upTile;
    public Tile downTile;

    public override Tile GetNextTile(Vector2 direction)
    {
        if (direction == Vector2.up && upTile != null)
            return upTile;
        else if (direction == Vector2.down && downTile != null)
            return downTile;

        return null;
    }
}

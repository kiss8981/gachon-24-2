using System;

public class TowerFactory
{
    public static Tower CreateTower(TowerType type)
    {
        switch (type)
        {
            case TowerType.Elsa:
                return new ElsaTower();
            case TowerType.Ttakkeum:
                return new TtakkeumTower();
            // 더 많은 타워 추가 가능
            default:
                throw new ArgumentException("Invalid Tower Type");
        }
    }
}

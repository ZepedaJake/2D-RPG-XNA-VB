
Public Structure Tile

    Public Property TerrainType As TileType
    'Public Property TileGFX As Texture2D
    Public Property SrcRectangle As Rectangle
    'Public Property Location As Vector2
    Public Property AniFrame As Integer

    'Tile Actions
    'Public Property IsActivated As Boolean
    Public Property IsBlocked As Boolean
    'Public Property IsTouchTrigger As Boolean
    Public Property IsStepTrigger As Boolean
    'Public Property TriggerScript As Boolean

End Structure

Public Enum TileType
    Wall
    RightEdge
    LeftEdge
    SideWall

    TopRight1
    TopRight2
    BottomRight1
    BottomRight2
    TopLeft1
    TopLeft2
    BottomLeft1
    BottomLeft2

    Blank
    Floor
    Pyramid
    FloorFancy

    PortalGreen
    PortalRed

    BronzeKey
    BronzeDoor
    GoldKey
    GoldDoor
    SilverKey
    SilverDoor

    Grass
    Water
    Foothill
    Mountain
End Enum


Public Structure Interact
    Public Property InteractItem As InteractType
    Public Property IsBlocked As Boolean
    Public Property SrcRectangle As Rectangle
    Public Property Alive As Boolean
End Structure

Public Enum InteractType
    'prevent drawing over tiles with black
    Empty


    BronzeKey
    SilverKey
    GoldKey

    BronzeDoor
    SilverDoor
    GoldDoor

    SideBronzeDoor
    SideSilverDoor
    SideGoldDoor

    BronzeSword
    SilverSword
    GoldSword

    BronzeShield
    SilverShield
    GoldShield

    PortalGreen
    PortalRed

    'wall. to be on a second layer
    Wall
    SideWall
    LeftLongWall1
    LeftLongWall2 'extends left
    RightLongWall1
    RightLongWall2
    InLeftCorner1
    InLeftCorner2 ' corner on the left
    InRightCorner1
    InRightCorner2
    InLeftMidCorner1
    InLeftMidCorner2 'corner near middle on left
    InRightMidCorner1
    InRightMidCorner2

    'simplified cornering
    MidCorner
    RightCorner
    LeftCorner
    HalfWallRight 'extends right
    HalfWallLeft
    Pyramid

    CursedSword
    CursedShield

    Fountain11
    Fountain12
    Fountain13
    Fountain14
    Fountain15

    Fountain21
    Fountain22
    Fountain23
    Fountain24
    Fountain25

    Fountain31
    Fountain32
    Fountain33
    Fountain34
    Fountain35

    HandLeft
    HandRight

    Black
    Hood
End Enum

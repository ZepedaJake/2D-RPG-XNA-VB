Public Structure Enemy
    Public Property HP As Integer
    Public Property Def As Integer
    Public Property Atk As Integer
    Public Property XP As Integer
    Public Property SrcRectangle As Rectangle
    Public Property EnemyName As EnemyType
    Public Property Alive As Boolean
    'Public Property X As Integer
    'Public Property Y As Integer

End Structure

Public Enum EnemyType
    'prevent drawing over tiles with black
    Empty

    RedSlime
    PinkSlime
    Golem
    Bat
    Knight
    AscendantMale
    AscendantFemale
    CursedAscendantMale
    CursedAscendantFemale
    HeavyKnight

End Enum
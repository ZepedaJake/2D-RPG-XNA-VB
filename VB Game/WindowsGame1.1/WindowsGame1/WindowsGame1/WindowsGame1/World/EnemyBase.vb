Public Class EnemyBase
    Public EnemyList(0, 0) As Enemy
    Public MapName As String = ""
    Public MapHeight As Integer
    Public MapWidth As Integer


    Public Sub New(width As Integer, height As Integer)
        ReDim EnemyList(width, height)

        MapHeight = height
        MapWidth = width


        'draw things
        For x = 0 To MapWidth
            For y = 0 To MapHeight
                EnemyList(x, y).EnemyName = InteractType.Empty
            Next
        Next



        'With EnemyList(4, 2)
        '    .EnemyName = EnemyType.HeavyKnight
        '    .HP = 85
        '    .Atk = 30
        '    .Def = 40
        '    .XP = 45
        'End With

        'With EnemyList(10, 10)
        '    .EnemyName = EnemyType.AscendantMale
        '    .HP = 200
        '    .Atk = 70
        '    .Def = 70
        '    .XP = 88
        'End With

        'With EnemyList(11, 10)
        '    .EnemyName = EnemyType.AscendantFemale
        '    .HP = 250
        '    .Atk = 50
        '    .Def = 85
        '    .XP = 88
        'End With

        'With EnemyList(12, 10)
        '    .EnemyName = EnemyType.CursedAscendantFemale
        '    .HP = 450
        '    .Atk = 100
        '    .Def = 120
        '    .XP = 130
        'End With

        'With EnemyList(13, 10)
        '    .EnemyName = EnemyType.CursedAscendantMale
        '    .HP = 300
        '    .Atk = 110
        '    .Def = 90
        '    .XP = 130
        'End With

        'With EnemyList(11, 13)
        '    .EnemyName = EnemyType.Golem
        '    .HP = 100
        '    .Atk = 30
        '    .Def = 50
        '    .XP = 50
        'End With

        'With EnemyList(15, 13)
        '    .EnemyName = EnemyType.Knight
        '    .HP = 70
        '    .Atk = 25
        '    .Def = 30
        '    .XP = 30
        'End With

        'With EnemyList(12, 15)
        '    .EnemyName = EnemyType.RedSlime
        '    .HP = 50
        '    .Atk = 15
        '    .Def = 10
        '    .XP = 15
        'End With

        'For x = 10 To 21
        '    With EnemyList(x, 22)
        '        .EnemyName = EnemyType.PinkSlime
        '        .HP = 10
        '        .Atk = 2
        '        .Def = 0
        '        .XP = 4
        '    End With
        'Next
        'For x = 13 To 22
        '    With EnemyList(x, 21)
        '        .EnemyName = EnemyType.Bat
        '        .HP = 15
        '        .Atk = 5
        '        .Def = 2
        '        .XP = 8
        '    End With
        'Next
        'get source
        For x = 0 To width
            For y = 0 To height
                EnemyList(x, y).SrcRectangle = GetTileSource(EnemyList(x, y).EnemyName)
            Next
        Next

        For x = 0 To MapWidth
            For y = 0 To MapHeight
                If EnemyList(x, y).EnemyName <> EnemyType.Empty Then
                    EnemyList(x, y).Alive = True
                Else
                    EnemyList(x, y).Alive = False
                End If

            Next
        Next

    End Sub
    Private Function GetTileSource(EType As EnemyType) As Rectangle
        Dim sRect As Rectangle
        Select Case EType
            Case EnemyType.Empty
                sRect = New Rectangle(64, 0, 32, 32)
            Case EnemyType.RedSlime
                sRect = New Rectangle(0, 96, 32, 32)
            Case EnemyType.PinkSlime
                sRect = New Rectangle(32, 96, 32, 32)
            Case EnemyType.Golem
                sRect = New Rectangle(64, 96, 32, 32)
            Case EnemyType.Bat
                sRect = New Rectangle(0, 128, 32, 32)
            Case EnemyType.Knight
                sRect = New Rectangle(96, 96, 32, 32)
            Case EnemyType.AscendantMale
                sRect = New Rectangle(0, 160, 32, 32)
            Case EnemyType.AscendantFemale
                sRect = New Rectangle(32, 160, 32, 32)
            Case EnemyType.CursedAscendantMale
                sRect = New Rectangle(0, 192, 32, 32)
            Case EnemyType.CursedAscendantFemale
                sRect = New Rectangle(32, 192, 32, 32)
            Case EnemyType.HeavyKnight
                sRect = New Rectangle(32, 128, 32, 32)
        End Select
        Return sRect
    End Function



End Class

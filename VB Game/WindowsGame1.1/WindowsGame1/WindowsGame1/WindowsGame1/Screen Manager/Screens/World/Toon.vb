Partial Public Class WorldScreen
    'screen position
    Public ToonScreenX As Integer = 5
    Public ToonScreenY As Integer = 5

    'Toon Map Coord
    Public ToonX As Integer = 0
    Public ToonY As Integer = 0

    'Toon Map Offset
    Public ToonOffsetX As Integer = 0
    Public ToonOffsetY As Integer = 0

    'movement
    Public ToonMoving As Boolean = False
    Public MoveTime As Double = 0
    Public MoveSpeed As Integer = 3
    Public MoveDir As Integer = 0
    Public LastDir As Integer = 1

    Private ToonFrame As Integer = 0

    'stats
    Public Shared ToonHP As Integer = 0

    Public Shared ToonAtk As Integer = 0
    Public Shared ToonAllAtk As Integer = 0
    Public Shared StatAtkBoost As Integer
    Public Shared ItemAtkBoost As Integer = 0

    Public Shared ToonDef As Integer = 0
    Public Shared ToonAllDef As Integer = 0
    Public Shared StatDefBoost As Integer = 0
    Public Shared ItemDefBoost As Integer = 0

    Public Shared ToonLvl As Integer = 0
    Public Shared XpTillLvl As Integer = 0
    Public Shared StatPoints As Integer = 0
    Public Shared SkillPoints As Integer = 0
    Public Shared SkillAttack As Integer = 0
    Public Shared SkillDefend As Integer = 0
    Public Shared SkillPower As Integer = 0
    Public Shared SkillHeal As Integer = 0


    Public Shared ToonFrontX As Integer = 0
    Public Shared ToonFrontY As Integer = 0

    Public Sub Move(dir As Integer)
        MoveDir = dir
        Select Case dir
            Case 1 'crouch
                ToonOffsetY -= MoveSpeed

                If ToonOffsetY <= -32 Then
                    MapY += 1
                    ToonOffsetY = 0
                End If
            Case 2 'Left
                ToonOffsetX += MoveSpeed

                If ToonOffsetX >= 32 Then
                    MapX -= 1
                    ToonOffsetX = 0
                End If


            Case 3 'Right
                ToonOffsetX -= MoveSpeed

                If ToonOffsetX <= -32 Then
                    MapX += 1
                    ToonOffsetX = 0
                End If
            Case 4 'Up 
                ToonOffsetY += MoveSpeed

                If ToonOffsetY >= 32 Then
                    MapY -= 1
                    ToonOffsetY = 0
                End If
        End Select

        If ToonOffsetX <> 0 Then
            ToonFrame = Math.Floor(Math.Abs(ToonOffsetX) / 32 * 3)
        ElseIf ToonOffsetY <> 0 Then
            ToonFrame = Math.Floor(Math.Abs(ToonOffsetY) / 32 * 3)
        End If

        If MoveDir <> 0 Then
            LastDir = dir
        End If

    End Sub

    Public Sub MoveToon(dir As Integer, toonx As Integer, toony As Integer)
        Try
            If (Map.TileList(toonx, toony).IsBlocked = False) AndAlso (Interacters.InteractList(toonx, toony).IsBlocked = False) Then
                MoveDir = dir
                ToonMoving = True
            End If
        Catch
        End Try
    End Sub


    Private Function FetchToonSrc(dir As Integer) As Rectangle
        Select Case dir
            Case 1 'down
                If Right = True Then
                    sRect = New Rectangle(32 * ToonFrame, 64, 32, 32)
                Else
                    sRect = New Rectangle(32 * (ToonFrame + 2), 64, 32, 32)
                End If
            'sRect = New Rectangle(32 * ToonFrame, 64, 32, 32)
            'sRect = New Rectangle(16, 384, 80, 176)
            'sRect = New Rectangle((80 * ToonFrame) + 16, 384, 80, 176)
            Case 2 'Left
                If Right Then
                    sRect = New Rectangle(32 * ToonFrame, 0, 32, 32)
                Else
                    sRect = New Rectangle(32 * (ToonFrame + 2), 0, 32, 32)
                End If
            'sRect = New Rectangle(32 * ToonFrame, 0, 32, 32)
            'sRect = New Rectangle(16, 192, 80, 176)
            'sRect = New Rectangle((80 * ToonFrame) + 16, 192, 80, 176)
            Case 3 'Right
                If Right Then
                    sRect = New Rectangle(32 * ToonFrame, 32, 32, 32)
                Else
                    sRect = New Rectangle(32 * (ToonFrame + 2), 32, 32, 32)
                End If

            'sRect = New Rectangle(16, 0, 80, 176)
            'sRect = New Rectangle((80 * ToonFrame) + 16, 0, 80, 176)
            Case 4 'up
                If Right Then
                    sRect = New Rectangle(32 * ToonFrame, 96, 32, 32)
                Else
                    sRect = New Rectangle(32 * (ToonFrame + 2), 96, 32, 32)
                End If

        End Select
        Return sRect
    End Function
End Class

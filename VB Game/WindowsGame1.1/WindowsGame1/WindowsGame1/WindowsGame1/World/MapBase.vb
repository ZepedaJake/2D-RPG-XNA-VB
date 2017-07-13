Public Class MapBase
    Public TileList(0, 0) As Tile
    Public StartLocation As Vector2
    Public MapName As String = ""
    Public MapHeight As Integer
    Public MapWidth As Integer



    Public Sub New(width As Integer, height As Integer, start As Vector2)
        ReDim TileList(width, height)

        StartLocation = New Vector2(start.X, start.Y)
        MapHeight = height
        MapWidth = width


        'Temp Map
        For X = 0 To width
            For Y = 0 To height
                TileList(X, Y) = New Tile
                With TileList(X, Y)
                    .TerrainType = TileType.Blank

                    .IsBlocked = False
                    .IsStepTrigger = False
                    '.TriggerScript = False
                End With
            Next
        Next

        'floor
        For z = 0 To width
            For c = 0 To height
                TileList(z, c).TerrainType = TileType.Floor
            Next
        Next

        'Draw Border

        'Corners
        With TileList(0, 0)
            .TerrainType = TileType.TopLeft1
            .IsBlocked = True
        End With

        With TileList(1, 0)
            .TerrainType = TileType.TopLeft2
            .IsBlocked = True
        End With

        With TileList(width - 1, 0)
            .TerrainType = TileType.TopRight1
            .IsBlocked = True
        End With

        With TileList(width, 0)
            .TerrainType = TileType.TopRight2
            .IsBlocked = True
        End With

        With TileList(0, height)
            .TerrainType = TileType.BottomLeft1
            .IsBlocked = True
        End With

        With TileList(1, height)
            .TerrainType = TileType.BottomLeft2
            .IsBlocked = True
        End With

        With TileList(width - 1, height)
            .TerrainType = TileType.BottomRight1
            .IsBlocked = True
        End With

        With TileList(width, height)
            .TerrainType = TileType.BottomRight2
            .IsBlocked = True
        End With

        'Draw Edges
        For c = 1 To height - 1
            TileList(0, c).TerrainType = TileType.RightEdge
            TileList(0, c).IsBlocked = True

        Next

        For c = 1 To height - 1
            TileList(width, c).TerrainType = TileType.LeftEdge
            TileList(width, c).IsBlocked = True

        Next

        For z = 2 To width - 2
            TileList(z, 0).TerrainType = TileType.Wall
            TileList(z, 0).IsBlocked = True
        Next

        For z = 2 To width - 2
            TileList(z, height).TerrainType = TileType.Wall
            TileList(z, height).IsBlocked = True
        Next





        For x = 0 To width
            For y = 0 To height
                TileList(x, y).SrcRectangle = GetTileSource(TileList(x, y).TerrainType)
            Next
        Next
    End Sub

    Private Function GetTileSource(TType As TileType) As Rectangle
        Dim sRect As Rectangle
        Select Case TType
            'Case TileType.Grass
            '    sRect = New Rectangle(0, 0, 16, 16)
            'Case TileType.Water
            '    sRect = New Rectangle(47, 0, 16, 16)
            'Case TileType.Foothill
            '    sRect = New Rectangle(33, 0, 16, 16)
            'Case TileType.Mountain
            '    sRect = New Rectangle(16, 0, 16, 16)

            'walls
            Case TileType.Wall
                sRect = New Rectangle(64, 64, 32, 32)
            Case TileType.RightEdge
                sRect = New Rectangle(0, 32, 32, 32)
            Case TileType.LeftEdge
                sRect = New Rectangle(128, 32, 32, 32)
            Case TileType.SideWall
                sRect = New Rectangle(192, 64, 32, 32)

            'corners
            Case TileType.TopLeft1
                sRect = New Rectangle(0, 0, 32, 32)
            Case TileType.TopLeft2
                sRect = New Rectangle(32, 0, 32, 32)
            Case TileType.TopRight1
                sRect = New Rectangle(96, 0, 32, 32)
            Case TileType.TopRight2
                sRect = New Rectangle(128, 0, 32, 32)
            Case TileType.BottomLeft1
                sRect = New Rectangle(0, 64, 32, 32)
            Case TileType.BottomLeft2
                sRect = New Rectangle(32, 64, 32, 32)
            Case TileType.BottomRight1
                sRect = New Rectangle(96, 64, 32, 32)
            Case TileType.BottomRight2
                sRect = New Rectangle(128, 64, 32, 32)

            Case TileType.Blank
                sRect = New Rectangle(64, 32, 32, 32)


            'other
            Case TileType.Pyramid
                sRect = New Rectangle(32, 32, 32, 32)
            Case TileType.Floor
                sRect = New Rectangle(96, 32, 32, 32)
            Case TileType.FloorFancy
                sRect = New Rectangle(192, 64, 32, 32)

            'Stairs
            Case TileType.PortalRed
                sRect = New Rectangle(192, 0, 32, 32)
            Case TileType.PortalGreen
                sRect = New Rectangle(192, 32, 32, 32)

            'Keys / Doors
            Case TileType.BronzeKey
                sRect = New Rectangle(160, 0, 32, 32)
            Case TileType.BronzeDoor
                sRect = New Rectangle(224, 0, 32, 32)
            Case TileType.SilverKey
                sRect = New Rectangle(160, 64, 32, 32)
            Case TileType.SilverDoor
                sRect = New Rectangle(224, 64, 32, 32)
            Case TileType.GoldKey
                sRect = New Rectangle(160, 32, 32, 32)
            Case TileType.GoldDoor
                sRect = New Rectangle(224, 32, 32, 32)
        End Select
        Return sRect
    End Function
End Class

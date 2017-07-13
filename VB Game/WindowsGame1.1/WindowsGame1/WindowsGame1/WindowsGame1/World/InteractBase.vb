Public Class InteractBase
    Public InteractList(0, 0) As Interact
    Public MapName As String = ""
    Public MapHeight As Integer
    Public MapWidth As Integer

    Public Sub New(width As Integer, height As Integer)
        ReDim InteractList(width, height)

        MapHeight = height
        MapWidth = width
        'draw things
        For x = 0 To MapWidth
            For y = 0 To MapHeight
                InteractList(x, y).InteractItem = InteractType.Empty
            Next
        Next

        'walls

        'end draw

        'get source
        For x = 0 To width
            For y = 0 To height
                InteractList(x, y).SrcRectangle = GetTileSource(InteractList(x, y).InteractItem)
            Next
        Next


        For x = 0 To MapWidth
            For y = 0 To MapHeight
                If InteractList(x, y).InteractItem <> InteractType.Empty Then
                    InteractList(x, y).Alive = True
                Else
                    InteractList(x, y).Alive = False
                End If

            Next
        Next
    End Sub



    Public Function GetTileSource(IType As InteractType) As Rectangle
        Dim sRect As Rectangle

        Select Case IType
            'nothing
            Case InteractType.Empty
                sRect = New Rectangle(64, 0, 32, 32)

            'Stairs
            Case InteractType.PortalRed
                sRect = New Rectangle(192, 0, 32, 32)
            Case InteractType.PortalGreen
                sRect = New Rectangle(192, 32, 32, 32)

            'Keys / Doors
            Case InteractType.BronzeKey
                sRect = New Rectangle(160, 0, 32, 32)
            Case InteractType.BronzeDoor
                sRect = New Rectangle(224, 0, 32, 32)
            Case InteractType.SilverKey
                sRect = New Rectangle(160, 64, 32, 32)
            Case InteractType.SilverDoor
                sRect = New Rectangle(224, 64, 32, 32)
            Case InteractType.GoldKey
                sRect = New Rectangle(160, 32, 32, 32)
            Case InteractType.GoldDoor
                sRect = New Rectangle(224, 32, 32, 32)
            Case InteractType.SideBronzeDoor
                sRect = New Rectangle(256, 0, 32, 32)
            Case InteractType.SideSilverDoor
                sRect = New Rectangle(256, 64, 32, 32)
            Case InteractType.SideGoldDoor
                sRect = New Rectangle(256, 32, 32, 32)
            Case InteractType.BronzeSword
                sRect = New Rectangle(288, 0, 32, 32)
            Case InteractType.BronzeShield
                sRect = New Rectangle(288, 32, 32, 32)
            Case InteractType.SilverSword
                sRect = New Rectangle(288, 64, 32, 32)
            Case InteractType.SilverShield
                sRect = New Rectangle(288, 96, 32, 32)
            Case InteractType.GoldSword
                sRect = New Rectangle(288, 128, 32, 32)
            Case InteractType.GoldShield
                sRect = New Rectangle(288, 160, 32, 32)

            'walls
            Case InteractType.Wall
                sRect = New Rectangle(64, 64, 32, 32)
            Case InteractType.SideWall
                sRect = New Rectangle(192, 64, 32, 32)

            Case InteractType.RightLongWall1
                sRect = New Rectangle(160, 128, 32, 32)
            Case InteractType.RightLongWall2
                sRect = New Rectangle(192, 128, 32, 32)

            Case InteractType.LeftLongWall1
                sRect = New Rectangle(224, 128, 32, 32)
            Case InteractType.LeftLongWall2
                sRect = New Rectangle(256, 128, 32, 32)

            Case InteractType.InLeftCorner1
                sRect = New Rectangle(160, 96, 32, 32)
            Case InteractType.InLeftCorner2
                sRect = New Rectangle(192, 96, 32, 32)

            Case InteractType.InRightCorner1
                sRect = New Rectangle(224, 96, 32, 32)
            Case InteractType.InRightCorner2
                sRect = New Rectangle(256, 96, 32, 32)

            Case InteractType.InLeftMidCorner1
                sRect = New Rectangle(160, 160, 32, 32)
            Case InteractType.InLeftMidCorner2
                sRect = New Rectangle(192, 160, 32, 32)

            Case InteractType.InRightMidCorner1
                sRect = New Rectangle(224, 160, 32, 32)
            Case InteractType.InRightMidCorner2
                sRect = New Rectangle(256, 160, 32, 32)

            Case InteractType.HalfWallRight
                sRect = New Rectangle(128, 192, 32, 32)
            Case InteractType.LeftCorner
                sRect = New Rectangle(160, 192, 32, 32)
            Case InteractType.MidCorner
                sRect = New Rectangle(192, 192, 32, 32)
            Case InteractType.RightCorner
                sRect = New Rectangle(224, 192, 32, 32)
            Case InteractType.HalfWallLeft
                sRect = New Rectangle(256, 192, 32, 32)

            Case InteractType.Pyramid
                sRect = New Rectangle(32, 32, 32, 32)

            Case InteractType.CursedSword
                sRect = New Rectangle(288, 192, 32, 32)
            Case InteractType.CursedShield
                sRect = New Rectangle(288, 224, 32, 32)

            Case InteractType.HandLeft
                sRect = New Rectangle(288, 256, 32, 32)
            Case InteractType.HandRight
                sRect = New Rectangle(288, 288, 32, 32)

            Case InteractType.Fountain11
                sRect = New Rectangle(0, 224, 32, 32)
            Case InteractType.Fountain12
                sRect = New Rectangle(32, 224, 32, 32)
            Case InteractType.Fountain13
                sRect = New Rectangle(64, 224, 32, 32)
            Case InteractType.Fountain14
                sRect = New Rectangle(96, 224, 32, 32)
            Case InteractType.Fountain15
                sRect = New Rectangle(128, 224, 32, 32)

            Case InteractType.Fountain21
                sRect = New Rectangle(0, 256, 32, 32)
            Case InteractType.Fountain22
                sRect = New Rectangle(32, 256, 32, 32)
            Case InteractType.Fountain23
                sRect = New Rectangle(64, 256, 32, 32)
            Case InteractType.Fountain24
                sRect = New Rectangle(96, 256, 32, 32)
            Case InteractType.Fountain25
                sRect = New Rectangle(128, 256, 32, 32)

            Case InteractType.Fountain31
                sRect = New Rectangle(0, 288, 32, 32)
            Case InteractType.Fountain32
                sRect = New Rectangle(32, 288, 32, 32)
            Case InteractType.Fountain33
                sRect = New Rectangle(64, 288, 32, 32)
            Case InteractType.Fountain34
                sRect = New Rectangle(96, 288, 32, 32)
            Case InteractType.Fountain35
                sRect = New Rectangle(128, 288, 32, 32)

            Case InteractType.Black
                sRect = New Rectangle(64, 32, 32, 32)
            Case InteractType.Hood
                sRect = New Rectangle(64, 128, 32, 32)
        End Select

        Return sRect

    End Function

End Class

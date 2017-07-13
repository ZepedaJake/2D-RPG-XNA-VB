Imports System.IO
Imports System.IO.Compression

Public Class MapHandler
    Private NewMap As MapBase
    Private MapName As String = ""
    Private NewInteracter As InteractBase
    Private NewEnemies As EnemyBase


    Private EnemyTemp As EnemyBase
    Private MapTemp As MapBase
    Private InteractTemp As InteractBase
    Private SavedMap As Integer


    'map stuff
    Public Function LoadMap(MapName As String) As MapBase
        Using fStream As FileStream = New FileStream("maps/" & MapName & ".map", FileMode.Open)
            Using zipStream As GZipStream = New GZipStream(fStream, CompressionMode.Decompress)
                Using reader As New BinaryReader(zipStream)
                    'Header info
                    MapName = reader.ReadString
                    NewMap = New MapBase(reader.ReadInt32, reader.ReadInt32, New Vector2(reader.ReadSingle, reader.ReadSingle))
                    NewMap.MapName = MapName


                    For X = 0 To NewMap.MapWidth
                        For Y = 0 To NewMap.MapHeight
                            NewMap.TileList(X, Y) = New Tile
                            With NewMap.TileList(X, Y)
                                .SrcRectangle = New Rectangle(reader.ReadInt32, reader.ReadInt32, 32, 32) 'adjust to tile sizes currently 32x32
                                .IsBlocked = reader.ReadBoolean
                                .IsStepTrigger = reader.ReadBoolean

                                '.TriggerScript = reader.ReadString
                            End With
                        Next
                    Next
                    reader.Close()
                End Using
                zipStream.Close()
            End Using
            fStream.Close()
        End Using

        Return NewMap
    End Function

    Public Sub SaveMap(Map As MapBase, MapName As String)
        Using fStream As FileStream = New FileStream("maps/" & MapName & ".map", FileMode.Create)
            Using zipStream As GZipStream = New GZipStream(fStream, CompressionMode.Compress)
                Using writer As New BinaryWriter(zipStream)
                    writer.Write(MapName)
                    writer.Write(Map.MapWidth)
                    writer.Write(Map.MapHeight)
                    writer.Write(Map.StartLocation.X)
                    writer.Write(Map.StartLocation.Y)


                    For X = 0 To Map.MapWidth
                        For Y = 0 To Map.MapHeight
                            writer.Write(Map.TileList(X, Y).SrcRectangle.X)
                            writer.Write(Map.TileList(X, Y).SrcRectangle.Y)
                            writer.Write(Map.TileList(X, Y).IsBlocked)
                            writer.Write(Map.TileList(X, Y).IsStepTrigger)

                            'writer.Write(Map.TileList(X, Y).TriggerScript)

                        Next
                    Next

                    writer.Close()
                End Using
                zipStream.Close()
            End Using
            fStream.Close()
        End Using
    End Sub
    'end map stuff

    Public Function LoadInteract(MapName As String) As InteractBase
        Using fStream As FileStream = New FileStream("maps/" & MapName & ".interacters", FileMode.Open)
            Using zipStream As GZipStream = New GZipStream(fStream, CompressionMode.Decompress)
                Using reader As New BinaryReader(zipStream)
                    'Header info
                    'MapName = reader.ReadString
                    NewInteracter = New InteractBase(reader.ReadInt32, reader.ReadInt32)
                    NewInteracter.MapName = MapName


                    For X = 0 To NewInteracter.MapWidth
                        For Y = 0 To NewInteracter.MapHeight
                            NewInteracter.InteractList(X, Y) = New Interact
                            With NewInteracter.InteractList(X, Y)
                                .InteractItem = reader.ReadInt32
                                .Alive = reader.ReadBoolean
                                .SrcRectangle = New Rectangle(reader.ReadInt32, reader.ReadInt32, 32, 32) 'adjust to tile sizes currently 32x32
                                .IsBlocked = reader.ReadBoolean
                                '.IsStepTrigger = reader.ReadBoolean

                                '.TriggerScript = reader.ReadString
                            End With
                        Next
                    Next
                    reader.Close()
                End Using
                zipStream.Close()
            End Using
            fStream.Close()
        End Using

        Return NewInteracter
    End Function

    Public Sub SaveInteract(Interacters As InteractBase, MapName As String)
        Using fStream As FileStream = New FileStream("maps/" & MapName & ".interacters", FileMode.Create)
            Using zipStream As GZipStream = New GZipStream(fStream, CompressionMode.Compress)
                Using writer As New BinaryWriter(zipStream)
                    'writer.Write(MapName)
                    writer.Write(Interacters.MapWidth)
                    writer.Write(Interacters.MapHeight)
                    'writer.Write(Map.StartLocation.X)
                    'writer.Write(Map.StartLocation.Y)


                    For X = 0 To Interacters.MapWidth
                        For Y = 0 To Interacters.MapHeight
                            writer.Write(Interacters.InteractList(X, Y).InteractItem)
                            writer.Write(Interacters.InteractList(X, Y).Alive)
                            writer.Write(Interacters.InteractList(X, Y).SrcRectangle.X)
                            writer.Write(Interacters.InteractList(X, Y).SrcRectangle.Y)
                            writer.Write(Interacters.InteractList(X, Y).IsBlocked)
                            'writer.Write(Interacters.InteractList(X, Y).IsStepTrigger)

                            'writer.Write(Map.TileList(X, Y).TriggerScript)

                        Next
                    Next

                    writer.Close()
                End Using
                zipStream.Close()
            End Using
            fStream.Close()
        End Using
    End Sub

    'load enemeis
    Public Function LoadEnemies(MapName As String) As EnemyBase
        Using fStream As FileStream = New FileStream("maps/" & MapName & ".enemies", FileMode.Open)
            Using zipStream As GZipStream = New GZipStream(fStream, CompressionMode.Decompress)
                Using reader As New BinaryReader(zipStream)
                    'Header info
                    'MapName = reader.ReadString
                    NewEnemies = New EnemyBase(reader.ReadInt32, reader.ReadInt32)
                    NewEnemies.MapName = MapName


                    For X = 0 To NewEnemies.MapWidth
                        For Y = 0 To NewEnemies.MapHeight
                            NewEnemies.EnemyList(X, Y) = New Enemy
                            With NewEnemies.EnemyList(X, Y)
                                .EnemyName = reader.ReadInt32
                                .SrcRectangle = New Rectangle(reader.ReadInt32, reader.ReadInt32, 32, 32) 'adjust to tile sizes currently 32x32
                                .HP = reader.ReadInt32
                                .Atk = reader.ReadInt32
                                .Def = reader.ReadInt32
                                .XP = reader.ReadInt32
                                .Alive = reader.ReadBoolean
                                '.X = reader.ReadInt32
                                '.Y = reader.ReadInt32
                                '.IsStepTrigger = reader.ReadBoolean

                                '.TriggerScript = reader.ReadString
                            End With
                        Next
                    Next
                    reader.Close()
                End Using
                zipStream.Close()
            End Using
            fStream.Close()
        End Using

        Return NewEnemies
    End Function

    Public Sub SaveEnemies(Enemies As EnemyBase, MapName As String)
        Using fStream As FileStream = New FileStream("maps/" & MapName & ".enemies", FileMode.Create)
            Using zipStream As GZipStream = New GZipStream(fStream, CompressionMode.Compress)
                Using writer As New BinaryWriter(zipStream)
                    'writer.Write(MapName)
                    writer.Write(Enemies.MapWidth)
                    writer.Write(Enemies.MapHeight)


                    'writer.Write(Map.StartLocation.X)
                    'writer.Write(Map.StartLocation.Y)


                    For X = 0 To Enemies.MapWidth
                        For Y = 0 To Enemies.MapHeight
                            writer.Write(Enemies.EnemyList(X, Y).EnemyName)
                            writer.Write(Enemies.EnemyList(X, Y).SrcRectangle.X)
                            writer.Write(Enemies.EnemyList(X, Y).SrcRectangle.Y)
                            writer.Write(Enemies.EnemyList(X, Y).HP)
                            writer.Write(Enemies.EnemyList(X, Y).Atk)
                            writer.Write(Enemies.EnemyList(X, Y).Def)
                            writer.Write(Enemies.EnemyList(X, Y).XP)
                            writer.Write(Enemies.EnemyList(X, Y).Alive)
                            'writer.Write(Enemies.EnemyList(X, Y).X)
                            'writer.Write(Enemies.EnemyList(X, Y).Y)

                            'writer.Write(Enemyers.EnemyList(X, Y).IsStepTrigger)

                            'writer.Write(Map.TileList(X, Y).TriggerScript)

                        Next
                    Next

                    writer.Close()
                End Using
                zipStream.Close()
            End Using
            fStream.Close()
        End Using
    End Sub

    'load enemeis
    Public Function ReloadEnemies(MapName As String) As EnemyBase
        Using fStream As FileStream = New FileStream("saves/" & MapName & "Enemies.tmp", FileMode.Open)
            Using zipStream As GZipStream = New GZipStream(fStream, CompressionMode.Decompress)
                Using reader As New BinaryReader(zipStream)
                    'Header info
                    'MapName = reader.ReadString
                    EnemyTemp = New EnemyBase(reader.ReadInt32, reader.ReadInt32)
                    EnemyTemp.MapName = MapName


                    For X = 0 To EnemyTemp.MapWidth
                        For Y = 0 To EnemyTemp.MapHeight
                            EnemyTemp.EnemyList(X, Y) = New Enemy
                            With EnemyTemp.EnemyList(X, Y)
                                .EnemyName = reader.ReadInt32
                                .SrcRectangle = New Rectangle(reader.ReadInt32, reader.ReadInt32, 32, 32) 'adjust to tile sizes currently 32x32
                                .HP = reader.ReadInt32
                                .Atk = reader.ReadInt32
                                .Def = reader.ReadInt32
                                .XP = reader.ReadInt32
                                .Alive = reader.ReadBoolean
                                '.X = reader.ReadInt32
                                '.Y = reader.ReadInt32
                                '.IsStepTrigger = reader.ReadBoolean

                                '.TriggerScript = reader.ReadString
                            End With
                        Next
                    Next
                    reader.Close()
                End Using
                zipStream.Close()
            End Using
            fStream.Close()
        End Using

        Return EnemyTemp
    End Function

    Public Sub ReSaveEnemies(Enemies As EnemyBase, MapName As String)
        Using fStream As FileStream = New FileStream("saves/" & MapName & "Enemies.tmp", FileMode.Create)
            Using zipStream As GZipStream = New GZipStream(fStream, CompressionMode.Compress)
                Using writer As New BinaryWriter(zipStream)
                    'writer.Write(MapName)
                    writer.Write(Enemies.MapWidth)
                    writer.Write(Enemies.MapHeight)


                    'writer.Write(Map.StartLocation.X)
                    'writer.Write(Map.StartLocation.Y)


                    For X = 0 To Enemies.MapWidth
                        For Y = 0 To Enemies.MapHeight
                            writer.Write(Enemies.EnemyList(X, Y).EnemyName)
                            writer.Write(Enemies.EnemyList(X, Y).SrcRectangle.X)
                            writer.Write(Enemies.EnemyList(X, Y).SrcRectangle.Y)
                            writer.Write(Enemies.EnemyList(X, Y).HP)
                            writer.Write(Enemies.EnemyList(X, Y).Atk)
                            writer.Write(Enemies.EnemyList(X, Y).Def)
                            writer.Write(Enemies.EnemyList(X, Y).XP)
                            writer.Write(Enemies.EnemyList(X, Y).Alive)
                            'writer.Write(Enemies.EnemyList(X, Y).X)
                            'writer.Write(Enemies.EnemyList(X, Y).Y)

                            'writer.Write(Enemyers.EnemyList(X, Y).IsStepTrigger)

                            'writer.Write(Map.TileList(X, Y).TriggerScript)

                        Next
                    Next

                    writer.Close()
                End Using
                zipStream.Close()
            End Using
            fStream.Close()
        End Using
    End Sub

    Public Function ReloadInteract(MapName As String) As InteractBase
        Using fStream As FileStream = New FileStream("saves/" & MapName & "Interacters.tmp", FileMode.Open)
            Using zipStream As GZipStream = New GZipStream(fStream, CompressionMode.Decompress)
                Using reader As New BinaryReader(zipStream)
                    'Header info
                    'MapName = reader.ReadString
                    InteractTemp = New InteractBase(reader.ReadInt32, reader.ReadInt32)
                    InteractTemp.MapName = MapName


                    For X = 0 To InteractTemp.MapWidth
                        For Y = 0 To InteractTemp.MapHeight
                            InteractTemp.InteractList(X, Y) = New Interact
                            With InteractTemp.InteractList(X, Y)
                                .InteractItem = reader.ReadInt32
                                .Alive = reader.ReadBoolean
                                .SrcRectangle = New Rectangle(reader.ReadInt32, reader.ReadInt32, 32, 32) 'adjust to tile sizes currently 32x32
                                .IsBlocked = reader.ReadBoolean
                                '.IsStepTrigger = reader.ReadBoolean

                                '.TriggerScript = reader.ReadString
                            End With
                        Next
                    Next
                    reader.Close()
                End Using
                zipStream.Close()
            End Using
            fStream.Close()
        End Using

        Return InteractTemp
    End Function

    Public Sub ReSaveInteract(Interacters As InteractBase, MapName As String)
        Using fStream As FileStream = New FileStream("saves/" & MapName & "Interacters.tmp", FileMode.Create)
            Using zipStream As GZipStream = New GZipStream(fStream, CompressionMode.Compress)
                Using writer As New BinaryWriter(zipStream)

                    writer.Write(Interacters.MapWidth)
                    writer.Write(Interacters.MapHeight)

                    For X = 0 To Interacters.MapWidth
                        For Y = 0 To Interacters.MapHeight
                            writer.Write(Interacters.InteractList(X, Y).InteractItem)
                            writer.Write(Interacters.InteractList(X, Y).Alive)
                            writer.Write(Interacters.InteractList(X, Y).SrcRectangle.X)
                            writer.Write(Interacters.InteractList(X, Y).SrcRectangle.Y)
                            writer.Write(Interacters.InteractList(X, Y).IsBlocked)
                        Next
                    Next
                    writer.Close()
                End Using
                zipStream.Close()
            End Using
            fStream.Close()
        End Using
    End Sub

    'save Terry
    Public Sub SaveTerry()
        Using fStream As FileStream = New FileStream("saves/terry.sav", FileMode.Create)
            Using zipStream As GZipStream = New GZipStream(fStream, CompressionMode.Compress)
                Using writer As New BinaryWriter(zipStream)

                    writer.Write(WorldScreen.ToonHP)

                    writer.Write(WorldScreen.ToonAtk)
                    writer.Write(WorldScreen.ToonAllAtk)
                    writer.Write(WorldScreen.StatAtkBoost)
                    writer.Write(WorldScreen.ItemAtkBoost)

                    writer.Write(WorldScreen.ToonDef)
                    writer.Write(WorldScreen.ToonAllDef)
                    writer.Write(WorldScreen.StatDefBoost)
                    writer.Write(WorldScreen.ItemDefBoost)


                    writer.Write(WorldScreen.ToonLvl)
                    writer.Write(WorldScreen.XpTillLvl)
                    writer.Write(WorldScreen.StatPoints)

                    writer.Write(WorldScreen.SkillPoints)
                    writer.Write(WorldScreen.SkillAttack)
                    writer.Write(WorldScreen.SkillDefend)
                    writer.Write(WorldScreen.SkillPower)
                    writer.Write(WorldScreen.SkillHeal)

                    writer.Write(WorldScreen.BronzeKeys)
                    writer.Write(WorldScreen.SilverKeys)
                    writer.Write(WorldScreen.GoldKeys)

                    writer.Close()
                End Using
                zipStream.Close()
            End Using
            fStream.Close()
        End Using
    End Sub

    Public Sub LoadTerry()
        Using fStream As FileStream = New FileStream("saves/terry.sav", FileMode.Open)
            Using zipStream As GZipStream = New GZipStream(fStream, CompressionMode.Decompress)
                Using reader As New BinaryReader(zipStream)

                    WorldScreen.ToonHP = reader.ReadInt32

                    WorldScreen.ToonAtk = reader.ReadInt32
                    WorldScreen.ToonAllAtk = reader.ReadInt32
                    WorldScreen.StatAtkBoost = reader.ReadInt32
                    WorldScreen.ItemAtkBoost = reader.ReadInt32

                    WorldScreen.ToonDef = reader.ReadInt32
                    WorldScreen.ToonAllDef = reader.ReadInt32
                    WorldScreen.StatDefBoost = reader.ReadInt32
                    WorldScreen.ItemDefBoost = reader.ReadInt32

                    WorldScreen.ToonLvl = reader.ReadInt32
                    WorldScreen.XpTillLvl = reader.ReadInt32
                    WorldScreen.StatPoints = reader.ReadInt32

                    WorldScreen.SkillPoints = reader.ReadInt32
                    WorldScreen.SkillAttack = reader.ReadInt32
                    WorldScreen.SkillDefend = reader.ReadInt32
                    WorldScreen.SkillPower = reader.ReadInt32
                    WorldScreen.SkillHeal = reader.ReadInt32

                    WorldScreen.BronzeKeys = reader.ReadInt32
                    WorldScreen.SilverKeys = reader.ReadInt32
                    WorldScreen.GoldKeys = reader.ReadInt32

                    reader.Close()
                End Using
                zipStream.Close()
            End Using
            fStream.Close()
        End Using
    End Sub

    'save map number
    Public Sub SaveOnClose()
        Using fStream As FileStream = New FileStream("saves/map.sav", FileMode.Create)
            Using zipStream As GZipStream = New GZipStream(fStream, CompressionMode.Compress)
                Using writer As New BinaryWriter(zipStream)

                    writer.Write(WorldScreen.CurrentFloor)

                    writer.Close()
                End Using
                zipStream.Close()
            End Using
            fStream.Close()
        End Using
    End Sub

    Public Function ReloadSave()
        Using fStream As FileStream = New FileStream("saves/map.sav", FileMode.Open)
            Using zipStream As GZipStream = New GZipStream(fStream, CompressionMode.Decompress)
                Using reader As New BinaryReader(zipStream)

                    SavedMap = reader.ReadInt32

                    reader.Close()
                End Using
                zipStream.Close()
            End Using
            fStream.Close()
        End Using
        Return SavedMap
    End Function

    Public Function ReLoadMap(MapName As String) As MapBase
        Using fStream As FileStream = New FileStream("saves/" & MapName & "Map.tmp", FileMode.Open)
            Using zipStream As GZipStream = New GZipStream(fStream, CompressionMode.Decompress)
                Using reader As New BinaryReader(zipStream)
                    'Header info
                    MapName = reader.ReadString
                    NewMap = New MapBase(reader.ReadInt32, reader.ReadInt32, New Vector2(reader.ReadSingle, reader.ReadSingle))
                    NewMap.MapName = MapName


                    For X = 0 To NewMap.MapWidth
                        For Y = 0 To NewMap.MapHeight
                            NewMap.TileList(X, Y) = New Tile
                            With NewMap.TileList(X, Y)
                                .SrcRectangle = New Rectangle(reader.ReadInt32, reader.ReadInt32, 32, 32) 'adjust to tile sizes currently 32x32
                                .IsBlocked = reader.ReadBoolean
                                .IsStepTrigger = reader.ReadBoolean

                                '.TriggerScript = reader.ReadString
                            End With
                        Next
                    Next
                    reader.Close()
                End Using
                zipStream.Close()
            End Using
            fStream.Close()
        End Using

        Return NewMap
    End Function

    Public Sub ReSaveMap(Map As MapBase, MapName As String)
        Using fStream As FileStream = New FileStream("saves/" & MapName & "Map.tmp", FileMode.Create)
            Using zipStream As GZipStream = New GZipStream(fStream, CompressionMode.Compress)
                Using writer As New BinaryWriter(zipStream)
                    writer.Write(MapName)
                    writer.Write(Map.MapWidth)
                    writer.Write(Map.MapHeight)
                    writer.Write(WorldScreen.ReturnPoint.X)
                    writer.Write(WorldScreen.ReturnPoint.Y)


                    For X = 0 To Map.MapWidth
                        For Y = 0 To Map.MapHeight
                            writer.Write(Map.TileList(X, Y).SrcRectangle.X)
                            writer.Write(Map.TileList(X, Y).SrcRectangle.Y)
                            writer.Write(Map.TileList(X, Y).IsBlocked)
                            writer.Write(Map.TileList(X, Y).IsStepTrigger)

                            'writer.Write(Map.TileList(X, Y).TriggerScript)

                        Next
                    Next

                    writer.Close()
                End Using
                zipStream.Close()
            End Using
            fStream.Close()
        End Using
    End Sub
End Class


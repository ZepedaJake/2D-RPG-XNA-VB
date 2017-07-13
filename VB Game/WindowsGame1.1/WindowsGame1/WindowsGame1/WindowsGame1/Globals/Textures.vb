Public Class Textures

    Public Shared Sanic As Texture2D
    Public Shared Drop As Texture2D
    Public Shared World1 As Texture2D
    Public Shared SpriteSheet1 As Texture2D
    Public Shared TitleArt As Texture2D
    Public Shared Menu As Texture2D

    'Toons
    Public Shared Stickman As Texture2D
    Public Shared Terry As Texture2D


    Public Shared Sub Load()
        TitleArt = Globals.Content.Load(Of Texture2D)("GFX/TitleArt")
        Sanic = Globals.Content.Load(Of Texture2D)("GFX/Sanic")
        Drop = Globals.Content.Load(Of Texture2D)("GFX/Drop")
        World1 = Globals.Content.Load(Of Texture2D)("GFX/WorldGFX/Sprites")
        Stickman = Globals.Content.Load(Of Texture2D)("GFX/Toons/stickmanSprite")
        SpriteSheet1 = Globals.Content.Load(Of Texture2D)("GFX/WorldGFX/NewSprites")
        Terry = Globals.Content.Load(Of Texture2D)("GFX/Toons/Terry")
        Menu = Globals.Content.Load(Of Texture2D)("GFX/Menu")
    End Sub
End Class

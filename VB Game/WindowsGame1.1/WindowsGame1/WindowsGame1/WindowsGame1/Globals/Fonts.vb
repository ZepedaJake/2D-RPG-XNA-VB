Public Class Fonts
    Public Shared Arial_20 As SpriteFont
    Public Shared Arial_8 As SpriteFont

    Public Shared Sub Load()
        Arial_20 = Globals.Content.Load(Of SpriteFont)("Fonts/Arial_20")
        Arial_8 = Globals.Content.Load(Of SpriteFont)("Fonts/Arial_8")

    End Sub
End Class

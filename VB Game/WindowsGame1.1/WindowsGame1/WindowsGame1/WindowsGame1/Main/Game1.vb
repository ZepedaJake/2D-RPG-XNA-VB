''' <summary>
''' This is the main type for your game
''' </summary>
Public Class Game1
    Inherits Microsoft.Xna.Framework.Game
    Private ScreenManager As ScreenManager
    Public Shared SongPlay As SoundEffectInstance
    Public Shared SongPlaying As Boolean = False
    Public Shared SongSetting As Integer
    Public Shared MessageCounter = 1

    Private DrawX As Integer
    Private DrawY As Integer
    Private DrawSize As Integer

    Public Sub New()
        Globals.Graphics = New GraphicsDeviceManager(Me)
        Content.RootDirectory = "Content"
    End Sub

    ''' <summary>
    ''' Allows the game to perform any initialization it needs to before starting to run.
    ''' This is where it can query for any required services and load any non-graphic
    ''' related content.  Calling MyBase.Initialize will enumerate through any components
    ''' and initialize them as well.
    ''' </summary>
    Protected Overrides Sub Initialize()
        ' TODO: Add your initialization logic here
        Me.IsMouseVisible = True
        Window.AllowUserResizing = True

        Globals.GameSize = New Vector2(720, 720)
        Globals.Graphics.PreferredBackBufferWidth = Globals.GameSize.X
        Globals.Graphics.PreferredBackBufferHeight = Globals.GameSize.Y



        Globals.Graphics.ApplyChanges()

        Globals.BackBuffer = New RenderTarget2D(Globals.Graphics.GraphicsDevice, Globals.GameSize.X, Globals.GameSize.Y, False, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents)

        MyBase.Initialize()
    End Sub

    ''' <summary>
    ''' LoadContent will be called once per game and is the place to load
    ''' all of your content.
    ''' </summary>
    Protected Overrides Sub LoadContent()
        ' Create a new SpriteBatch, which can be used to draw textures.
        Globals.SpriteBatch = New SpriteBatch(GraphicsDevice)
        Globals.Content = Me.Content

        'load fonts/textures/sound
        Fonts.Load()
        Textures.Load()
        Music.Load()

        'Add Default Screens ex. title screen
        ScreenManager = New ScreenManager()

        ScreenManager.AddScreen(New TitleScreen)
        'ScreenManager.AddScreen(New MainMenu)




    End Sub
    ''' <summary>
    ''' Allows the game to run logic such as updating the world,
    ''' checking for collisions, gathering input, and playing audio.
    ''' </summary>
    ''' <param name="gameTime">Provides a snapshot of timing values.</param>


    Protected Overrides Sub Update(ByVal gameTime As GameTime)
        ' Allows the game to exit
        If MainMenu.ExitGame = True Then
            Me.Exit()
        End If
        ' TODO: Add your update logic here
        MyBase.Update(gameTime)
        Globals.WindowFocused = Me.IsActive
        Globals.GameTime = gameTime

        'update screens
        ScreenManager.Update()

        'Input Update
        Input.Update()

        If WorldScreen.MapSong >= 1 And WorldScreen.MapSong <= 5 And MainMenu.InDialouge = False Then
            If SongSetting <> 1 Then
                Try
                    SongPlay.Dispose()
                    SongPlaying = False
                Catch
                End Try
            End If

            If SongPlaying = False Then
                SongPlay = Music.LowFloors.CreateInstance()
                SongPlay.IsLooped = True
                SongPlay.Play()
                SongPlaying = True
                SongSetting = 1
            End If
        ElseIf WorldScreen.MapSong >= 6 And WorldScreen.MapSong <= 13 And MainMenu.InDialouge = False And WorldScreen.BossInitialize < 0 Then

            If SongSetting <> 2 Then
                Try
                    SongPlay.Dispose()
                    SongPlaying = False
                Catch
                End Try
            End If

            If SongPlaying = False Then
                SongPlay = Music.HighFLoors.CreateInstance()
                SongPlay.IsLooped = True
                SongPlay.Play()
                SongPlaying = True
                SongSetting = 2
            End If
        ElseIf MainMenu.InDialouge = True And WorldScreen.MapSong <> 0 Then
            If SongSetting <> 3 Then
                Try
                    SongPlay.Dispose()
                    SongPlaying = False
                Catch
                End Try
            End If
            If SongPlaying = False Then
                SongPlay = Music.Dialouge.CreateInstance()
                SongPlay.IsLooped = True
                SongPlay.Play()
                SongPlaying = True
                SongSetting = 3
            End If
        ElseIf MainMenu.InDialouge = False And WorldScreen.BossInitialize >= 0 Then
            If SongSetting <> 4 Then
                Try
                    SongPlay.Dispose()
                    SongPlaying = False
                Catch
                End Try
            End If
            If SongPlaying = False Then
                SongPlay = Music.Boss.CreateInstance()
                SongPlay.IsLooped = True
                SongPlay.Play()
                SongPlaying = True
                SongSetting = 4
            End If
        ElseIf WorldScreen.MapSong = 0 Then
                SongPlaying = False


        End If


    End Sub

    ''' <summary>
    ''' This is called when the game should draw itself.
    ''' </summary>
    ''' <param name="gameTime">Provides a snapshot of timing values.</param>
    Protected Overrides Sub Draw(ByVal gameTime As GameTime)
        Globals.Graphics.GraphicsDevice.SetRenderTarget(Globals.BackBuffer)
        GraphicsDevice.Clear(Color.Black)
        MyBase.Draw(gameTime)

        'Draw Contents Of Screen Manager
        ScreenManager.Draw()

        Globals.Graphics.GraphicsDevice.SetRenderTarget(Nothing)

        'Draw BackBuffer
        Globals.SpriteBatch.Begin()
        'Globals.SpriteBatch.Draw(Globals.BackBuffer, New Rectangle(0, 0, Globals.Graphics.GraphicsDevice.Viewport.Width, Globals.Graphics.GraphicsDevice.Viewport.Height), Color.White)

        If Globals.Graphics.GraphicsDevice.Viewport.Width > Globals.Graphics.GraphicsDevice.Viewport.Height Then
            DrawSize = Globals.Graphics.GraphicsDevice.Viewport.Height
        ElseIf Globals.Graphics.GraphicsDevice.Viewport.Width < Globals.Graphics.GraphicsDevice.Viewport.Height
            DrawSize = Globals.Graphics.GraphicsDevice.Viewport.Width
        Else
            DrawSize = Globals.Graphics.GraphicsDevice.Viewport.Height
        End If

        DrawX = (Globals.Graphics.GraphicsDevice.Viewport.Width - DrawSize) / 2
        DrawY = (Globals.Graphics.GraphicsDevice.Viewport.Height - DrawSize) / 2

        Globals.SpriteBatch.Draw(Textures.SpriteSheet1, New Rectangle(0, 0, Globals.Graphics.GraphicsDevice.Viewport.Width, Globals.Graphics.GraphicsDevice.Viewport.Height), New Rectangle(0, 0, 1, 1), Color.White)

        Globals.SpriteBatch.Draw(Globals.BackBuffer, New Rectangle(DrawX, DrawY, DrawSize, DrawSize), Color.White)

        Globals.SpriteBatch.End()
    End Sub


End Class

Public Class Music
    Public Shared Title As SoundEffect
    Public Shared HighFloors As SoundEffect
    Public Shared LowFloors As SoundEffect
    Public Shared Death As SoundEffect
    Public Shared Dialouge As SoundEffect
    Public Shared Boss As SoundEffect

    Public Shared Hit As SoundEffect
    Public Shared Heal As SoundEffect
    Public Shared Pickup As SoundEffect

    Public Shared Sub Load()
        Title = Globals.Content.Load(Of SoundEffect)("Music/Title")
        HighFloors = Globals.Content.Load(Of SoundEffect)("Music/HighFloors")
        LowFloors = Globals.Content.Load(Of SoundEffect)("Music/LowFloors")
        Death = Globals.Content.Load(Of SoundEffect)("Music/Death")
        Dialouge = Globals.Content.Load(Of SoundEffect)("Music/Dialouge")
        Boss = Globals.Content.Load(Of SoundEffect)("Music/Boss")

        'SFX
        Hit = Globals.Content.Load(Of SoundEffect)("Sound/Hit")
        Heal = Globals.Content.Load(Of SoundEffect)("Sound/Heal")
        Pickup = Globals.Content.Load(Of SoundEffect)("Sound/Pickup")

    End Sub
End Class

  Public Sub Main()
    'Your Code here
        DropVials()
  End Sub
    Private Sub DropVials()
        For Each c As Container In Client.Inventory.GetContainers
            For Each i As item In c.GetItems
                If i.id = 285 Then
                    i.Move(ItemLocation.FromLocation(client.PlayerLocation))
                    Exit Sub
                End If
            Next
        Next
    End Sub
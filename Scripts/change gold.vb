  Public Sub Main()
    'Your Code here
    ChangeGold()
  End Sub

  private Sub ChangeGold
      For Each c As Container In Client.Inventory.GetContainers
          For Each i As Item In c.GetItems
              If i.Id = 3031 AndAlso i.Count = 100 Then
                  ChangeGold(i)
                  Exit Sub
              End if
              If i.Id = 3035 AndAlso i.Count = 100 Then
                  ChangeGold(i)
                  Exit Sub
              End if 
          Next
      Next
      
  End Sub
  Private Sub ChangeGold(byval item As Item)
      item.Use()
      Wait(1000)
  End Sub

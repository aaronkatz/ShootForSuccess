Module SpriteStuff
    Structure Letter
        Dim aPic() As Image
        Dim aLocation As Point
        Dim aActive As Boolean
        Dim aTotalFrames As Integer
        Dim aCurrentFrame As Integer
        Dim aLetter As Char
        Dim aMoveCounter As Integer
        Dim aScore As Integer
    End Structure
    Structure Player
        Dim pPic As Image
        Dim pLocation As Point
        Dim pSpeed As Single
        Dim pTimeSinceLastShot As Integer
    End Structure
    Structure TextCursor
        Dim tPic As Image
        Dim tLocation As Point
        Dim tSpeed As Single
        Dim tActive As Boolean
    End Structure
    Public mTextCursors As List(Of TextCursor) = New List(Of TextCursor)
    Public mOldCursors As List(Of TextCursor) = New List(Of TextCursor)
    Public mSuccess As List(Of Char) = New List(Of Char)

    Public mLetters(9, 4) As Letter
    Public mPlayer As Player
    Public mSs(1) As Image
    Public mUs(1) As Image
    Public mCs(1) As Image
    Public mEs(1) As Image
    Public mTextPic As Image
    Public mGameTime As Integer
    Public mScore As Integer

    Public Sub makeAllLetters()
        'SUCCESS
        For i As Integer = 0 To mLetters.GetLength(0) - 1
            For j As Integer = 0 To mLetters.GetLength(1) - 1
                Randomize()
                Dim tempNum As Integer
                tempNum = Int(Rnd() * 7)
                If tempNum = 0 Or tempNum = 5 Or tempNum = 6 Then
                    makeLetterS(mLetters(i, j))
                ElseIf tempNum = 1 Then
                    makeLetterU(mLetters(i, j))
                ElseIf tempNum = 2 Or tempNum = 3 Then
                    makeLetterC(mLetters(i, j))
                ElseIf tempNum = 4 Then
                    makeLetterE(mLetters(i, j))
                End If
                mLetters(i, j).aMoveCounter = 5



                mLetters(i, j).aLocation.X = 50 + 70 * i
                mLetters(i, j).aLocation.Y = 50 + 60 * j
            Next j
        Next i




    End Sub
    Public Function touching(ByVal guy1 As TextCursor, ByVal guy2 As Letter)
        If guy1.tActive = True And guy2.aActive = True Then
            If guy1.tLocation.X < guy2.aLocation.X + guy2.aPic(guy2.aCurrentFrame).Width And guy1.tLocation.X + guy1.tPic.Width > guy2.aLocation.X Then
                If guy1.tLocation.Y < guy2.aLocation.Y + guy2.aPic(guy2.aCurrentFrame).Height And guy1.tLocation.Y + guy1.tPic.Height > guy2.aLocation.Y Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function
    Public Sub makeLetterS(ByRef pl As Letter)
        pl.aLetter = "S"
        pl.aPic = mSs

        pl.aScore = 5
        pl.aActive = True
        pl.aTotalFrames = 2
        Randomize()
        Dim tempNum As Integer
        tempNum = Int(Rnd() * 2)
        pl.aCurrentFrame = tempNum

    End Sub
    Public Sub makeLetterU(ByRef pl As Letter)
        pl.aLetter = "U"
        pl.aPic = mUs

        pl.aScore = 15
        pl.aActive = True
        pl.aTotalFrames = 2
        Randomize()
        Dim tempNum As Integer
        tempNum = Int(Rnd() * 2)
        pl.aCurrentFrame = tempNum

    End Sub
    Public Sub checkCollisions()
        For i As Integer = 0 To mLetters.GetLength(0) - 1
            For j As Integer = 0 To mLetters.GetLength(1) - 1
                For t As Integer = 0 To mTextCursors.Count - 1
                    If touching(mTextCursors(t), mLetters(i, j)) Then
                        Dim tempCursor As TextCursor = mTextCursors(t)
                        tempCursor.tActive = False
                        mTextCursors(t) = tempCursor
                        mSuccess.Add(mLetters(i, j).aLetter)

                        mLetters(i, j).aActive = False
                        mScore += mLetters(i, j).aScore
                    End If
                Next t
            Next j
        Next i

    End Sub
    '    s1
    'su2
    'suc3
    'succ4
    'succe5
    'succes6
    'success7
    Public Sub checkSuccess()
        If mSuccess.Count = 1 Or mSuccess.Count = 6 Then
            If mSuccess(mSuccess.Count - 1) <> "S" Then
                mSuccess.Clear()
            End If
        ElseIf mSuccess.Count = 2 Then
            If mSuccess(mSuccess.Count - 1) <> "U" Then
                mSuccess.Clear()
            End If
        ElseIf mSuccess.Count = 3 Or mSuccess.Count = 4 Then
            If mSuccess(mSuccess.Count - 1) <> "C" Then
                mSuccess.Clear()
            End If
        ElseIf mSuccess.Count = 5 Then
            If mSuccess(mSuccess.Count - 1) <> "E" Then
                mSuccess.Clear()
            End If
        ElseIf mSuccess.Count = 6 Then
            If mSuccess(mSuccess.Count - 1) <> "S" Then
                mSuccess.Clear()
            End If
        ElseIf mSuccess.Count = 7 Then
            If mSuccess(mSuccess.Count - 1) <> "S" Then
                mSuccess.Clear()
            Else
                mScore += 200
                mSuccess.Add("")
            End If
        ElseIf mSuccess.Count = 9 Then
            If mSuccess(mSuccess.Count - 1) <> "S" Then

                mSuccess.Clear()

            Else
                mSuccess.Clear()
                mSuccess.Add("S")
            End If




        End If
    End Sub
    Public Sub drawSuccess()
        For i As Integer = 0 To mSuccess.Count - 1
            mOffG.DrawString(mSuccess(i), New Font("Arial", 20, FontStyle.Bold), New SolidBrush(Color.Gold), 0 + (20 * i), 20)
        Next i

    End Sub
    Public Sub makeLetterC(ByRef pl As Letter)
        pl.aLetter = "C"
        pl.aPic = mCs
        pl.aScore = 10

        pl.aActive = True
        pl.aTotalFrames = 2
        Randomize()
        Dim tempNum As Integer
        tempNum = Int(Rnd() * 2)
        pl.aCurrentFrame = tempNum

    End Sub
    Public Sub makeLetterE(ByRef pl As Letter)
        pl.aLetter = "E"
        pl.aPic = mEs
        pl.aScore = 20

        pl.aActive = True
        pl.aTotalFrames = 2
        Randomize()
        Dim tempNum As Integer
        tempNum = Int(Rnd() * 2)
        pl.aCurrentFrame = tempNum

    End Sub
   
    Public Sub drawPlayer()
        mOffG.DrawImage(mPlayer.pPic, mPlayer.pLocation.X, mPlayer.pLocation.Y)
    End Sub
    Public Sub updatePlayer()
        mPlayer.pTimeSinceLastShot += 1
        mPlayer.pLocation.X += mPlayer.pSpeed
        If mPlayer.pLocation.X <= 0 Then
            mPlayer.pLocation.X = 0
        ElseIf mPlayer.pLocation.X >= 800 - mPlayer.pPic.Width Then
            mPlayer.pLocation.X = 800 - mPlayer.pPic.Width
        End If
    End Sub
    Public Sub drawLetter(ByVal guy As Letter)
        mOffG.DrawImage(guy.aPic(guy.aCurrentFrame), guy.aLocation.X, guy.aLocation.Y)
    End Sub

    Public Sub animateLetter(ByRef guy As Letter)
        guy.aCurrentFrame += 1
        If guy.aCurrentFrame >= guy.aTotalFrames Then
            guy.aCurrentFrame = 0
        End If
    End Sub
    Public Sub updateLetter(ByRef guy As Letter)



        If guy.aMoveCounter < 10 Then
            guy.aLocation.X += 7
        ElseIf guy.aMoveCounter >= 10 And guy.aMoveCounter < 15 Then
            guy.aLocation.Y += 5
        ElseIf guy.aMoveCounter >= 15 And guy.aMoveCounter < 25 Then
            guy.aLocation.X -= 7
        ElseIf guy.aMoveCounter >= 25 And guy.aMoveCounter < 30 Then
            guy.aLocation.Y += 5
        End If
        If guy.aMoveCounter <> 30 Then
            guy.aMoveCounter += 1
        Else
            guy.aMoveCounter = 0
        End If
    End Sub
    Public Sub makeTextCursor()
        Dim tempText As TextCursor
        tempText.tPic = mTextPic
        tempText.tLocation.X = mPlayer.pLocation.X
        tempText.tLocation.Y = mPlayer.pLocation.Y - tempText.tPic.Height
        tempText.tSpeed = 10
        tempText.tActive = True
        mTextCursors.Add(tempText)

    End Sub
    Public Sub updateTextCursor(ByRef guy As TextCursor)
        If guy.tActive = True Then
            guy.tLocation.Y -= guy.tSpeed
            If guy.tLocation.Y < 0 - guy.tPic.Height Then
                guy.tActive = False
            End If
        End If

    End Sub
    Public Sub drawTextCursor(ByVal guy As TextCursor)
        If guy.tActive = True Then
            mOffG.DrawImage(guy.tPic, guy.tLocation.X, guy.tLocation.Y)
        End If

    End Sub
    Public Sub shootTextCursor()
        If mPlayer.pTimeSinceLastShot >= 10 Then
            mPlayer.pTimeSinceLastShot = 0
            makeTextCursor()

        End If
    End Sub
    Public Sub removeOld()
        For Each textC As TextCursor In mTextCursors
            If textC.tActive = False Then
                mOldCursors.Add(textC)
            End If
        Next textC
        For Each oldTextC As TextCursor In mOldCursors
            mTextCursors.Remove(oldTextC)
        Next oldTextC
        mOldCursors.Clear()
    End Sub
End Module

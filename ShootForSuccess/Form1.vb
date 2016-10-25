Public Class MainForm
    Dim cInGame As Boolean
    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Left Then
            mPlayer.pSpeed = -15
        End If
        If e.KeyCode = Keys.Right Then
            mPlayer.pSpeed = 15
        End If
        If e.KeyCode = Keys.Space Then
            shootTextCursor()

        End If
     
     
    End Sub

    Private Sub MainForm_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Left Then
            mPlayer.pSpeed = 0
        End If
        If e.KeyCode = Keys.Right Then
            mPlayer.pSpeed = 0
        End If
        If e.KeyCode = Keys.Enter Then
            cInGame = True
            loadGame()

        End If
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        loadBackground()
        cInGame = False
        Me.Height = mBackground.bHeight + 38
        Me.Width = mBackground.bWidth + 16
        Me.MaximumSize = New Size(mBackground.bWidth + 16, mBackground.bHeight + 38)
        Me.MinimumSize = New Size(mBackground.bWidth + 16, mBackground.bHeight + 38)

        Me.WindowState = FormWindowState.Maximized

        onScreen.Top = mBackground.bLocation.X
        onScreen.Left = mBackground.bLocation.Y
        onScreen.Height = mBackground.bHeight
        onScreen.Width = mBackground.bWidth





    End Sub
    Public Sub loadGame()
        mScore = 0
        mSuccess.Clear()

        mGameTime = 0
        mTextCursors.Clear()
        loadPlayer()
        mPlayer.pTimeSinceLastShot = 50
        Dim index As Integer
        For index = 0 To 1
            mSs(index) = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\Pics\s" & index & ".png")
            mUs(index) = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\Pics\u" & index & ".png")
            mCs(index) = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\Pics\c" & index & ".png")
            mEs(index) = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\Pics\e" & index & ".png")
        Next index
        mTextPic = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\Pics\TextCursor.png")
        makeAllLetters()

    End Sub
    Private Sub loadPlayer()
        mPlayer.pPic = Image.FromFile(IO.Path.GetDirectoryName(Application.ExecutablePath) & "\Pics\PointerCursor.png")
        mPlayer.pSpeed = 0
        mPlayer.pTimeSinceLastShot = 50
        mPlayer.pLocation.X = 375
        mPlayer.pLocation.Y = Me.Height - 38 - mPlayer.pPic.Height
    End Sub
    Private Sub drawScreen()
        mG = onScreen.CreateGraphics
        mOffG = Graphics.FromImage(mOffScreenImage)
        drawBackground()
        If cInGame = True Then


            drawPlayer()
            For Each t As TextCursor In mTextCursors
                drawTextCursor(t)
            Next t

            For i As Integer = 0 To mLetters.GetLength(0) - 1
                For j As Integer = 0 To mLetters.GetLength(1) - 1
                    If mLetters(i, j).aActive = True Then
                        drawLetter(mLetters(i, j))
                    End If
                Next j
            Next i
            mOffG.DrawString("Score: " + mScore.ToString, New Font("Arial", 12, FontStyle.Bold), New SolidBrush(Color.Red), 0, 0)
            If winState() Then
                mOffG.DrawString("You Win", New Font("Arial", 50, FontStyle.Bold), New SolidBrush(Color.Red), 300, 275)
            End If
            If loseState() Then
                mOffG.DrawString("You Lose", New Font("Arial", 50, FontStyle.Bold), New SolidBrush(Color.Red), 300, 275)
            End If
            drawSuccess()

            mOffG.DrawString("Press ENTER to restart", New Font("Arial", 12, FontStyle.Bold), New SolidBrush(Color.Red), 600, 0)
        Else
            mOffG.DrawString("Press ENTER to start", New Font("Arial", 12, FontStyle.Bold), New SolidBrush(Color.Red), 600, 0)
            mOffG.DrawString("Shoot For Success", New Font("Arial", 50, FontStyle.Bold), New SolidBrush(Color.Gold), 100, 225)
        End If

        mG.DrawImage(mOffScreenImage, 0, 0)
        mG.Dispose()
        mOffG.Dispose()
    End Sub
    Private Function winState()
        For i As Integer = 0 To mLetters.GetLength(0) - 1
            For j As Integer = 0 To mLetters.GetLength(1) - 1
                If mLetters(i, j).aActive = True Then
                    Return False
                End If
            Next j
        Next i
        Return True
    End Function
    Private Function loseState()
        For i As Integer = 0 To mLetters.GetLength(0) - 1
            For j As Integer = 0 To mLetters.GetLength(1) - 1
                If mLetters(i, j).aLocation.Y >= mPlayer.pLocation.Y - mLetters(i, j).aPic(0).Height Then
                    Return True
                End If
            Next j
        Next i
        Return False
    End Function
    Private Sub GameTimer_Tick(sender As Object, e As EventArgs) Handles GameTimer.Tick
        mGameTime += 1
        drawScreen()
        If cInGame Then


            If winState() = False And loseState() = False Then
               

                updateEverything()
                checkCollisions()
                checkSuccess()
            End If

        End If
    End Sub
    Private Sub updateEverything()
        updatePlayer()
        For t As Integer = 0 To mTextCursors.Count - 1
            updateTextCursor(mTextCursors(t))

        Next t
        If mGameTime Mod 6 = 0 Then
            For i As Integer = 0 To mLetters.GetLength(0) - 1
                For j As Integer = 0 To mLetters.GetLength(1) - 1
                    If mLetters(i, j).aActive = True Then
                        animateLetter(mLetters(i, j))
                    End If

                Next j
            Next i
        End If
        If mGameTime Mod 12 = 0 Then
            For i As Integer = 0 To mLetters.GetLength(0) - 1
                For j As Integer = 0 To mLetters.GetLength(1) - 1
                    If mLetters(i, j).aActive = True Then
                        updateLetter(mLetters(i, j))
                    End If

                Next j
            Next i
        End If

        removeOld()

    End Sub
End Class

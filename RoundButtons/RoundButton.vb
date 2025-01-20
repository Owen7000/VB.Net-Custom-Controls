' RoundButton.vb - Contains all the code required to make a round button.
' Author: Owen Plimer. https://github.com/Owen7000
' Year: 2025

Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

''' <summary>
'''     Use this control to draw custom-made, rounded buttons.
'''     You can set the border colour and border width in the properties menu
''' </summary>
Public Class RoundButton
    Inherits Button ' Tell .Net that we want to build on top of the existing button class

    ' Custom Proprties
    Private _borderColor As Color = Color.Black
    Private _borderWidth As Integer = 1


    ''' <summary>
    '''     Handle getting and setting of the border colour
    ''' </summary>
    ''' <returns>_borderColor As Color</returns>
    <Category("Appearance"), Description("The colour of the button's border.")>
    Public Property BorderColor() As Color
        Get
            Return _borderColor
        End Get
        Set(value As Color)
            _borderColor = value
            Me.Invalidate() ' Redraw the control when the property changes
        End Set
    End Property


    ''' <summary>
    '''     Handle getting and setting of the border width
    ''' </summary>
    ''' <returns>_borderWidth As Integer</returns>
    <Category("Appearance"), Description("The width of the button's border.")>
    Public Property BorderWidth() As Integer
        Get
            Return _borderWidth
        End Get
        Set(value As Integer)
            _borderWidth = value
            Me.Invalidate() ' Redraw the control when the property changes
        End Set
    End Property


    ''' <summary>
    '''     Create a new rounded button
    ''' </summary>
    Public Sub New()
    End Sub


    ''' <summary>
    '''     This method is fired by your form every time the paintEvent occurs.
    '''     This causes the control to be completely redrawn
    ''' </summary>
    ''' <param name="pevent"></param>
    Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
        MyBase.OnPaint(pevent)

        Dim graphics As Graphics = pevent.Graphics
        graphics.SmoothingMode = SmoothingMode.AntiAlias

        ' Define the rounded rectangle path
        Dim path As New GraphicsPath()
        path.AddArc(0, 0, 20, 20, 180, 90)
        path.AddArc(Me.Width - 21, 0, 20, 20, 270, 90)
        path.AddArc(Me.Width - 21, Me.Height - 21, 20, 20, 0, 90)
        path.AddArc(0, Me.Height - 21, 20, 20, 90, 90)
        path.CloseFigure()

        ' Set the button's region to the rounded rectangle
        Me.Region = New Region(path)

        ' Fill the button with colour
        graphics.FillPath(New SolidBrush(Me.BackColor), path)

        ' Draw the button's text
        Using format As New StringFormat()
            format.Alignment = StringAlignment.Center
            format.LineAlignment = StringAlignment.Center
            graphics.DrawString(Me.Text, Me.Font, New SolidBrush(Me.ForeColor), Me.ClientRectangle, format)
        End Using

        ' Draw the button's border
        Dim pen As New Pen(_borderColor, _borderWidth)
        graphics.DrawPath(pen, path)
    End Sub


    ''' <summary>
    '''     When the mouse moves over the button, set the cursor to the hand icon
    '''     for visual feedback to the user
    ''' </summary>
    ''' <param name="e">EventArgs</param>
    Protected Overrides Sub OnMouseHover(e As EventArgs)
        MyBase.OnMouseHover(e)
        Me.Cursor = Cursors.Hand
    End Sub
End Class

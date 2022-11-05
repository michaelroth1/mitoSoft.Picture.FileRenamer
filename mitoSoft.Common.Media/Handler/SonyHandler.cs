using mitoSoft.Common.Media.Contracts;
using System;
using System.IO;

namespace mitoSoft.Common.Media.Handler
{
    internal class SonyHandler : IHandler
    {
        public DateTime GetShootingDate(FileInfo file)
        {
            throw new NotImplementedException();
        }
    }
}

/*
 
Friend Class SonyHandler
    Implements IHandler

    Public Function GetShootingDate(file As FileInfo) As DateTime Implements IHandler.GetShootingDate
        If file.Extension.ToLower = ".arw" Then
            Return Me.GetPictureShootingDate(file)
        ElseIf file.Extension.ToLower = ".mp4" Then
            Return Me.GetVideoShootingDate(file)
        Else
            Throw New NoShootingDateFoundException
        End If
    End Function

    ''' <summary>
    ''' Metadaten auslesen -> nach "Aufnahmedatum" suchen
    ''' </summary>
    ''' <returns></returns>
    Private Function GetPictureShootingDate(file As FileInfo) As DateTime
        Dim detailString As String = FileDetailsHandler.GetDetailsOf(file, "Aufnahmedatum")
        Dim [date] As DateTime = detailString.Trim.ConvertToDateTime("‎dd.‎MM.‎yyyy ‏‎HH:mm")
        Return [date]
    End Function

    Private Function GetVideoShootingDate(file As FileInfo) As Date
        Dim xmlFile As String = $"{file.Directory}\{file.Name.Replace(file.Extension, "")}M01.xml"

        If IO.File.Exists(xmlFile) = False Then Throw New FileNotFoundException($"No description file found for movie file {file.Name}")

        Dim xml As New XmlDocument()
        xml.Load(xmlFile)

        Dim dateString As String = String.Empty

        For Each n As XmlNode In xml.ChildNodes(1)
            If n.Name = "CreationDate" Then
                dateString = n.Attributes("value").Value
                Exit For
            End If
        Next

        If String.IsNullOrEmpty(dateString) Then Throw New Exception($"No 'Creation Date' attribute found in movie file {file.Name}")

        Dim [date] As DateTime = System.Convert.ToDateTime(dateString)
        Return [date]
    End Function
End Class

 */
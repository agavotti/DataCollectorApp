Imports Application.Interfaces
Imports Domain.Entities
Imports Microsoft.Data.SqlClient
Namespace Data
    Public Class AlbumRepository
        Implements IAlbumRepository

        Private ReadOnly _connectionString As String


        Public Sub New(connectionString As String)
            _connectionString = connectionString
        End Sub
        Private Function GetConnection() As SqlConnection
            Return New SqlConnection(_connectionString)
        End Function

        Public Sub CreateAlbum(album As Album) Implements IAlbumRepository.CreateAlbum
            Throw New NotImplementedException()
        End Sub

        Public Sub UpdateAlbum(album As Album) Implements IAlbumRepository.UpdateAlbum
            Throw New NotImplementedException()
        End Sub

        Public Sub DeleteAlbum(id As Integer) Implements IAlbumRepository.DeleteAlbum
            Throw New NotImplementedException()
        End Sub

        Public Function GetAlbums(filterTitle As String) As List(Of Album) Implements IAlbumRepository.GetAlbums
            Dim albums As New List(Of Album)()
            Using conn As New SqlConnection(_connectionString)
                conn.Open()
                Dim query = "SELECT * FROM Albums WHERE (@Title IS NULL OR Title LIKE '%' + @Title + '%')"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Title", If(String.IsNullOrEmpty(filterTitle), DBNull.Value, filterTitle))
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            albums.Add(New Album() With {
                                .Id = reader.GetInt32(0),
                                .UserId = reader.GetInt32(1),
                                .Title = reader.GetString(2)
                            })
                        End While
                    End Using
                End Using
            End Using
            Return albums
        End Function

        Public Function GetAlbumById(id As Integer) As Album Implements IAlbumRepository.GetAlbumById
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
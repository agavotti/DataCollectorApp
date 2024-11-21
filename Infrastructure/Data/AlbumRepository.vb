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
            Dim existsQuery As String = "SELECT COUNT(*) FROM Albums WHERE Title = @Title AND UserId = @UserId"
            Dim insertQuery As String = "INSERT INTO Albums (Title, UserId) VALUES (@Title, @UserId); SELECT SCOPE_IDENTITY();"

            Using connection As New SqlConnection(_connectionString)

                connection.Open()

                Using checkCommand As New SqlCommand(existsQuery, connection)
                    checkCommand.Parameters.AddWithValue("@Title", album.Title)
                    checkCommand.Parameters.AddWithValue("@UserId", album.UserId)

                    Dim count As Integer = Convert.ToInt32(checkCommand.ExecuteScalar())
                    If count > 0 Then
                        Throw New Exception("El álbum ya existe en la base de datos.")
                    End If
                End Using

                Using insertCommand As New SqlCommand(insertQuery, connection)
                    insertCommand.Parameters.AddWithValue("@Title", album.Title)
                    insertCommand.Parameters.AddWithValue("@UserId", album.UserId)

                    Dim id As Object = insertCommand.ExecuteScalar()
                    If id IsNot Nothing Then
                        album.Id = Convert.ToInt32(id)
                    End If
                End Using
            End Using
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
Imports Application.Interfaces
Imports Domain.Entities
Imports Microsoft.Data.SqlClient
Namespace Data
    Public Class PhotoRepository
        Implements IPhotoRepository

        Private ReadOnly _connectionString As String


        Public Sub New(connectionString As String)
            _connectionString = connectionString
        End Sub
        Private Function GetConnection() As SqlConnection
            Return New SqlConnection(_connectionString)
        End Function

        Public Sub CreatePhoto(photo As Photo) Implements IPhotoRepository.CreatePhoto
            Dim existsAlbumQuery As String = "SELECT COUNT(*) FROM Albums WHERE Id = @AlbumId"
            Dim existsPhotoQuery As String = "SELECT COUNT(*) FROM Photos WHERE Title = @Title AND AlbumId = @AlbumId"
            Dim insertQuery As String = "INSERT INTO Photos (Title, AlbumId, ThumbnailUrl, Url) VALUES (@Title, @AlbumId, @ThumbnailUrl, @Url); SELECT SCOPE_IDENTITY();"

            Using connection As New SqlConnection(_connectionString)

                connection.Open()

                Using checkCommand As New SqlCommand(existsAlbumQuery, connection)
                    checkCommand.Parameters.AddWithValue("@AlbumId", photo.AlbumId)

                    Dim count As Integer = Convert.ToInt32(checkCommand.ExecuteScalar())
                    If count = 0 Then
                        Throw New Exception("El álbum no existe para la foto que esta queriendo crear.")
                    End If
                End Using

                Using checkCommand As New SqlCommand(existsPhotoQuery, connection)
                    checkCommand.Parameters.AddWithValue("@Title", photo.Title)
                    checkCommand.Parameters.AddWithValue("@AlbumId", photo.AlbumId)

                    Dim count As Integer = Convert.ToInt32(checkCommand.ExecuteScalar())
                    If count > 0 Then
                        Throw New Exception("La foto ya existe en la base de datos.")
                    End If
                End Using

                Using insertCommand As New SqlCommand(insertQuery, connection)
                    insertCommand.Parameters.AddWithValue("@Title", photo.Title)
                    insertCommand.Parameters.AddWithValue("@AlbumId", photo.AlbumId)
                    insertCommand.Parameters.AddWithValue("@ThumbnailUrl", photo.ThumbnailUrl)
                    insertCommand.Parameters.AddWithValue("@Url", photo.Url)

                    Dim id As Object = insertCommand.ExecuteScalar()
                    If id IsNot Nothing Then
                        photo.Id = Convert.ToInt32(id)
                    End If
                End Using
            End Using
        End Sub

        Public Sub UpdatePhoto(album As Photo) Implements IPhotoRepository.UpdatePhoto
            Throw New NotImplementedException()
        End Sub

        Public Sub DeletePhoto(id As Integer) Implements IPhotoRepository.DeletePhoto
            Throw New NotImplementedException()
        End Sub

        Public Function GetPhotos(filterTitle As String, albumId As Integer?) As List(Of Photo) Implements IPhotoRepository.GetPhotos
            Dim photos As New List(Of Photo)()
            Using conn As New SqlConnection(_connectionString)

                conn.Open()
                Dim query = "SELECT * FROM Photos WHERE (@Title IS NULL OR Title LIKE '%' + @Title + '%')
                                                  AND (@AlbumId IS NULL OR AlbumId = @AlbumId)"
                Using cmd As New SqlCommand(query, conn)

                    cmd.Parameters.AddWithValue("@Title", If(String.IsNullOrEmpty(filterTitle), DBNull.Value, filterTitle))
                    cmd.Parameters.AddWithValue("@AlbumId", If(Not (albumId.HasValue), DBNull.Value, albumId))
                    Using reader = cmd.ExecuteReader()

                        While reader.Read()
                            photos.Add(New Photo() With {
                                .Id = reader.GetInt32(0),
                                .AlbumId = reader.GetInt32(1),
                                .Title = reader.GetString(2),
                                .ThumbnailUrl = reader.GetString(3),
                                .Url = reader.GetString(4)
                            })
                        End While

                    End Using
                End Using
            End Using
            Return photos
        End Function

        Public Function GetPhotoById(id As Integer) As Photo Implements IPhotoRepository.GetPhotoById
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
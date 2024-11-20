Imports System.Net.Http
Imports Application.Interfaces
Imports Domain.Entities
Imports Newtonsoft.Json
Namespace Services
    Public Class ExternalApiService
        Implements IExternalApiService

        Public Function GetAlbums() As List(Of Album) Implements IExternalApiService.GetAlbums
            Using client As New HttpClient()
                Dim response = client.GetStringAsync("https://jsonplaceholder.typicode.com/albums").Result
                Return JsonConvert.DeserializeObject(Of List(Of Album))(response)
            End Using
        End Function

        Public Function GetPhotos() As List(Of Photo) Implements IExternalApiService.GetPhotos
            Using client As New HttpClient()
                Dim response = client.GetStringAsync("https://jsonplaceholder.typicode.com/photos").Result
                Return JsonConvert.DeserializeObject(Of List(Of Photo))(response)
            End Using
        End Function

    End Class
End Namespace

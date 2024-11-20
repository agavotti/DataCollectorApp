Imports Domain.Entities

Namespace Interfaces
    Public Interface IExternalApiService
        Function GetAlbums() As List(Of Album)
        Function GetPhotos() As List(Of Photo)
    End Interface
End Namespace
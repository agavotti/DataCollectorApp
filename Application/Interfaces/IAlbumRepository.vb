Namespace Interfaces
    Public Interface IAlbumRepository
        Function GetAlbums(filterTitle As String) As List(Of Domain.Entities.Album)
        Function GetAlbumById(id As Integer) As Domain.Entities.Album
        Sub CreateAlbum(album As Domain.Entities.Album)
        Sub UpdateAlbum(album As Domain.Entities.Album)
        Sub DeleteAlbum(id As Integer)
    End Interface
End Namespace

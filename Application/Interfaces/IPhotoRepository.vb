
Namespace Interfaces
    Public Interface IPhotoRepository
        Function GetPhotos(filterTitle As String, albumId As Integer?) As List(Of Domain.Entities.Photo)
        Function GetPhotoById(id As Integer) As Domain.Entities.Photo
        Sub CreatePhoto(photo As Domain.Entities.Photo)
        Sub UpdatePhoto(photo As Domain.Entities.Photo)
        Sub DeletePhoto(id As Integer)

    End Interface
End Namespace

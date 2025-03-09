using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Mappings;

public static class GenreMapping
{
   public static Genre ToEntity(this GenreDto dto)
   {
      return new Genre
      {
         Name = dto.Name
      };
   }

   //  public static Genre ToEntity(this UpdateGenreDto dto, int id)
   //  {
   //      return new Genre
   //      {
   //          Id = id,
   //          Name = dto.Name
   //      };
   //  }

   public static GenreDto ToDto(this Genre entity)
   {
      return new GenreDto(entity.Id, entity.Name);
   }
}
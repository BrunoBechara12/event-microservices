
using Application.Dto;
using Domain.Entities;

namespace Application.Mappers;
public class ReturnEventCreatedMapper
{
    public static ReturnEventCreatedDto ToDto(Event eventItem) 
    {
        return new ReturnEventCreatedDto
        {
            Id = eventItem.Id,
            Name = eventItem.Name,
        };
    }
}

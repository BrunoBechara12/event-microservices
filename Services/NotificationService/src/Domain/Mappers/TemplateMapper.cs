using Domain.DTOs.Template.Responses;
using TemplateEntity = Domain.Entities.MessageTemplate;

namespace Domain.Mappers;

public static class TemplateMapper
{
    public static TemplateResponseDto ToResponseDto(this TemplateEntity entity)
    {
        return new TemplateResponseDto(
            entity.Id,
            entity.Name,
            entity.Type.ToString(),
            entity.Template,
            entity.IsActive,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}

namespace Domain.DTOs.Template.Responses;

public record TemplateResponseDto(
    int Id,
    string Name,
    string Type,
    string Template,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

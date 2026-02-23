using Domain.Entities;

namespace Domain.DTOs.Template.Requests;

public record CreateTemplateRequestDto(
    string Name,
    NotificationType Type,
    string Template
);

using Domain.DTOs.Template.Requests;
using Domain.DTOs.Template.Responses;
using Result;

namespace Domain.Ports.Input;

public interface ITemplateUseCase
{
    Task<Result<TemplateResponseDto>> GetById(int id);
    Task<Result<IEnumerable<TemplateResponseDto>>> GetAll();
    Task<Result<TemplateResponseDto>> Create(CreateTemplateRequestDto input);
    Task<Result<TemplateResponseDto>> Update(int id, UpdateTemplateRequestDto input);
    Task<Result<bool>> Delete(int id);
}

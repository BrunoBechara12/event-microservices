using Domain.DTOs.Template.Requests;
using Domain.DTOs.Template.Responses;
using Domain.Entities;
using Domain.Mappers;
using Domain.Ports.Input;
using Domain.Ports.Output;
using Result;

namespace Application.UseCases;

public class TemplateUseCase : ITemplateUseCase
{
    private readonly IMessageTemplateRepository _templateRepository;

    public TemplateUseCase(IMessageTemplateRepository templateRepository)
    {
        _templateRepository = templateRepository;
    }

    public async Task<Result<TemplateResponseDto>> GetById(int id)
    {
        var template = await _templateRepository.GetById(id);
        
        if (template == null)
            return Result<TemplateResponseDto>.Failure($"Template with Id={id} not found");

        return Result<TemplateResponseDto>.Success(template.ToResponseDto(), "Template found");
    }

    public async Task<Result<IEnumerable<TemplateResponseDto>>> GetAll()
    {
        var templates = await _templateRepository.GetAll();

        return Result<IEnumerable<TemplateResponseDto>>.Success(templates.Select(t => t.ToResponseDto()), "Templates retrieved");
    }

    public async Task<Result<TemplateResponseDto>> Create(CreateTemplateRequestDto input)
    {
        var template = MessageTemplate.Create(input.Name, input.Type, input.Template);

        await _templateRepository.Create(template);
                    
        return Result<TemplateResponseDto>.Success(template.ToResponseDto(), "Template created");
    }

    public async Task<Result<TemplateResponseDto>> Update(int id, UpdateTemplateRequestDto input)
    {
        var template = await _templateRepository.GetById(id);
        
        if (template == null)
            return Result<TemplateResponseDto>.Failure($"Template with Id={id} not found");

        template.Update(input.Name, input.Template);

        await _templateRepository.Update(template);

        return Result<TemplateResponseDto>.Success(template.ToResponseDto(), "Template updated");
    }

    public async Task<Result<bool>> Delete(int id)
    {
        var template = await _templateRepository.GetById(id);
        
        if (template == null)
            return Result<bool>.Failure($"Template with Id={id} not found");

        await _templateRepository.Delete(id);
                
        return Result<bool>.Success(true, "Template deleted");
    }
}

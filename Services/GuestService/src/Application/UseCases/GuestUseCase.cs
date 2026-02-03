using Domain.Mappers;
using Result;
using static Domain.Entities.Guest;
using Domain.Ports.Output;
using Domain.DTOs.Guest.Requests;
using Domain.DTOs.Guest.Responses;
using Domain.Ports.Input;

namespace Application.UseCases;
public class GuestUseCase : IGuestUseCase
{
    private readonly IGuestRepository _guestRepository;
    private readonly IEventRepository _eventRepository;

    public GuestUseCase(IGuestRepository guestRepository, IEventRepository eventRepository)
    {
        _guestRepository = guestRepository;
        _eventRepository = eventRepository;
    }

    public async Task<Result<IEnumerable<DetailedGuestResponseDto>>> Get()
    {
        var guests = await _guestRepository.Get();

        if (guests == null || !guests.Any())
        {
            return Result<IEnumerable<DetailedGuestResponseDto>>.Success(null, "No guests found.");
        }

        var guestOutput = guests.Select(g => g.ToDetailedResponseDto());

        return Result<IEnumerable<DetailedGuestResponseDto>>.Success(guestOutput!, "Guests found with success!");
    }

    public async Task<Result<DetailedGuestResponseDto>> GetById(int id)
    {
        var guest = await _guestRepository.GetById(id);

        if (guest == null)
        {
            return Result<DetailedGuestResponseDto>.Failure("Guest not found.");
        }

        var guestOutput = guest.ToDetailedResponseDto();

        return Result<DetailedGuestResponseDto>.Success(guestOutput!, "Guest found with success!");
    }

    public async Task<Result<DefaultGuestResponseDto>> Create(CreateGuestRequestDto input)
    {
        var eventExists = await _eventRepository.GetById(input.EventId);
        
        if (eventExists == null)
        {
            return Result<DefaultGuestResponseDto>.Failure("Event not found");
        }

        var newGuest = CreateGuest(
            input.EventId,
            input.Name, 
            input.Email, 
            input.PhoneNumber
        );

        var createdGuest = await _guestRepository.Create(newGuest);

        var guestOutput = createdGuest.ToDefaultResponseDto();

        return Result<DefaultGuestResponseDto>.Success(guestOutput!, "Guest created with success!");
    }

    public async Task<Result<DefaultGuestResponseDto>> Update(UpdateGuestRequestDto input)
    {
        var guestItem = await _guestRepository.GetById(input.Id);

        if (guestItem == null)
        {
            return Result<DefaultGuestResponseDto>.Failure("Guest not found.");
        }

        guestItem.UpdateGuest(
            input.Name,
            input.Email,
            input.PhoneNumber,
            guestItem.Status
        );

        await _guestRepository.Update(guestItem);

        var guestOutput = guestItem.ToDefaultResponseDto();

        return Result<DefaultGuestResponseDto>.Success(guestOutput!, "Guest updated with success!");
    }

    public async Task<Result<DefaultGuestResponseDto>> Delete(int id)
    {
        var guestItem = await _guestRepository.GetById(id);

        if (guestItem == null)
        {
            return Result<DefaultGuestResponseDto>.Failure("Guest not found.");
        }

        await _guestRepository.Delete(guestItem);

        return Result<DefaultGuestResponseDto>.Success(null, "Guest deleted with success!");
    }

    public async Task<Result<DefaultGuestResponseDto>> AcceptInvite(InviteResponseRequestDto input)
    {
        var guestItem = await _guestRepository.GetById(input.Id);

        if (guestItem == null)
        {
            return Result<DefaultGuestResponseDto>.Failure("Guest not found.");
        }

        guestItem.AcceptInvite();

        await _guestRepository.Update(guestItem);

        var guestOutput = guestItem.ToDefaultResponseDto();

        return Result<DefaultGuestResponseDto>.Success(guestOutput!, "Invite accepted with success!");
    }

    public async Task<Result<DefaultGuestResponseDto>> DeclineInvite(InviteResponseRequestDto input)
    {
        var guestItem = await _guestRepository.GetById(input.Id);

        if (guestItem == null)
        {
            return Result<DefaultGuestResponseDto>.Failure("Guest not found.");
        }

        guestItem.DeclineInvite();

        await _guestRepository.Update(guestItem);

        var guestOutput = guestItem.ToDefaultResponseDto();

        return Result<DefaultGuestResponseDto>.Success(guestOutput!, "Invite declined with success!");
    }
}

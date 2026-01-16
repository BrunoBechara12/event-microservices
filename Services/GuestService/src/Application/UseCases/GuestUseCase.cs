using Application.Ports.Input;
using Application.UseCases.Inputs;
using Application.UseCases.Outputs;
using Domain.Ports;
using Result;
using static Domain.Entities.Guest;

namespace Application.UseCases;
public class GuestUseCase : IGuestUseCase
{
    private readonly IGuestRepository _guestRepository;

    public GuestUseCase(IGuestRepository guestRepository)
    {
        _guestRepository = guestRepository;
    }

    public async Task<Result<IEnumerable<DetailedGuestOutput>>> Get()
    {
        var guests = await _guestRepository.Get();

        if (guests == null || !guests.Any())
        {
            return Result<IEnumerable<DetailedGuestOutput>>.Success(null, "No guests found.");
        }

        var guestOutput = guests.Select(g => g.ToDetailedGuestOutput());

        return Result<IEnumerable<DetailedGuestOutput>>.Success(guestOutput!, "Guests found with success!");
    }

    public async Task<Result<DetailedGuestOutput>> GetById(int id)
    {
        var guest = await _guestRepository.GetById(id);

        if (guest == null)
        {
            return Result<DetailedGuestOutput>.Failure("Guest not found.");
        }

        var guestOutput = guest.ToDetailedGuestOutput();

        return Result<DetailedGuestOutput>.Success(guestOutput!, "Guest found with success!");
    }

    public async Task<Result<DefaultGuestOutput>> Create(CreateGuestInput input)
    {
        var newGuest = CreateGuest(
            input.Name, 
            input.Email, 
            input.PhoneNumber
        );

        var createdGuest = await _guestRepository.Create(newGuest);

        var guestOutput = createdGuest.ToDefaultGuestOutput();

        return Result<DefaultGuestOutput>.Success(guestOutput!, "Guest created with success!");
    }

    public async Task<Result<DefaultGuestOutput>> Update(UpdateGuestInput input)
    {
        var guestItem = await _guestRepository.GetById(input.Id);

        if (guestItem == null)
        {
            return Result<DefaultGuestOutput>.Failure("Guest not found.");
        }

        guestItem.UpdateGuest(
            input.Name,
            input.Email,
            input.PhoneNumber,
            guestItem.Status
        );

        await _guestRepository.Update(guestItem);

        var guestOutput = guestItem.ToDefaultGuestOutput();

        return Result<DefaultGuestOutput>.Success(guestOutput!, "Guest updated with success!");
    }

    public async Task<Result<DefaultGuestOutput>> Delete(int id)
    {
        var guestItem = await _guestRepository.GetById(id);

        if (guestItem == null)
        {
            return Result<DefaultGuestOutput>.Failure("Guest not found.");
        }

        await _guestRepository.Delete(guestItem);

        return Result<DefaultGuestOutput>.Success(null, "Guest deleted with success!");
    }

    public async Task<Result<DefaultGuestOutput>> AcceptInvite(InviteResponseInput input)
    {
        var guestItem = await _guestRepository.GetById(input.Id);

        if (guestItem == null)
        {
            return Result<DefaultGuestOutput>.Failure("Guest not found.");
        }

        guestItem.AcceptInvite();

        await _guestRepository.Update(guestItem);

        var guestOutput = guestItem.ToDefaultGuestOutput();

        return Result<DefaultGuestOutput>.Success(guestOutput!, "Invite accepted with success!");
    }

    public async Task<Result<DefaultGuestOutput>> DeclineInvite(InviteResponseInput input)
    {
        var guestItem = await _guestRepository.GetById(input.Id);

        if (guestItem == null)
        {
            return Result<DefaultGuestOutput>.Failure("Guest not found.");
        }

        guestItem.DeclineInvite();

        await _guestRepository.Update(guestItem);

        var guestOutput = guestItem.ToDefaultGuestOutput();

        return Result<DefaultGuestOutput>.Success(guestOutput!, "Invite declined with success!");
    }
}

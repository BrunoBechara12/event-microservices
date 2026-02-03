using Microsoft.AspNetCore.Mvc;
using Domain.DTOs.Guest.Requests;
using Domain.Ports.Input;

namespace Adapters.Primary.API.Controllers;

[Route("api/guests")]
[ApiController]
public class GuestsController : ControllerBase
{
    private readonly IGuestUseCase _guestUseCase;

    public GuestsController(IGuestUseCase guestUseCase)
    {
        _guestUseCase = guestUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var guests = await _guestUseCase.Get();

        if (guests.RequestSuccess)
        {
            return Ok(new
            {
                message = guests.Message,
                data = guests.Data
            });
        }

        return BadRequest(new { message = guests.Message });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var guest = await _guestUseCase.GetById(id);

        if (guest.RequestSuccess)
        {
            return Ok(new
            {
                message = guest.Message,
                data = guest.Data
            });
        }

        return BadRequest(new { message = guest.Message });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateGuestRequestDto input)
    {
        var guest = await _guestUseCase.Create(input);

        if (guest.RequestSuccess)
        {
            return Ok(new
            {
                message = guest.Message,
                data = guest.Data
            });
        }

        return BadRequest(new { message = guest.Message });
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateGuestRequestDto input)
    {
        var guest = await _guestUseCase.Update(input);

        if (guest.RequestSuccess)
        {
            return Ok(new
            {
                message = guest.Message,
                data = guest.Data
            });
        }

        return BadRequest(new { message = guest.Message });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var guest = await _guestUseCase.Delete(id);

        if (guest.RequestSuccess)
        {
            return Ok(new
            {
                message = guest.Message,
                data = guest.Data
            });
        }

        return BadRequest(new { message = guest.Message });
    }

    [HttpPost("{id}/accept")]
    public async Task<IActionResult> AcceptInvite(int id)
    {
        var input = new InviteResponseRequestDto(id);
        var guest = await _guestUseCase.AcceptInvite(input);

        if (guest.RequestSuccess)
        {
            return Ok(new
            {
                message = guest.Message,
                data = guest.Data
            });
        }

        return BadRequest(new { message = guest.Message });
    }

    [HttpPost("{id}/decline")]
    public async Task<IActionResult> DeclineInvite(int id)
    {
        var input = new InviteResponseRequestDto(id);
        var guest = await _guestUseCase.DeclineInvite(input);

        if (guest.RequestSuccess)
        {
            return Ok(new
            {
                message = guest.Message,
                data = guest.Data
            });
        }

        return BadRequest(new { message = guest.Message });
    }
}

namespace ApiGateway.DTOs;

public record AuthResponseDto(string AccessToken, string RefreshToken, DateTime ExpiresAt);

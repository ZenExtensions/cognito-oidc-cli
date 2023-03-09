using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Refit;

namespace ZenExtensions.CognitoOidc.Cli.Apis
{
    public interface ICognitoApi
    {
        [Post("/oauth2/token")]
        Task<AccessTokenResponse> GetTokenAsync(
            [Body(BodySerializationMethod.UrlEncoded)] AccessTokenResquest request,
            [Header("Authorization")] string authorizationToken
        );
    }

    public record struct AccessTokenResquest(
        [property: JsonPropertyName("grant_type")] string GrantType,
        [property: JsonPropertyName("client_id")] string ClientId  
    );

    public record struct AccessTokenResponse(
        [property: JsonPropertyName("access_token")] string AccessToken,
        [property: JsonPropertyName("expires_in")] int ExpiresIn,
        [property: JsonPropertyName("token_type")] string TokenType  
    );

    public record struct CognitoError(
        [property: JsonPropertyName("error")] string Error
    );
}